using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Common.Attributes
{
    public interface IAttribute<T>
    {
        T Value { get; }
    }
}
