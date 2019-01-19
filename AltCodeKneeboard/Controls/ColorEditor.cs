using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Cyotek.Windows.Forms;
using SDColor = System.Drawing.Color;

namespace AltCodeKneeboard.Controls
{
    internal class ColorEditor : UITypeEditor
    {
        public override bool GetPaintValueSupported(ITypeDescriptorContext context) => true;

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) => UITypeEditorEditStyle.Modal;

        public override void PaintValue(PaintValueEventArgs e)
        {
            var color = (SDColor)e.Value;
            e.Graphics.FillRectangle(new SolidBrush(color), e.Bounds);
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (value.GetType() != typeof(SDColor))
            {
                return value;
            }

            var color = (SDColor)value;
            var svc = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            if (svc != null)
            {
                using (var dlg = new ColorPickerDialog())
                {
                    dlg.Color = color;
                    dlg.ShowAlphaChannel = true;
                    if (svc.ShowDialog(dlg) == DialogResult.OK)
                    {
                        return dlg.Color;
                    }
                }
            }

            return value;
        }
    }

    internal class ColorConverterEx : ColorConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => false;

        // Currently not using CSS
        //public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        //{
        //    if (destinationType == typeof(string))
        //    {
        //        var c = (SDColor)value;
        //        if (c.A < 0xFF) return string.Format("rgba({0}, {1}, {2}, {3}", c.R, c.G, c.B, c.A / 0xFF);
        //        else return string.Format("#{0:X2}{1:X2}{2:X2}", c.R, c.G, c.B);
        //    }
        //    return base.ConvertTo(context, culture, value, destinationType);
        //}

        //public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        //{
        //    if (value != null && value.GetType() == typeof(string))
        //    {
        //        var parser = new StylesheetParser();
        //        var stylesheet = parser.Parse($".color {{ color: {value}; }}");
        //        var rule = stylesheet.StyleRules.First() as StyleRule;
        //        var color = rule.Style.Color;
        //        // TODO Parse color string (ExCSS should have normalized it to something .NET can understand using ColorConverter.
        //        // If not, it is useless

        //    }
        //    return base.ConvertFrom(context, culture, value);
        //}
    }
}
