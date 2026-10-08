using HabitTracker.Data;
using HabitTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace HabitTracker.Controllers
{

    [Authorize]
    public class StatisticsController : Controller
    {

        private readonly ApplicationDbContext db;
        private readonly UserManager<User> userManager;


        public StatisticsController(
            ApplicationDbContext db,
            UserManager<User> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }



        public async Task<IActionResult> Index()
        {

            var user = await userManager.GetUserAsync(User);


            var habits = await db.Habits
                .Where(x => x.UserId == user!.Id)
                .Include(x => x.Completions)
                .ToListAsync();



            StatisticsViewModel model = new StatisticsViewModel();


            model.TotalHabits = habits.Count;


            model.CompletedToday =
                habits.Count(x => x.DoneToday);



            model.TotalCompletions =
                habits.Sum(x => x.Completions.Count);



            model.BestStreak =
                habits.Count == 0
                ? 0
                : habits.Max(x => x.Streak);



            return View(model);

        }

    }

}