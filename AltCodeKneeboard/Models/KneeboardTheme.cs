using AltCodeKneeboard.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using R = AltCodeKneeboard.Properties.Resources;

namespace AltCodeKneeboard.Models
{
    internal class KneeboardTheme : INotifyPropertyChanged, ICustomTypeDescriptor
    {
        private readonly Dictionary<string, object> _Defaults = new Dictionary<string, object>();

        public KneeboardTheme()
        {
            Filename = null;

            // Defaults
            var defaultFont = new Font("Calibri", 8, FontStyle.Regular, GraphicsUnit.Point);
            _Defaults.Add(nameof(DefaultBackColor), SystemColors.Control);
            _Defaults.Add(nameof(HeaderBackColor), SystemColors.Control);
            _Defaults.Add(nameof(AltCodeBackColor), SystemColors.Control);
            _Defaults.Add(nameof(AltCodeHoverBackColor), SystemColors.Control);
            _Defaults.Add(nameof(DefaultForeColor), SystemColors.ControlText);
            _Defaults.Add(nameof(HeaderForeColor), SystemColors.ControlText);
            _Defaults.Add(nameof(AltCodeForeColor), SystemColors.ControlText);
            _Defaults.Add(nameof(AltCodeBorderColor), SystemColors.ControlText);
            _Defaults.Add(nameof(AltCodeHoverForeColor), SystemColors.ControlText);
            _Defaults.Add(nameof(AltCodeHoverBorderColor), SystemColors.ControlText);
            _Defaults.Add(nameof(ScrollbarForeColor), SystemColors.ControlText);
            _Defaults.Add(nameof(ScrollbarWidth), 8.0f);
            _Defaults.Add(nameof(DefaultAlignment), HorizontalAlignment.Center);
            _Defaults.Add(nameof(HeaderDividerSize), 2.0f);
            _Defaults.Add(nameof(DefaultFont), defaultFont);
            _Defaults.Add(nameof(HeaderFont), new Font(defaultFont.FontFamily, 28, FontStyle.Regular, GraphicsUnit.Point));
            _Defaults.Add(nameof(AltCodeCharFont), new Font(defaultFont.FontFamily, 20, FontStyle.Regular, GraphicsUnit.Point));
            _Defaults.Add(nameof(AltCodeSize), new Size(72, 72));
            _Defaults.Add(nameof(AltCodeMargin), new Padding(2));

            Reset();
        }

        public void Reset()
        {
            foreach (PropertyDescriptor prop in GetProperties())
            {
                prop.ResetValue(this);
            }
        }

        public void Persist()
        {
            foreach (PropertyDescriptor prop in GetProperties())
            {
                if (prop.ShouldSerializeValue(this))
                {
                    _Defaults[prop.Name] = prop.GetValue(this);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _Name;
        public string Name
        {
            get => _Name;
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        private string _Filename;
        public string Filename
        {
            get => _Filename;
            set
            {
                if (_Filename != value)
                {
                    _Filename = value;
                    OnPropertyChanged(nameof(Filename));
                    Name = string.IsNullOrEmpty(_Filename) ? R.Category_Style_Default : Path.GetFileNameWithoutExtension(_Filename);
                }
            }
        }

        private Color _DefaultForeColor;
        [CategoryRes(nameof(R.Category_Style_Default))]
        [DisplayNameRes(nameof(R.DisplayName_Style_DefaultForeColor))]
        [DescriptionRes(nameof(R.Description_Style_DefaultForeColor))]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(ColorConverterEx))]
        //[CssProperty("body", "color")]
        public Color DefaultForeColor
        {
            get => _DefaultForeColor;
            set
            {
                if (_DefaultForeColor != value)
                {
                    _DefaultForeColor = value;
                    OnPropertyChanged(nameof(DefaultForeColor));
                }
            }
        }

        private Color _DefaultBackColor;
        [CategoryRes(nameof(R.Category_Style_Default))]
        [DisplayNameRes(nameof(R.DisplayName_Style_DefaultBackColor))]
        [DescriptionRes(nameof(R.Description_Style_DefaultBackColor))]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(ColorConverterEx))]
        //[CssProperty("body", "background-color")]
        public Color DefaultBackColor
        {
            get => _DefaultBackColor;
            set
            {
                if (_DefaultBackColor != value)
                {
                    _DefaultBackColor = value;
                    OnPropertyChanged(nameof(DefaultBackColor));
                }
            }
        }

