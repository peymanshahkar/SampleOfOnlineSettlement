using IPE.SmsIrClient;
using IPE.SmsIrClient.Models.Requests;
using System;
using System.Text.Json;


namespace OnlineSettlement
{
    public class SmsHelper
    {
        public const short ProviderId = 1;
        public const long _lineNumber = 30002108000450;
        private const string _apiKey = "ociwfjOshfOJpwHndGK6mmV2oqw8MfjrJNc2";
        public SmsHelper()
        {
        }

        public Log_Sms SendMessage(string mobile, string message)
        {
            return LikeToLikeSend(new string[] { message }, new string[] { mobile });
        }
        public Log_Sms SendConfirmCode(string mobile, string confirmedCode)
        {
            SmsIr smsIr = new SmsIr(_apiKey);

            //if (!string.IsNullOrEmpty(App.Settings.RedirectSmsTo))
            //    mobile = App.Settings.RedirectSmsTo;

            var log = new Log_Sms
            {
                PhoneNumber = mobile,
                Body = confirmedCode,
                IsSuccessful = true,
                ProviderId = ProviderId,
            };

            try
            {
                var verificationSendResult =
                smsIr.VerifySendAsync(mobile, 100000,
                    new VerifySendParameter[] {
                        new VerifySendParameter("CODE", confirmedCode)
                    }).Result;
                log.ResultMessage = verificationSendResult.Message;
                log.ResultCode = verificationSendResult.Status.ToString();
                log.ResultBody = JsonSerializer.Serialize(verificationSendResult);
                if (verificationSendResult.Status != 1)
                    log.IsSuccessful = false;
            }
            catch (Exception ex)
            {
                log.ResultMessage = ex.Message;
                log.ResultCode = "666";
                log.IsSuccessful = false;
            }
            return log;
        }

        public async Task< Log_Sms> SendConfirmCode(string mobile, string PersonName,long Amount,string PayLink)
        {
            SmsIr smsIr = new SmsIr(_apiKey);

            var log = new Log_Sms
            {
                PhoneNumber = mobile,
                Body = PayLink,
                IsSuccessful = true,
                ProviderId = ProviderId,
            };
            string amount = Amount.ToString("N0")+" ریال";
            try
            {
                var verificationSendResult =
                await smsIr.VerifySendAsync(mobile, 961986,
                    new VerifySendParameter[] {
                        new VerifySendParameter("PERSONNAME", PersonName.Substring(0,24)),
                        new VerifySendParameter("AMOUNT",amount ),
                        new VerifySendParameter("PAYLINK", PayLink)
                    });
                log.ResultMessage = verificationSendResult.Message;
                log.ResultCode = verificationSendResult.Status.ToString();
                log.ResultBody = JsonSerializer.Serialize(verificationSendResult);
                if (verificationSendResult.Status != 1)
                    log.IsSuccessful = false;
            }
            catch (Exception ex)
            {
                log.ResultMessage = ex.Message;
                log.ResultCode = "666";
                log.IsSuccessful = false;
            }
            return log;
        }

        public async Task<Log_Sms> SendPayConfirm(string mobile, string PersonName,string Code)
        {
            SmsIr smsIr = new SmsIr(_apiKey);

            var log = new Log_Sms
            {
                PhoneNumber = mobile,
                Body = Code,
                IsSuccessful = true,
                ProviderId = ProviderId,
            };
            
            try
            {
                var verificationSendResult =
                await smsIr.VerifySendAsync(mobile, 290821,
                    new VerifySendParameter[] {
                        new VerifySendParameter("PERSONNAME", PersonName.Substring(0,24)),
                        new VerifySendParameter("CODE", Code.Substring(0,24))
                    });
                log.ResultMessage = verificationSendResult.Message;
                log.ResultCode = verificationSendResult.Status.ToString();
                log.ResultBody = JsonSerializer.Serialize(verificationSendResult);
                if (verificationSendResult.Status != 1)
                    log.IsSuccessful = false;
            }
            catch (Exception ex)
            {
                log.ResultMessage = ex.Message;
                log.ResultCode = "666";
                log.IsSuccessful = false;
            }
            return log;
        }


        public Log_Sms LikeToLikeSend(string mobile, string message)
        {
            string[] mobiles = new string[1];
            string[] messages = new string[1];
            mobiles[0] = mobile;
            messages[0] = message;

            return LikeToLikeSend(mobiles, messages);
        }

        /// <summary>
        /// این متد برای ارسال به گروهی از موبایل‌ها با متن‌های متفاوت برای هر کدام، مورد استفاده قرار می‌گیرد.
        /// همچنین شما می‌توانید با مقداردهی به پارامتر زمان ارسال، از قابلیت ارسال پیامک زمانبندی شده نیز استفاده نمایید.
        /// </summary>
        public Log_Sms LikeToLikeSend(string[] mobiles, string[] messages)
        {
            SmsIr smsIr = new SmsIr(_apiKey);
            //if (!string.IsNullOrEmpty(App.Settings.RedirectSmsTo))
            //{
            //    mobiles = new string[] { App.Settings.RedirectSmsTo };
            //    messages = new string[] { messages[0] };
            //}

            // TODO: Create log per reciever and return list of logs.
            var log = new Log_Sms
            {
                PhoneNumber = mobiles[0],
                Body = messages[0],
                IsSuccessful = true,
                ProviderId = ProviderId,
            };

            try
            {
                var verificationSendResult =
                smsIr.LikeToLikeSend(_lineNumber,
                    messages, mobiles);
                log.ResultMessage = verificationSendResult.Message;
                log.ResultCode = verificationSendResult.Status.ToString();
                log.ResultBody = JsonSerializer.Serialize(verificationSendResult);
                if (verificationSendResult.Status != 1)
                    log.IsSuccessful = false;
            }
            catch (Exception ex)
            {
                log.ResultMessage = ex.Message;
                log.ResultCode = "666";
                log.IsSuccessful = false;
            }
            return log;
        }


        public string GenerateOtpCode()
        {
            var chars = "0123456789";
            var random = new Random((int)System.DateTime.Now.Ticks);
            var result = new string(
                Enumerable.Repeat(chars, 4)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }
    }
}
