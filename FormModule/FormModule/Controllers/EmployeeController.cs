using FormModule.Data;
using FormModule.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FormModule.Controllers
{
    public class EmployeeController : Controller
    {
        private  readonly ApplicationContext context;
            public EmployeeController(ApplicationContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var result = context.Employees.ToList();
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult Create(Employee model)
        {
            if(ModelState.IsValid)
            {
                var emp = new Employee()
                {
                    Name=model.Name,
                    Mobile=model.Mobile,
                    Salary=model.Salary,
                    State=model.State,
                    Country=model.Country
                };
                context.Employees.Add(emp);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Empty field can't be submitted !";
              return View();
            }
            
        }
        public IActionResult Delete(int id)
        {
            var emp = context.Employees.SingleOrDefault(e => e.Id == id);
            context.Employees.Remove(emp);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public  IActionResult Edit(int id) 
        {
            var emp=context.Employees.SingleOrDefault(e=>e.Id==id);
            var result = new Employee()
            {
                Name = emp.Name,
                Mobile = emp.Mobile,
                Salary = emp.Salary,
                State = emp.State,
                Country = emp.Country
            };
            return View(result);
        }
       
        [HttpPost]
        public IActionResult Edit(Employee model)
        {
            var emp=new Employee()
            {
                Name = model.Name,
                Mobile = model.Mobile,
                Salary = model.Salary,
                State = model.State,
                Country = model.Country
            };
            context.Employees.Update(emp);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
         public IActionResult Update()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Update(int id)
        {
            return View();
        }

    }

}
