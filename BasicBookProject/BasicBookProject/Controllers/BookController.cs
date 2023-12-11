using BasicBookProject.Models;
using BasicBookProject.Models.Abstracts;
using BasicBookProject.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BasicBookProject.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBookTypeRepository _bookTypeRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;

        public BookController(IBookRepository bookRepository, IBookTypeRepository bookTypeRepository, IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = bookRepository;
            _bookTypeRepository = bookTypeRepository;
            _webHostEnvironment = webHostEnvironment;
        }

   //     [Authorize(Roles ="Admin,Student")]
        public IActionResult Index()
        {
            // Listeleme işlemi
            // List<Book> objBookList = _bookRepository.GetAll().ToList();
            List<Book> objBookList = _bookRepository.GetAll(includeProps:"BookType").ToList();
            return View(objBookList);
        }

       // [Authorize(Roles =UserRoles.Role_Admin)]
        public IActionResult AddUpdate(int? id)
        {
            IEnumerable<SelectListItem> BookTypeList = _bookTypeRepository.GetAll()
                .Select(k => new SelectListItem
                {
                    Text = k.Name,
                    Value = k.Id.ToString()
                });
            ViewBag.BookTypeList = BookTypeList;

            if (id == null || id == 0)
            {
                return View();
            }
            else
            {
                Book? bookVt = _bookRepository.Get(x => x.Id == id);
                if(bookVt ==null)
                {
                    return NotFound();
                }
                return View(bookVt);
            }
        }

        [HttpPost]
       // [Authorize(Roles =UserRoles.Role_Admin)]
        public IActionResult AddUpdate(Book book,IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string bookPath = Path.Combine(wwwRootPath,@"img");

                if(file != null)
                {
                using (var fileStream = new FileStream(Path.Combine(bookPath,file!.FileName),FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                book.ImageURL = @"\img\" + file.FileName;
                }

                if (book.Id ==0)
                {
                    _bookRepository.Add(book);
                    TempData["Success"] = "Yeni Kitap Başarıyla oluşturulmuştur!";
                }
                else
                {
                    _bookRepository.Update(book);
                    TempData["Success"] = "Kitap Başarıyla Güncellenmiştir!";
                }

                _bookRepository.Save();
                return RedirectToAction("Index");
            }

            return View();
        }

        //public IActionResult Update(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }

        //    Book? bookVt = _bookRepository.Get(x => x.Id == id);
        //    if (bookVt == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(bookVt);
        //}

        //[HttpPost]
        //public IActionResult Update(Book book)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _bookRepository.Update(book);
        //        _bookRepository.Save();
        //        TempData["Success"] = "Kitap Başarıyla Güncellenmiştir!";
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}

      //  [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            Book? bookVt = _bookRepository.Get(x => x.Id == id); // T get(Expression<Func<T,bool>>expression) olayı

            if (bookVt == null)
                return NotFound();

            return View(bookVt);
        }
     //   [Authorize(Roles = UserRoles.Role_Admin)]

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Book bookVt = _bookRepository.Get(x => x.Id == id);
            if (bookVt == null)
                return NotFound();

            _bookRepository.Delete(bookVt);
            _bookRepository.Save();
            TempData["Success"] = "Kitap Silme İşlemi Başarılı";
            return RedirectToAction("Index");
        }
    }
}
