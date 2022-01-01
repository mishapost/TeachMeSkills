using System;

namespace HomeWork2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("В данной программе будем работать с двумерными матрицами чисел");
                var matrix = CreateMatrix(EnterNumber("Введите к-во столбцов матрицы"), EnterNumber("Введите к-во строк матрицы"));
                GenerateMenu(matrix);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.GetBaseException().Message}");
            }
            finally
            {
                Console.WriteLine("Нажмите Enter для завершения программы");
                Console.ReadLine();
            }
        }

        /// <summary> Корректный ввод числа </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static int EnterNumber(string message)
        {
            Console.Clear();
            var check = false;
            int numeric;
            do
            {
                Console.WriteLine(message);

                if (int.TryParse(Console.ReadLine(), out numeric))
                {
                    check = true;
                }
                else
                {
                    Console.WriteLine("Ошибка! Введенное значение не является числом!");
                }
            } while (!check);
            return numeric;
        }

        /// <summary> Создание матрицы и ввод ее значений </summary>
        /// <param name="columns"></param>
        /// <param name="lines"></param>
        /// <returns></returns>
        private static int[,] CreateMatrix(int columns, int lines)
        {
            if (columns <= 0) throw new Exception("Количество столбцов матрицы должно быть целым положительным числом");
            if (lines <= 0) throw new Exception("Количество строк матрицы должно быть целым положительным числом");

            var matrix = new int[lines, columns];

            // Заполняю матрицу
            for (var i = 0; i < lines; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    matrix[i, j] = EnterNumber($"Введите значение элемента матрицы [{i},{j}]");
                }
            }

            return matrix;
        }

        /// <summary>
        /// Печать матрицы на консоль
        /// </summary>
        /// <param name="matrix"></param>
        private static void WriteMatrix(int[,] matrix)
        {
            Console.WriteLine("Введенная матрица:");

            for (var i = 0; i < matrix.GetUpperBound(0) + 1; i++)
            {
                for (var j = 0; j < matrix.GetUpperBound(1) + 1; j++)
                {
                    Console.Write($"{matrix[i, j]} \t");
                }
                Console.WriteLine();
            }
        }

        private static void GenerateMenu(int[,] matrix)
        {
            if (matrix == null) throw new Exception("Что-то пошло не так. Берите пиво и рыбку и звоните разработчику");
            string menuItem;
            do
            {
                Console.Clear();
                Console.WriteLine("1) Отобразить исходную матрицу");
                Console.WriteLine("2) Найти количество положительных/отрицательных чисел в матрице");
                Console.WriteLine("3) Сортировка элементов матрицы построчно (в двух направлениях)");
                Console.WriteLine("4) Инверсия элементов матрицы построчно");
                Console.WriteLine("0) Выход");
                Console.WriteLine();
                Console.WriteLine("Введите номер действия:");

                menuItem = Console.ReadLine();
                Console.Clear();
                switch (menuItem)
                {
                    case "1":
                        WriteMatrix(matrix);
                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                    case "4":
                        break;
                    case "0":
                        Console.WriteLine("Bye,Bye, Bye...");
                        return;
                    default:
                        Console.WriteLine("Пора сходить к окулисту. Нет такого пункта меню.");
                        break;
                }

                Console.WriteLine();
                Console.WriteLine("Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
            } while (string.IsNullOrEmpty(menuItem) || menuItem != "0");



        }

    }
}
