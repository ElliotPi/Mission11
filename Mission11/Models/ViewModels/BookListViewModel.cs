namespace Mission11.Models.ViewModels
{
    public class BookListViewModel  // This ViewModel contains all the model we need for the Index view
    {
        public IQueryable<Book> Books { get; set;}
        public PaginationInfo PaginationInfo { get; set;} = new PaginationInfo();
    }
}
