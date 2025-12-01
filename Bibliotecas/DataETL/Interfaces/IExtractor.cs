namespace DataETL.Interfaces
{
    public interface IExtractor<T>
    {
        public abstract Task<IEnumerable<T>> ExtractAsync();

       
    }
}
