using AltCodeKneeboard.Data;
using AltCodeKneeboard.Models;
using AltCodeKneeboard.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using R = AltCodeKneeboard.Properties.Resources;

namespace AltCodeKneeboard.Kneeboard
{
    internal class KneeboardLayout
    {
        private readonly AltCodeData _AltCodeData;
        private readonly List<FlowRow> _Rows = new List<FlowRow>();
        private readonly List<GroupContainer> _Grouped = new List<GroupContainer>();

        private int[] _Favorites;
        private int[] _HiddenGroups;
        private int _ScrollOffset;
        private Size _LastSize;
        private SortMode _Sort;
        private int _HoveredCell = -1;
        private int _TotalHeight;

        public KneeboardLayout(AltCodeData altCodeData, int[] favorites, int[] hiddenGroups)
        {
            _AltCodeData = altCodeData;
            _Favorites = favorites ?? new int[0];
            _HiddenGroups = hiddenGroups ?? new int[0];

            // Add all by group
            foreach (var group in _AltCodeData.Groups.Groups)
            {
                var groupItems = _AltCodeData.AltCodes.AltCodes.Where(code => code.Groups.Contains(group.ID)).ToList();
                if (groupItems.Count > 0)
                {
                    _Grouped.Add(new GroupContainer(group, groupItems));
                }
            }
        }

        #region Public methods

        public KneeboardTheme Theme { get; private set; }

        public bool SetTheme(KneeboardTheme theme, PropertyChangedEventHandler handler)
        {
            if (Theme != theme)
            {
                if (Theme != null) Theme.PropertyChanged -= handler;
                Theme = theme;
                if (Theme != null) Theme.PropertyChanged += handler;
                return true;
            }
            return false;
        }

        public bool Scroll(int offset)
        {
            _ScrollOffset -= offset;
            if (_ScrollOffset < 0) _ScrollOffset = 0;
            if (_ScrollOffset > _TotalHeight - _LastSize.Height) _ScrollOffset = _TotalHeight - _LastSize.Height;
            return true;
        }

        public bool Hover(Point p, out AltCode hovered)
        {
            var cell = FindCellAtPoint(p);
            hovered = cell?.Item;
            var cellId = cell?.ID ?? -1;
            if (_HoveredCell != cellId)
            {
                _HoveredCell = cellId;
                return true;
            }
            return false;
        }

        public bool ClearHover()
        {
            if (_HoveredCell != -1)
            {
                _HoveredCell = -1;
                return true;
            }
            return false;
        }

        public bool SetSortMode(SortMode sortMode)
        {
            if (_Sort != sortMode)
            {
                _Sort = sortMode;
                return true;
            }
            return false;
        }

        public bool SetFavorites(int[] favorites)
        {
            if (!IntArrayEquals(_Favorites, favorites))
            {
                _Favorites = favorites;
                return true;
            }
            return false;
        }

        public bool SetHiddenGroups(int[] hiddenGroups)
        {
            if (!IntArrayEquals(_HiddenGroups, hiddenGroups))
            {
                _HiddenGroups = hiddenGroups;
                return true;
            }
            return false;
        }

        public void Layout(Size size)
        {
            Layout(size, false);
        }

