using System;

namespace ProjectOneStockManagement.Models
{
    [Serializable]
    public class Item
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Category { get; set; }
        public int Company { get; set; }
        public int ReorderLevel { get; set; }
        public int AvailableQuantity { get; set; }
        public int StockInQuantity { get; set; }

    }
}