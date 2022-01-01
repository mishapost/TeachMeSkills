using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace HomeWork3
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Введите полный путь к файлу, откуда надо считать текст (Есть файл по умолчанию: test.txt)");
                var txt = new WordProcessing(ReadFromFile(Console.ReadLine()));
                GenerateMenu(txt);
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Я не упал, ни при каких условиях, хоть и произошла ошибка: {ex.GetBaseException().Message}");
            }
            finally
            {
                Console.WriteLine("Нажмите Enter для завершения программы");
                Console.ReadLine();
            }
        }

        private static string ReadFromFile(string path)
        {
            if (string.IsNullOrEmpty(path)) { throw new Exception("Так дело не пойдет, мне надо знать имя файла, откуда читать данные"); }

            string data;
            using (var sr = new StreamReader(path))
            {
                data = sr.ReadToEnd();
            }
            if (string.IsNullOrEmpty(data)) { throw new Exception("Файл по указанному пути нашел, открыл и прочитал, но он оказался пустой"); }

            return data;
        }

        private static void GenerateMenu(WordProcessing txt)
        {
            if (txt == null) throw new Exception("Что-то пошло не так. Берите пиво и рыбку и звоните разработчику");
            string menuItem; 
            do
            {
                Console.Clear();
                Console.WriteLine("1) Показать исходные данные, с которыми будем работать");
                Console.WriteLine("2) Найти слова, содержащие максимальное количество цифр");
                Console.WriteLine("3) Найти самое длинное слово и определить, сколько раз оно встретилось в тексте.");
                Console.WriteLine("4) Заменить цифры от 0 до 9 на слова «ноль», «один», …, «девять»");
                Console.WriteLine("5) Вывести на экран сначала вопросительные, а затем восклицательные предложения.");
                Console.WriteLine("6) Вывести на экран только предложения, не содержащие запятых.");
                Console.WriteLine("7) Найти слова, начинающиеся и заканчивающиеся на одну и ту же букву");
                Console.WriteLine("0) Выход");
                Console.WriteLine();
                Console.WriteLine("Введите номер действия:");
                
                menuItem = Console.ReadLine();
                List<string> words;
                List<string> suggestions;
                Console.Clear();
                switch (menuItem)
                {
                    case "1":
                        Console.WriteLine(txt.SourceText);
                        break;
                    case "2":
                        // Толком не понял условие задания. Что значит слова содержащее макс к-во цифр?? Не может быть в слове цифр
                        // Считаю, что под такими словами имеются ввиду только одни цифры
                        words = txt.GetWords();
                        if (words != null)
                        {
                            Console.WriteLine("Слова состоящие только из цифр:");
                            foreach (var item in words.Where(item => int.TryParse(item, out _)))
                            {
                                Console.WriteLine(item);
                            }
                        } else Console.WriteLine("В тексте нет никаких слов :-)");
                        break;
                    case "3":
                        words = txt.GetWords();
                        if (words != null)
                        {
                            var maxLengthWord = words.Max(a=>a.Length);
                            var maxWord = words.FirstOrDefault(a => a.Length == maxLengthWord);
                            var count = words.Where(a => a.Equals(maxWord))?.Count() ?? 0;
                            Console.WriteLine($"В тексте самое длинное слово по к-ву букв: \"{maxWord}\" и встречается оно в тексте {count} раз(а)");
                        } else Console.WriteLine("В тексте нет никаких слов :-)");
                        break;
                    case "4":
                        Console.WriteLine(txt.SourceText.Replace("0","Ноль")
                                                        .Replace("1", "Один")
                                                        .Replace("2", "Два")
                                                        .Replace("3", "Три")
                                                        .Replace("4", "Четыре")
                                                        .Replace("5", "Пять")
                                                        .Replace("6", "Шесть")
                                                        .Replace("7", "Семь")
                                                        .Replace("8", "Восемь")
                                                        .Replace("9", "Девять"));
                        break;
                    case "5":
                        suggestions = txt.GetSuggestions();
                        if (suggestions != null)
                        {
                            Console.WriteLine("Вопросительные предложения:");
                            foreach (var item in suggestions.Where(a=>a.LastIndexOf('?') == a.Length-1).Distinct())
                            {
                                Console.WriteLine(item);
                            }

                            Console.WriteLine();
                            Console.WriteLine("Восклицательные предложения:");
                            foreach (var item in suggestions.Where(a => a.LastIndexOf('!') == a.Length - 1).Distinct())
                            {
                                Console.WriteLine(item);
                            }
                        } else Console.WriteLine("Предложения не обнаружены");
                        break;
                    case "6":
                        suggestions = txt.GetSuggestions();
                        if (suggestions != null)
                        {
                            Console.WriteLine("Предложения, не содержащие запятых:");
                            foreach (var item in suggestions.Where(a => a.LastIndexOf(',') == -1).Distinct())
                            {
                                Console.WriteLine(item);
                            }
                        }
                        else Console.WriteLine("Предложения не обнаружены");
                        break;
                    case "7":
                        words = txt.GetWords();
                        if (words != null)
                        {
                            Console.WriteLine("Слова начинающиеся и заканчивающиеся на одну и туже букву");
                            foreach (var item in words.Distinct())
                            {
                                var word = item.ToUpper();
                                var lastIndex = word.Length - 1;
                                if (word[0] == word[lastIndex])
                                {
                                    Console.WriteLine(item);
                                }
                            }
                        }
                        else Console.WriteLine("В тексте нет никаких слов :-)");
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
            } while (string.IsNullOrEmpty(menuItem) || menuItem!="0");



        }


    }
}
