using System;
using System.Linq;

/// <summary>
///  WIP: https://codereview.stackexchange.com/questions/147856/generic-null-empty-check-for-each-property-of-a-class
/// </summary>
namespace UserNames.Lib.Extensions
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class ValidatorAttribute : Attribute
    {
        public abstract bool Validate(object value);
    }

    public class DefaultValidatorAttribute : ValidatorAttribute
    {
        public override bool Validate(object value)
        {
            if (value is string)
                return !string.IsNullOrWhiteSpace((string)value);

             return value != null;
        }
    }

    public class IntValidatorAttribute : ValidatorAttribute
    {
        public override bool Validate(object value)
        {
            return (int)value >= 0;
        }
    }

    public static class ValidationExtensions
    {
        public static bool AllPropertiesValid(this object obj)
        {
            if (obj == null)
                return false;

            return obj.GetType().GetProperties().All(p =>
            {
                var attrib = p.GetCustomAttributes(typeof(ValidatorAttribute), true)
                                 .FirstOrDefault() as ValidatorAttribute ?? new DefaultValidatorAttribute();

                return attrib.Validate(p.GetValue(obj));
            });
        }
    }
}
