using ExpensesByType.Data;
using ExpensesByType.Models;
using ExpensesByType.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensesByType.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ExpenseController(ApplicationDbContext db)
        {
            _db = db;
        }


        public IActionResult Index(int? expenseTypeId, string name, int page = 1,
            SortState sortOrder = SortState.ExpenseNameAsc)
        {
            int pageSize = 3;
            IEnumerable<Expense> ExpensesList = _db.Expenses;
           
            foreach (var obj in ExpensesList)
            {
                obj.ExpenseType = _db.ExpenseTypes.FirstOrDefault(u => u.Id == obj.ExpenseTypeId);
            }

            if (expenseTypeId != null && expenseTypeId != 0)
            {
                ExpensesList = ExpensesList.Where(p => p.ExpenseTypeId == expenseTypeId);
            }
            if (!String.IsNullOrEmpty(name))
            {
                ExpensesList = ExpensesList.Where(p => p.ExpenseName.Contains(name));
            }

            // сортировка
            switch (sortOrder)
            {
                case SortState.ExpenseNameDesc:
                    ExpensesList = ExpensesList.OrderByDescending(s => s.ExpenseName);
                    break;
                case SortState.AmountAsc:
                    ExpensesList = ExpensesList.OrderBy(s => s.Amount);
                    break;
                case SortState.AmountDesc:
                    ExpensesList = ExpensesList.OrderByDescending(s => s.Amount);
                    break;
                case SortState.ExpenseTypeNameAsc:
                    ExpensesList = ExpensesList.OrderBy(s => s.ExpenseType.Name);
                    break;
                case SortState.ExpenseTypeNameDesc:
                    ExpensesList = ExpensesList.OrderByDescending(s => s.ExpenseType.Name);
                    break;
                default:
                    ExpensesList = ExpensesList.OrderBy(s => s.ExpenseName);
                    break;
            }

            var count = ExpensesList.Count();
            var items = ExpensesList.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            IEnumerable<ExpenseType> ExpenseTypesList = _db.ExpenseTypes;
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                Expenses = items,
                PageViewModel = pageViewModel,
                FilterViewModel = new FilterViewModel(ExpenseTypesList.ToList(), expenseTypeId, name),
                SortViewModel = new SortViewModel(sortOrder),
            };


            return View(viewModel);
            
        }

        // GET-Create
        public IActionResult Create()
        {
            //IEnumerable<SelectListItem> TypeDropDown = _db.ExpenseTypes.Select(i => new SelectListItem
            //{
            //    Text = i.Name,
            //    Value = i.Id.ToString()
            //});

            //ViewBag.TypeDropDown = TypeDropDown;

            ExpenseVM expenseVM = new ExpenseVM()
            {
                Expense = new Expense(),
                TypeDropDown = _db.ExpenseTypes.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            return View(expenseVM);
        }

        // POST-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ExpenseVM obj)
        {
            if (ModelState.IsValid)
            {
                //obj.ExpenseTypeId = 1;
                _db.Expenses.Add(obj.Expense);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
           
        }


        // GET Delete
        public IActionResult Delete(int? id)
        {
           
            if (id == null || id==0)
            {
                return NotFound();
            }
            var obj = _db.Expenses.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
            return View(obj);

        }

        // POST Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Expenses.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
           
            _db.Expenses.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        // GET Update
        public IActionResult Update(int? id)
        {
            ExpenseVM expenseVM = new ExpenseVM()
            {
                Expense = new Expense(),
                TypeDropDown = _db.ExpenseTypes.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            if (id == null || id == 0)
            {
                return NotFound();
            }
            expenseVM.Expense = _db.Expenses.Find(id);
            if (expenseVM.Expense == null)
            {
                return NotFound();
            }
            return View(expenseVM);

        }

        // POST UPDATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ExpenseVM obj)
        {
            if (ModelState.IsValid)
            {
                _db.Expenses.Update(obj.Expense);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);

        }
    }
}
