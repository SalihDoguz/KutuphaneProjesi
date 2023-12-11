using BasicBookProject.Models;
using BasicBookProject.Models.Abstracts;
using BasicBookProject.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasicBookProject.Controllers
{
    [Authorize(Roles =UserRoles.Role_Admin)]
    public class BookTypeController : Controller
    {
        private readonly IBookTypeRepository _bookTypeRepository;
        
        public BookTypeController(IBookTypeRepository bookTypeRepository)
        {
            _bookTypeRepository = bookTypeRepository;
        }

        public IActionResult Index()
        {
            List<BookType> objBookTypeList = _bookTypeRepository.GetAll().ToList();
            return View(objBookTypeList);
        }

        public IActionResult BookAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BookAdd(BookType bookType)
        {
            if (ModelState.IsValid)
            {
                _bookTypeRepository.Add(bookType);
                _bookTypeRepository.Save();
                TempData["Success"] = "Yeni Kitap Türü Ekleme İşlemi Başarılı!";
                return RedirectToAction("Index", "BookType");
            }
            else
            {
                return View();
            }

        }

        public IActionResult BookUpdate(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            BookType? bookTypeVt = _bookTypeRepository.Get(x => x.Id == id); // expression

            if (bookTypeVt == null)
            {
                return NotFound();
            }

            return View(bookTypeVt);
        }

        [HttpPost]
        public IActionResult BookUpdate(BookType bookType)
        {
            if (ModelState.IsValid)
            {
                _bookTypeRepository.Update(bookType);
                _bookTypeRepository.Save();
                TempData["Success"] = "Kitap Türü Güncelleme İşlemi Başarılı!";
                return RedirectToAction("Index", "BookType");
            }
            return View();
        }

        public IActionResult BookDelete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            BookType? bookTypeVt = _bookTypeRepository.Get(x => x.Id ==id);
            if (bookTypeVt == null)
            {
                return NotFound();
            }
            return View(bookTypeVt);
        }

        [HttpPost, ActionName("BookDelete")]
        public IActionResult BookDelete(BookType bookType)
        {
            //BookType bookTypeVt = _context.BookTypes.Find(id);
            //if (bookTypeVt == null)
            //    return NotFound();

            //_context.BookTypes.Remove(bookTypeVt);
            //_context.SaveChanges();
            //return RedirectToAction("Index");

            _bookTypeRepository.Delete(bookType);
            _bookTypeRepository.Save();
            TempData["Success"] = "Kitap Türü Silme İşlemi Başarılı!";
            return RedirectToAction("Index","BookType");
        }
    }
}
