using System.Collections.Generic;

namespace Pay.Api.Core.Models
{
    public class PagedResponseBase<TEntity> where TEntity : class
    {
        public PagedResponseBase()
        {
            this.Results = new List<TEntity>();
        }
        public ICollection<TEntity> Results { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }
        public int FirstRowOnPage { get; set; }
        public int LastRowOnPage { get; set; }
	}
}
