using System;
using System.ComponentModel;
using R = AltCodeKneeboard.Properties.Resources;

namespace AltCodeKneeboard.Controls
{
    internal class YesNoConverter : BooleanConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (value is bool && destinationType == typeof(string))
            {
                return values[(bool)value ? 1 : 0];
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            string txt = value as string;
            if (values[0] == txt) return false;
            if (values[1] == txt) return true;
            return base.ConvertFrom(context, culture, value);
        }

        private static readonly string[] values = new string[] { R.No, R.Yes };
    }
}
