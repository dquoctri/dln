using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Authentication.Model.Converters
{
    public class CollectionValueComparer<T> : ValueComparer<ICollection<T>>
    {
#pragma warning disable CS8604 // Possible null reference argument for parameter 'second' in 'bool Enumerable.SequenceEqual<T>(IEnumerable<T> first, IEnumerable<T> second)'.
#pragma warning disable CS8604 // Possible null reference argument for parameter 'first' in 'bool Enumerable.SequenceEqual<T>(IEnumerable<T> first, IEnumerable<T> second)'.
        public CollectionValueComparer() : base((c1, c2) => c1.SequenceEqual(c2),
#pragma warning restore CS8604 // Possible null reference argument for parameter 'first' in 'bool Enumerable.SequenceEqual<T>(IEnumerable<T> first, IEnumerable<T> second)'.
#pragma warning restore CS8604 // Possible null reference argument for parameter 'second' in 'bool Enumerable.SequenceEqual<T>(IEnumerable<T> first, IEnumerable<T> second)'.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
          c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())), c => (ICollection<T>)c.ToHashSet())
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        {
        }
    }
}
