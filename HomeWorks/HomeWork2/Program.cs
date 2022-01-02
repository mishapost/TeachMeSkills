using System;
using System.ComponentModel.Design;
using System.Data;

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

        /// <summary> Печать матрицы на консоль </summary>
        /// <param name="matrix"></param>
        private static void WriteMatrix(int[,] matrix)
        {
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
                        Console.WriteLine("Исходная матрица:");
                        WriteMatrix(matrix);
                        break;
                    case "2":
                        var countPositive = 0;
                        var countNegative = 0;
                        foreach(var item in matrix)
                        {
                            if (item >= 0) {countPositive++;} else {countNegative++;}
                        }
                        Console.WriteLine($"В введенной матрице положительных элементов {countPositive} шт., отрицательных {countNegative} шт.");
                        break;
                    case "3":
                        var rows = matrix.GetUpperBound(0) + 1;
                        var columns = matrix.GetUpperBound(1) + 1;

                        var matrixSortByAsc = new int[rows, columns];
                        var matrixSortByDesc = new int[rows, columns];

                        for (var i = 0; i < rows; i++)
                        {
                            var column = matrix.GetUpperBound(1) + 1;
                            var newRowArray = new int[column];
                            for (var j = 0; j < column; j++)
                            {
                                newRowArray[j] = matrix[i, j];
                            }

                            newRowArray = SortingArray(newRowArray);
                            // сортирую по возрастанию
                            for (var j = 0; j < column; j++)
                            {
                                matrixSortByAsc[i,j] = newRowArray[j];
                            }

                            // сортирую по убыванию
                            var counter = column;
                            for (var j = 0; j < column; j++)
                            {
                                if (counter > 0) { matrixSortByDesc[i, j] = newRowArray[counter - 1]; }
                                counter--;
                            }
                        }

                        Console.WriteLine("Исходная матрица:");
                        WriteMatrix(matrix);
                        Console.WriteLine();
                        Console.WriteLine("Матрица, отсортированная построчно по возрастанию:");
                        WriteMatrix(matrixSortByAsc);
                        Console.WriteLine();
                        Console.WriteLine("Матрица, отсортированная построчно по убыванию:");
                        WriteMatrix(matrixSortByDesc);

                        break;
                    case "4":
                        // Не понял толком условие задачи. Делаю запись наоборот 1 2 3 => 3 2 1
                        var newMatrix = new int[matrix.GetUpperBound(0) + 1, matrix.GetUpperBound(1) + 1];
                        for (var i = 0; i < matrix.GetUpperBound(0) + 1; i++)
                        {
                            var column = matrix.GetUpperBound(1) + 1;
                            var newRowArray = new int[column];
                            for (var j = 0; j < column; j++)
                            {
                                newRowArray[j] = matrix[i, j];
                            }
                            
                            // в новую матрицу ложу задом наперед
                            var counter = column;
                            for (var j = 0; j < column; j++)
                            {
                                if (counter > 0) { newMatrix[i, j] = newRowArray[counter - 1]; }
                                counter--;
                            }
                        }
                        Console.WriteLine("Исходная матрица:");
                        WriteMatrix(matrix);
                        Console.WriteLine();
                        Console.WriteLine("Матрица с инверсией элементов построчно:");
                        WriteMatrix(newMatrix);
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
        
        private static int[] SortingArray(int[] arr)
        {
            for (var i = 1; i < arr.Length; i++)
            {
                for (var j = i; j >0 && arr[j-1]>arr[j]; j--)
                {
                    (arr[j - 1], arr[j]) = (arr[j], arr[j - 1]);
                }
            }
            return arr;
        }
    }
}
