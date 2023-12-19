using System;
using UnityEngine;

namespace YNL.Attribute
{

    public abstract class HidingAttribute : PropertyAttribute { }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public class HideIfBoolAttribute : HidingAttribute
    {
        public readonly string variable;
        public readonly bool state;

        public HideIfBoolAttribute(string variable, bool state, int order = 0)
        {
            this.variable = variable;
            this.state = state;
            this.order = order;
        }
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public class HideIfNullAttribute : HidingAttribute
    {
        public readonly string variable;
        public readonly bool isNull;

        public HideIfNullAttribute(string variable, bool isNull = true, int order = 0)
        {
            this.variable = variable;
            this.isNull = isNull;
            this.order = order;
        }
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public class HideIfEnumAttribute : HidingAttribute
    {
        public readonly string variable;
        public readonly bool hideIfEqual;
        public readonly int state;

        public HideIfEnumAttribute(string variable, int state, HideIf hideIf = HideIf.Equal)
        {
            this.variable = variable;
            this.hideIfEqual = hideIf == HideIf.Equal;
            this.state = state;
            this.order = -1;
        }
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public class HideIfValueAttribute : HidingAttribute
    {
        public readonly string variable;
        public readonly HideIf hideIf;
        public readonly int value;

        public HideIfValueAttribute(string variable, int value, HideIf hideIf = HideIf.Equal)
        {
            this.variable = variable;
            this.hideIf = hideIf;
            this.value = value;
            this.order = -1;
        }
    }

    public enum HideIf
    {
        Equal,
        NotEqual,
        Greater,
        Lower
    }
}