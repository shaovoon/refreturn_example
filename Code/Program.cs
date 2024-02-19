using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CSharpRefReturnBenchmark
{
    struct Point
    {
        public int x;
        public int y;
    }
    class Coordinate
    {
        public Point _Point;
        public Point Pt
        {
            set { this._Point = value; }
            get { return this._Point; }
        }
        public ref Point RefPt
        {
            get { return ref this._Point; }
        }
    }
    class Program
    {
        static void DisplayElapseTime(string title, TimeSpan ts)
        {
            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}.{1:000}",
                ts.Seconds,
                ts.Milliseconds);
            Console.WriteLine(title + elapsedTime);
        }
        
        static void Main(string[] args)
        {
            const int MAX_LOOP = 1000;
            List<Coordinate> list = new List<Coordinate>();
            for (int i = 0; i < 100000; ++i)
            {
                list.Add(new Coordinate());
            }

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            for (int j = 0; j < MAX_LOOP; ++j)
            {
                for (int i = 0; i < list.Count; ++i)
                {
                    Point pt = list[i].Pt;
                    pt.x = i + j;
                    pt.y = i + j;
                    list[i].Pt = pt;
                }
            }

            stopWatch.Stop();
            DisplayElapseTime("        Value Return RunTime:", stopWatch.Elapsed);

            Stopwatch stopWatch2 = new Stopwatch();
            stopWatch2.Start();

            for (int j = 0; j < MAX_LOOP; ++j)
            {
                for (int i = 0; i < list.Count; ++i)
                {
                    ref Point pt = ref list[i].RefPt;
                    pt.x = i + j;
                    pt.y = i + j;
                }
            }
            stopWatch2.Stop();
            DisplayElapseTime("          Ref Return RunTime:", stopWatch2.Elapsed);

            Stopwatch stopWatch3 = new Stopwatch();
            stopWatch3.Start();

            for (int j = 0; j < MAX_LOOP; ++j)
            {
                for (int i = 0; i < list.Count; ++i)
                {
                    list[i]._Point.x = i + j;
                    list[i]._Point.y = i + j;
                }
            }
            stopWatch3.Stop();
            DisplayElapseTime("Public Member Access RunTime:", stopWatch3.Elapsed);

            Console.WriteLine("Press any key and Enter to end the program!");
            Console.ReadKey();
        }
    }
}
