using System;
using System.IO;

namespace Lab3_3
{
    class Program
    {
        private const string FILE_NAME = "input.txt"; // приватна константа з назвою вхідного файла
        private const string FILE_NAME_OUTPUT = "output.txt"; // приватна константа з назвою вихідного файла
        static void Main(string[] args)
        {
            Console.WriteLine("Консольний застосунок, реалізує вказані дії:");
            Console.WriteLine("текстовий рядок імпортується з деякого наперед створеного файла {0}\nа результати роботи програмипотрібно записати у новостворений під час виконання проекту файл {1}", FILE_NAME, FILE_NAME_OUTPUT);
            Console.WriteLine("б) видаляє всі слова, що складаються тільки з латинських літер.\n");
            try
            {
                string str = InputFile(); // збереження в змінну str вміст вхідного файла
                str = DellVowels(str); // видаляє всі слова, що складаються тільки з латинських літер
                OutputFile(str); // записує відредагований текст у файл
            }
            catch (Exception ex)
            {
                Console.WriteLine("УВАГА: {0}", ex.Message); // замість крашу виводить в консоль, що сталось 
            }
            Console.WriteLine(); // pause
        }
        private static string InputFile(string str = null) // зчитує текст з файла, та повертає його у вигляді деякої змінної типу string
        {
            if (!File.Exists(FILE_NAME)) // якщо файл не існує створює виключення з відповідним текстом
            {
                throw new Exception("Файл " + FILE_NAME + " не існує"); 
            }
            using (StreamReader sr = File.OpenText(FILE_NAME)) // відкриває для читання існуючий текстовий файл та зберігає посилання на файл у знінну sr
            {
                String input; // для збереження вмісту файла
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
            return str; // повернення вмісту файла
        }

        private static void OutputFile(string str) // записує текст у файл
        {
            StreamWriter file = new StreamWriter(FILE_NAME_OUTPUT); // використовуємо зміну file для доступу до вихідного файла
            file.Write(str); // записує у вихідний файл вказаний текст
            file.Close(); // закриття файла
        }
        static string DellVowels(string str) // видаляє всі слова, що складаються тільки з латинських літер
        {
            str = str.Trim(); // видалення зайвих пробілів
            char[] vowels = "abcdefghijklmnopqrstuvwxyz".ToCharArray(); // масив з латинських літер
            string[] buf = str.Split(' '); // розбиває рядок на декілька рядків
            string result = ""; // для збереження результату
            for (int i = 0; i < buf.Length; i++) // цикл по словам
            {
                if (Test(buf[i], vowels)) // якщо метод повертає true
                {// обєднує це слово з попередніми
                    result = string.Concat(result, " ", buf[i]);
                }
            }
            return result.Trim(); // повернення результату
        }
        static bool Test(string str, char[] vowels) // перевіряє чи слово складається лише з латинських літер
        {
            bool tmp; // флажок
            for (int i = 0; i < str.Length; i++) // цикл по буквам у слові
            {
                tmp = false;
                for (int j = 0; j < vowels.Length; j++) // цикл по латинських літерах
                {
                    // якщо літера є в масиві в любому регістрі tmp = true;
                    if (str[i] == vowels[j] || str[i] == char.ToUpper(vowels[j]))
                    {
                        tmp = true;
                    }
                    if (tmp) { break; }// якщо tmp = true можна покинути цей цикл
                }
                if (!tmp) // якщо  tmp дорівнює false повертає true
                {
                    return true;
                }
            }
            return false; // в іншому випадку повертає false
        }
    }
}
