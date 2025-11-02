using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OnlineSettlement.Data;
using OnlineSettlement.Model;

namespace OnlineSettlement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DebitController : ControllerBase
    {
        IDebitRepository _DebitRep;
        private string token = "c1b2c3d4-e5f6-7890-1234-56789abcdep0";
        public DebitController(IDebitRepository debitRep)
        {
            _DebitRep = debitRep;
        }

        [HttpPost]
        [Route("AddDebit")]
        public async Task<IActionResult> AddDebit([FromBody] DebitAddDto debitAdd)
        {
            
            if(!ModelState.IsValid)
            {
                return Ok(new ResponceDto
                {
                    StatusCode = "400",
                    Message = "داده های ارسالی نامعتبر می باشد"
                });
            }

            if(debitAdd.Token!=token)
            {
                return Ok(new ResponceDto
                {
                    StatusCode = "401",
                    Message = "دسترسی غیرمجاز"
                });
            }
            var debit = debitAdd.ConvertToDebit();

            debit.LinkId = Guid.NewGuid().ToString().Substring(0, 18);

            await _DebitRep.AddDebitAsync(debit);

            SmsHelper smshelper = new SmsHelper();
          
            await smshelper.SendConfirmCode(debit.Mobile, debit.FullName, Convert.ToInt64(debit.DebitAmount), debit.LinkId);


            return Ok(new ResponceDto
            {
                StatusCode="200",
                Message="عملیات با موفقیت انجام شد"
            });

        }
    }
}
