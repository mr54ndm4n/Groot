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

            var iamGrootObject = Groot.GetObjectFromCsv<Tree>(path, false);
            Console.WriteLine("eiei");
        }
    }

    public class Tree
    {
        [GrootField("Name")]
        public string Name1 { get; set; }
        
        [GrootField("Name")]
        public string Name2 { get; set; }
        
        public string Name { get; set; }
        
        [GrootField("is_tree_dicotyledon")]
        public bool IsDicotyledon { get; set; }
        
        [GrootField("tree_width")]
        public decimal Width { get; set; }
        
        [GrootField("tree_height")]
        public decimal Height { get; set; }
    }
}