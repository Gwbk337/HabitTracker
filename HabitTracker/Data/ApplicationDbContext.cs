using HabitTracker.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace HabitTracker.Data
{
    public class ApplicationDbContext
        : IdentityDbContext<User, IdentityRole<int>, int>
    {

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public DbSet<Habit> Habits { get; set; } = null!;


        public DbSet<HabitCompletion> HabitCompletions { get; set; } = null!;

    }
}