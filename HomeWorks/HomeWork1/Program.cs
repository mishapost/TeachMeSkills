using System;
using System.Globalization;

namespace HomeWork1
{
    class Program
    {
        static void Main()
        {
            try
            {
                GetDate();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetBaseException().Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }

        private static void GetDate()
        {
            Console.WriteLine("Введите число дня недели");
            var numberDay = Console.ReadLine();

            if (numberDay != null && int.TryParse(numberDay, out var nDay))
            {
                Console.WriteLine(CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat.GetDayName((DayOfWeek)nDay));
            }
        }

    }
}
