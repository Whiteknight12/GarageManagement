namespace APIClient.Models.APIModels
{
    public class Car
    {
        public int CarID { get; set; }
        public string? OwnerAddress { get; set; }
        public string? OwnerName { get; set; }
        public string? OwnerPhone { get; set; }
        public DateTime? SentDate { get; set; }
        public string? CarBrand { get; set; }
        public long? CarNumber { get; set; }
    }
}
