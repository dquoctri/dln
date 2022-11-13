using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Authentication.Model.Converters
{
    public class EnumCollectionJsonValueConverter<T> : ValueConverter<ISet<T>, string> where T : Enum
    {
        public EnumCollectionJsonValueConverter() : base(
            v => JsonConvert.SerializeObject(v.Select(x => x.ToString())),
#pragma warning disable CS8604 // Possible null reference argument for parameter 'source' in 'IEnumerable<T> Enumerable.Select<string, T>(IEnumerable<string> source, Func<string, T> selector)'.
#pragma warning disable CS8603 // Possible null reference return.
            (v) => JsonConvert.DeserializeObject<ISet<string>>(v).Select(x => (T)Enum.Parse(typeof(T), x)) as ISet<T>)
#pragma warning restore CS8603 // Possible null reference return.
#pragma warning restore CS8604 // Possible null reference argument for parameter 'source' in 'IEnumerable<T> Enumerable.Select<string, T>(IEnumerable<string> source, Func<string, T> selector)'.
        {
        }
    }

    public class EnumCollectionJsonValueConverter2<T> : ValueConverter<ISet<T>, string> where T : Enum
    {
        public EnumCollectionJsonValueConverter2(Expression<Func<ISet<T>, string>> convertToProviderExpression,
            Expression<Func<string, ISet<T>>> convertFromProviderExpression, ConverterMappingHints? mappingHints = null)
            : base(convertToProviderExpression, convertFromProviderExpression, mappingHints)
        {
        }
    }

    public static class EnumCollectionJsonValueConverter3<T> where T : Enum
    {
        public delegate string ConvertToProvider(ISet<T> list);
        public delegate ISet<T> ConvertFromProvider(string value);

        public static string Serialize(ISet<T> list)
        {
            return JsonConvert.SerializeObject(list.Select(x => x.ToString()));
        }
        public static ISet<T> Deserialize(string value)
        {
            var v = JsonConvert.DeserializeObject<ISet<string>>(value);
            if (v == null) return new HashSet<T>();
            if (v.Select(x => (T)Enum.Parse(typeof(T), x)) is ISet<T> t)
            {
                return (ISet<T>)v;
            }
            return new HashSet<T>();
        }

        public static ValueConverter<ISet<T>, string> CreateConverter()
        {
            return new ValueConverter<ISet<T>, string>(v => Serialize(v), v => Deserialize(v));
        }
    }




}
