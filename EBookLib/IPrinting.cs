using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBookLib
{
    /// <summary>
    /// Интерфейс определяющий метод Print и событие onPrint;
    /// </summary>
    public interface IPrinting
    {
        public event EventHandler onPrint; 
        public void Print();
    }
}
