using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scrambler.Mathematics;

namespace Scrambler.Cyphers
{
    public class MD4:Cypher
    {
        private List<int> data;
        private int dataLenth;
        private int[] constTable = new int[64];
        private List<int> X = new List<int>();
        //init buffer
        private uint hexA = 0x67452301;
        private uint hexB = 0xEFCDAB89;
        private uint hexC = 0x98BADCFE;
        private uint hexD = 0x10325476;

        public MD4()
        {
        }
        
        private string HeshSum(string data)
        {
            this.data = new List<int>(Mathematics.Converter.ToBits(data));
            dataLenth = this.data.Count;

            alignmentFlow();
            addingLenthMessage();
            initConstTable();


            int a = (int)hexA;
            int b = (int)hexB;
            int c = (int)hexC;
            int d = (int)hexD;

            int aa = a;
            int bb = b;
            int cc = c;
            int dd = d;


            for (int i = 511; i < data.Length; i += 512)
            {
                int dataCounter = 511;
                int[] tempData = new int[512];
                for (int j = 0; j < 512; j++)
                {
                    tempData[j] = data[i - dataCounter];
                    dataCounter--;
                }
                dataCounter = 512;

                int counter = 31;
                for (int l = 31; l < tempData.Length; l += 32)
                {
                    int[] tempBits = new int[32];
                    for (int j = 0; j < 32; j++)
                    {
                        tempBits[j] = X[l - counter];
                        counter--;
                    }
                    X.Add(Convert.ToInt32(tempBits.ToString(), 2));
                    counter = 31;
                }

                //first stage
                a = roundFunctionF(a, b, c, d, 0, 7, 1);
                a = roundFunctionF(a, b, c, d, 4, 7, 5);
                a = roundFunctionF(a, b, c, d, 8, 7, 9);
                a = roundFunctionF(a, b, c, d, 12, 7, 13);

                d = roundFunctionF(d, a, b, c, 1, 12, 2);
                d = roundFunctionF(d, a, b, c, 5, 12, 6);
                d = roundFunctionF(d, a, b, c, 9, 12, 10);
                d = roundFunctionF(d, a, b, c, 13, 12, 14);

                c = roundFunctionF(c, d, a, b, 2, 17, 3);
                c = roundFunctionF(c, d, a, b, 6, 17, 7);
                c = roundFunctionF(c, d, a, b, 10, 17, 11);
                c = roundFunctionF(c, d, a, b, 14, 17, 15);

                b = roundFunctionF(b, c, d, a, 3, 22, 4);
                b = roundFunctionF(b, c, d, a, 7, 22, 8);
                b = roundFunctionF(b, c, d, a, 11, 22, 12);
                b = roundFunctionF(b, c, d, a, 15, 22, 16);

                //second stage
                a = roundFunctionF(a, b, c, d, 1, 5, 17);
                a = roundFunctionF(a, b, c, d, 5, 5, 21);
                a = roundFunctionF(a, b, c, d, 9, 5, 25);
                a = roundFunctionF(a, b, c, d, 13, 5, 29);

                d = roundFunctionF(d, a, b, c, 6, 9, 18);
                d = roundFunctionF(d, a, b, c, 10, 9, 22);
                d = roundFunctionF(d, a, b, c, 14, 9, 26);
                d = roundFunctionF(d, a, b, c, 2, 9, 30);

                c = roundFunctionF(c, d, a, b, 11, 14, 19);
                c = roundFunctionF(c, d, a, b, 15, 14, 23);
                c = roundFunctionF(c, d, a, b, 3, 14, 27);
                c = roundFunctionF(c, d, a, b, 7, 14, 31);

                b = roundFunctionF(b, c, d, a, 0, 20, 20);
                b = roundFunctionF(b, c, d, a, 4, 20, 24);
                b = roundFunctionF(b, c, d, a, 8, 20, 28);
                b = roundFunctionF(b, c, d, a, 12, 20, 32);

                //third stage
                a = roundFunctionF(a, b, c, d, 5, 4, 33);
                a = roundFunctionF(a, b, c, d, 1, 4, 37);
                a = roundFunctionF(a, b, c, d, 13, 4, 41);
                a = roundFunctionF(a, b, c, d, 9, 4, 45);

                d = roundFunctionF(d, a, b, c, 8, 11, 34);
                d = roundFunctionF(d, a, b, c, 4, 11, 38);
                d = roundFunctionF(d, a, b, c, 0, 11, 42);
                d = roundFunctionF(d, a, b, c, 12, 11, 46);

                c = roundFunctionF(c, d, a, b, 11, 16, 35);
                c = roundFunctionF(c, d, a, b, 7, 16, 39);
                c = roundFunctionF(c, d, a, b, 3, 16, 43);
                c = roundFunctionF(c, d, a, b, 15, 16, 47);

                b = roundFunctionF(b, c, d, a, 14, 23, 36);
                b = roundFunctionF(b, c, d, a, 10, 23, 40);
                b = roundFunctionF(b, c, d, a, 6, 23, 44);
                b = roundFunctionF(b, c, d, a, 2, 23, 48);

                //fourth stage
                a = roundFunctionF(a, b, c, d, 0, 6, 49);
                a = roundFunctionF(a, b, c, d, 12, 6, 53);
                a = roundFunctionF(a, b, c, d, 8, 6, 57);
                a = roundFunctionF(a, b, c, d, 4, 6, 61);

                d = roundFunctionF(d, a, b, c, 7, 10, 50);
                d = roundFunctionF(d, a, b, c, 3, 10, 54);
                d = roundFunctionF(d, a, b, c, 15, 10, 58);
                d = roundFunctionF(d, a, b, c, 11, 10, 62);

                c = roundFunctionF(c, d, a, b, 14, 15, 51);
                c = roundFunctionF(c, d, a, b, 10, 15, 55);
                c = roundFunctionF(c, d, a, b, 6, 15, 59);
                c = roundFunctionF(c, d, a, b, 2, 15, 63);

                b = roundFunctionF(b, c, d, a, 5, 21, 52);
                b = roundFunctionF(b, c, d, a, 1, 21, 56);
                b = roundFunctionF(b, c, d, a, 13, 21, 60);
                b = roundFunctionF(b, c, d, a, 9, 21, 64);

                a += aa;
                b += bb;
                c += cc;
                d += dd;
            }

            return a.ToString("X") + b.ToString("X") + c.ToString("X") + d.ToString("X");      
        }