        private Font _DefaultFont;
        [CategoryRes(nameof(R.Category_Style_Default))]
        [DisplayNameRes(nameof(R.DisplayName_Style_DefaultFont))]
        [DescriptionRes(nameof(R.Description_Style_DefaultFont))]
        //[CssFontProperty("body")]
        public Font DefaultFont
        {
            get => _DefaultFont;
            set
            {
                if (_DefaultFont != value)
                {
                    _DefaultFont = value;
                    OnPropertyChanged(nameof(DefaultFont));
                }
            }
        }

        private HorizontalAlignment _DefaultAlignment;
        [CategoryRes(nameof(R.Category_Style_Default))]
        [DisplayNameRes(nameof(R.DisplayName_Style_DefaultAlignment))]
        [DescriptionRes(nameof(R.Description_Style_DefaultAlignment))]
        //[CssProperty("body", "text-align")]
        public HorizontalAlignment DefaultAlignment
        {
            get => _DefaultAlignment;
            set
            {
                if (_DefaultAlignment != value)
                {
                    _DefaultAlignment = value;
                    OnPropertyChanged(nameof(DefaultAlignment));
                }
            }
        }

        private float _ScrollWidth;
        [CategoryRes(nameof(R.Category_Style_ScrollBar))]
        [DisplayNameRes(nameof(R.DisplayName_Style_ScrollWidth))]
        [DescriptionRes(nameof(R.Description_Style_ScrollWidth))]
        //[CssProperty(".scrollbar", "width")]
        public float ScrollbarWidth
        {
            get => _ScrollWidth;
            set
            {
                if (_ScrollWidth != value)
                {
                    _ScrollWidth = value;
                    OnPropertyChanged(nameof(ScrollbarWidth));
                }
            }
        }

