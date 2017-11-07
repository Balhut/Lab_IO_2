using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lab2
{
    class Program
    {
        //==================ZAD6=====================
        static void myAsyncCallback(IAsyncResult result)
        {
            object[] data = (object[])result.AsyncState;
            FileStream stream = data[0] as FileStream;
            byte[] buffer = (byte[])data[1];
            Console.WriteLine(Encoding.ASCII.GetString(buffer));
            stream.Close();
            Console.WriteLine("==========");
        }
        static void zad6()
        {
            byte[] buffer = new byte[1024];
            string path = Directory.GetCurrentDirectory() + "\\items.txt";
            FileStream fs = new FileStream(path, FileMode.Open);
            fs.BeginRead(buffer, 0, buffer.Length, myAsyncCallback, new object[] { fs, buffer });
        }

        //==================ZAD7=====================
        static void zad7()
        {
            byte[] buffer = new byte[1024];
            string path = Directory.GetCurrentDirectory() + "\\items.txt";
            FileStream fs = new FileStream(path, FileMode.Open);
            var ar = fs.BeginRead(buffer, 0, buffer.Length, null, new object[] { fs, buffer });
            fs.EndRead(ar);
            Console.WriteLine(Encoding.ASCII.GetString(buffer));
            fs.Close();
            Console.WriteLine("==========");
        }

        //==================ZAD8=====================
        delegate void myDelegate(int n);
        public static void Fibo_I(int len)
        {
            int a = 0, b = 1, c = 0;
            Console.Write("{0} {1}", a, b);

            for (int i = 2; i < len; i++)
            {
                c = a + b;
                Console.Write(" {0}", c);
                a = b;
                b = c;
            }
        }

        public static void Fibo_R(int len)
        {
            Fibo_RTemp(0, 1, 1, len);
        }
        private static void Fibo_RTemp(int a, int b, int counter, int len)
        {
            if (counter <= len)
            {
                Console.Write("{0} ", a);
                Fibo_RTemp(b, a + b, counter + 1, len);
            }
        }
        static void zad8()
        {
            myDelegate dg1 = new myDelegate(Fibo_I);
            myDelegate dg2 = new myDelegate(Fibo_R);
            Stopwatch stopWatch = new Stopwatch();
            Stopwatch stopWatch2 = new Stopwatch();
            stopWatch.Start();
            var result1 = dg1.BeginInvoke(10, null, new object[] { dg1 });
            dg1.EndInvoke(result1);
            stopWatch.Stop();
            Console.WriteLine();
            stopWatch2.Start();
            var result2 = dg2.BeginInvoke(10, null, new object[] { dg2 });
            dg1.EndInvoke(result2);
            stopWatch2.Stop();
            string time1 = stopWatch.ElapsedMilliseconds.ToString();
            string time2 = stopWatch2.ElapsedMilliseconds.ToString();
            Console.WriteLine("Czasy wykonania:");
            Console.WriteLine("Iteracyjnie: " + time1);
            Console.WriteLine("Rekurencyjnie: " + time2);
        }

        static void Main(string[] args)
        {
            //zad6();
            //zad7();
            zad8();
        }
    }
}