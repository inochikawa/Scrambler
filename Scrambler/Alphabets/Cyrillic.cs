using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrambler.Alphabets
{
    public class Cyrillic: Alphabet
    {
        public Cyrillic()
        {
            base.UpperCase = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ ,.";
            base.LowerCase = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя ,.";

            Quantity = 36;
        }
    }
}
