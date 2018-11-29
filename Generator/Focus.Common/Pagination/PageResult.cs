namespace Focus.Common.Pagination
{
    public class PageResult<T>
    {
        public T[] Items { get; set; }
        public int AllItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
}
