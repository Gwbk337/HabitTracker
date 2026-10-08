using System.ComponentModel.DataAnnotations;

namespace HabitTracker.Models
{
    public class Habit
    {
        public int Id { get; set; }


        [Required]
        public string Name { get; set; } = "";


        public string Category { get; set; } = "Другое";


        public DateTime CreatedDate { get; set; } = DateTime.Now;


        // Владелец привычки
        public int UserId { get; set; }

        public User User { get; set; } = null!;


        public List<HabitCompletion> Completions { get; set; } = new();


        public bool DoneToday
        {
            get
            {
                return Completions.Any(x =>
                    x.Date.Date == DateTime.Today);
            }
        }


        public int Streak
        {
            get
            {
                int count = 0;

                DateTime day = DateTime.Today;


                while (
                    Completions.Any(x =>
                    x.Date.Date == day))
                {
                    count++;
                    day = day.AddDays(-1);
                }


                return count;
            }
        }
    }
}