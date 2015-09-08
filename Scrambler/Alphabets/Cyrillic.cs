using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrambler.Alphabets
{
    public class Cyrillic: Alphabet
    {
        public Cyrillic(string a, string b):base(a,b)
        {
            Letters = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ ,.";
            Quantity = Letters.Length;
        }
    }
}
