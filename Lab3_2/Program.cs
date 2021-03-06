﻿using System;
using System.Linq;
using System.IO;

namespace Lab3_2
{
    public class Program
    {
        private const string FILE_NAME = "MyFile.txt"; // приватна константа з назвою вхідного файла
        private const string FILE_NAME_OUTPUT = "output.txt"; // приватна константа з назвою вихідного файла
        private static double[] y; // приватний масив для збереження 

        private static void Main(string[] args)
        {
            Console.WriteLine("Консольний застосунок, реалізує вказані дії:");
            Console.WriteLine("Створює інший файл ({0}) і записує в нього числа з першого файла ({1}) за таким правилом:", FILE_NAME_OUTPUT, FILE_NAME);
            Console.WriteLine("парні числа помножені на 2, непарні числа збільшені на 1, усі від'ємні числа перетворити за модулем.");
            try
            {
                InputFile(); // метод який зчитає та облобляє вхідний файл за вище вказаними правилами
                OutputFile(); // метод який записує масив чисел у вихідний файл
            }
            catch (Exception e)
            {
                Console.WriteLine("Помилка: " + e.Message); // замість крашу виводить в консоль, що сталось 
            }
            _ = Console.ReadKey(); // pause
        }

        private static void InputFile(string str = null) // метод який зчитає та облобляє вхідний файл за вище вказаними правилами
        {
            if (!File.Exists(FILE_NAME)) // якщо файл не існує створює виключення з відповідним текстом
            {
                throw new Exception("Файл " + FILE_NAME + " не існує");
            }
            using (StreamReader sr = File.OpenText(FILE_NAME)) // відкриває для читання існуючий текстовий файл та зберігає посилання на файл у знінну sr
            {
                string input; // для збереження вмісту файла
                while ((input = sr.ReadLine()) != null) // зберігає у input рядок доки не буде кінець файла
                {
                    str = string.Concat(str, input); // обєднує всі рядки вхідного файла в одну знінну str
                }
                sr.Close(); // закриття файла
            }
            if (null == str) // якщо невдалося хоть щось зберезти з файлу, створюється виключення що файл був порожній
            {
                throw new Exception("Файл " + FILE_NAME + " порожній");
            }

            str = str.TrimEnd(';'); // видаляє з кінця файла знак роздільник
            string[] x = str.Split(';', ' '); // розбиває рядок на декілька рядків за знаком-роздільником
            string originalStr; // для зберігання оригінального фрагмента рядка, для того щоби було понятно через що сталася помилка
            x = x.Where(s => s.Length != 0).ToArray(); // видаляє порожні елементи масива
            y = new double[x.Length]; // масив для збереження цифр як цифр а не слів

            for (int i = 0; i < x.Length; i++) // цикс по елементах масива x
            {
                originalStr = x[i]; // для зберігання оригінального фрагменту рядка, для того щоби було понятно через що сталася помилка
                if (!double.TryParse(x[i], out y[i])) // якщо не вдалося перетворити елемент масиву x[i] в число
                {
                    if (x[i].IndexOf('.') != -1) // якщо проблема була в тому що у файлі була крапка замість коми
                    {
                        x[i] = x[i].Replace('.', ','); // заміняє крапку на кому
                        if (double.TryParse(x[i], out y[i])) // якщо проблема виправлена пропустити ітерацію
                        {
                            continue;
                        }
                    }// в інакшому випадку вивести в консоль який фрагмент тексту невдалося перетворити в число
                    throw new Exception("Невдалось перетворити \"" + originalStr + "\" в число");
                }
            }

            // застосування правил перетворення чисел які були вказані в завданні
           //парні числа помножені на 2, непарні числа збільшені на 1, усі від'ємні числа перетворити за модулем.
            for (int i = 0; i < y.Length; i++) //цикл по масиву з числами
            {
                if (y[i] % 2 == 0) // якщо число парне
                {
                    y[i] *= 2; // помножити його на 2
                }
                else // інакше (якщо воно не парне)
                {
                    y[i]++; // збільшити число на одиницю
                }

                if (y[i] < 0) // якщо число відємне
                {
                    y[i] = -y[i]; // перетворити за модулем.
                }
            }
        }
        private static void OutputFile() // метод який записує масив чисел у вихідний файл
        {
            StreamWriter file = new StreamWriter(FILE_NAME_OUTPUT); // використовуємо зміну file для доступу до вихідного файла

            file.Write("arr = { "); // запис у вихідний файл вказаний текст
            for (int i = 0; i < y.Length - 1; i++) // цикл по масиву y (масив з числами)
            {
                file.Write((y[i].ToString().Replace(',', '.')) + ", "); // число y[i] перетворюється в текст і в ньому заміняється кома на крапку, та додається кома в кінець
            }
            file.Write(y[y.Length - 1].ToString().Replace(',', '.') + " };"); // запис останнього числа аналогічно до попередніх, за винятком того що в кінці замість коми буде знак }
            file.Close(); // закриття файла
        }
    }
}
