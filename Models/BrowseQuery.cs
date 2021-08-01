using System;

namespace EzRepo.Models
{
    public class BrowseQuery
    {
        public int Skip { get; set; }
        public int Take { get; set; }  
        public string Search { get; set; }
    }
    public class BrowseQuery<TQueryable> : BrowseQuery
    {
        public Func<TQueryable, TQueryable> CustomQuery { get; set; }
    }
}