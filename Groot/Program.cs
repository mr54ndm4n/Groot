using System;
using System.IO;

namespace Groot
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var path = Path.GetFullPath("input/input.csv");
            var iamGroot = Groot.GetDictFromCsv(path);
            Console.WriteLine(iamGroot[2]["booleanColumn"]);

            var iamGrootObject = Groot.GetObjectFromCsv<Eiei>(path);
            Console.WriteLine("eiei");
        }
    }

    public class Eiei
    {
        public int intColumn { get; set; }
        public int int2Column { get; set; }
        public string stringColumn { get; set; }
        public bool booleanColumn { get; set; }
        public decimal decimalColumn { get; set; }
        public decimal decimal2Column { get; set; }
        public long longNullableColumn { get; set; }
    }
}