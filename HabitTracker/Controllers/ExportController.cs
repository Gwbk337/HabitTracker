using HabitTracker.Data;
using HabitTracker.Models;
using HabitTracker.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;


namespace HabitTracker.Controllers
{

    [Authorize]
    public class ExportController : Controller
    {

        private readonly ApplicationDbContext db;
        private readonly UserManager<User> userManager;
        private readonly Exporter exporter;



        public ExportController(
            ApplicationDbContext db,
            UserManager<User> userManager,
            Exporter exporter)
        {
            this.db = db;
            this.userManager = userManager;
            this.exporter = exporter;
        }



        public async Task<IActionResult> Report()
        {

            var user =
                await userManager.GetUserAsync(User);



            var habits =
                await db.Habits
                .Where(x => x.UserId == user!.Id)
                .Include(x => x.Completions)
                .ToListAsync();



            string text =
                exporter.CreateReport(
                    user!,
                    habits);



            return File(
                Encoding.UTF8.GetBytes(text),
                "text/plain",
                "HabitTracker_Report.txt"
            );

        }

    }

}