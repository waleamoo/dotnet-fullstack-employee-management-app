using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerApp.Models;

namespace ServerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private DataContext context;
        public EmployeesController(DataContext ctx)
        {
            context = ctx;
        }

        // GET:/api/employee/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var employee = await context.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        // GET:/api/employee
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await context.Employees.ToListAsync();
            return Ok(employees);
        }

        // POST:/api/employee
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
        {
            employee.Id = Guid.NewGuid();
            await context.Employees.AddAsync(employee);
            await context.SaveChangesAsync();
            return Ok(employee);
        }

        // PUT:/api/employee/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, Employee updateEmployee)
        {
            var employee = await context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            employee.Name = updateEmployee.Name;
            employee.Email = updateEmployee.Email;
            employee.Salary = updateEmployee.Salary;
            employee.Phone = updateEmployee.Phone;
            employee.Department = updateEmployee.Department;
            await context.SaveChangesAsync();
            return Ok(employee);
        }

        // DELETE:/api/employee/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var employee = await context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            context.Employees.Remove(employee);
            await context.SaveChangesAsync();
            return Ok();
        }


    }
}
