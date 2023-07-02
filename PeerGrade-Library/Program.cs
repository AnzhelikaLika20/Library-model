using EBookLib;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace PeerGrade_Library
{
    internal class Program
    {
        /// <summary>
        /// Список букв, с которых начанается название хотя бы одной книги библиотеки.
        /// </summary>
        static List<char>? availableLetters;
        static Random rand = new Random();
        /// <summary>
        /// Метод считывыает из консоли значение, описывающее количество объектов в библиотеке.
        /// </summary>
        /// <param name="n">Переменная, хранящая количество объектов в библиотеке. Передается по ссылке.</param>
        static void EnterCountOfObjects(out int n)
        {
            bool tryParse;
            do
            {
                Console.WriteLine("Введите натуральное число - количество объектов.");
                tryParse = int.TryParse(Console.ReadLine(), out n) && (n > 0);
                if (!tryParse)
                    Console.WriteLine("Введено некорректное значение.");
            }
            while (!tryParse);
        }
        /// <summary>
        /// Метод генерирует строку длины от 1 до 10, которая начинается с заглавной буквы.
        /// </summary>
        /// <returns>Строка заданного формата.</returns>
        static string GenerateString()
        {
            int lengthOfString = rand.Next(1, 11);
            string str = "";
            for (int j = 0; j < lengthOfString; j++)
            {
                int letterNumber = rand.Next('a', 'z' + 1);
                if (j == 0)
                    str += (char)(letterNumber - 32);
                else
                    str += (char)letterNumber;
            }
            return str;
        }
        /// <summary>
        /// Метод генерирует параметры объектов заданного формата и добавляет случайно сгенерированные объекты в библиотеку.
        /// </summary>нуж
        /// <param name="myLibrary">Объект библиотеки, в который нужно добавить печатные издания.</param>
        /// <param name="countOfObjects">Количество объектов, которые нужно добавить в библиотеку.</param>
        static void GenerateAndAddObjects(ref MyLibrary<PrintEdition> myLibrary, int countOfObjects)
        {
            for (int i = 0; i < countOfObjects; i++)
            {
                try
                {
                    int chooseObject = rand.Next(0, 2);
                    int pages = rand.Next(-10, 101);
                    string name = GenerateString();
                    PrintEdition item;
                    if (chooseObject == 0)
                    {
                        string author = GenerateString();
                        item = new Book(name, pages, author);
                        availableLetters.Add(item.Name[0]);
                    }
                    else
                    {
                        int period = rand.Next(-10, 101);
                        item = new Magazine(name, pages, period);
                    }
                    myLibrary.Add(item);
                    item.onPrint += PrintHandler;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    i--;
                }
            }
            Console.WriteLine($"\nБыло сгенерировано {countOfObjects} объекта(ов)!");
        }
        /// <summary>
        /// Метод-обработчик события onPrint. Выводит сообщение о том, что книга напечатана. 
        /// </summary>
        /// <param name="sender">Объект, который вызвал событие.</param>
        /// <param name="e">Информация о событии./</param>
        public static void PrintHandler(object? sender, EventArgs e)
        {
            Console.WriteLine("PRINTED!");
            Console.WriteLine(sender);
        }
        /// <summary>
        /// Метод-обработчик события onTake. Выводит сообщение о том, что книги, названия которых начачинаются на букву letter, были извлечены из библиоткеки.
        /// </summary>
        /// <param name="sender">Объект, который вызвал событие.</param>
        /// <param name="letter">Печатные зидания, которые начинаются с буквы letter, были удалены.</param>
        public static void TakeHandler(object? sender, char letter)
        {
            Console.WriteLine($"ATTENTION! Books starts with {letter} were taken!\n");
        }
        /// <summary>
        /// Вывод линии, которая разделяет смысловые части вывода в консоли.
        /// </summary>
        static void PrintLine()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("--------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.White;
        }
        /// <summary>
        /// Метод для сериализации объекта MyLibrary в файл "PrintEdition.txt".
        /// </summary>
        /// <param name="myLibrary">Объект для сериализации.</param>
        public static void DataContractSerialize(MyLibrary<PrintEdition> myLibrary)
        {
            DataContractJsonSerializer dataContract = new DataContractJsonSerializer(typeof(MyLibrary<PrintEdition>));
            using var stream = new FileStream("MyLibrary.txt", FileMode.Create);
            using var writer = JsonReaderWriterFactory.CreateJsonWriter(stream, System.Text.Encoding.Unicode, false, true);
            dataContract.WriteObject(writer, myLibrary);
            Console.WriteLine("JSON-сериализация успешно завершена!");
        }
        /// <summary>
        /// Метод для десериализации объекта MyLibrary из файла "PrintEdition.txt".
        /// </summary>
        /// <returns>Возвращает десериализованный объект MyLibrary из файла "PrintEdition.txt".</returns>
        public static MyLibrary<PrintEdition> DataContractDeserialize()
        {
            DataContractJsonSerializer dataContract = new DataContractJsonSerializer(typeof(MyLibrary<PrintEdition>));
            MyLibrary<PrintEdition> new_library = new MyLibrary<PrintEdition>();
            using (var stream = new FileStream("MyLibrary.txt", FileMode.Open))
            {
                new_library = (MyLibrary<PrintEdition>)dataContract.ReadObject(stream);
            }
            Console.WriteLine("JSON-десериализация успешно завершена!\n");
            return new_library;
        }
        static void Main(string[] args)
        {
            do
            {
                try
                {
                    Console.WriteLine("НАЖМИТЕ:\nF - ЧТОБЫ ЗАВЕРШИТЬ ПРОГРММУ\nЛЮБУЮ КЛАВИШУ - ЧТОБЫ ПРОДОЖДИТЬ\n");
                    var key = Console.ReadKey(true).Key;
                    if (key == ConsoleKey.F)
                        break;
                    availableLetters = new List<char>();
                    MyLibrary<PrintEdition> myLibrary = new MyLibrary<PrintEdition>();
                    myLibrary.onTake += TakeHandler;
                    int countOfObjects;
                    EnterCountOfObjects(out countOfObjects);
                    GenerateAndAddObjects(ref myLibrary, countOfObjects);
                    PrintLine();
                    foreach (var item in myLibrary)
                        if (item is Book)
                            item.Print();
                    PrintLine();
                    Console.WriteLine(myLibrary);
                    PrintLine();
                    int letterNumbInList = rand.Next(0, availableLetters.Count);
                    if(availableLetters.Count != 0)
                        myLibrary.TakeBooks((char)(availableLetters[letterNumbInList]));
                    else
                        Console.WriteLine("В библиотеке нет книг, поэтому ни одно печатное издание извлечено не будет.");
                    foreach (var item in myLibrary)
                        Console.WriteLine(item);
                    PrintLine();
                    DataContractSerialize(myLibrary);
                    MyLibrary<PrintEdition> myLibraryFromFile = new MyLibrary<PrintEdition>();
                    myLibraryFromFile = DataContractDeserialize();
                    foreach (var item in myLibraryFromFile)
                        Console.WriteLine(item);
                    PrintLine();
                    Console.WriteLine($"Среднее количество страниц печатных изданий библиотеки: {myLibraryFromFile.AverageCountOfPages:f2}");
                    Console.WriteLine($"Среднее количество страниц журанлов библиотеки: {myLibraryFromFile.AverageCountOfMagazinePages:f2}");
                    PrintLine();
                    PrintLine();
                }
                catch (Exception ex) when (ex is DivideByZeroException | ex is IOException
                | ex is SerializationException | ex is ArgumentException)
                {
                    Console.WriteLine(ex.Message);
                }
                catch { }
            }
            while (true);
        }
    }
}