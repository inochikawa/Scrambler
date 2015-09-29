using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrambler.Cyphers
{
    public class DES : Cypher
    {
        #region const fields

        #region permutation tables

        private readonly int[] ipTable = { 58, 50, 42, 34, 26, 18, 10, 2,
                                           60, 52, 44, 36, 28, 20, 12, 4,
                                           62, 54, 46, 38, 30, 22, 14, 6,
                                           64, 56, 48, 40, 32, 24, 16, 8,
                                           57, 49, 41, 33, 25, 17, 9,  1,
                                           59, 51, 43, 35, 27, 19, 11, 3,
                                           61, 53, 45, 37, 29, 21, 13, 5,
                                           63, 55, 47, 39, 31, 23, 15, 7 };

        private readonly int[] inverseIpTable = { 40, 8, 48, 16, 56, 24, 64, 32,
                                                  39, 7, 47, 15, 55, 23, 63, 31,
                                                  38, 6, 46, 14, 54, 22, 62, 30,
                                                  37, 5, 45, 13, 53, 21, 61, 29,
                                                  36, 4, 44, 12, 52, 20, 60, 28,
                                                  35, 3, 43, 11, 51, 19, 59, 27,
                                                  34, 2, 42, 10, 50, 18, 58, 26,
                                                  33, 1, 41, 9,  49, 17, 57, 25 };

        private readonly int[] ipKeyTable = {57, 49, 41, 33,  25,  17, 9,
                                             1,  58, 50, 42,  34,  26, 18,
                                             10, 2,  59, 51,  43,  35, 27,
                                             19, 11, 3,  60,  52,  44, 36,
                                             63, 55, 47, 39,  31,  23, 15,
                                             7,  62, 54, 46,  38,  30, 22,
                                             14, 6,  61, 53,  45,  37, 29,
                                             21, 13, 5,  28,  20,  12, 4  };

        private readonly int[] sublKeyPermutation = { 14, 17, 11, 24,  1,  5,
                                                      3, 28, 15,  6, 21, 10,
                                                      23, 19, 12,  4, 26,  8,
                                                      16,  7, 27, 20, 13,  2,
                                                      41, 52, 31, 37, 47, 55,
                                                      30, 40, 51, 45, 33, 48,
                                                      44, 49, 39, 56, 34, 53,
                                                      46, 42, 50, 36, 29, 32 };

        private readonly int[] pBoxExpansion = { 32, 1,  2,  3,  4,  5,
                                                 4,  5,  6,  7,  8,  9,
                                                 8,  9,  10, 11, 12, 13,
                                                 12, 13, 14, 15, 16, 17,
                                                 16, 17, 18, 19, 20, 21,
                                                 20, 21, 22, 23, 24, 25,
                                                 24, 25, 26, 27, 28, 29,
                                                 28, 29, 30, 31, 32,  1};

        private readonly int[] cryptoFunctoinPermutation = {16,  7, 20, 21,
                                                            29, 12, 28, 17,
                                                             1, 15, 23, 26,
                                                             5, 18, 31, 10,
                                                             2,  8, 24, 14,
                                                            32, 27,  3,  9,
                                                            19, 13, 30,  6,
                                                            22, 11,  4, 25};

        #endregion

        private readonly int[,] keyShifts = { { 1,  1,  2,  2,  2,  2,  2,  2,  1,  2,  2,  2,  2,  2,  2,  1 },
                                              {-1, -1, -2, -2, -2, -2, -2, -2, -1, -2, -2, -2, -2, -2, -2, -1 },};

        private const int roundIteration = 1;

        #region S-blocks

        private readonly int[,] s1 = { { 14, 4,  13, 1, 2,  15, 11, 8,  3,  10, 6,  12, 5,  9,  0, 7, },
                                        { 0, 15, 7,  4, 14, 2,  13, 1,  10, 6,  12, 11, 9,  5,  3, 8, },
                                        { 4, 1,  14, 8, 13, 6,  2,  11, 15, 12, 9,  7,  3,  10, 5, 0  },
                                        { 15,12, 8,  2, 4,  9,  1,  7,  5,  11, 3,  14, 10, 0,  6, 13 } };

        private readonly int[,] s2 = { { 15, 1,  8,  14, 6,  11, 3,  4,  9,  7, 2,  13, 12, 0, 5,  10 },
                                       { 3,  13, 4,  7,  15, 2,  8,  14, 12, 0, 1,  10, 6,  9, 11, 5  },
                                       { 0,  14, 7,  11, 10, 4,  13, 1,  5,  8, 12, 6,  9,  3, 2,  15 },
                                       { 13, 8,  10, 1,  3,  15, 4,  2,  11, 6, 7,  12, 0,  5, 14, 9  } };

        private readonly int[,] s3 = { { 10, 0, 9, 14, 6, 3, 15, 5, 1, 13, 12, 7, 11, 4, 2, 8 },
                                       { 13, 7, 0, 9, 3, 4, 6, 10, 2, 8, 5, 14, 12, 11, 15, 1 },
                                       { 13, 6, 4, 9, 8, 15, 3, 0, 11, 1, 2, 12, 5, 10, 14, 7 },
                                       { 1, 10, 13, 0, 6, 9, 8, 7, 4, 15, 14, 3, 11, 5, 2, 12 }};

        private readonly int[,] s4 = { { 7, 13, 14,  3,  0,  6,  9, 10,  1,  2,  8,  5, 11, 12,  4, 15  },
                                       { 13,  8, 11,  5,  6, 15,  0,  3,  4,  7,  2, 12,  1, 10, 14,  9 },
                                       { 10,  6,  9,  0, 12, 11,  7, 13, 15,  1,  3, 14,  5,  2,  8,  4 },
                                       { 3, 15,  0,  6, 10,  1, 13,  8,  9,  4,  5, 11, 12,  7,  2, 14  } };

        private readonly int[,] s5 = { { 2, 12,  4,  1,  7, 10, 11,  6,  8,  5,  3, 15, 13,  0, 14,  9 },
                                       { 14, 11,  2, 12,  4,  7, 13,  1,  5,  0, 15, 10,  3,  9,  8,  6 },
                                       { 4,  2,  1, 11, 10, 13,  7,  8, 15,  9, 12,  5,  6,  3,  0, 14 },
                                       { 11,  8, 12,  7,  1, 14,  2, 13,  6, 15,  0,  9, 10,  4,  5,  3}};

        private readonly int[,] s6 = { { 12,  1, 10, 15,  9,  2,  6,  8,  0, 13,  3,  4, 14,  7,  5, 11 },
                                      { 10, 15,  4,  2,  7, 12,  9,  5,  6,  1, 13, 14,  0, 11,  3,  8 },
                                      { 9, 14, 15,  5,  2,  8, 12,  3,  7,  0,  4, 10,  1, 13, 11,  6 },
                                      { 4,  3,  2, 12,  9,  5, 15, 10, 11, 14,  1,  7,  6,  0,  8, 13} };

        private readonly int[,] s7 = { { 4, 11,  2, 14, 15,  0,  8, 13,  3, 12,  9,  7,  5, 10,  6,  1  },
                                       { 13,  0, 11,  7,  4,  9,  1, 10, 14,  3,  5, 12,  2, 15,  8,  6 },
                                       { 1,  4, 11, 13, 12,  3,  7, 14, 10, 15,  6,  8,  0,  5,  9,  2  },
                                       { 6, 11, 13,  8,  1,  4, 10,  7,  9,  5,  0, 15, 14,  2,  3, 12} };

        private readonly int[,] s8 = { { 13,  2,  8,  4,  6, 15, 11,  1, 10,  9,  3, 14,  5,  0, 12,  7 },
                                       { 1, 15, 13,  8, 10,  3,  7,  4, 12,  5,  6, 11,  0, 14,  9,  2  },
                                       { 7, 11,  4,  1,  9, 12, 14,  2,  0,  6, 10, 13, 15,  3,  5,  8  },
                                       { 2,  1, 14,  7,  4, 10,  8, 13, 15, 12,  9,  0,  3,  5,  6, 11} };
        #endregion

        #endregion
        
        private string inputKey;

        public DES(string key)
        {
            this.inputKey = key;
        }
              
        public override string Decrypt(string text)
        {
            StringBuilder result = new StringBuilder();
            List<int> bits = new List<int>();
            foreach (var bit in text)
            {
                bits.Add(bit-48);
            }
            int[,] separatedBits = separateBits(bits.ToArray());
            for (int i = 0; i < separatedBits.GetLength(0); i++)
            {
                int[] tempBits = new int[separatedBits.GetLength(1)];
                for (int j = 0; j < tempBits.Length; j++)
                {
                    tempBits[j] = separatedBits[i, j];
                }
                tempBits = initialPermutation(tempBits);
                tempBits = inverceDESRoundes(tempBits);
                tempBits = finalPermutation(tempBits);
                for (int j = 0; j < tempBits.Length; j++)
                {
                    result.Append(tempBits[j]);
                }
            }

            return Mathematics.Converter.GetStringFromBits(result.ToString());
        }

        public override string Encrypt(string text)
        {
            StringBuilder result = new StringBuilder();
            string expansionText = additionalWhitespaces(text);
            int[,] separatedBits = separateBits(getBits(expansionText));
            for (int i = 0; i < separatedBits.GetLength(0); i++)
            {
                int[] tempBits = new int[separatedBits.GetLength(1)];
                for (int j = 0; j < tempBits.Length; j++)
                {
                    tempBits[j] = separatedBits[i, j];
                }
                tempBits = initialPermutation(tempBits);
                tempBits = DESRoundes(tempBits);
                tempBits = finalPermutation(tempBits);
                for (int j = 0; j < tempBits.Length; j++)
                {
                    result.Append(tempBits[j]);
                }
            }

            return result.ToString();
        }

        private string additionalWhitespaces(string text)
        {
            StringBuilder result = new StringBuilder(text);
            while(getBits(result.ToString()).Length % 64 != 0)
            {
                result.Append(" ");
            }
            return result.ToString();
        }

        public static string binaryToString(string data)
        {
            List<Byte> byteList = new List<Byte>(Encoding.ASCII.GetBytes(data));
            return Encoding.ASCII.GetString(byteList.ToArray());
        }

        private int[] getBits(string input)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in input.ToCharArray())
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

        private int[,] separateBits(int[] bits)
        {
            int rowCount = 0;
            if (bits.Length % 64 == 0)
                rowCount = bits.Length / 64;
            else
                rowCount = bits.Length / 64 + 1;
            int[,] separetedBits = new int[rowCount, 64];
            int bitsCount = 0;
            for (int i = 0; i < separetedBits.GetLength(0); i++)
            {
                for (int j = 0; j < separetedBits.GetLength(1); j++)
                {
                    if (bitsCount == bits.Length)
                        break;
                    separetedBits[i, j] = bits[bitsCount];
                    bitsCount++;
                }
            }

            return separetedBits;
        }

        private int[] initialPermutation(int[] bits)
        {
            int[] resultBits = new int[bits.Length];
            for (int i = 0; i < bits.Length; i++)
            {
                resultBits[i] = bits[ipTable[i]-1];
            }
            return resultBits;
        }

        private int[] finalPermutation(int[] bits)
        {
            int[] resultBits = new int[bits.Length];
            for (int i = 0; i < bits.Length; i++)
            {
                resultBits[i] = bits[inverseIpTable[i]-1];
            }
            return resultBits;
        }

        private List<int[]> keyVariants()
        {
            List<int[]> keys = new List<int[]>();
            int[] tempKey = key(inputKey);
            int[] resultKey = new int[48];

            for (int cycl = 0; cycl < roundIteration; cycl++)
            {
                tempKey = keyShift(tempKey, cycl, 0);
                resultKey = keyPermutation(tempKey);
                keys.Add(resultKey);
            }
            return keys;
        }

        private int[] DESRoundes(int[] bits)
        {
            int[] tempL = new int[32];
            int[] tempR = new int[32];

            int[] resultL = new int[32];
            int[] resultR = new int[32];
            int[] resultKey = new int[48];

            for (int i = 0; i < bits.Length; i++)
            {
                if (i < 32)
                    tempL[i] = bits[i];
                else
                    tempR[i-32] = bits[i];
            }

            for (int cycl = 0; cycl < roundIteration; cycl++)
            {
                resultKey = keyVariants()[cycl];

                resultL = tempR;
                
                for (int i = 0; i < 32; i++)
                {
                    resultR[i] = tempL[i] + cryptoFunction(tempR, resultKey)[i];
                    if (resultR[i] > 1)
                        resultR[i] = 0;
                }

                tempL = resultL;
                tempR = resultR;
            }

            int[] result = new int[64];
            for (int i = 0; i < 64; i++)
            {
                if (i < 32)
                    result[i] = resultL[i];
                else
                    result[i] = resultR[i - 32];
            }

            return result;
        }

        private int[] inverceDESRoundes(int[] bits)
        {
            int[] tempL = new int[32];
            int[] tempR = new int[32];

            int[] resultL = new int[32];
            int[] resultR = new int[32];
            int[] resultKey = new int[48];

            for (int i = 0; i < bits.Length; i++)
            {
                if (i < 32)
                    tempL[i] = bits[i];
                else
                    tempR[i - 32] = bits[i];
            }

            for (int cycl = roundIteration-1; cycl > -1; cycl--)
            {
                resultKey = keyVariants()[cycl];

                resultR = tempL;

                for (int i = 0; i < 32; i++)
                {
                    resultL[i] = tempR[i] + cryptoFunction(tempL, resultKey)[i];
                    if (resultL[i] > 1)
                        resultL[i] = 0;
                }

                tempL = resultL;
                tempR = resultR;
            }

            int[] result = new int[64];
            for (int i = 0; i < 64; i++)
            {
                if (i < 32)
                    result[i] = resultL[i];
                else
                    result[i] = resultR[i - 32];
            }

            return result;
        }

        private int[] cryptoFunction(int[] R, int[] key)
        {
            int[] expansionR = new int[48];
            for (int i = 0; i < 48; i++)
            {
                expansionR[i] = R[pBoxExpansion[i]-1];
            }

            int[] modulu = new int[48];
            for (int i = 0; i < 48; i++)
            {
                modulu[i] = expansionR[i] + key[i];
                if (modulu[i] > 1)
                    modulu[i] = 0;
            }

            int[,] separatedBits = new int[8, 6];

            int moduluCounter = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    separatedBits[i, j] = modulu[moduluCounter];
                    moduluCounter++;
                }
            }

            List<int[,]> S = new List<int[,]>() { s1, s2, s3, s4, s5, s6, s7, s8 };
            List<int> B = new List<int>();
            for (int i = 0; i < 8; i++)
            {
                int[] b = new int[6];
                for (int j = 0; j < 6; j++)
                {
                    b[j] = separatedBits[i, j];
                }
                
                int row = Convert.ToInt32(b[0].ToString() + b[5].ToString(), 2);
                int column = Convert.ToInt32(b[1].ToString() + b[2].ToString() + b[3].ToString() + b[4].ToString(), 2);

                string bjString = Convert.ToString(S[i][row, column], 2);
                int[] bj = new int[4];
                for (int k = 3; k > -1; k--)
                {
                    if (k - (bj.Length - bjString.Length) < 0)
                        break;
                    bj[k] = bjString[k-(bj.Length - bjString.Length)]-48;
                }
                for (int k = 0; k < 4; k++)
                {
                    B.Add(bj[k]);
                }
            }

            int[] result = new int[32];
            for (int i = 0; i < 32; i++)
            {
                result[i] = B[cryptoFunctoinPermutation[i]-1];
            }
            return result;
        }
        
        private int[] key(string keyString)
        {
            int[] keyBits = getBits(keyString);
            if (keyBits.Length != 64)
            {
                return new int[56];
            }
            else
            {
                int[] ipKey = new int[56];
                for (int i = 0; i < 56; i++)
                {
                    ipKey[i] = keyBits[ipKeyTable[i] - 1];
                }
                
                return ipKey;
            }
        }

        private int[] keyShift(int[] key, int keyNumber, int encrypt)
        {
            int[] C = new int[28];
            int[] D = new int[28];

            for (int i = 0; i < 56; i++)
            {
                if (i < 28)
                    C[i] = key[i];
                else
                    D[i-28] = key[i];
            }
            //shift C-box
            for (int i = 0; i < 28; i++)
            {
                if (i + keyShifts[encrypt, keyNumber] > C.Length - 1)
                {
                    C[i] = C[i + keyShifts[encrypt, keyNumber] - C.Length];
                }
                else if (i + keyShifts[encrypt, keyNumber] < 0)
                {
                    C[i] = C[i + keyShifts[encrypt, keyNumber] + C.Length];
                }
                else
                    C[i] = C[i + keyShifts[encrypt, keyNumber]];
            }
            //shift D-box
            for (int i = 0; i < 28; i++)
            {
                if (i + keyShifts[encrypt, keyNumber] > D.Length - 1)
                {
                    D[i] = D[i + keyShifts[encrypt, keyNumber] - D.Length];
                }
                else if (i + keyShifts[encrypt, keyNumber] < 0)
                {
                    D[i] = D[i + keyShifts[encrypt, keyNumber] + D.Length];
                }
                else
                    D[i] = D[i + keyShifts[encrypt, keyNumber]];
            }
            int[] result = new int[56];
            for (int i = 0; i < 56; i++)
            {
                if (i < 28)
                    result[i] = C[i];
                else
                    result[i] = D[i-28];
            }
            return result;
        }

        private int[] keyPermutation(int[] key)
        {
            int[] keyResult = new int[48];
            for (int i = 0; i < 48; i++)
            {
                keyResult[i] = key[sublKeyPermutation[i] - 1];
            }

            return keyResult;
        }
    }
    
}
