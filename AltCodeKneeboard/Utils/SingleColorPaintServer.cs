using Svg;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltCodeKneeboard.Utils
{
    internal class SingleColorPaintServer : SvgPaintServer
    {
        private readonly Color _Color;

        public SingleColorPaintServer(Color color)
        {
            _Color = color;
        }

        public override SvgElement DeepCopy()
        {
            return new SingleColorPaintServer(_Color);
        }

        public override Brush GetBrush(SvgVisualElement styleOwner, ISvgRenderer renderer, float opacity, bool forStroke = false)
        {
            return new SolidBrush(_Color);
        }
    }
}
