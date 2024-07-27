using AdvPOS.Data;
using AdvPOS.Models;
using AdvPOS.Models.UserAccountViewModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace AdvPOS.Controllers.API
{
    [Route("api/[controller]")] // api/authManagement
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserProfileAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserProfileAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetUserProfile")]
        public async Task<IActionResult> GetUserProfile()
        {
            var items = await _context.UserProfile.ToListAsync();
            return Ok(items);
        }

        [HttpGet]
        [Route("GetUserProfile/{id}")]
        public async Task<IActionResult> GetUserProfile(Int64 id)
        {
            var item = await _context.UserProfile.FirstOrDefaultAsync(x => x.UserProfileId == id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }
        [HttpPost]
        public async Task<IActionResult> CreateItem(UserProfile data)
        {
            if (ModelState.IsValid)
            {
                await _context.UserProfile.AddAsync(data);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetUserProfileBy", new { data.UserProfileId }, data);
            }

            return new JsonResult("Something went wrong") { StatusCode = 500 };
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, UserProfileCRUDViewModel item)
        {
            if (id != item.UserProfileId)
                return BadRequest();

            var existItem = await _context.UserProfile.FirstOrDefaultAsync(x => x.UserProfileId == id);

            if (existItem == null)
                return NotFound();

            existItem = item;

            // Implement the changes on the database level
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var existItem = await _context.UserProfile.FirstOrDefaultAsync(x => x.UserProfileId == id);

            if (existItem == null)
                return NotFound();

            _context.UserProfile.Remove(existItem);
            await _context.SaveChangesAsync();

            return Ok(existItem);
        }
    }
}