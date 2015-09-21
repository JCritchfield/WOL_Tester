using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace WOL_Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] mac_add = new byte[6];

            Console.WriteLine("This program will test the Wake-on-LAN Feature given a specific MAC Address");
            for (int i = 0; i < 6; i++)
            {
                Console.Write("MAC Byte #{0}: ", i+1);
                string response = Console.ReadLine();
                mac_add[i] = response.ToByte();
            }

            //Start the WOL process
            using (UdpClient client = new UdpClient())
            {
                client.Connect(IPAddress.Broadcast, 40000);
                List<Byte> packet = new List<Byte>();
                for(int i = 0; i < 6; i++) {
                    packet.Add(0xFF);
                }

                for(int i = 0; i < 16; i++) {
                    packet.AddRange(mac_add);
                }

                client.Send(packet.ToArray(), packet.Count);
            }

            Console.Read();
        }
    }

    public static class ByteConverter
    {
        public static byte ToByte(this string byteToConvert)
        {
            if (byteToConvert.Length > 2)
                throw new Exception("Byte string source is too long!");
            if (byteToConvert.Length < 2)
                throw new Exception("Byte string source too short!");

            byte firstChar = getValue(byteToConvert[0]);
            byte secondChar = getValue(byteToConvert[1]);

            byte mergedByte = firstChar;
            mergedByte <<= 4;
            mergedByte |= secondChar;

            return mergedByte;
        }

        private static byte getValue(char value) 
        {
            switch (value)
            {
                case '0':
                    return 0x00;
                case '1':
                    return 0x01;
                case '2':
                    return 0x02;
                case '3':
                    return 0x03;
                case '4':
                    return 0x04;
                case '5':
                    return 0x05;
                case '6':
                    return 0x06;
                case '7':
                    return 0x07;
                case '8':
                    return 0x08;
                case '9':
                    return 0x09;
                case 'a':
                case 'A':
                    return 0x0A;
                case 'b':
                case 'B':
                    return 0x0B;
                case 'c':
                case 'C':
                    return 0x0C;
                case 'd':
                case 'D':
                    return 0x0D;
                case 'f':
                case 'F':
                    return 0x0F;
                default:
                    return 0x00;
            }
        }
    }
}
