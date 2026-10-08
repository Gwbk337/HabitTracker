using HabitTracker.Data;
using HabitTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace HabitTracker.Controllers
{

    [Authorize]
    public class HabitController : Controller
    {

        private readonly ApplicationDbContext db;
        private readonly UserManager<User> userManager;


        public HabitController(
            ApplicationDbContext db,
            UserManager<User> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }



        public async Task<IActionResult> Index()
        {

            var user =
                await userManager.GetUserAsync(User);


            var habits =
                await db.Habits
                .Where(x => x.UserId == user!.Id)
                .Include(x => x.Completions)
                .ToListAsync();


            return View(habits);
        }





        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {

            var user =
                await userManager.GetUserAsync(User);



            var count =
                await db.Habits
                .CountAsync(x => x.UserId == user!.Id);



            // Бесплатная версия
            if (!user!.IsPremium && count >= 3)
            {
                TempData["Error"] =
                    "Бесплатная версия позволяет только 3 привычки";

                return RedirectToAction("Index");
            }



            Habit habit = new Habit
            {
                Name = name,
                UserId = user.Id
            };


            db.Habits.Add(habit);

            await db.SaveChangesAsync();


            return RedirectToAction("Index");
        }





        public async Task<IActionResult> Complete(int id)
        {

            var habit =
                await db.Habits
                .Include(x => x.Completions)
                .FirstAsync(x => x.Id == id);



            HabitCompletion completion =
                new HabitCompletion
                {
                    HabitId = id,
                    Date = DateTime.Now
                };


            db.HabitCompletions.Add(completion);


            await db.SaveChangesAsync();


            return RedirectToAction("Index");
        }




        public async Task<IActionResult> Delete(int id)
        {

            var habit =
                await db.Habits.FindAsync(id);


            if (habit != null)
            {
                db.Habits.Remove(habit);

                await db.SaveChangesAsync();
            }


            return RedirectToAction("Index");
        }

    }
}