using Microsoft.AspNetCore.Identity;

namespace HabitTracker.Models
{
    public class User : IdentityUser<int>
    {

        public string Name { get; set; } = "";


        public bool IsPremium { get; set; } = false;


        public List<Habit> Habits { get; set; } = new();

    }
}