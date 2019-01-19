using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using AltCodeKneeboard.Controls;
using R = AltCodeKneeboard.Properties.Resources;

namespace AltCodeKneeboard.Models
{
    [TypeConverter(typeof(BorderRadiusConverter))]
    internal struct BorderRadius
    {
        public static readonly BorderRadius None = new BorderRadius(0);

        public BorderRadius(int all)
        {
            _All = true;
            _TopLeft = _TopRight = _BottomLeft = _BottomRight = all;
        }

        public BorderRadius(int topLeft, int topRight, int bottomLeft, int bottomRight)
        {
            _TopLeft = topLeft;
            _TopRight = topRight;
            _BottomLeft = bottomLeft;
            _BottomRight = bottomRight;
            _All = _TopLeft == _TopRight && _TopLeft == _BottomLeft && _TopLeft == _BottomRight;
        }

        private bool _All;
        private int _TopLeft;
        private int _TopRight;
        private int _BottomLeft;
        private int _BottomRight;

        [DescriptionRes(nameof(R.Description_BorderRadius_All))]
        [DisplayNameRes(nameof(R.DisplayName_BorderRadius_All))]
        [RefreshProperties(RefreshProperties.All)]
        public int All
        {
            get => _All ? _TopLeft : -1;
            set
            {
                if (!_All || _TopLeft != value)
                {
                    _All = true;
                    _TopLeft = _TopRight = _BottomLeft = _BottomRight = value;
                }
            }
        }

        [DescriptionRes(nameof(R.Description_BorderRadius_TopLeft))]
        [DisplayNameRes(nameof(R.DisplayName_BorderRadius_TopLeft))]
        [RefreshProperties(RefreshProperties.All)]
        public int TopLeft
        {
            get => _TopLeft;
            set
            {
                if (_All || _TopLeft != value)
                {
                    _All = false;
                    _TopLeft = value;
                }
            }
        }

        [DescriptionRes(nameof(R.Description_BorderRadius_TopRight))]
        [DisplayNameRes(nameof(R.DisplayName_BorderRadius_TopRight))]
        [RefreshProperties(RefreshProperties.All)]
        public int TopRight
        {
            get => _All ? _TopLeft : _TopRight;
            set
            {
                if (_All || _TopRight != value)
                {
                    _All = false;
                    _TopRight = value;
                }
            }
        }

        [DescriptionRes(nameof(R.Description_BorderRadius_BottomLeft))]
        [DisplayNameRes(nameof(R.DisplayName_BorderRadius_BottomLeft))]
        [RefreshProperties(RefreshProperties.All)]
        public int BottomLeft
        {
            get => _All ? _TopLeft : _BottomLeft;
            set
            {
                if (_All || _BottomLeft != value)
                {
                    _All = false;
                    _BottomLeft = value;
                }
            }
        }

        [DescriptionRes(nameof(R.Description_BorderRadius_BottomRight))]
        [DisplayNameRes(nameof(R.DisplayName_BorderRadius_BottomRight))]
        [RefreshProperties(RefreshProperties.All)]
        public int BottomRight
        {
            get => _All ? _TopLeft : _BottomRight;
            set
            {
                if (_All || _BottomRight != value)
                {
                    _All = false;
                    _BottomRight = value;
                }
            }
        }

        public override string ToString()
        {
            return $"{TopLeft}, {TopRight}, {BottomLeft}, {BottomRight}";
        }

        public override bool Equals(object other)
        {
            if (other is BorderRadius)
            {
                return ((BorderRadius)other) == this;
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hashCode = TopLeft;
            hashCode = (hashCode << 5) + hashCode ^ TopRight;
            hashCode = (hashCode << 5) + hashCode ^ BottomLeft;
            hashCode = (hashCode << 5) + hashCode ^ BottomRight;
            return hashCode;
        }

        public static bool operator ==(BorderRadius lhs, BorderRadius rhs)
        {
            return lhs.TopLeft == rhs.TopLeft && lhs.TopRight == rhs.TopRight && lhs.BottomLeft == rhs.BottomLeft && lhs.BottomRight == rhs.BottomRight;
        }

        public static bool operator !=(BorderRadius lhs, BorderRadius rhs)
        {
            return !(lhs == rhs);
        }

        internal bool ShouldSerializeAll() => _All;

    }

