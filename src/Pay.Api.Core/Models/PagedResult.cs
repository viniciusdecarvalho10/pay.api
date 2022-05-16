using System.Collections.Generic;

namespace Pay.Api.Core.Models
{
    public class PagedResult<TEntity> : PagedResultBase where TEntity : class
    {
        public IList<TEntity> Results { get; set; }
        public PagedResult()
        {
            Results = new List<TEntity>();
        }
    }
}