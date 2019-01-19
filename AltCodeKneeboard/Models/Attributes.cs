using System;
using System.ComponentModel;
using R = AltCodeKneeboard.Properties.Resources;

namespace AltCodeKneeboard.Models
{
    internal class CategoryResAttribute : CategoryAttribute
    {
        public CategoryResAttribute(string resourceName) : base(resourceName) { }

        protected override string GetLocalizedString(string value)
        {
            return R.ResourceManager.GetString(value);
        }
    }

    internal class DisplayNameResAttribute : DisplayNameAttribute
    {
        private readonly string _ResourceName;

        public DisplayNameResAttribute(string resourceName)
        {
            _ResourceName = resourceName;
        }

        public override string DisplayName => R.ResourceManager.GetString(_ResourceName);
    }

    internal class DescriptionResAttribute : DescriptionAttribute
    {
        private readonly string _ResourceName;

        public DescriptionResAttribute(string resourceName)
        {
            _ResourceName = resourceName;
        }

        public override string Description => R.ResourceManager.GetString(_ResourceName);
    }

    // Currently not using CSS
    //[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    //internal class CssPropertyAttribute : Attribute
    //{
    //    public CssPropertyAttribute(string selector, string propertyName)
    //    {
    //        Selector = selector;
    //        PropertyName = propertyName;
    //    }

    //    public string Selector { get; }

    //    public string PropertyName { get; }
    //}

    //[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    //internal class CssFontPropertyAttribute : Attribute
    //{
    //    public CssFontPropertyAttribute(string selector)
    //    {
    //        Selector = selector;
    //    }

    //    public string Selector { get; }
    //}

    //[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    //internal class CssSizePropertyAttribute : Attribute
    //{
    //    public CssSizePropertyAttribute(string selector)
    //    {
    //        Selector = selector;
    //    }

    //    public string Selector { get; }
    //}

}