    public class BorderRadiusConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(InstanceDescriptor))
            {
                return true;
            }
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            string valueStr = value as string;
            if (valueStr != null)
            {
                valueStr = valueStr.Trim();

                if (valueStr.Length == 0)
                {
                    return null;
                }
                else
                {
                    // Parse 4 integer values.
                    if (culture == null)
                    {
                        culture = CultureInfo.CurrentCulture;
                    }
                    char sep = culture.TextInfo.ListSeparator[0];
                    string[] tokens = valueStr.Split(new char[] { sep });
                    int[] values = new int[tokens.Length];
                    TypeConverter intConverter = TypeDescriptor.GetConverter(typeof(int));
                    for (int i = 0; i < values.Length; i++)
                    {
                        // Note: ConvertFromString will raise exception if value cannot be converted.
                        values[i] = (int)intConverter.ConvertFromString(context, culture, tokens[i]);
                    }
                    if (values.Length == 4)
                    {
                        return new BorderRadius(values[0], values[1], values[2], values[3]);
                    }
                    else
                    {
                        throw new ArgumentException(R.FailedToParseBorderRadius);
                    }
                }
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException("destinationType");
            }

            if (value is BorderRadius)
            {
                if (destinationType == typeof(string))
                {
                    BorderRadius val = (BorderRadius)value;

                    if (culture == null)
                    {
                        culture = CultureInfo.CurrentCulture;
                    }
                    string sep = culture.TextInfo.ListSeparator + " ";
                    TypeConverter intConverter = TypeDescriptor.GetConverter(typeof(int));
                    string[] args = new string[4];
                    int nArg = 0;

                    // Note: ConvertToString will raise exception if value cannot be converted.
                    args[nArg++] = intConverter.ConvertToString(context, culture, val.TopLeft);
                    args[nArg++] = intConverter.ConvertToString(context, culture, val.TopRight);
                    args[nArg++] = intConverter.ConvertToString(context, culture, val.BottomLeft);
                    args[nArg++] = intConverter.ConvertToString(context, culture, val.BottomRight);

                    return string.Join(sep, args);
                }
                else if (destinationType == typeof(InstanceDescriptor))
                {
                    BorderRadius val = (BorderRadius)value;

                    if (val.ShouldSerializeAll())
                    {
                        return new InstanceDescriptor(
                            typeof(BorderRadius).GetConstructor(new Type[] { typeof(int) }),
                            new object[] { val.All });
                    }
                    else
                    {
                        return new InstanceDescriptor(
                            typeof(BorderRadius).GetConstructor(new Type[] { typeof(int), typeof(int), typeof(int), typeof(int) }),
                            new object[] { val.TopLeft, val.TopRight, val.BottomLeft, val.BottomRight });
                    }
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (propertyValues == null)
            {
                throw new ArgumentNullException("propertyValues");
            }

            BorderRadius original = (BorderRadius)context.PropertyDescriptor.GetValue(context.Instance);

            int all = (int)propertyValues["All"];
            if (original.All != all)
            {
                return new BorderRadius(all);
            }
            else
            {
                return new BorderRadius(
                    (int)propertyValues["TopLeft"],
                    (int)propertyValues["TopRight"],
                    (int)propertyValues["BottomLeft"],
                    (int)propertyValues["BottomRight"]);
            }
        }

        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context) => true;

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(BorderRadius), attributes);
            return props.Sort(new string[] { "All", "TopLeft", "TopRight", "BottomLeft", "BottomRight" });
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context) => true;
    }

}
