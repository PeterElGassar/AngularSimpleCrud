using System.Linq;
using APIApp.Date;
using APIApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeparmentController : ControllerBase
    {
        private readonly AppContext _context;
        public DeparmentController(AppContext context)
        {
            _context = context;

        }

        [HttpGet]
        public JsonResult GetDeparments()
        {
            var departments = _context.Departments.ToList();

            return new JsonResult(departments);
        }
        [HttpPost]
        public JsonResult PostDeparment(Department dto)
        {
            _context.Departments.Add(dto);
            _context.SaveChanges();

            return new JsonResult("Added Successufully.");
        }

        [HttpPut]
        public JsonResult PutDeparment(Department dto)
        {
            var departmentInDb = _context.Departments.Find(dto.Id);
            departmentInDb.Name = dto.Name;
            _context.SaveChanges();


            return new JsonResult("Updated Successufully.");
        }

        [HttpDelete("{id}")]
        public JsonResult DeleteDeparment(int id)
        {
            var departmentInDb = _context.Departments.Find(id);
            if (departmentInDb != null)       
                _context.Departments.Remove(departmentInDb);
            
            _context.SaveChanges();

            return new JsonResult("Deleted Successufully.");
        }
    }
}