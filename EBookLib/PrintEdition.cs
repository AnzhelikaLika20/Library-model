using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace EBookLib
{
    /// <summary>
    /// Абстрактный класс, описывающий печатное издание. Поддерживает сериализацию.
    /// </summary>
    [DataContract, KnownType(typeof(Book)), KnownType(typeof(Magazine))]
    public abstract class PrintEdition : IPrinting
    {
        [DataMember]
        int pages;
        public event EventHandler? onPrint;
        [DataMember]
        ///Автосвойство устанавливает и возвращает название печатного издания.
        public string Name { get; private set; }
        /// <summary>
        /// Свойство устанавливает и возвращает количество страниц печатного издания.
        /// </summary>
        public int Pages
        {
            get
            {
                return pages;
            }
            private set
            {
                if (value < 0)
                    throw new ArgumentException("Число страниц печатного издания не может быть отрицательной! Объект будет создан повторно.");
                pages = value;
            }
        }
        public PrintEdition(string name, int pages)
        {
            Name = name;
            Pages = pages;
        }
        public override string ToString()
        {
            return $"name={Name}; pages={Pages}";
        }
        /// <summary>
        /// Метод реализует интерфейс IPrinting. Вызывает событие onPrint.
        /// </summary>
        public void Print()
        {
            onPrint?.Invoke(this, EventArgs.Empty);
        }
    }
}