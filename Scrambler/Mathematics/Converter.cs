using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrambler.Mathematics
{
    public static class Converter
    {
        public static string GetStringFromBits(string data)
        {
            List<Byte> byteList = new List<Byte>();

            for (int i = 0; i < data.Length; i += 8)
            {
                byteList.Add(Convert.ToByte(data.Substring(i, 8), 2));
            }
            return Encoding.UTF8.GetString(byteList.ToArray());
        }

        public static int[] GetBitsFromString(string data)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in data.ToCharArray())
            {
                sb.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
            }

            List<int> result = new List<int>();
            foreach (var letter in sb.ToString())
            {
                result.Add(letter - 48);
            }
            return result.ToArray();
        }

        public static int[] GetBitsFromInt(int data)
        {
            string resultString = Convert.ToString(data, 2);
            List<int> result = new List<int>();
            foreach (var bit in resultString)
            {
                result.Add(bit - 48);
            }
            return result.ToArray();

            //return x < 2 ? x % 2 : (x % 2) + 10 * Perevod(x / 2);
            //while (true)
            //{
            //    if (data < 2)
            //    {
            //        result.Add(data % 2);
            //        return result.ToArray();
            //    }
            //    else
            //    {
            //        result.Add((data % 2) + 10 * data / 2);
            //        data /= 2;
            //    }
            //}
        }
    }
}
