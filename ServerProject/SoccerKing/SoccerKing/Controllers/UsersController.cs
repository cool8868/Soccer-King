﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerKing.Models;
using Deepleo.Weixin.SDK;
using Microsoft.Extensions.Options;
using System.Net.Http;
using Codeplex.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SoccerKing.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : Controller
	{
		private readonly soccerkingContext _context;
		private readonly WxConfigurtaionServices _wxConfigService;
		
		public UsersController(soccerkingContext context, WxConfigurtaionServices wxService  )
		{
			_context = context;
			_wxConfigService = wxService;
		}
		// GET api/<controller>/5
		[HttpGet("{idx}")]
		public async Task<ActionResult<Users>> Get(int idx)
		{
			return await _context.Users.FindAsync(idx);
		}

		// POST: api/Todo
		[HttpPost]
		public async Task<IActionResult> PostUsers(Users users)
		{
			_context.Users.Add(users);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		// PUT: api/Todo/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutUsers(string id, Users users)
		{
			if (id != users.OpenId)
			{
				return BadRequest();
			}

			_context.Entry(users).State = EntityState.Modified;
			await _context.SaveChangesAsync();

			return NoContent();
		}


		[HttpGet("login/{token}")]
		public IActionResult GetAccessToken(string token)
		{
			//Log l = new Log();
			//l.Content = "clientToken:" + token;
			//l.Rowtime = DateTime.Now;
			//_context.Log.Add(l);
			//_context.SaveChanges();

			string appId = _wxConfigService.WxConfiguration.AppId;
			string appSecret = _wxConfigService.WxConfiguration.AppSecret;
			var serverToken = GetAccessToken(token, appId, appSecret);

			//l = new Log();
			//l.Content = serverToken.ToString();
			//l.Rowtime = DateTime.Now;
			//_context.Log.Add(l);
			//_context.SaveChanges();

			return NoContent();
		}

		public dynamic GetAccessToken(string code, string appId, string appSecret)
		{
			var client = new HttpClient();
			var result = client.GetAsync(string.Format("https://api.weixin.qq.com/sns/jscode2session?appid={0}&secret={1}&js_code={2}&grant_type=authorization_code", appId, appSecret, code)).Result;
			if (!result.IsSuccessStatusCode) return null;
			return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
		}

	}
}
