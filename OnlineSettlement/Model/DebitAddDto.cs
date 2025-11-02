using System.ComponentModel.DataAnnotations;

namespace OnlineSettlement.Model
{
    public class DebitAddDto
    {
        public string Token { get; set; }

        public string FullName { get; set; }
    
        public string NationCode { get; set; }
       
        public string Mobile { get; set; }

        public string Description { get; set; }

       
        public decimal DebitAmount { get; set; }
        //public string DoctorName { get; set; }
        //public string FactorNumber { get; set; }

        public Debit ConvertToDebit()
        {
            return new Debit
            {
                CreateDate=DateTime.Now,
                DebitAmount=this.DebitAmount,
                Description=this.Description,
                FullName=this.FullName,
                IsPayed=false,
                Mobile=this.Mobile,
                NationCode=this.NationCode,
                ResultCode=string.Empty,
                DoctorName=string.Empty,
                FactorNumber=string.Empty,
                PayDate=null
            };
        }
    }
}
