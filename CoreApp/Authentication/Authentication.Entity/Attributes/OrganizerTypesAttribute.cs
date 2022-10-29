using Model.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Entity.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class OrganizerTypesAttribute : Attribute, IAttribute<OrganizerType[]>
    {
        private readonly OrganizerType[] _value;

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