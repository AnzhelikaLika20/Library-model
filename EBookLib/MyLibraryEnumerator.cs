using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBookLib
{
    /// <summary>
    /// Класс, который реализует итератор в MyLibrary.
    /// </summary>
    /// <typeparam name="T">Обобщение класса LibraryEnumerator.</typeparam>
    public class MyLibraryEnumerator<U> : IEnumerator<U>
    {
        int position = -1;
        List<U> library;
        public MyLibraryEnumerator(List<U> library)
        {
            this.library = library;
        }
        /// <summary>
        /// Метод реализует интерфейс IEnumerator<T>. Возвращает объект с текущим индексов из списка печатных изданий.
        /// </summary>
        public U Current => library[position];

        object IEnumerator.Current => Current;
        /// <summary>
        /// Метод реализует интерфейс IEnumerator<T>. Метод осуществляет проверку на наличие следующего элемента в списке печтных изданий библиотеки, увеличивая текущий индекс.
        /// </summary>
        /// <returns>Возвращает True, если есть следущий объект и False - если его нет</returns>
        public bool MoveNext()
        {
            if (++position < library.Count)
                return true;
            return false;
        }
        /// <summary>
        /// Метод реализует интерфейс IEnumerator<T>. Метод сбрасывает значение текущего индекса до первоначального значения.
        /// </summary>
        public void Reset()
        {
            position = -1;
        }
        public void Dispose() { }
    }
}
