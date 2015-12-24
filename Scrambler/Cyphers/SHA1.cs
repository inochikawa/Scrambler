using Scrambler.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrambler.Cyphers
{
    public class SHA1 : Cypher
    {
        private uint h0 = 0x67452301;
        private uint h1 = 0xEFCDAB89;
        private uint h2 = 0x98BADCFE;
        private uint h3 = 0x10325476;
        private uint h4 = 0xC3D2E1F0;

        public SHA1()
        {

        }

        public string HeshSum(string text)
        {
            List<int> textBits = new List<int>(text.ToBits());
            textBits.Add(1);
            int textLength = textBits.Count;
            while (textLength % 512 != 448)
            {
                textBits.Add(0);
                textLength++;
            }

            textBits.AddRange(textLength.ToBits().ElongationTo64Bit());

            if (textBits.Count % 512 != 0)
                return "Text length is not proportional 512";

            int[] textBuffer = new int[512];
            int textCounter = 0;
            for (int i = 0; i < textBits.Count + 1; i++)
            {
                if (i % 512 == 0 && i != 0)
                {
                    List<int[]> W = new List<int[]>();
                    int[] bufferW = new int[16];
                    int bufferCounterW = 0;

                    for (int j = 0; j < textBuffer.Length + 1; j++)
                    {
                        if (j % 16 == 0 && j != 0)
                        {
                            W.Add(bufferW);
                            Array.Clear(bufferW, 0, bufferW.Length);
                            bufferCounterW = 0;
                        }
                        if (j != textBuffer.Length)
                            bufferW[bufferCounterW] = textBuffer[j];
                        bufferCounterW++;
                    }

                    for (int ki = 16; ki < 80; ki++)
                    {
                        int[] tempW = (Convert.ToInt32(W[ki - 3].ToArray().ToStringArray(), 2) ^
                                        Convert.ToInt32(W[ki - 8].ToArray().ToStringArray(), 2) ^
                                        Convert.ToInt32(W[ki - 14].ToArray().ToStringArray(), 2) ^
                                        Convert.ToInt32(W[ki - 16].ToArray().ToStringArray(), 2)).ToBits();
                        tempW = tempW.BitWiseLeftShift(1);
                        W.Add(tempW);
                    }

                    uint a = h0;
                    uint b = h1;
                    uint c = h2;
                    uint d = h3;
                    uint e = h4;
                    uint k = 0;
                    uint f = 0;

                    for (int numerator = 0; numerator < 80; numerator++)
                    {
                        if (0 <= numerator && numerator <= 19)
                        {
                            f = (b & c) | ((~b) & d);
                            k = 0x5A827999;
                        }
                        else if (20 <= numerator && numerator <= 39)
                        {
                            f = b ^ c ^ d;
                            k = 0x6ED9EBA1;
                        }
                        else if (40 <= numerator && numerator <= 59)
                        {
                            f = (b & c) | (b & d) | (c & d);
                            k = 0x8F1BBCDC;
                        }
                        else if (60 <= numerator && numerator <= 79)
                        {
                            f = b ^ c ^ d;
                            k = 0xCA62C1D6;
                        }

                        uint temp = Convert.ToUInt32(a.ToBits().BitWiseLeftShift(5).ToStringArray(), 2) + f + e + k + Convert.ToUInt32(W[numerator].ToStringArray(), 2);
                        e = d;
                        d = c;
                        c = Convert.ToUInt32(b.ToBits().BitWiseLeftShift(2).ToStringArray(), 2);
                        b = a;
                        a = temp;
                    }

                    h0 = h0 + a;
                    h1 = h1 + b;
                    h2 = h2 + c;
                    h3 = h3 + d;
                    h4 = h4 + e;

                    textCounter = 0;
                }
                if(i != textBits.Count)
                    textBuffer[textCounter] = textBits[i];
                textCounter++;
            }

            return h0.ToString("X");// + h1.ToString("X") + h2.ToString("X") + h3.ToString("X") + h4.ToString("X") ;
        }

        public override string Decrypt(string text)
        {
            return HeshSum(text);
        }

        public override string Encrypt(string text)
        {
            return HeshSum(text);
        }
    }
}
