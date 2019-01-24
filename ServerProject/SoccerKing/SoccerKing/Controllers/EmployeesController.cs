using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerKing.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SoccerKing.Controllers
{
	[Route("api/[controller]")]
	public class EmployeesController : ControllerBase
	{
		private readonly soccerkingContext _context;

		public EmployeesController(soccerkingContext context)
		{
			_context = context;
		}

		// GET api/<controller>/5
		[HttpGet("{idx}")]
		public async Task<ActionResult<Employee>> Get(int idx)
		{
			return await _context.Employee.FindAsync(idx);
		}

		// GET api/<controller>/5
		[HttpGet("u/{uid}")]
		public async Task<ActionResult<IEnumerable<Employee>>> GetByUid(string uid)
		{
			return await _context.Employee.Where(b => b.Uid == uid).ToListAsync();
		}

		// POST: api/Todo
		[HttpPost]
		public async Task<ActionResult<Users>> PostEmployee(Employee Employee)
		{
			_context.Employee.Add(Employee);
			await _context.SaveChangesAsync();

			return CreatedAtAction("Get", new { id = Employee.Idx }, Employee);
		}

		// PUT: api/Todo/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutEmployee(int id, Employee Employee)
		{
			if (id != Employee.Idx)
			{
				return BadRequest();
			}

			_context.Entry(Employee).State = EntityState.Modified;
			await _context.SaveChangesAsync();

			return NoContent();
		}

		// DELETE: api/Todo/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<Employee>> DeleteEmployee(int id)
		{
			var Employee = await _context.Employee.FindAsync(id);
			if (Employee == null)
			{
				return NotFound();
			}

			_context.Employee.Remove(Employee);
			await _context.SaveChangesAsync();

			return NoContent();
		}
	}
}
