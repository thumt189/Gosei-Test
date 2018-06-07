using Gosei.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gosei.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeQualificationEntities db = new EmployeeQualificationEntities();
        // GET: Employee
        public ActionResult Index(string key, int? page, string sort)
        {
            //TempData["isFirstLoad"] = true;

            //if (TempData["message"] == null)
            //{
            //    TempData["isFirstLoad"] = false;
            //}
            List<Employee> list;
            ViewBag.FirstNameSortParm = String.IsNullOrEmpty(sort) ? "FirstName" : "";
            ViewBag.LastNameSortParm = String.IsNullOrEmpty(sort) ? "LastName" : "";
            switch (sort)
            {
                case "FirstName":
                    list = db.Employees.OrderByDescending(x => x.FirstName).ToList();
                    break;
                case "LastName":
                    list = db.Employees.OrderByDescending(x => x.LastName).ToList();
                    break;
                default:
                    if (!String.IsNullOrEmpty(key))
                    {
                        list = db.Employees.Where(x => x.FirstName.Contains(key) || x.LastName.Contains(key) || x.MiddleName.Contains(key)).ToList();
                    }
                    else
                    {
                        list = db.Employees.ToList();
                    }
                    break;
            }

            List<ModelEmployees> emp = new List<ModelEmployees>();

            foreach (var item in list)
            {
                emp.Add(new ModelEmployees()
                {
                    Id = item.Id,
                    FirstName = item.FirstName,
                    MiddleName = item.MiddleName,
                    LastName = item.LastName,
                    Gender = item.Gender,
                    BirthDate = item.BirthDate,
                    Email = item.Email,
                    Note = item.Note
                });
            }

            int pageSize = 5;
            int pageNumber = 1;
            pageNumber = (page.HasValue ? Convert.ToInt32(page) : 1);
            IPagedList<ModelEmployees> view = null;
            view = emp.ToPagedList(pageNumber, pageSize);
            return View(view);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Employee emp = db.Employees.Find(id);
            ModelView emp_view = new ModelView();
            emp_view.Employee = new ModelEmployees();
            emp_view.EmployeeQualifis = new List<ModelEmployeeQua>();

            emp_view.Employee.Id = emp.Id;
            emp_view.Employee.FirstName = emp.FirstName;
            emp_view.Employee.MiddleName = emp.MiddleName;
            emp_view.Employee.LastName = emp.LastName;
            emp_view.Employee.Gender = emp.Gender;
            emp_view.Employee.BirthDate = emp.BirthDate;
            emp_view.Employee.Email = emp.Email;
            emp_view.Employee.Note = emp.Note;

            List<EmployeeQualification> qualifis = emp.EmployeeQualifications.ToList();
            foreach (var item in qualifis)
            {
                emp_view.EmployeeQualifis.Add(new ModelEmployeeQua()
                {
                    Id = item.Id,
                    EmployeeId = item.EmployeeId,
                    QualificationId = item.QualificationId,
                    Institution = item.Institution,
                    City = item.City,
                    ValidFrom = item.ValidFrom,
                    ValidTo = item.ValidTo,
                    Note = item.Note,
                    QuaName = item.Qualification.Name
                });
            }

            return View(emp_view);
        }

        [HttpPost]
        public ActionResult Edit(ModelEmployees item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Employee emp = new Employee();
                    emp.Id = item.Id;
                    emp.FirstName = item.FirstName;
                    emp.MiddleName = item.MiddleName;
                    emp.LastName = item.LastName;
                    emp.Gender = item.Gender;
                    emp.BirthDate = item.BirthDate;
                    emp.Email = item.Email;
                    emp.Note = item.Note;
                    db.Entry(emp).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["message"] = "Edit success";
                    return RedirectToAction("Index", "Employee");
                }
                catch (Exception e)
                {
                    ViewBag.edit_massage = e.Message;
                }
            }
            return View(item);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(ModelEmployees item)
        {
            if (ModelState.IsValid)
            {
                Employee emp = new Employee();
                emp.Id = item.Id;
                emp.FirstName = item.FirstName;
                emp.MiddleName = item.MiddleName;
                emp.LastName = item.LastName;
                emp.Gender = item.Gender;
                emp.BirthDate = item.BirthDate;
                emp.Email = item.Email;
                emp.Note = item.Note;
                db.Employees.Add(emp);
                TempData["message"] = "Insert success";
                db.SaveChanges();
                return RedirectToAction("Index", "Employee");
            }
            return View(item);
        }

        public ActionResult Delete(int id)
        {
            Employee emp = db.Employees.Find(id);
            var qua = db.EmployeeQualifications.Where(x => x.EmployeeId == id);
            if (emp != null)
            {
                if (qua != null)
                {
                    db.EmployeeQualifications.RemoveRange(qua);
                }
                db.Employees.Remove(emp);
                TempData["message"] = "Delete success";
                db.SaveChanges();

                return RedirectToAction("Index", "Employee");
            }

            return View(emp);
        }

        public ActionResult Create_Qualifi(ModelView model)
        {
            if (ModelState.IsValid)
            {
                EmployeeQualification data = new EmployeeQualification();

                db.EmployeeQualifications.Add(data);
                TempData["message"] = "Insert success";
                db.SaveChanges();
                return RedirectToAction("Edit", "Employee");
            }
            return View();
        }
    }
}