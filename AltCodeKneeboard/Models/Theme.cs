using AltCodeKneeboard.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace AltCodeKneeboard.Models
{    
    [Serializable]
    [XmlType(AnonymousType=true, Namespace="http://schneenet.com/Kneeboard/Theme.xsd")]
    [XmlRoot(Namespace="http://schneenet.com/Kneeboard/Theme.xsd", IsNullable=false, ElementName = "Theme")]
    internal class Theme
    {
        [XmlElement("AltCodeBackColor", typeof(ThemeColor))]
        [XmlElement("AltCodeBorderColor", typeof(ThemeColor))]
        [XmlElement("AltCodeBorderRadius", typeof(ThemeBorderRadiusDef))]
        [XmlElement("AltCodeBorderSize", typeof(float))]
        [XmlElement("AltCodeCharAlignment", typeof(ThemeHorizontalAlignment))]
        [XmlElement("AltCodeCharFont", typeof(ThemeFontDef))]
        [XmlElement("AltCodeCodeAlignment", typeof(ThemeHorizontalAlignment))]
        [XmlElement("AltCodeCodeFont", typeof(ThemeFontDef))]
        [XmlElement("AltCodeCollapseMargins", typeof(bool))]
        [XmlElement("AltCodeForeColor", typeof(ThemeColor))]
        [XmlElement("AltCodeHoverBackColor", typeof(ThemeColor))]
        [XmlElement("AltCodeHoverBorderColor", typeof(ThemeColor))]
        [XmlElement("AltCodeHoverForeColor", typeof(ThemeColor))]
        [XmlElement("AltCodeMargin", typeof(ThemePadding))]
        [XmlElement("AltCodePadding", typeof(ThemePadding))]
        [XmlElement("AltCodeSize", typeof(Size))]
        [XmlElement("DefaultAlignment", typeof(ThemeHorizontalAlignment))]
        [XmlElement("DefaultBackColor", typeof(ThemeColor))]
        [XmlElement("DefaultFont", typeof(ThemeFontDef))]
        [XmlElement("DefaultForeColor", typeof(ThemeColor))]
        [XmlElement("HeaderAlignment", typeof(ThemeHorizontalAlignment))]
        [XmlElement("HeaderBackColor", typeof(ThemeColor))]
        [XmlElement("HeaderDividerSize", typeof(float))]
        [XmlElement("HeaderFont", typeof(ThemeFontDef))]
        [XmlElement("HeaderForeColor", typeof(ThemeColor))]
        [XmlElement("HeaderMargin", typeof(ThemePadding))]
        [XmlElement("HeaderPadding", typeof(ThemePadding))]
        [XmlElement("ScrollbarForeColor", typeof(ThemeColor))]
        [XmlElement("ScrollbarWidth", typeof(float))]
        [XmlChoiceIdentifier("ItemsElementName")]
        public object[] Items { get; set; }
        
        [XmlElement("ItemsElementName")]
        [XmlIgnore]
        public ItemsChoiceType[] ItemsElementName { get; set; }

        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlIgnore]
        public Dictionary<string, object> Values
        {
            get
            {
                var values = new Dictionary<string, object>();
                if (Items != null && ItemsElementName != null && Items.Length == ItemsElementName.Length)
                {
                    for (int i = 0; i < Items.Length; i++)
                    {
                        var propName = ItemsElementName[i].ToString();
                        var propType = GetType().GetProperty("Items").GetCustomAttributes<XmlElementAttribute>().First(attr => attr.ElementName == propName).Type;
                        var tvInterface = propType.GetInterface("IThemeValue");
                        if (tvInterface != null)
                        {
                            var value = tvInterface.GetProperty("Value").GetValue(Items[i]);
                            values.Add(propName, value);
                        }
                        else
                        {
                            values.Add(propName, Items[i]);
                        }
                    }
                }
                return values;
            }
            set
            {
                var elements = new List<ItemsChoiceType>();
                var elementValues = new List<object>();
                foreach (var kvp in value)
                {
                    var propName = kvp.Key;
                    var propType = GetType().GetProperty("Items").GetCustomAttributes<XmlElementAttribute>().FirstOrDefault(attr => attr.ElementName == propName)?.Type;
                    ItemsChoiceType element;
                    if (propType != null && Enum.TryParse(propName, out element))
                    {
                        elements.Add(element);
                        var tvInterface = propType.GetInterface("IThemeValue");
                        if (tvInterface != null)
                        {
                            var newValue = Activator.CreateInstance(propType);
                            tvInterface.GetProperty("Value").SetValue(newValue, kvp.Value);
                            elementValues.Add(newValue);
                        }
                        else
                        {
                            elementValues.Add(kvp.Value);
                        }
                    }
                }
                Items = elementValues.ToArray();
                ItemsElementName = elements.ToArray();
            }
        }
    }

    internal interface IThemeValue<T>
    {
        T Value { get; set; }
    }
    
    [Serializable]
    [XmlType(TypeName = "Color", Namespace="http://schneenet.com/Kneeboard/Theme.xsd")]
    internal class ThemeColor : IThemeValue<Color>
    {
        [XmlIgnore]
        public Color Value { get; set; }
        
        [XmlText]
        public string ValueStr
        {
            get => Value.A < 0xFF ? string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", Value.A, Value.R, Value.G, Value.B) : string.Format("#{0:X2}{1:X2}{2:X2}", Value.R, Value.G, Value.B);
            set => Value = ColorUtils.ParseHex(value);
        }
    }
    
    [Serializable]
    [XmlType(TypeName = "BorderRadiusDef", Namespace="http://schneenet.com/Kneeboard/Theme.xsd")]
    internal class ThemeBorderRadiusDef : IThemeValue<BorderRadius>
    {
        public ThemeBorderRadius ThemeValue { get; set; }

        [XmlIgnore]
        public BorderRadius Value
        {
            get => new BorderRadius(ThemeValue.TopLeft, ThemeValue.TopRight, ThemeValue.BottomLeft, ThemeValue.BottomRight);
            set
            {
                ThemeValue = new ThemeBorderRadius
                {
                    TopLeft = value.TopLeft,
                    TopRight = value.TopRight,
                    BottomLeft = value.BottomLeft,
                    BottomRight = value.BottomRight
                };
            }
        }
    }
    
    [Serializable]
    [XmlType(AnonymousType=true, Namespace="http://schneenet.com/Kneeboard/Theme.xsd")]
    internal class ThemeBorderRadius
    {
        [XmlAttribute]
        public int TopLeft { get; set; }

        [XmlAttribute]
        public int TopRight { get; set; }

        [XmlAttribute]
        public int BottomLeft { get; set; }

        [XmlAttribute]
        public int BottomRight { get; set; }
    }
    
    [Serializable]
    [XmlType(TypeName = "Size", Namespace="http://schneenet.com/Kneeboard/Theme.xsd")]
    internal class ThemeSizeDef : IThemeValue<Size>
    {
        [XmlElement("Size")]
        public ThemeSize ThemeValue { get; set; }

        [XmlIgnore]
        public Size Value
        {
            get => new Size(ThemeValue.Width, ThemeValue.Height);
            set => ThemeValue = new ThemeSize { Width = value.Width, Height = value.Height };
        }
    }
    
    [Serializable]
    [XmlType(AnonymousType=true, Namespace="http://schneenet.com/Kneeboard/Theme.xsd")]
    internal class ThemeSize
    {
        [XmlAttribute]
        public int Width { get; set; }

        [XmlAttribute]
        public int Height { get; set; }
    }
    
    [Serializable]
    [XmlType(TypeName = "Padding", Namespace="http://schneenet.com/Kneeboard/Theme.xsd")]
    internal class ThemePaddingDef : IThemeValue<Padding>
    {
        [XmlElement("Padding")]
        public ThemePadding ThemeValue { get; set; }

        [XmlIgnore]
        public Padding Value
        {
            get => new Padding(ThemeValue.Left, ThemeValue.Top, ThemeValue.Right, ThemeValue.Bottom);
            set => ThemeValue = new ThemePadding { Bottom = value.Bottom, Left = value.Left, Right = value.Right, Top = value.Top };
        }
    }
    
    [Serializable]
    [XmlType(AnonymousType=true, Namespace="http://schneenet.com/Kneeboard/Theme.xsd")]
    internal class ThemePadding
    {
        [XmlAttribute]
        public int Left { get; set; }
        
        [XmlAttribute]
        public int Top { get; set; }
        
        [XmlAttribute]
        public int Right { get; set; }
        
        [XmlAttribute]
        public int Bottom { get; set; }
    }
    
    [Serializable]
    [XmlType(TypeName = "HorizontalAlignment", Namespace="http://schneenet.com/Kneeboard/Theme.xsd")]
    internal class ThemeHorizontalAlignment : IThemeValue<HorizontalAlignment>
    {
        [XmlText]
        public HorizontalAlignmentDef ThemeValue { get; set; }

        [XmlIgnore]
        public HorizontalAlignment Value
        {
            get => (HorizontalAlignment)Enum.Parse(typeof(HorizontalAlignment), ThemeValue.ToString());
            set => ThemeValue = (HorizontalAlignmentDef)Enum.Parse(typeof(HorizontalAlignmentDef), value.ToString());
        }
    }
    
    [Serializable]
    [XmlType(Namespace="http://schneenet.com/Kneeboard/Theme.xsd")]
    public enum HorizontalAlignmentDef
    {
        Left,
        Center,
        Right,
    }
    
    [Serializable]
    [XmlType(TypeName = "Font", Namespace="http://schneenet.com/Kneeboard/Theme.xsd")]
    internal class ThemeFontDef : IThemeValue<Font>
    {
        [XmlElement("Font")]
        public ThemeFont ThemeValue { get; set; }

        [XmlIgnore]
        public Font Value
        {
            get
            {
                var style = FontStyle.Regular;
                if (ThemeValue.Bold) style |= FontStyle.Bold;
                if (ThemeValue.Italic) style |= FontStyle.Italic;
                if (ThemeValue.Underline) style |= FontStyle.Underline;
                if (ThemeValue.Strikethrough) style |= FontStyle.Strikeout;
                var unit = (GraphicsUnit)Enum.Parse(typeof(GraphicsUnit), ThemeValue.SizeUnit.ToString());
                return new Font(ThemeValue.Family, ThemeValue.Size, style, unit);
            }
            set
            {
                ThemeValue = new ThemeFont
                {
                    Family = value.FontFamily.Name,
                    Size = value.Size,
                    SizeUnit = (SizeUnitDef)Enum.Parse(typeof(SizeUnitDef), value.Unit.ToString()),
                    Bold = value.Bold,
                    Italic = value.Italic,
                    Underline = value.Underline,
                    Strikethrough = value.Strikeout
                };
            }
        }
    }
    
    [Serializable]
    [XmlType(AnonymousType=true, Namespace="http://schneenet.com/Kneeboard/Theme.xsd")]
    internal class ThemeFont
    {
        [XmlAttribute]
        public string Family { get; set; }

        [XmlAttribute]
        public float Size { get; set; }

        [XmlAttribute]
        public SizeUnitDef SizeUnit { get; set; }

        [XmlAttribute]
        [DefaultValue(false)]
        public bool Bold { get; set; }

        [XmlAttribute]
        [DefaultValue(false)]
        public bool Italic { get; set; }

        [XmlAttribute]
        [DefaultValue(false)]
        public bool Underline { get; set; }

        [XmlAttribute]
        [DefaultValue(false)]
        public bool Strikethrough { get; set; }
    }
    
    [Serializable]
    [XmlType(Namespace="http://schneenet.com/Kneeboard/Theme.xsd")]
    public enum SizeUnitDef
    {
        [XmlEnum("in")]
        Inch,
        [XmlEnum("mm")]
        Millimeter,
        [XmlEnum("pt")]
        Point,
        [XmlEnum("px")]
        Pixels
    }
    
    [Serializable]
    [XmlType(Namespace="http://schneenet.com/Kneeboard/Theme.xsd", IncludeInSchema=false)]
    public enum ItemsChoiceType
    {
        AltCodeBackColor,
        AltCodeBorderColor,
        AltCodeBorderRadius,
        AltCodeBorderSize,
        AltCodeCharAlignment,
        AltCodeCharFont,
        AltCodeCodeAlignment,
        AltCodeCodeFont,
        AltCodeCollapseMargins,
        AltCodeForeColor,
        AltCodeHoverBackColor,
        AltCodeHoverBorderColor,
        AltCodeHoverForeColor,
        AltCodeMargin,
        AltCodePadding,
        AltCodeSize,
        DefaultAlignment,
        DefaultBackColor,
        DefaultFont,
        DefaultForeColor,
        HeaderAlignment,
        HeaderBackColor,
        HeaderDividerSize,
        HeaderFont,
        HeaderForeColor,
        HeaderMargin,
        HeaderPadding,
        ScrollbarForeColor,
        ScrollbarWidth,
    }
}
