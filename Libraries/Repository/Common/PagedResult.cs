using System.Collections.Generic;

namespace Libraries.Repository.Common
{
    public class PagedResult<T> : PagedResultBase where T : class
    {
        public IList<T> Results { get; set; }
		public PagedResult()
		{
			Results = new List<T>();
		}
    }
}