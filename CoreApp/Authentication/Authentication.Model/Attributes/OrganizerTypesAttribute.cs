﻿using Model.Common.Attributes;

namespace Authentication.Model.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class OrganizerTypesAttribute : Attribute, IAttribute<OrganizerType[]>
    {
        private readonly OrganizerType[] _value = new OrganizerType[] { OrganizerType.NORMAL };

        public OrganizerTypesAttribute(params OrganizerType[] values)
        {
            this._value = values;
        }

        public OrganizerType[] Value
        {
            get { return this._value; }
        }
    }
}