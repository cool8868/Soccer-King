using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerKing.Models;

namespace SoccerKing.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StoryController : ControllerBase
    {
		private readonly soccerkingContext _context;

		public StoryController(soccerkingContext context)
		{
			_context = context;
		}
		// GET: api/<controller>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Story>>> GetAll()
		{
			return await _context.Story.ToListAsync();
		}

		// GET api/<controller>/5
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}

		// POST api/<controller>
		[HttpPost]
		public void Post([FromBody]string value)
		{
		}

		// PUT api/<controller>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE api/<controller>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}