        private void alignmentFlow()
        {
            data.Add(1); // add single bit to the end of flow
            int counter = data.Count;
            //add the required number of zeros
            while (counter % 512 != 448)
            {
                data.Add(0);
                counter++;
            }
        }

        private void addingLenthMessage()
        {
            List<int> lastBits = new List<int>(Converter.ToBits(dataLenth));

            int counter = lastBits.Count - 1;
            for (int i = 0; i < 64; i++)
            {
                if (counter < 0) data.Add(0);
                else
                {
                    data.Add(lastBits[counter]);
                    counter--;
                }
            }
        }

        private int bitFunF(int x, int y, int z)
        {
            // (X and Y) or (not X and Z)
            return LogicOperations.OR(LogicOperations.AND(x, y), LogicOperations.AND(LogicOperations.NOT(x), z));
        }

        private int bitFunG(int x, int y, int z)
        {
            // (X and Y) or (not Z and Y)
            return LogicOperations.OR(LogicOperations.AND(x, y), LogicOperations.AND(LogicOperations.NOT(z), y));
        }

        private int bitFunH(int x, int y, int z)
        {
            // X xor Y xor Z
            return LogicOperations.XOR(LogicOperations.XOR(x, y), z);
        }

        private int bitFunI(int x, int y, int z)
        {
            // Y xor (not Z or X)
            return LogicOperations.XOR(y, LogicOperations.OR(LogicOperations.NOT(z), x));
        }

