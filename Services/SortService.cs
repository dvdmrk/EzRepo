namespace EzRepo.Services
{
    public interface ISortService<TIndex>
    {
        TIndex Sort(TIndex index);
    }
    public class SortService<TIndex> : ISortService<TIndex>
    {
        public TIndex Sort(TIndex index)
        {
            return index;
        }
    }
}