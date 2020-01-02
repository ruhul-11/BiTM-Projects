namespace ProjectOneStockManagement.Models
{
    public class ItemSummary
    {
        public int SL { get; set; }
        public string Item { get; set; }
        public string Company { get; set; }
        public string Category { get; set; }
        public int AvailableQuantity { get; set; }
        public int ReorderLevel { get; set; }
    }
}