        private Color _ScrollbarForeColor;
        [CategoryRes(nameof(R.Category_Style_ScrollBar))]
        [DisplayNameRes(nameof(R.DisplayName_Style_ScrollbarForeColor))]
        [DescriptionRes(nameof(R.Description_Style_ScrollbarForeColor))]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(ColorConverterEx))]
        //[CssProperty(".scrollbar", "color")]
        public Color ScrollbarForeColor
        {
            get => _ScrollbarForeColor;
            set
            {
                if (_ScrollbarForeColor != value)
                {
                    _ScrollbarForeColor = value;
                    OnPropertyChanged(nameof(ScrollbarForeColor));
                }
            }
        }

        private Color _HeaderForeColor;
        [CategoryRes(nameof(R.Category_Style_Header))]
        [DisplayNameRes(nameof(R.DisplayName_Style_HeaderForeColor))]
        [DescriptionRes(nameof(R.Description_Style_HeaderForeColor))]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(ColorConverterEx))]
        //[CssProperty(".header", "color")]
        public Color HeaderForeColor
        {
            get => _HeaderForeColor;
            set
            {
                if (_HeaderForeColor != value)
                {
                    _HeaderForeColor = value;
                    OnPropertyChanged(nameof(HeaderForeColor));
                }
            }
        }

        private Color _HeaderBackColor;
        [CategoryRes(nameof(R.Category_Style_Header))]
        [DisplayNameRes(nameof(R.DisplayName_Style_HeaderBackColor))]
        [DescriptionRes(nameof(R.Description_Style_HeaderBackColor))]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(ColorConverterEx))]
        //[CssProperty(".header", "background-color")]
        public Color HeaderBackColor
        {
            get => _HeaderBackColor;
            set
            {
                if (_HeaderBackColor != value)
                {
                    _HeaderBackColor = value;
                    OnPropertyChanged(nameof(HeaderBackColor));
                }
            }
        }

        private Padding _HeaderMargin;
        [CategoryRes(nameof(R.Category_Style_Header))]
        [DisplayNameRes(nameof(R.DisplayName_Style_HeaderMargin))]
        [DescriptionRes(nameof(R.Description_Style_HeaderMargin))]
        //[CssProperty(".header", "margin")]
        public Padding HeaderMargin
        {
            get => _HeaderMargin;
            set
            {
                if (_HeaderMargin != value)
                {
                    _HeaderMargin = value;
                    OnPropertyChanged(nameof(HeaderMargin));
                }
            }
        }

        private Padding _HeaderPadding;
        [CategoryRes(nameof(R.Category_Style_Header))]
        [DisplayNameRes(nameof(R.DisplayName_Style_HeaderPadding))]
        [DescriptionRes(nameof(R.Description_Style_HeaderPadding))]
        //[CssProperty(".header", "padding")]
        public Padding HeaderPadding
        {
            get => _HeaderPadding;
            set
            {
                if (_HeaderPadding != value)
                {
                    _HeaderPadding = value;
                    OnPropertyChanged(nameof(HeaderPadding));
                }
            }
        }

        private Font _HeaderFont;
        [CategoryRes(nameof(R.Category_Style_Header))]
        [DisplayNameRes(nameof(R.DisplayName_Style_HeaderFont))]
        [DescriptionRes(nameof(R.Description_Style_HeaderFont))]
        //[CssFontProperty(".header")]
        public Font HeaderFont
        {
            get => _HeaderFont ?? DefaultFont;
            set
            {
                if (_HeaderFont != value)
                {
                    _HeaderFont = value;
                    OnPropertyChanged(nameof(HeaderFont));
                }
            }
        }

        private HorizontalAlignment? _HeaderAlignment;
        [CategoryRes(nameof(R.Category_Style_Header))]
        [DisplayNameRes(nameof(R.DisplayName_Style_HeaderAlignment))]
        [DescriptionRes(nameof(R.Description_Style_HeaderAlignment))]
        //[CssProperty(".header", "text-align")]
        public HorizontalAlignment HeaderAlignment
        {
            get => _HeaderAlignment ?? DefaultAlignment;
            set
            {
                if (_HeaderAlignment != value)
                {
                    _HeaderAlignment = value;
                    OnPropertyChanged(nameof(HeaderAlignment));
                }
            }
        }

        private float _HeaderDividerSize;
        [CategoryRes(nameof(R.Category_Style_Header))]
        [DisplayNameRes(nameof(R.DisplayName_Style_HeaderDividerSize))]
        [DescriptionRes(nameof(R.Description_Style_HeaderDividerSize))]
        //[CssProperty(".header .divider", "height")]
        public float HeaderDividerSize
        {
            get => _HeaderDividerSize;
            set
            {
                if (_HeaderDividerSize != value)
                {
                    _HeaderDividerSize = value;
                    OnPropertyChanged(nameof(HeaderDividerSize));
                }
            }
        }

        private Color _AltCodeForeColor;
        [CategoryRes(nameof(R.Category_Style_Tile))]
        [DisplayNameRes(nameof(R.DisplayName_Style_AltCodeForeColor))]
        [DescriptionRes(nameof(R.Description_Style_AltCodeForeColor))]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(ColorConverterEx))]
        //[CssProperty(".tile", "color")]
        public Color AltCodeForeColor
        {
            get => _AltCodeForeColor;
            set
            {
                if (_AltCodeForeColor != value)
                {
                    _AltCodeForeColor = value;
                    OnPropertyChanged(nameof(AltCodeForeColor));
                }
            }
        }

        private Color _AltCodeBackColor;
        [CategoryRes(nameof(R.Category_Style_Tile))]
        [DisplayNameRes(nameof(R.DisplayName_Style_AltCodeBackColor))]
        [DescriptionRes(nameof(R.Description_Style_AltCodeBackColor))]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(ColorConverterEx))]
        //[CssProperty(".tile", "background-color")]
        public Color AltCodeBackColor
        {
            get => _AltCodeBackColor;
            set
            {
                if (_AltCodeBackColor != value)
                {
                    _AltCodeBackColor = value;
                    OnPropertyChanged(nameof(AltCodeBackColor));
                }
            }
        }

        private Color _AltCodeBorderColor;
        [CategoryRes(nameof(R.Category_Style_Tile))]
        [DisplayNameRes(nameof(R.DisplayName_Style_AltCodeBorderColor))]
        [DescriptionRes(nameof(R.Description_Style_AltCodeBorderColor))]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(ColorConverterEx))]
        //[CssProperty(".tile", "border-color")]
        public Color AltCodeBorderColor
        {
            get => _AltCodeBorderColor;
            set
            {
                if (_AltCodeBorderColor != value)
                {
                    _AltCodeBorderColor = value;
                    OnPropertyChanged(nameof(AltCodeBorderColor));
                }
            }
        }

        private Color _AltCodeHoverForeColor;
        [CategoryRes(nameof(R.Category_Style_Tile))]
        [DisplayNameRes(nameof(R.DisplayName_Style_AltCodeHoverForeColor))]
        [DescriptionRes(nameof(R.Description_Style_AltCodeHoverForeColor))]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(ColorConverterEx))]
        //[CssProperty(".tile:hover", "color")]
        public Color AltCodeHoverForeColor
        {
            get => _AltCodeHoverForeColor;
            set
            {
                if (_AltCodeHoverForeColor != value)
                {
                    _AltCodeHoverForeColor = value;
                    OnPropertyChanged(nameof(AltCodeHoverForeColor));
                }
            }
        }

        private Color _AltCodeHoverBackColor;
        [CategoryRes(nameof(R.Category_Style_Tile))]
        [DisplayNameRes(nameof(R.DisplayName_Style_AltCodeHoverBackColor))]
        [DescriptionRes(nameof(R.Description_Style_AltCodeHoverBackColor))]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(ColorConverterEx))]
        //[CssProperty(".tile:hover", "background-color")]
        public Color AltCodeHoverBackColor
        {
            get => _AltCodeHoverBackColor;
            set
            {
                if (_AltCodeHoverBackColor != value)
                {
                    _AltCodeHoverBackColor = value;
                    OnPropertyChanged(nameof(AltCodeHoverBackColor));
                }
            }
        }

        private Color _AltCodeHoverBorderColor;
        [CategoryRes(nameof(R.Category_Style_Tile))]
        [DisplayNameRes(nameof(R.DisplayName_Style_AltCodeHoverBorderColor))]
        [DescriptionRes(nameof(R.Description_Style_AltCodeHoverBorderColor))]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(ColorConverterEx))]
        //[CssProperty(".tile:hover", "border-color")]
        public Color AltCodeHoverBorderColor
        {
            get => _AltCodeHoverBorderColor;
            set
            {
                if (_AltCodeHoverBorderColor != value)
                {
                    _AltCodeHoverBorderColor = value;
                    OnPropertyChanged(nameof(AltCodeHoverBorderColor));
                }
            }
        }

        private Size _AltCodeSize;
        [CategoryRes(nameof(R.Category_Style_Tile))]
        [DisplayNameRes(nameof(R.DisplayName_Style_AltCodeSize))]
        [DescriptionRes(nameof(R.Description_Style_AltCodeSize))]
        //[CssSizeProperty(".tile")]
        public Size AltCodeSize
        {
            get => _AltCodeSize;
            set
            {
                if (_AltCodeSize != value)
                {
                    _AltCodeSize = value;
                    OnPropertyChanged(nameof(AltCodeSize));
                }
            }
        }

        private Padding _AltCodePadding;
        [CategoryRes(nameof(R.Category_Style_Tile))]
        [DisplayNameRes(nameof(R.DisplayName_Style_AltCodePadding))]
        [DescriptionRes(nameof(R.Description_Style_AltCodePadding))]
        //[CssProperty(".tile", "padding")]
        public Padding AltCodePadding
        {
            get => _AltCodePadding;
            set
            {
                if (_AltCodePadding != value)
                {
                    _AltCodePadding = value;
                    OnPropertyChanged(nameof(AltCodePadding));
                }
            }
        }

        private Padding _AltCodeMargin;
        [CategoryRes(nameof(R.Category_Style_Tile))]
        [DisplayNameRes(nameof(R.DisplayName_Style_AltCodeMargin))]
        [DescriptionRes(nameof(R.Description_Style_AltCodeMargin))]
        //[CssProperty(".tile", "margin")]
        public Padding AltCodeMargin
        {
            get => _AltCodeMargin;
            set
            {
                if (_AltCodeMargin != value)
                {
                    _AltCodeMargin = value;
                    OnPropertyChanged(nameof(AltCodeMargin));
                }
            }
        }

        private bool _AltCodeCollapseMargins;
        [CategoryRes(nameof(R.Category_Style_Tile))]
        [DisplayNameRes(nameof(R.DisplayName_Style_AltCodeCollapseMargins))]
        [DescriptionRes(nameof(R.Description_Style_AltCodeCollapseMargins))]
        [TypeConverter(typeof(YesNoConverter))]
        //[CssProperty(".tile", "padding")]
        public bool AltCodeCollapseMargins
        {
            get => _AltCodeCollapseMargins;
            set
            {
                if (_AltCodeCollapseMargins != value)
                {
                    _AltCodeCollapseMargins = value;
                    OnPropertyChanged(nameof(AltCodeCollapseMargins));
                }
            }
        }

        private float _AltCodeBorderSize;
        [CategoryRes(nameof(R.Category_Style_Tile))]
        [DisplayNameRes(nameof(R.DisplayName_Style_AltCodeBorderSize))]
        [DescriptionRes(nameof(R.Description_Style_AltCodeBorderSize))]
        //[CssProperty(".tile", "border-size")]
        public float AltCodeBorderSize
        {
            get => _AltCodeBorderSize;
            set
            {
                if (_AltCodeBorderSize != value)
                {
                    _AltCodeBorderSize = value;
                    OnPropertyChanged(nameof(AltCodeBorderSize));
                }
            }
        }

        private BorderRadius _AltCodeBorderRadius;
        [CategoryRes(nameof(R.Category_Style_Tile))]
        [DisplayNameRes(nameof(R.DisplayName_Style_AltCodeBorderRadius))]
        [DescriptionRes(nameof(R.Description_Style_AltCodeBorderRadius))]
        //[CssProperty(".tile", "border-radius")]
        public BorderRadius AltCodeBorderRadius
        {
            get => _AltCodeBorderRadius;
            set
            {
                if (_AltCodeBorderRadius != value)
                {
                    _AltCodeBorderRadius = value;
                    OnPropertyChanged(nameof(AltCodeBorderRadius));
                }
            }
        }

        private Font _AltCodeCharFont;
        [CategoryRes(nameof(R.Category_Style_Tile))]
        [DisplayNameRes(nameof(R.DisplayName_Style_AltCodeCharFont))]
        [DescriptionRes(nameof(R.Description_Style_AltCodeCharFont))]
        //[CssFontProperty(".tile .preview")]
        public Font AltCodeCharFont
        {
            get => _AltCodeCharFont ?? DefaultFont;
            set
            {
                if (_AltCodeCharFont != value)
                {
                    _AltCodeCharFont = value;
                    OnPropertyChanged(nameof(AltCodeCharFont));
                }
            }
        }

        private HorizontalAlignment? _AltCodeCharAlignment;
        [CategoryRes(nameof(R.Category_Style_Tile))]
        [DisplayNameRes(nameof(R.DisplayName_Style_AltCodeCharAlignment))]
        [DescriptionRes(nameof(R.Description_Style_AltCodeCharAlignment))]
        //[CssProperty(".tile .preview", "text-align")]
        public HorizontalAlignment AltCodeCharAlignment
        {
            get => _AltCodeCharAlignment ?? DefaultAlignment;
            set
            {
                if (_AltCodeCharAlignment != value)
                {
                    _AltCodeCharAlignment = value;
                    OnPropertyChanged(nameof(AltCodeCharAlignment));
                }
            }
        }

        private Font _AltCodeCodeFont;
        [CategoryRes(nameof(R.Category_Style_Tile))]
        [DisplayNameRes(nameof(R.DisplayName_Style_AltCodeCodeFont))]
        [DescriptionRes(nameof(R.Description_Style_AltCodeCodeFont))]
        //[CssFontProperty(".tile .alt-code")]
        public Font AltCodeCodeFont
        {
            get => _AltCodeCodeFont ?? DefaultFont;
            set
            {
                if (_AltCodeCodeFont != value)
                {
                    _AltCodeCodeFont = value;
                    OnPropertyChanged(nameof(AltCodeCodeFont));
                }
            }
        }

        private HorizontalAlignment? _AltCodeCodeAlignment;
        [CategoryRes(nameof(R.Category_Style_Tile))]
        [DisplayNameRes(nameof(R.DisplayName_Style_AltCodeCodeAlignment))]
        [DescriptionRes(nameof(R.Description_Style_AltCodeCodeAlignment))]
        //[CssProperty(".tile .alt-code", "text-align")]
        public HorizontalAlignment AltCodeCodeAlignment
        {
            get => _AltCodeCodeAlignment ?? DefaultAlignment;
            set
            {
                if (_AltCodeCodeAlignment != value)
                {
                    _AltCodeCodeAlignment = value;
                    OnPropertyChanged(nameof(AltCodeCodeAlignment));
                }
            }
        }

        #region ICustomTypeDescriptor impl

        public bool IsDirty
        {
            get
            {
                foreach (PropertyDescriptor prop in GetProperties())
                {
                    if (prop.CanResetValue(this)) return true;
                }
                return false;
            }
        }

        public AttributeCollection GetAttributes() => TypeDescriptor.GetAttributes(this, true);
        public string GetClassName() => TypeDescriptor.GetClassName(this, true);
        public string GetComponentName() => TypeDescriptor.GetComponentName(this, true);
        public TypeConverter GetConverter() => TypeDescriptor.GetConverter(this, true);
        public EventDescriptor GetDefaultEvent() => TypeDescriptor.GetDefaultEvent(this, true);
        public PropertyDescriptor GetDefaultProperty() => TypeDescriptor.GetDefaultProperty(this, true);
        public object GetEditor(Type editorBaseType) => TypeDescriptor.GetEditor(this, editorBaseType, true);
        public EventDescriptorCollection GetEvents() => TypeDescriptor.GetEvents(this, true);
        public EventDescriptorCollection GetEvents(Attribute[] attributes) => TypeDescriptor.GetEvents(this, true);
        public object GetPropertyOwner(PropertyDescriptor pd) => this;
        public PropertyDescriptorCollection GetProperties(Attribute[] attributes) => GetProperties();
        
        public PropertyDescriptorCollection GetProperties()
        {
            return new PropertyDescriptorCollection(null)
            {
                BuildWithDefault<Color>(nameof(DefaultForeColor)),
                BuildWithDefault<Color>(nameof(DefaultBackColor)),
                BuildWithDefault<Font>(nameof(DefaultFont)),
                BuildWithDefault<HorizontalAlignment>(nameof(DefaultAlignment)),
                BuildWithDefault<float>(nameof(ScrollbarWidth)),
                BuildWithDefaultForwarding<Color>(nameof(ScrollbarForeColor), nameof(DefaultForeColor)),
                BuildWithDefaultForwarding<Color>(nameof(HeaderForeColor), nameof(DefaultForeColor)),
                BuildWithDefaultForwarding<Color>(nameof(HeaderBackColor), nameof(DefaultBackColor)),
                BuildWithDefault<Padding>(nameof(HeaderMargin)),
                BuildWithDefault<Padding>(nameof(HeaderPadding)),
                BuildWithDefaultForwarding<Font>(nameof(HeaderFont), nameof(DefaultFont)),
                BuildWithDefaultForwarding<HorizontalAlignment>(nameof(HeaderAlignment), nameof(DefaultAlignment)),
                BuildWithDefault<float>(nameof(HeaderDividerSize)),
                BuildWithDefaultForwarding<Color>(nameof(AltCodeForeColor), nameof(DefaultForeColor)),
                BuildWithDefaultForwarding<Color>(nameof(AltCodeBackColor), nameof(DefaultBackColor)),
                BuildWithDefaultForwarding<Color>(nameof(AltCodeBorderColor), nameof(DefaultForeColor)),
                BuildWithDefaultForwarding<Color>(nameof(AltCodeHoverForeColor), nameof(DefaultForeColor)),
                BuildWithDefaultForwarding<Color>(nameof(AltCodeHoverBackColor), nameof(DefaultBackColor)),
                BuildWithDefaultForwarding<Color>(nameof(AltCodeHoverBorderColor), nameof(DefaultForeColor)),
                BuildWithDefault<Size>(nameof(AltCodeSize)),
                BuildWithDefault<Padding>(nameof(AltCodePadding)),
                BuildWithDefault<Padding>(nameof(AltCodeMargin)),
                BuildWithDefault<bool>(nameof(AltCodeCollapseMargins)),
                BuildWithDefault<float>(nameof(AltCodeBorderSize)),
                BuildWithDefault<BorderRadius>(nameof(AltCodeBorderRadius)),
                BuildWithDefaultForwarding<Font>(nameof(AltCodeCharFont), nameof(DefaultFont)),
                BuildWithDefaultForwarding<HorizontalAlignment>(nameof(AltCodeCharAlignment), nameof(DefaultAlignment)),
                BuildWithDefaultForwarding<Font>(nameof(AltCodeCodeFont), nameof(DefaultFont)),
                BuildWithDefaultForwarding<HorizontalAlignment>(nameof(AltCodeCodeAlignment), nameof(DefaultAlignment))
            };
        }

        private PropertyDescriptor BuildWithDefault<T>(string propName)
        {
            var prop = GetType().GetProperty(propName);
            var defaultValue = _Defaults.ContainsKey(propName) ? _Defaults[propName] : default(T);
            return new DefaultValuePropertyDescriptor<T>(prop, defaultValue, prop.GetCustomAttributes().ToArray());
        }

        private PropertyDescriptor BuildWithDefaultForwarding<T>(string propName, string defaultPropName)
        {
            var prop = GetType().GetProperty(propName);
            var defaultValue = _Defaults.ContainsKey(propName) ? _Defaults[propName] : default(T);
            var defaultProp = GetType().GetProperty(defaultPropName);
            return new ForwardingDefaultValuePropertyDescriptor<T>(prop, defaultValue, defaultProp, prop.GetCustomAttributes().ToArray());
        }

        #endregion
    }

    internal class DefaultValuePropertyDescriptor<T> : PropertyDescriptor
    {
        private readonly PropertyInfo _Property;
        private readonly object _DefaultValue;

        public DefaultValuePropertyDescriptor(PropertyInfo property, object defaultValue, Attribute[] attrs) : base(property.Name, attrs)
        {
            _Property = property;
            _DefaultValue = defaultValue;
        }

        public override Type ComponentType => typeof(T);
        public override bool IsReadOnly => false;
        public override Type PropertyType => _Property.PropertyType;

        public virtual object GetDefaultValue(object component)
        {
            return _DefaultValue;
        }

        public override bool CanResetValue(object component)
        {
            var value = GetValue(component);
            return !Equals(value, GetDefaultValue(component));
        }

        public override object GetValue(object component)
        {
            return _Property.GetValue(component);
        }

        public override void ResetValue(object component)
        {
            SetValue(component, GetDefaultValue(component));
        }

        public override void SetValue(object component, object value)
        {
            _Property.SetValue(component, value);
        }

        public override bool ShouldSerializeValue(object component)
        {
            return CanResetValue(component);
        }
    }

    internal class ForwardingDefaultValuePropertyDescriptor<T> : DefaultValuePropertyDescriptor<T>
    {
        private readonly PropertyInfo _DefaultProperty;

        public ForwardingDefaultValuePropertyDescriptor(PropertyInfo property, object defaultValue, PropertyInfo defaultProperty, Attribute[] attrs) : base(property, defaultValue, attrs)
        {
            _DefaultProperty = defaultProperty;
        }

        public override object GetDefaultValue(object component)
        {
            // Check if we provided an override and return that
            var defaultValue = base.GetDefaultValue(component);
            if (!Equals(defaultValue, default(T))) return defaultValue;

            // Otherwise, grab the value from the other reference property
            return _DefaultProperty.GetValue(component);
        }

    }

}
