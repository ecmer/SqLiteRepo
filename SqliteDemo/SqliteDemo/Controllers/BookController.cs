using System.Web;
using System.Web.Mvc;
using SqliteDemo.Models.Entity;
using SqliteDemo.Models.Transaction;

namespace SqliteTest.Controllers
{
    /*
     * This Controller handles requests related to Books.
     */
    public class BookController : Controller
    {
        /*
         * Handle a request for a listing of books.
         */
        public ActionResult List()
        {
            Book[] books = BookManager.GetAllBooks();
            return View(books);
        }

        /*
		 * Handle a GET request for the Add Book form.
         */
        [HttpGet]
        public ActionResult AddBook()
        {
            return View(new Book());
        }

        /*
         * Handle the POST request from the Add Book form. The form parameters
         * are encapsulated in a Book object.
         */
        [HttpPost]
        public ActionResult AddBook(Book newBook)
        {
            // Validate book data from the transaction
            if (newBook == null)
            {
                ViewBag.message = "Error: Invalid Request - please try again";
                return View(new Book());
            }
            if (newBook.Title == null || newBook.Title.Length == 0)
            {
                ViewBag.message = "Error: A title is required";
                return View(newBook);
            }
            if (newBook.ISBN == 0)
            {
                ViewBag.message = "Error: An ISBN is required";
                return View(newBook);
            }

            // Add the book
            bool result = BookManager.AddNewBook(newBook);
            if (result)
            {
                ViewBag.message = "Book added";
            }
            else
            {
                ViewBag.message = "That book could not be added";
            }

            Book[] books = BookManager.GetAllBooks();
            return View("List", books);
        }

        [HttpGet]
        public ActionResult DeleteBook()
        {
            return View(new Book());
        }
        [HttpPost]
        public ActionResult DeleteBook(Book delBook)
        {
            if (delBook.ISBN == 0)
            {
                ViewBag.message = "Error: An ISBN is required";
                return View(delBook);
            }
            bool result = BookManager.DeleteBook(delBook);
            if (result == false)
            {
                ViewBag.message = "Error: An ISBN is wrong";
                return View(delBook);
            }
            ViewBag.message = "Book deleted";
            Book[] books = BookManager.GetAllBooks();
            return View("List", books);
        }

        [HttpGet]
        public ActionResult ChangeBook()
        {
            return View(new Book());
        }
        [HttpPost]
        public ActionResult ChangeBook(Book changeBook)
        {
            bool result = BookManager.ChangeBook(changeBook);
            if (result == false)
            {
                ViewBag.message = "Error: Book not found";
                return View(changeBook);
            }
            ViewBag.message = "Book changed";
            Book[] books = BookManager.GetAllBooks();
            return View("List", books);
        }
    }
}