        private int FunF(int wordX, int wordY, int wordZ)
        {
            int[] x = wordX.ToBits();
            int[] y = wordY.ToBits();
            int[] z = wordZ.ToBits();

            List<int> result = new List<int>();

            for (int i = 0; i < x.Length; i++)
            {
                result.Add(bitFunF(x[i], y[i], z[i]));
            }

            return Convert.ToInt32(result.ToString(), 2);
        }

        private int FunG(int wordX, int wordY, int wordZ)
        {
            int[] x = wordX.ToBits();
            int[] y = wordY.ToBits();
            int[] z = wordZ.ToBits();

            StringBuilder result = new StringBuilder();

            for (int i = 0; i < x.Length; i++)
            {
                result.Append(bitFunG(x[i], y[i], z[i]));
            }

            return Convert.ToInt32(result.ToString(), 2);
        }

        private int FunH(int wordX, int wordY, int wordZ)
        {
            int[] x = wordX.ToBits();
            int[] y = wordY.ToBits();
            int[] z = wordZ.ToBits();

            List<int> result = new List<int>();

            for (int i = 0; i < x.Length; i++)
            {
                result.Add(bitFunH(x[i], y[i], z[i]));
            }

            return Convert.ToInt32(result.ToString(), 2);
        }

        private int FunI(int wordX, int wordY, int wordZ)
        {
            int[] x = wordX.ToBits();
            int[] y = wordY.ToBits();
            int[] z = wordZ.ToBits();

            List<int> result = new List<int>();

            for (int i = 0; i < x.Length; i++)
            {
                result.Add(bitFunI(x[i], y[i], z[i]));
            }

            return Convert.ToInt32(result.ToString(), 2);
        }

        private void initConstTable()
        {
            for (int i = 0; i < constTable.Length; i++)
            {
                constTable[i] = (int) (Math.Pow(2, 32) * Math.Abs(Math.Sin(i)));
            }
        }

        private int roundFunctionF(int a, int b, int c, int d, int k, int s, int i)
        {
            int firstAction = a + FunF(b, c, d) + X[k] + constTable[i];
            firstAction = Convert.ToInt32(bitWiseLeftShift(firstAction.ToBits(), s).ToString(), 2);
            return b + firstAction;
        }

        private int roundFunctionG(int a, int b, int c, int d, int k, int s, int i)
        {
            int firstAction = a + FunG(b, c, d) + X[k] + constTable[i];
            firstAction = Convert.ToInt32(bitWiseLeftShift(firstAction.ToBits(), s).ToString(), 2);
            return b + firstAction;
        }

        private int roundFunctionH(int a, int b, int c, int d, int k, int s, int i)
        {
            int firstAction = a + FunH(b, c, d) + X[k] + constTable[i];
            firstAction = Convert.ToInt32(bitWiseLeftShift(firstAction.ToBits(), s).ToString(), 2);
            return b + firstAction;
        }

        private int roundFunctionI(int a, int b, int c, int d, int k, int s, int i)
        {
            int firstAction = a + FunI(b, c, d) + X[k] + constTable[i];
            firstAction = Convert.ToInt32(bitWiseLeftShift(firstAction.ToBits(), s).ToString(), 2);
            return b + firstAction;
        }

        private int[] bitWiseLeftShift(int[] data, int shift)
        {
            List<int> result = new List<int>();
            for (int i = shift; i < data.Length; i++)
                result.Add(data[i]);

            for (int i = 0; i < shift; i++)
                result.Add(data[i]);

            return result.ToArray();
        }

        public override string Encrypt(string text)
        {
            return HeshSum(text);
        }

        public override string Decrypt(string text)
        {
            return HeshSum(text);
        }
    }
}