        public void Layout(Size size, bool force)
        {
            if (_LastSize != size || force)
            {
                _LastSize = size;
                _Rows.Clear();

                // Size is zero, don't render anything
                if (Theme.AltCodeSize.IsEmpty) return;

                // Determine cell count
                int cellCount;
                if (Theme.AltCodeCollapseMargins) cellCount = (_LastSize.Width - Theme.AltCodeMargin.Horizontal) / (Theme.AltCodeSize.Width + Math.Max(Theme.AltCodeMargin.Left, Theme.AltCodeMargin.Right));
                else cellCount = _LastSize.Width / (Theme.AltCodeSize.Width + Theme.AltCodeMargin.Horizontal);

                if (cellCount < 1) cellCount = 1; // May not fit, but we have to do something

                int totalHeight = 0;
                int cellId = 0;

                if (_Favorites != null && _Favorites.Length > 0)
                {
                    AddHeader(R.Favorites, ref totalHeight);
                    AddRows(_Favorites.Join(_AltCodeData.AltCodes.AltCodes, k1 => k1, code => code.Unicode, (k1, code) => code).Batch(cellCount), ref totalHeight, ref cellId);
                }

                switch (_Sort)
                {
                    case SortMode.Grouped:
                        foreach (var group in _Grouped)
                        {
                            if (_HiddenGroups != null && _HiddenGroups.Contains(group.Group.ID)) continue;

                            AddHeader(group.Group.Name, ref totalHeight);
                            AddRows(group.AltCodes.Batch(cellCount), ref totalHeight, ref cellId);
                        }
                        break;
                    case SortMode.UnicodeOrder:
                        AddHeader(R.AllSortedByUnicode, ref totalHeight);
                        AddRows(_AltCodeData.AltCodes.AltCodes.OrderBy(ac => ac.Unicode).Batch(cellCount), ref totalHeight, ref cellId);
                        break;
                    case SortMode.Alphabetically:
                        AddHeader(R.AllSortedByDescription, ref totalHeight);
                        AddRows(_AltCodeData.AltCodes.AltCodes.OrderBy(ac => ac.Description).Batch(cellCount), ref totalHeight, ref cellId);
                        break;
                }

                _TotalHeight = totalHeight;
            }
        }

        public bool HasScrollbar()
        {
            return _TotalHeight > _LastSize.Height;
        }

        public RectangleF GetScrollThumbRect(RectangleF scrollRect)
        {
            float thumbSize = Math.Max((float)scrollRect.Height / _TotalHeight * scrollRect.Height, scrollRect.Width);
            float maxScrollOffset = _TotalHeight - scrollRect.Height;
            float maxThumbOffset = scrollRect.Height - thumbSize;
            float thumbY = (_ScrollOffset / maxScrollOffset) * maxThumbOffset;
            return new RectangleF(scrollRect.X, scrollRect.Y + thumbY, scrollRect.Width - 1, thumbSize - 1);
        }

