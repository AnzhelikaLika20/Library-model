using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace EBookLib
{
    /// <summary>
    /// Класс, описывающий книгу. Поддерживает сериализацию.
    /// </summary>
    [DataContract]
    public class Book : PrintEdition
    {
        [DataMember]
        ///Автосвойсто, устанавливающее и возвращающее имя автора книги.
        public string Author { get; private set; }
        public Book(string name, int pages, string author) : base(name, pages)
        {
            Author = author;
        }
        public override string ToString()
        {
            return base.ToString() + $" author={Author}";
        }
    }
}
