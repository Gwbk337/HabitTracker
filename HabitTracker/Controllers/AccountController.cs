using HabitTracker.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HabitTracker.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;


        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }



        // Регистрация
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }




        [HttpPost]
        public async Task<IActionResult> Register(
            User user,
            string password)
        {

            if (!ModelState.IsValid)
            {
                return View(user);
            }



            user.UserName = user.Email;



            var result = await _userManager.CreateAsync(
                user,
                password
            );



            if (result.Succeeded)
            {

                await _signInManager.SignInAsync(
                    user,
                    false
                );


                return RedirectToAction(
                    "Index",
                    "Habit"
                );
            }



            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(
                    "",
                    error.Description
                );
            }


            return View(user);
        }





        // Вход
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }




        [HttpPost]
        public async Task<IActionResult> Login(
            string email,
            string password)
        {


            var result =
                await _signInManager.PasswordSignInAsync(
                    email,
                    password,
                    false,
                    false
                );



            if (result.Succeeded)
            {
                return RedirectToAction(
                    "Index",
                    "Habit"
                );
            }



            ViewBag.Error =
                "Неверный email или пароль";


            return View();
        }





        // Выход
        public async Task<IActionResult> Logout()
        {

            await _signInManager.SignOutAsync();


            return RedirectToAction(
                "Login"
            );
        }

    }
}