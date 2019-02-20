using Groot;

namespace GrootUnitTest.models
{
    public class Item
    {
        [GrootField("name")]
        public string Name { get; set; }

        [GrootField("amount")]
        public int Amount { get; set; }

        [GrootField("type")]
        public ItemType Type { get; set; }
    }
}