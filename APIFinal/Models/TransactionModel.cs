namespace APIFinal.Models
{
    public class TransactionModel
    {

        public long Amount { get; set; }
        public int BetTypeId { get; set; }

        public string Currency { get; set; }
        public string PrivateToken { get; set; }

        public string PaymentType { get; set; }
        public DateTime CreateDate { get; set; }
        public int Status { get; set; }
        public int GameId { get; set; }
        public int RoundId { get; set; }

        public string TransactionId { get; set; }
    }
}