        public void Render(Graphics g)
        {
            // Translate for scroll offset
            g.TranslateTransform(0, -_ScrollOffset);

            //var firstRow = true;
            foreach (var row in _Rows)
            {
                // Skip rows that are out of the visible region because of scrolling
                if (row.Y + row.Height < _ScrollOffset) continue;
                if (row.Y > _ScrollOffset + _LastSize.Height) continue;

                var headerRow = row as HeaderFlowRow;
                var itemRow = row as ItemFlowRow;
                if (itemRow != null)
                {
                    //var firstCell = true;

                    foreach (var cell in itemRow.Cells)
                    {
                        // Determine bounds
                        var foreColor = _HoveredCell == cell.ID ? Theme.AltCodeHoverForeColor : Theme.AltCodeForeColor;
                        //var x = cell.X + (!firstCell && Theme.AltCodeCollapseMargins ? 0 : Theme.AltCodeMargin.Left);
                        //var y = row.Y + (!firstRow && Theme.AltCodeCollapseMargins ? 0 : Theme.AltCodeMargin.Top);
                        //var altCodeRect = new Rectangle(x, y, Theme.AltCodeSize.Width, Theme.AltCodeSize.Height);
                        var altCodeRect = cell.Bounds;
                        var borderRect = new Rectangle(altCodeRect.Location, new Size(altCodeRect.Width - 1, altCodeRect.Height - 1));
                        var path = FormUtils.RoundedRectangle(borderRect, Theme.AltCodeBorderRadius.TopLeft, Theme.AltCodeBorderRadius.TopRight, Theme.AltCodeBorderRadius.BottomLeft, Theme.AltCodeBorderRadius.BottomRight);

                        // Draw background
                        g.FillPath(new SolidBrush(_HoveredCell == cell.ID ? Theme.AltCodeHoverBackColor : Theme.AltCodeBackColor), path);

                        // Draw character
                        var charRect = new RectangleF(altCodeRect.X + Theme.AltCodePadding.Left, altCodeRect.Y + Theme.AltCodePadding.Top, altCodeRect.Width - Theme.AltCodePadding.Horizontal, (int)((altCodeRect.Height - Theme.AltCodePadding.Vertical) * 0.70f));
                        var disp = char.ConvertFromUtf32(cell.Item.Unicode);
                        g.DrawString(disp, Theme.AltCodeCharFont, foreColor, charRect, Theme.AltCodeCharAlignment, StringAlignment.Center);

                        // Draw code
                        var code = R.Alt + " + " + UnicodeAltCode.GetAltCode(cell.Item.Unicode);
                        var codeRect = new RectangleF(altCodeRect.X + Theme.AltCodePadding.Left, altCodeRect.Y + Theme.AltCodePadding.Top + charRect.Height, altCodeRect.Width - Theme.AltCodePadding.Horizontal, altCodeRect.Height - Theme.AltCodePadding.Vertical - charRect.Height);
                        g.DrawString(code, Theme.AltCodeCodeFont, foreColor, codeRect, Theme.AltCodeCodeAlignment, StringAlignment.Center);

                        // Draw border
                        g.DrawPath(new Pen(new SolidBrush(_HoveredCell == cell.ID ? Theme.AltCodeHoverBorderColor : Theme.AltCodeBorderColor), Theme.AltCodeBorderSize), path);

                        path.Dispose();
                        //firstCell = false;
                    }

                    //firstRow = false;
                }
                else if (headerRow != null)
                {
                    // Draw header background
                    var headerRect = new Rectangle(Theme.HeaderMargin.Left, headerRow.Y + Theme.HeaderMargin.Top, _LastSize.Width - Theme.HeaderMargin.Horizontal, headerRow.Height - Theme.HeaderMargin.Vertical);
                    g.FillRectangle(new SolidBrush(Theme.HeaderBackColor), headerRect);

                    // Draw header text
                    var headerBox = new RectangleF(headerRect.X + Theme.HeaderPadding.Left, headerRect.Y + Theme.HeaderPadding.Top, headerRect.Width - Theme.HeaderPadding.Horizontal, headerRect.Height - Theme.HeaderPadding.Vertical);
                    g.DrawString(headerRow.HeaderText, Theme.HeaderFont, Theme.HeaderForeColor, headerBox, Theme.HeaderAlignment, StringAlignment.Center);

                    // Draw header divider
                    var lineY = headerRow.Y + headerRow.Height - Theme.HeaderDividerSize / 2.0f;
                    g.DrawLine(new Pen(new SolidBrush(Theme.HeaderForeColor), Theme.HeaderDividerSize), headerRect.Left, lineY, headerRect.Right, lineY);

                    //firstRow = true;
                }
            }

            g.ResetTransform();
        }

        public AltCode FindAtPoint(Point point)
        {
            return FindCellAtPoint(point)?.Item;
        }

        #endregion

        #region Private members

        private void AddHeader(string headerText, ref int totalHeight)
        {
            var measuredHeader = TextRenderer.MeasureText(headerText, Theme.HeaderFont, new Size(_LastSize.Width - (Theme.HeaderMargin.Horizontal + Theme.HeaderPadding.Horizontal), int.MaxValue));
            measuredHeader.Height += Theme.HeaderMargin.Vertical + Theme.HeaderPadding.Vertical;
            _Rows.Add(new HeaderFlowRow(measuredHeader.Height, measuredHeader.Width, totalHeight, headerText));
            totalHeight += measuredHeader.Height + (int)Math.Ceiling(Theme.HeaderDividerSize);
        }

