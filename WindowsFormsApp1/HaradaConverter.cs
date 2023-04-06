using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SniperManagerApp
{
    internal class HaradaConverter
    {
        public static int T7ToNormal(long decodedDecimal)
        {
            long RAX, RBX, RCX, RDX, RDI, RSI, RBP, RSP, R8, R9, R10, R11;
            int EBX;
            double[] xmm=new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            RDI = decodedDecimal;
            byte[] bytes;

            RDI = (RDI & 0xFFFFFFFF) ^ 0x1D;
            RAX = (RDI & 0xFF);
            RBX = Pointers.TEKKEN7_DECODE_MAINCONST;
            if ((RAX & 0x1F) > 0)
            {
                RAX = (RAX & 0xFF) & 0x1F;
                RCX = RAX;
                RDX = Pointers.TEKKEN7_DECODE_SECONFCONST;
                while (RCX > 0)
                {
                    if ((RDX & RBX) != 0) { RAX = 1; }
                    else { RAX = 0; }
                    RBX = RAX + RBX * 2;
                    RCX--;
                }
            }
            xmm[1] = Pointers.TEKKEN7_CONSTS["49178"];
            xmm[0] = Pointers.TEKKEN7_CONSTS["2F3B8"];

            //ucrtbase.pow(ref xmm);
            xmm[1] = 0x3FF0000000000000;

            RBX = ((RBX & 0xFFFFFFFF) & unchecked((long)0xFFFFFFFFFFFFFFE0));   //and ebx,-20
            RBX = ((RBX & 0xFFFFFFFF) ^ ((RDI & 0xFFFFFFFF)));
            RAX = ((RBX & 0xFFFFFFFF) << 0x8);
            RCX = 0x100;
            RDX = (RAX & 0xFFFFFFFF) >> 31;                                     //cdq

            int result = (int)(RAX & 0xFFFFFFFF) / (int)(RCX & 0xFFFFFFFF);
            int remainder = (int)(RAX & 0xFFFFFFFF) % (int)(RCX & 0xFFFFFFFF);
            RAX = result; RDX = remainder;

            return (int)RAX;
        }
        public static int T7ToHarada(long decodedDecimal)
        {
            long RAX, RBX, RCX, RDX, RDI, RSI, RBP, RSP, R8, R9, R10, R11;
            RCX = decodedDecimal;

            RAX = RCX & 0xFF;
            RDI = Pointers.TEKKEN7_DECODE_MAINCONST;
            RBX = Pointers.TEKKEN7_DECODE_SECONFCONST;
            R9 = RDI;

            //jna TekkenGame-Win64-Shipping.exe.text+2AED15
            if (((RAX & 0xFF) & 0x1F) > 0)
            {
                RAX = (RAX & 0xFF) & 0x1F;
                RDX = RAX & 0xFF;
                while (RDX > 0)
                {
                    if ((RBX & R9) != 0) { RAX = 1; }
                    else { RAX = 0; }
                    R9 = RAX + R9 * 2;
                    RDX--;
                }
            }
            //TekkenGame-Win64-Shipping.exe.text+2AED15
            R9 = ((R9 & 0xFFFFFFFF) & unchecked((long)0xFFFFFFFFFFFFFFE0)); //and r9d,-20
            R9 = (R9 & 0xFFFFFFFF) ^ (RCX & 0xFFFFFFFF);
            R9 = (R9 & 0xFFFFFFFF) ^ 0x1D;
            R9 = (R9 & 0xFFFFFFFF) & 0x00FFFFFF;
            R10 = R9;
            R11 = 0;

            R8 = 0;
            //TekkenGame-Win64-Shipping.exe.text+2AED30
            do
            {
                RCX = (R8 & 0xFF) & 0xFFFFFFFF;
                RAX = RDI;
                RCX = (RCX & 0xFF) + 8;
                //je TekkenGame-Win64-Shipping.exe.text+2AED55
                RDX = (RCX & 0xFF);
                while (RDX > 0)
                {
                    RCX = RCX >> 8 * 4;
                    RCX = RCX << 8 * 4;
                    if ((RBX & RAX) != 0) { RCX++; }

                    RAX = (long)(RCX + RAX * 2);
                    RDX--;
                }
                //TekkenGame-Win64-Shipping.exe.text+2AED55
                RAX = (RAX & 0xFFFFFFFF) ^ (R10 & 0xFFFFFFFF);
                R8 += 8;
                R11 = (R11 & 0xFFFFFFFF) ^ (RAX & 0xFFFFFFFF);
                R10 = (R10 & 0xFFFFFFFF) >> 8;
            } while ((R8 & 0xFFFFFFFF) < 0x18);
            //jb TekkenGame-Win64 - Shipping.exe.text + 2AED30
            RCX = 0x00000001;
            RAX = (R11 & 0xFF);
            if (RAX == 0) { RAX = (RCX & 0xFFFFFFFF); }
            RAX = (RAX & 0xFFFFFFFF) << 0x18;
            RAX += (R9 & 0xFFFFFFFF);

            return (int)(RAX & 0xFFFFFFFF);
        }
    }
}
