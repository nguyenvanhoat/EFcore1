using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_1.Models;

namespace MVC_1.Areas.Database.Controllers
{
    [Area("Database")]
    [Route("/database-manage/[action]")]
    public class DbManageController : Controller
    {
        private readonly AppDbContext _dbcontext;

        public DbManageController (AppDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DeleteDb()
        {
            return View();
        }

        [TempData]
        public string StatusMess { get; set;}

        [HttpPost]
        public async Task<IActionResult> DeleteDbAsync()
        {
            var success = await _dbcontext.Database.EnsureDeletedAsync();

            StatusMess = success ? "Xóa thành công" : "Không thể xóa";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Migrate()
        {
            await _dbcontext.Database.MigrateAsync();

            StatusMess = "Cập nhật Database thành công";
            return RedirectToAction(nameof(Index));
        }
    }
}