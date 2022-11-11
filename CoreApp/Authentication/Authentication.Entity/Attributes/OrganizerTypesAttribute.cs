using Model.Common.Attributes;

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