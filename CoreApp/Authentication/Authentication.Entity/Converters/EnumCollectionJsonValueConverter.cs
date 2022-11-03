using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace Authentication.Entity.Converters
{
    public class EnumCollectionJsonValueConverter<T> : ValueConverter<ISet<T>, string> where T : Enum
    {
        public EnumCollectionJsonValueConverter() : base(
            v => JsonConvert.SerializeObject(v.Select(x => x.ToString())),
            v => JsonConvert.DeserializeObject<ISet<string>>(v).Select(x => (T)Enum.Parse(typeof(T), x)) as ISet<T>)
        { 
        }
    }
}
