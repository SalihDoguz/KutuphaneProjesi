using BasicBookProject.Models;
using BasicBookProject.Models.Abstracts;
using BasicBookProject.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BasicBookProject.Controllers
{
    [Authorize(Roles =UserRoles.Role_Admin)]
    public class HireController : Controller
    {
        private readonly IHireRepository _hireRepository;
        private readonly IBookRepository _bookRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;

        public HireController(IHireRepository hireRepository, IBookRepository bookRepository, IWebHostEnvironment webHostEnvironment)
        {
            _hireRepository = hireRepository;
            _bookRepository = bookRepository;
            _webHostEnvironment = webHostEnvironment;
        }


        public IActionResult Index()
        {
            List<Hire> objHireList = _hireRepository.GetAll(includeProps: "Book").ToList();
            return View(objHireList);
        }

        public IActionResult AddUpdate(int? id)
        {
            IEnumerable<SelectListItem> bookList = _bookRepository.GetAll()
                .Select(k => new SelectListItem
                {
                    Text = k.BookName,
                    Value = k.Id.ToString()
                });
            //if(bookList == null)
            //{
            //    return NotFound();
            //}

            ViewBag.BookList = bookList;

            if (id == null || id == 0)
            {
                // Ekle
                return View();
            }
            else
            {
                Hire? hire = _hireRepository.Get(h => h.Id == id);
                if (hire == null)
                {
                    return NotFound();
                }
                return View(hire);
            }
        }

        [HttpPost]
        public IActionResult AddUpdate(Hire hire)
        {
            if (ModelState.IsValid)
            {
                if (hire.Id == 0)
                {
                    _hireRepository.Add(hire);
                    TempData["Success"] = "Yeni Kiralama Kaydı Başarıyla Oluşturuldu!";
                }
                else
                {
                    _hireRepository.Update(hire);
                    TempData["Success"] = "Kiralama Kayıt Güncelleme Başarılı";
                }

                _hireRepository.Save();
                //return View("Index");
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            IEnumerable<SelectListItem> bookList = _bookRepository.GetAll()
                .Select(x => new SelectListItem
                {
                    Text = x.BookName,
                    Value = x.Id.ToString()
                });
            ViewBag.BookList = bookList;    

            if (id == 0 || id == null)
            {
                return NotFound();
            }

            Hire? hire = _hireRepository.Get(h => h.Id == id);
            if (hire == null)
            {
                return NotFound();
            }
            return View(hire);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Hire? hire = _hireRepository.Get(h => h.Id == id);
            if (hire == null)
            {
                return NotFound();
            }

            _hireRepository.Delete(hire);
            _hireRepository.Save();
            TempData["Success"] = "Kiralama Kayıt Silme Başarılı!";
            return RedirectToAction("Index");
        }

    }
}