        private void AddRows(IEnumerable<IEnumerable<AltCode>> rows, ref int totalHeight, ref int cellId)
        {
            var firstRow = true;
            // Add item rows
            foreach (var rowItems in rows)
            {
                if (firstRow || !Theme.AltCodeCollapseMargins) totalHeight += Theme.AltCodeMargin.Top;

                var cells = new List<FlowCell>();
                var x = 0;
                var firstCell = true;
                foreach (var code in rowItems)
                {
                    if (firstCell || !Theme.AltCodeCollapseMargins) x += Theme.AltCodeMargin.Left;

                    var boundsRect = new Rectangle(x, totalHeight, Theme.AltCodeSize.Width, Theme.AltCodeSize.Height);
                    cells.Add(new FlowCell(code, boundsRect, cellId));
                    cellId++;

                    x += Theme.AltCodeSize.Width;
                    x += (!Theme.AltCodeCollapseMargins || firstCell ? Theme.AltCodeMargin.Right : Math.Max(Theme.AltCodeMargin.Left, Theme.AltCodeMargin.Right));

                    firstCell = false;
                }

                _Rows.Add(new ItemFlowRow(Theme.AltCodeSize.Height, x, totalHeight, cells));

                totalHeight += Theme.AltCodeSize.Height;
                totalHeight += (!Theme.AltCodeCollapseMargins || firstRow ? Theme.AltCodeMargin.Bottom : Math.Max(Theme.AltCodeMargin.Top, Theme.AltCodeMargin.Bottom));

                firstRow = false;
            }
        }

        private FlowCell FindCellAtPoint(Point pt)
        {
            var x = pt.X;
            var y = pt.Y + _ScrollOffset;
            foreach (var row in _Rows)
            {
                if (new Rectangle(0, row.Y, _LastSize.Width, row.Height).Contains(x, y))
                {
                    var itemRow = row as ItemFlowRow;
                    if (itemRow != null)
                    {
                        foreach (var cell in itemRow.Cells)
                        {
                            //new Rectangle(cell.X + Theme.AltCodeMargin.Left, row.Y + Theme.AltCodeMargin.Top, Theme.AltCodeSize.Width - Theme.AltCodeMargin.Horizontal, Theme.AltCodeSize.Height - Theme.AltCodeMargin.Vertical)
                            if (cell.Bounds.Contains(x, y)) return cell;
                        }
                        return null; // Not hovered over a cell in the row, short-circuit
                    }
                    else return null; // Header or other row, short-circuit
                }
            }
            return null; // Not hovered over any row
        }

        private bool IntArrayEquals(int[] lhs, int[] rhs)
        {
            if (lhs == null || rhs == null) return false;
            if (lhs.Length != rhs.Length) return false;
            for (int i = 0; i < lhs.Length; i++)
            {
                if (lhs[i] != rhs[i]) return false;
            }
            return true;
        }

        #endregion

        #region Utility classes

        private abstract class FlowRow
        {
            public FlowRow(int height, int width, int y)
            {
                Height = height;
                Width = width;
                Y = y;
            }

            public int Height { get; }
            public int Width { get; }

            public int Y { get; }
        }

        private class HeaderFlowRow : FlowRow
        {
            public HeaderFlowRow(int height, int width, int y, string headerText) : base(height, width, y)
            {
                HeaderText = headerText;
            }

            public string HeaderText { get; }
        }

        private class ItemFlowRow : FlowRow
        {
            public ItemFlowRow(int height, int width, int y, IEnumerable<FlowCell> cells) : base(height, width, y)
            {
                Cells = cells;
            }

            public IEnumerable<FlowCell> Cells { get; }
        }

        private class FlowCell
        {
            public FlowCell(AltCode item, Rectangle bounds, int id)
            {
                Item = item;
                //X = x;
                //Width = width;
                Bounds = bounds;
                ID = id;
            }

            public AltCode Item { get; }

            public Rectangle Bounds { get; }

            //public int X { get; }
            //public int Width { get; }

            public int ID { get; }
        }

        private class GroupContainer
        {
            public GroupContainer(Group group, IEnumerable<AltCode> grouped)
            {
                Group = group;
                AltCodes = grouped;
            }

            public Group Group { get; }
            public IEnumerable<AltCode> AltCodes { get; }
        }

        #endregion
    }
}
