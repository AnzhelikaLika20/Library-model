using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace EBookLib
{
    /// <summary>
    /// Обобщенный класс, определяющий библиотеку печатных изданий. Поддерживает сериализацию. 
    /// </summary>
    /// <typeparam name="U">Обобщение Т, которе является объектом PrintEdition.</typeparam>
    [DataContract]
    public class MyLibrary<U> : IEnumerable<U>  where U: PrintEdition
    {
        public event EventHandler<char>? onTake;
        [DataMember]
        List<U> library;
        public List<U> Library { get => library; }
        public MyLibrary()
        {
            library = new();
        }
        /// <summary>
        /// Свойство возвращает среднее количество страниц в печатных изданиях библиотеки.
        /// </summary>
        public double AverageCountOfPages
        {
            get
            {
                int count = 0;
                if(library.Count == 0)
                    throw new DivideByZeroException("В библиотке нет печатных изданий, среднее значение страниц в печатном издании расчитать нельзя!");
                foreach (var item in Library)
                    count += item.Pages;
                return (double)count / Library.Count;
            }
        }
        /// <summary>
        /// Свойство возвращает среднее количество страниц в журналах библиотеки.
        /// </summary>
        public double AverageCountOfMagazinePages
        {
            get
            {
                int pages = 0, count = 0;
                foreach (var item in Library)
                    if(item is Magazine)
                    {
                        pages += item.Pages;
                        count++;
                    }
                if (count == 0)
                    throw new DivideByZeroException("В библиотке нет журналов, среднее значение страниц в жаурналах расчитать нельзя!");
                return (double)pages / count;
            }
        }
        /// <summary>
        /// Обобщенный метод, реализующийа  интерфейс IEnumerable<T>.
        /// </summary>
        /// <returns>Возвращает объект-Enumerator.</returns>
        public IEnumerator<U> GetEnumerator()
        {
            return new MyLibraryEnumerator<U>(library);
        }
        /// <summary>
        /// Необобщенный метод, реализующий интерфейс IEnumerable<T>.
        /// </summary>
        /// <returns>Возвращает объект-Enumerator вызовом своего обобщенного аналога.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        /// <summary>
        /// Метод для извлечения книг, начинающихся с буквы start из библиотеки.
        /// </summary>
        /// <param name="start">Книги, начинающиеся с данный буквы, будут удалены из библиотеки.</param>
        public void TakeBooks(char start)
        {
            for(int i = 0; i < Library.Count; i++)
                if (Library[i] is Book && Library[i].Name[0] == start)
                    library.Remove(Library[i]);
            onTake?.Invoke(this, start);
        }
        /// <summary>
        /// Метод, для добвления печатных изданий в список объектов библиотеки MyLibrary.
        /// </summary>
        /// <param name="printed">Печатное издание, которое будет добавлено в список объектов библиотеки MyLibrary.</param>
        public void Add(U printed)
        {
            library.Add(printed);
        }
        public override string ToString()
        {
            int pageCount = 0;
            for (int i = 0; i < library.Count; i++)
                pageCount += library[i].Pages;
            StringBuilder s = new StringBuilder();
            s.Append("Общее число страниц печатных изданий библиотеки: " + pageCount + "\n");
            foreach (var item in library)
                s.Append("\n" + item);
            return s.ToString();
        }
    }
}
