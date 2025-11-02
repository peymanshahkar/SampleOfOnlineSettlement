using System.ComponentModel.DataAnnotations;
namespace OnlineSettlement.Model
{
    public class Debit
    {
        public int DebitId { get; set; }

        [Required(ErrorMessage ="لطفا نام و نام خانوادگی را وارد کنید")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "لطفا کدملی را وارد کنید")]
        public string NationCode { get; set; }
        [Required(ErrorMessage = "لطفا شماره موبایل را وارد کنید")] [RegularExpression(@"^09\d{9}$", ErrorMessage = "شماره موبایل نامعتبر است")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "لطفا شرح بدهی را وارد کنید")]
        public string Description { get; set; }
        public string DoctorName { get; set; }
        public string FactorNumber { get; set; }

        [Required(ErrorMessage = "لطفا مبلغ بدهی را وارد کنید")]
        public decimal DebitAmount { get; set; }
        public string LinkId { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsPayed { get; set; }
        public DateTime? PayDate { get; set; }



        public string ResultCode { get; set; } = string.Empty;

        public string PersianCreateDate
        {
            get
            {
                if (CreateDate == null)
                    return "";

                return CreateDate.ToPersianDate();
            }
        }

        public string PersianPayDate
        {
            get
            {
                if (PayDate == null)
                    return "";

                return PayDate.Value.ToPersianDate();
            }
        }
    }
}
