using HabitTracker.Models;
using System.Text;

namespace HabitTracker.Services
{
    public class Exporter
    {

        public string CreateReport(
            User user,
            List<Habit> habits)
        {

            StringBuilder sb = new();


            sb.AppendLine("===== HabitTracker =====");
            sb.AppendLine();


            sb.AppendLine(
                $"Пользователь: {user.Email}"
            );


            sb.AppendLine(
                $"Дата отчета: {DateTime.Now}"
            );


            sb.AppendLine();


            sb.AppendLine("Привычки:");
            sb.AppendLine();



            foreach (var habit in habits)
            {

                sb.AppendLine(
                    $"Название: {habit.Name}"
                );


                sb.AppendLine(
                    $"Серия: {habit.Streak} дней"
                );


                sb.AppendLine(
                    $"Выполнений: {habit.Completions.Count}"
                );


                sb.AppendLine("-------------------");

            }



            return sb.ToString();

        }

    }
}