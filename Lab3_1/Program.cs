using System;
using System.Linq;
namespace Lab3_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Консольний застосунок, реалізує вказані дії:");
            Console.WriteLine("а) видаляє всі слова, що починаються з голосних літер [аеиіоуяюєї] \nб) підраховує кількість слів у тексті.");
            Console.WriteLine("Для виходу введіть: q\n");

            string str = null; // Для зберігання введеного рядка
            do
            {
                try
                {
                    Console.Write("Введіть рядок: ");
                    str = Console.ReadLine().Trim(); // Збереження введеного рядка, та видалення зайвих пробілів
                    if (str.Length == 0) // якщо введений рядок дорівнює нулю
                    {                   // створити виняток
                        throw new Exception("Довжина рядка дорівнює нулю");
                    }
                    Console.WriteLine("Кількість введених слів: " + NumberOfWords(str)); // виводить кількість введених слів

                    string dellVowels = DellVowels(str); // зберігає відформатований рядок в якому видалені слова, що починаються з голосних літер [аеиіоуяюєї]
                    Console.WriteLine("\nresult: {0}", dellVowels); // виводить цей рядок на екран
                    Console.WriteLine("Кількість слів: " + NumberOfWords(dellVowels)); // підраховує скільки в новому рядку слів
                }
                catch (Exception ex)
                {
                    Console.WriteLine("УВАГА: {0}", ex.Message); // замість крашу виводить що сталось в консоль
                }
                Console.WriteLine(); // порожній рядок
            } while (str != "q" && str != "й"); // зациклює це діло доки не буде команди на вихід
        }
        static int NumberOfWords(string str) // підраховує кількість слів в рядку
        {
            // символи які розділюють слова
            string[] tmp = str.Split(' '); // розбиває рядок на декілька рядків
            tmp = tmp.Where(s => s.Length != 0).ToArray(); // видаляє зайві пробіли
            str = string.Join(" ", tmp); // зліплює то всьо до купи
            return str.Split(' ').Length; // повертає кількість слів
        }
        static string DellVowels(string str) // видаляє з рядка[str] слова, що починаються з голосних літер [аеиіоуяюєї]
        {
            str = str.Trim();
            char[] vowels = "аеиіоуяюєї".ToCharArray(); // масив з голосними літерами
            string[] buf = str.Split(' '); // розбиває рядок на слова
            string result = null; // зберігає результат виконання цього метода
            for (int i = 0; i < buf.Length; i++) // цикл по словам
            {
                if (!Test(buf[i], vowels)) // якщо метод Test повернув false то слово не починається з голосних літер [аеиіоуяюєї]
                {
                    result = string.Concat(result, " ", buf[i]); // зберігання слова яке не починається з голосних літер в змінну result, зі збереження минулих брережених слів
                }
            }
            // якщо result не дорівнює null, повертає результат виконання метода, в іншому випадку створює виняток - "Після видалення не залишилось слів" 
            return result != null ? result.Trim() : throw new Exception("Після видалення не залишилось слів");
        }
        static bool Test(string str, char[] vowels) // метод повертає true якщо слово починається з символа який є в масиві vowels (в даному випадку це масив голосних літер [аеиіоуяюєї])
        {
            for (int i = 0; i < vowels.Length; i++) // цикл по масиву з голосними літерами
            {
                if (str == "") { continue; }
                // якщо перший символ в слові, дорівнює символу з масива, повертає true (перевіряє у нижньому та верхньому регістрі)
                if (str[0] == vowels[i] || str[0] == char.ToUpper(vowels[i]))
                {
                    return true;
                }
            }
            return false; // якщо слово не починається з голосних літер, повертає false
        }
    }
}
