namespace APIFinal.Models
{
    public class BetModel
    {
        public long Amount { get; set; }
        public int BetTypeId { get; set; }

        public string Currency { get; set; }
        public int GameId { get; set; }
        public int ProductId { get; set; }
        public int roundId { get; set; }
        
    }
}
