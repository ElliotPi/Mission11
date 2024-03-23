using Microsoft.AspNetCore.Mvc;
using Mission11.Models;
using Mission11.Models.ViewModels;
using System.Diagnostics;

namespace Mission11.Controllers
{
    public class HomeController : Controller
    {
        private IBookRepository _repo;

        public HomeController(IBookRepository temp)
        {
            _repo = temp;
        }
        
        public IActionResult Index(int pageNum)
        {
            int pageSize = 10;

            var BookListInfo = new BookListViewModel 
            {
                Books = _repo.Books
                    .OrderBy(x => x.BookId)
                    .Skip((pageNum - 1) * pageSize)     // if pageNum is 1 (first page) and pageSize is 10, the calculation would be (1 - 1) * 10 = 0,
                    .Take(pageSize),                    // meaning no items are skipped for the first page. For the second page (pageNum = 2), 
                PaginationInfo = new PaginationInfo     // the calculation would be (2 - 1) * 10 = 10,
                {                                       // so the query would skip the first 10 items to get to the items for the second page.
                    CurrentPage = pageNum,
                    ItemsPerPage = pageSize,
                    TotalItems = _repo.Books.Count()
                }
            };
   
            return View(BookListInfo); // Passing all these info to the view
        }

    }
}
