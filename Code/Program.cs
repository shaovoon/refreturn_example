using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        
        /*
        static void Main(string[] args)
        {
            // ref-local variable example
            //int a = 10;
            //ref int b = ref a;
            //b = 20;
            //Console.WriteLine("{0}", a); // display 20

            RefReturnExample();
            //CopyPropertyExample();
            //PublicMemberExample();
            Console.ReadKey();
        }
        
        static void PublicMemberExample()
        {
            Coordinate cd = new Coordinate();
            cd._Point.x = 10;
            cd._Point.y = 20;
            Console.WriteLine("{0},{1}", cd.Pt.x, cd.Pt.y); // display 10,20
        }
        static void CopyPropertyExample()
        {
            Coordinate cd = new Coordinate();
            Point pt = cd.Pt; // a copy
            pt.x = 10;
            pt.y = 20;
            Console.WriteLine("{0},{1}", cd.Pt.x, cd.Pt.y); // display 0,0
            cd.Pt = pt;
            Console.WriteLine("{0},{1}", cd.Pt.x, cd.Pt.y); // display 10,20

        }
        static void RefReturnExample()
        {
            Coordinate cd = new Coordinate();
            ref Point pt = ref cd.RefPt;
            pt.x = 10;
            pt.y = 20;
            Console.WriteLine("{0},{1}", cd.Pt.x, cd.Pt.y); // display 10,20
        }
        */
        static void DisplayElapseTime(string title, TimeSpan ts)
        {
            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}.{2:000}",
                ts.Minutes, ts.Seconds,
                ts.Milliseconds);
            Console.WriteLine(title + elapsedTime);
        }
        
        static void Main(string[] args)
        {
            List<Coordinate> list = new List<Coordinate>();
            for (int i = 0; i < 10000000; ++i)
            {
                list.Add(new Coordinate());
            }

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            for(int i=0; i<list.Count; ++i)
            {
                Point pt = list[i].Pt;
                pt.x = 10;
                pt.y = 20;
                list[i].Pt = pt;
            }

            stopWatch.Stop();
            DisplayElapseTime("Value Return RunTime:", stopWatch.Elapsed);

            Stopwatch stopWatch2 = new Stopwatch();
            stopWatch2.Start();

            for (int i = 0; i < list.Count; ++i)
            {
                ref Point pt = ref list[i].RefPt;
                pt.x = 10;
                pt.y = 20;
            }

            stopWatch2.Stop();
            DisplayElapseTime("Ref Return RunTime:", stopWatch2.Elapsed);

            Stopwatch stopWatch3 = new Stopwatch();
            stopWatch3.Start();

            for(int i=0; i<list.Count; ++i)
            {
                list[i]._Point.x = 10;
                list[i]._Point.y = 20;
            }

            stopWatch3.Stop();
            DisplayElapseTime("Public Member access RunTime:", stopWatch3.Elapsed);

            Console.ReadKey();
        }
    }
}
