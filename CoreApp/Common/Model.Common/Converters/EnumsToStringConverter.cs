using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Common.Converters
{
    public class EnumsToStringConverter<T> : ValueConverter<ISet<T>, string> where T : Enum
    {
        public EnumsToStringConverter() : base(
        v => string.Join(",", v),
        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
              .Select(s => (T)Enum.Parse(typeof(T), s))
              .ToHashSet())
        {
        }
    }
}
