using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;


namespace EBookLib
{
    /// <summary>
    /// Класс, описывающий журнал. Поддерживает сериализацию.
    /// </summary>
    [DataContract]
    public class Magazine : PrintEdition
    {
        [DataMember]
        int period;
        /// <summary>
        /// Свойство устанавливает и возвращает периодичность публикации журанала.
        /// </summary>
        public int Period
        {
            get
            {
                return period;
            }
            private set
            {
                if (value < 0)
                    throw new ArgumentException("Периодичность выпуска журнала не может быть отрицательной! Объект будет создан повторно.\n");
                period = value;
            }
        }
        public Magazine(string name, int pages, int period) : base(name, pages)
        {
            Period = period;
        }
        public override string ToString()
        {
            return base.ToString() + $" period={Period}";
        }
    }
}
