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
            int differenceLength = 8 - data.Length;
            StringBuilder dif = new StringBuilder();

            if(differenceLength > 0)
                for (int i = 0; i < differenceLength; i++)
                    dif.Append("0");

            data += dif;

            List<Byte> byteList = new List<Byte>();

            for (int i = 0; i < data.Length; i += 8)
            {
                byteList.Add(Convert.ToByte(data.Substring(i, 8), 2));
            }
            return Encoding.UTF8.GetString(byteList.ToArray());
        }

        //public static string GetStringFromBits(int[] data)
        //{
        //    List<Byte> byteList = new List<Byte>();

        //    for (int i = 0; i < data.Length; i += 8)
        //    {
        //        byteList.Add(Convert.ToByte(data.Substring(i, 8), 2));
        //    }
        //    return Encoding.UTF8.GetString(byteList.ToArray());
        //}

        public static int[] ToBits(this string data)
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

        public static int[] ToBits(this int data)
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

        public static int[] ToBits(this uint data)
        {
            string resultString = Convert.ToString((int) data, 2);
            List<int> result = new List<int>();
            foreach (var bit in resultString)
            {
                result.Add(bit - 48);
            }
            return result.ToArray();
        }

        public static string ToStringArray(this int[] data)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                result.Append(data[i]);
            return result.ToString();
        }

        public static int[] GetBitsFromHex(this string hex)
        {
            List<int> result = new List<int>();
            string[] hexValuesSplit = hex.Split(' ');
            foreach (string hexValue in hexValuesSplit)
            {
                // Convert the number expressed in base-16 to an integer.
                int value = Convert.ToInt32(hexValue, 16);
                result.AddRange(ToBits(value));
            }
            return result.ToArray();
        }

        public static string GetHexFromBits(int[] bits)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 8; i < bits.Length; i+=8)
            {
                int counter = 8;
                StringBuilder bufferBits = new StringBuilder(); ;
                for (int j = 0; j < 8; j++)
                {
                    bufferBits.Append(bits[i - counter]);
                    counter--;
                }
                int bufferResult = int.Parse(GetStringFromBits(bufferBits.ToString()));

                result.Append(Convert.ToString(bufferResult, 16) + " ");
            }
            return result.ToString().Substring(0, result.Length - 1);
        }

        public static int[] ElongationTo32Bit(this int[] data)
        {
            int[] result = new int[32];
            int differenceLength = 32 - data.Length;
            if (differenceLength <= 0)
                return null;

            for (int i = differenceLength; i < 32; i++)
                result[i] = data[i - differenceLength];

            return result;
        }

        public static int[] ElongationTo64Bit(this int[] data)
        {
            int[] result = new int[64];
            int differenceLength = 64 - data.Length;
            if (differenceLength <= 0)
                return null;

            for (int i = differenceLength; i < 64; i++)
                result[i] = data[i - differenceLength];

            return result;
        }

        public static int[] BitWiseLeftShift(this int[] data, int shift)
        {
            List<int> result = new List<int>();
            for (int i = shift; i < data.Length; i++)
                result.Add(data[i]);

            for (int i = 0; i < shift; i++)
                result.Add(data[i]);

            return result.ToArray();
        }
    }
}
