namespace Model.Common.Attributes
{
    public interface IAttribute<T>
    {
        T Value { get; }
    }
}
