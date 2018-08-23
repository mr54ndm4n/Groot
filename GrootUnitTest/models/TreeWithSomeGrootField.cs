using Groot;

namespace GrootUnitTest.models
{
    public class TreeWithSomeGrootField
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