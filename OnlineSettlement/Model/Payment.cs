namespace OnlineSettlement.Model
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int DebitId { get; set; }
        public string? ResultCode { get; set; }
        public string? ResultMessage { get; set; }
        public string? ResultBody { get; set; }
        public int PaymentStatus { get; set; }
        public decimal Amount { get; set; }
        public string? VerifyBody { get; set; }
        public string? VerifyCode { get; set; }
        public string? VerifyMessage { get; set; }
        public string? CallBackUrl { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}