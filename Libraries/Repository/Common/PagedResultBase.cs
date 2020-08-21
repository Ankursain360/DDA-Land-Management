namespace Libraries.Repository.Common
{
    public class PagedResultBase
    {
        public int RowCount { get; set; }
		public int CurrentPage { get; set; }
		public int PazeSize { get; set; }
    }
}