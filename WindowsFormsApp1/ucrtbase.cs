using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SniperManagerApp
{
    internal class ucrtbase
    {
        private static byte BitArrayToInt(BitArray bitArray)
        {
            byte res = 0;
            for(int i=0;i< bitArray.Length; i++)
            //for(int i= bitArray.Length-1; i>= 0; i--)
            {
                int j = 0;
                if (bitArray[i]) j = 1;
                res +=(byte) (j * (int)Math.Pow(2, i));
            }
            return res;
        }
        private static BitArray StringToByteArray(string str)
        {
            BitArray temp= new BitArray(str.Length);
            int i,j ;
            for (i = str.Length - 1,j=0 ; i >= 0; i--,j++)
            //for (int i=0;i< str.Length; i++)
            {
                temp[j] = str[i].Equals('1');
            }
            return temp;
        }

        public static double Cvtdq2pd(long src, int offset)
        {
            var numberbinary = Convert.ToString(src, 2).PadLeft(64, '0');

            char sign = numberbinary[0];
            string storedexponentSTR = "00000"+string.Concat(numberbinary.Skip(1).Take(11));
            string storedmantissaSTR = string.Concat(numberbinary.Skip(12).Take(52));

            //работа с экспонентой
            byte[] exponentArray= new byte[3];

            int i,j;
            for (i = 0, j = 0; i < exponentArray.Length; i++,j=j+4)
            {
                string str = string.Concat(storedexponentSTR.Skip(j).Take(4));
                exponentArray[i] = BitArrayToInt(StringToByteArray("0000"+str));
            }
            exponentArray=new byte[8] { exponentArray[2], exponentArray[1], exponentArray[0],0,0,0,0,0 };
            int exponent=BitConverter.ToInt32(exponentArray, 0);

            if (sign.Equals('0')) { exponent -= offset; }
            else { exponent += offset; }

            BitArray q = new BitArray(BitConverter.GetBytes(exponent));
            //string s = "00000000000";
            string s = "";
            for(i=0/*,j= s.Length-1*/; i < 11; i++/*,j--*/)
            {
                if (q[i]) { s = "1"+s; }
                else { s = "0"+s; }
            }
            s = string.Concat(sign, s, storedmantissaSTR);
            double result = Convert.ToDouble(s);


            return result;
            
        }
        //00000100 00000000
        public static void pow(ref double[] xmm)
        {
            long RAX, RBX, RCX, RDX, RDI, RSI, RBP, RSP, R8, R9, R10, R11;
            long RSP50;

            RDX = Convert.ToInt64(xmm[0]);
            R8 = Convert.ToInt64(xmm[1]);
            R10 = Pointers.TEKKEN7_UCRTBASE_WCTYPE["4A98"];
            if((R10 & R8) == 0){ goto l11E0; }
            if(R8 == Pointers.TEKKEN7_UCRTBASE_WCTYPE["4A88"]) { goto l1230; }
            R9 = Pointers.TEKKEN7_UCRTBASE_WCTYPE["4A60"];
            R9 &= RDX;
            if(R9== Pointers.TEKKEN7_UCRTBASE_WCTYPE["4A60"]) 
            {
                RSP50= Pointers.TEKKEN7_UCRTBASE_WCTYPE["4A90"];
                goto l10E0;
            }
            if(RDX== Pointers.TEKKEN7_UCRTBASE_WCTYPE["4A88"]) { goto l11A0; }
            if(RDX== Pointers.TEKKEN7_UCRTBASE_WCTYPE["4A90"]) { goto l1360; }
            R9 = Pointers.TEKKEN7_UCRTBASE_WCTYPE["4A70"];
            R9 &= RDX;
            if (R9 == Pointers.TEKKEN7_UCRTBASE_WCTYPE["4A70"]) { goto l1430; }
            R10 = Pointers.TEKKEN7_UCRTBASE_WCTYPE["4A70"];
            R10 &= R8;
            if(R10> Pointers.TEKKEN7_UCRTBASE_WCTYPE["4A50"]) { goto l1310; }
            R10 = Pointers.TEKKEN7_UCRTBASE_WCTYPE["4A70"];
            R10 &= R8;
            if(R10< Pointers.TEKKEN7_UCRTBASE_WCTYPE["4A58"]) { goto l14B0; }
            xmm[3] = Convert.ToInt64(xmm[0]) >> 0x34;
            R8 = Convert.ToInt64(xmm[0]);
            xmm[3] -= Pointers.TEKKEN7_UCRTBASE_WCTYPE["4AA0"];
            //xmm[6] = Cvtdq2pd(Convert.ToInt64(xmm[0]), (int)xmm[3]);
            xmm[6] = 0x3FF0000000000000;
            xmm[2] = (int)xmm[0] & Pointers.TEKKEN7_UCRTBASE_WCTYPE["4B90"];
            if (xmm[6].CompareTo(Pointers.TEKKEN7_UCRTBASE_WCTYPE["4C80"]) == 0) { goto l10A0; }
            R9 = R8;
            R8 &= Pointers.TEKKEN7_UCRTBASE_WCTYPE["4B08"];
            R9 &= Pointers.TEKKEN7_UCRTBASE_WCTYPE["4B10"];
            R9 = R9 << 1;
            R8 += R9;
            xmm[1] = R8;
            R8 = R8 >> 0x2C;


        l10A0:
        l10E0:
        l11A0:
        l11E0:
            RAX = 0;
            R11 = RDX;
            R9 = 0;
            R11 |= 0;
            R9 &= RDX;
            if(R9==0) { RAX = RDX; }
            R9 = -1;
            R9 &= RAX;
            if (R9 == 0) { goto l121B; }
            R9 &= 0;
            if (R9 == 0) { goto l1590; }

        l121B:
            xmm[0] = 1.00;
            xmm[6] = 0;
            xmm[7] = 0;


        l1230:
        l1310:
        l1360:
        l1430:
        l14B0:
        l1590:;

        }
    }
}
