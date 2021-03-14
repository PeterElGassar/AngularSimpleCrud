using System.IO;
using System.Linq;
using APIApp.Date;
using APIApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace APIApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly AppContext _context;
        private readonly IWebHostEnvironment _env;
        public EmployeeController(AppContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public JsonResult GetEmployees()
        {
            var employees = _context.Employees.ToList();

            return new JsonResult(employees);
        }
        [HttpPost]
        public JsonResult PostEmployee(Employee dto)
        {
            _context.Employees.Add(dto);
            _context.SaveChanges();

            return new JsonResult("Added Successufully.");
        }

        [HttpPut]
        public JsonResult PutEmployee(Employee dto)
        {
            var departmentInDb = _context.Employees.Find(dto.Id);
            departmentInDb.Name = dto.Name;
            departmentInDb.ImageUrl = dto.ImageUrl;
            departmentInDb.DepartmentName = dto.DepartmentName;
            departmentInDb.DateOfJoin = dto.DateOfJoin;
            _context.SaveChanges();


            return new JsonResult("Updated Successufully.");
        }

        [HttpDelete("{id}")]
        public JsonResult DeleteEmployee(int id)
        {
            var departmentInDb = _context.Employees.Find(id);
            if (departmentInDb != null)
                _context.Employees.Remove(departmentInDb);

            _context.SaveChanges();

            return new JsonResult("Deleted Successufully.");
        }

        [HttpPost]
        [Route("savefile")]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                //extract the first file whitch attached in the request body
                var postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Images/" + fileName;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                return new JsonResult(fileName);
            }
            catch(System.Exception e)
            {
                return new JsonResult(e.Message);
            }

        }

        [HttpGet]
        [Route("GetAllDepartments")]
        public JsonResult GetAllDepartments()
        {
            var departments = _context.Departments.ToList();

            return new JsonResult(departments);
        }
    }
}