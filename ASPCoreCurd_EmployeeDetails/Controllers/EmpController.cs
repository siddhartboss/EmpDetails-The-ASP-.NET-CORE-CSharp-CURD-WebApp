using ASPCoreCurd_EmployeeDetails.Data;
using ASPCoreCurd_EmployeeDetails.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ASPCoreCurd_EmployeeDetails.Controllers
{
    public class EmpController : Controller
    {
        private readonly ApplicationDBContext _applicationDBContext;
        public EmpController(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var EmpDetails = _applicationDBContext.EmpDetails.ToList();
            return View(EmpDetails);
        }

        [HttpGet]
        public IActionResult View(Guid id)
        {
            var ViewEmpDetails = _applicationDBContext.EmpDetails.Where(x => x.Id == id).FirstOrDefault();
            if (ViewEmpDetails != null)
            {
                ViewEmpModel viewEmpModel = new ViewEmpModel()
                {
                    Id = ViewEmpDetails.Id,
                    Name = ViewEmpDetails.Name,
                    Email = ViewEmpDetails.Email,
                    Salary = ViewEmpDetails.Salary,
                    DateOfBirth = ViewEmpDetails.DateOfBirth,
                    Department = ViewEmpDetails.Department
                };

                return View(ViewEmpDetails);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Delete(EmpModel empModel)
        {
            var empDetails = _applicationDBContext.EmpDetails.Find(empModel.Id);
            if (empDetails != null)
            {
                _applicationDBContext.EmpDetails.Remove(empDetails);
                _applicationDBContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Add(AddEmpModelView addEmpModelView)
        {
            EmpModel empModel = new EmpModel()
            {
                Id = Guid.NewGuid(),
                Name= addEmpModelView.Name,
                Email= addEmpModelView.Email,
                Salary= addEmpModelView.Salary,
                DateOfBirth= addEmpModelView.DateOfBirth,
                Department= addEmpModelView.Department
            };

            _applicationDBContext.EmpDetails.Add(empModel);
            _applicationDBContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult View(ViewEmpModel viewEmpModel)
        {
            var emp = _applicationDBContext.EmpDetails.Find(viewEmpModel.Id);
            if (emp != null)
            {
                emp.Email = viewEmpModel.Email;
                emp.Name= viewEmpModel.Name;
                emp.Salary= viewEmpModel.Salary;
                emp.DateOfBirth= viewEmpModel.DateOfBirth;
                emp.Department= viewEmpModel.Department;

                _applicationDBContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
