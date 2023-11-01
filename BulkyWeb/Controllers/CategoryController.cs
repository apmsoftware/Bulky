using Bulky.DataAccess;
using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var objCategoryList = _unitOfWork.categoryRepository.GetAll(null).ToList();
            return View(objCategoryList);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.categoryRepository.Add(obj);
                _unitOfWork.Save();
                return RedirectToAction("Index", "Category");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            if (Id == 0 || Id == null)
            {
                return NotFound();
            }

            Category? categoryFromDb = _unitOfWork.categoryRepository.Get(x=>x.Id==Id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.categoryRepository.Update(obj);
                _unitOfWork.categoryRepository.SaveChanges();
                return RedirectToAction("Index", "Category");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Delete(int? Id)
        {
            if (Id == 0 || Id == null)
            {
                return NotFound();
            }

            Category? categoryFromDb = _unitOfWork.categoryRepository.Get(x => x.Id == Id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? Id)
        {
            Category? obj = _unitOfWork.categoryRepository.Get(x => x.Id == Id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.categoryRepository.Remove(obj);
            _unitOfWork.categoryRepository.SaveChanges();
            return RedirectToAction("Index", "Category");
        }
    }
}
