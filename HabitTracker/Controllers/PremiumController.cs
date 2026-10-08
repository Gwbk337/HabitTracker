using HabitTracker.Data;
using HabitTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace HabitTracker.Controllers
{

    [Authorize]
    public class PremiumController : Controller
    {

        private readonly UserManager<User> userManager;
        private readonly ApplicationDbContext db;


        public PremiumController(
            UserManager<User> userManager,
            ApplicationDbContext db)
        {
            this.userManager = userManager;
            this.db = db;
        }



        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);

            return View(user);
        }





        [HttpPost]
        public async Task<IActionResult> Buy()
        {

            var user =
                await userManager.GetUserAsync(User);


            user!.IsPremium = true;


            await db.SaveChangesAsync();


            TempData["Success"] =
                "Premium успешно активирован!";


            return RedirectToAction("Index");

        }

    }

}