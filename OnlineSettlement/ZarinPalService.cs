// Services/ZarinPalService.cs
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using OnlineSettlement.Model;
public class ZarinPalService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public ZarinPalService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    public async Task<string> RequestPayment(
        decimal amount,
        string description,
        string? mobile = null,
        string? email = null)
    {
        var merchantId = _config["Merchant"];
        var callbackUrl = _config["CallbackUrl"];

        var metadata = new Dictionary<string, string>();

        if (!string.IsNullOrEmpty(mobile))
            metadata.Add("mobile", mobile);

        if (!string.IsNullOrEmpty(email))
            metadata.Add("email", email);

        var requestData = new
        {
            merchant_id = merchantId,
            amount = amount , //واحد پولی ریال
            description,
            callback_url = callbackUrl,
            metadata
        };

        var response = await _httpClient.PostAsJsonAsync(URLs.requestUrl, requestData);
        var responseData = await response.Content.ReadAsStringAsync();
        var jObject = JObject.Parse(responseData);


        string errorscode = jObject["errors"].ToString();
        string dataauth = jObject["data"].ToString();

        if (dataauth != "[]")
        {
            return jObject["data"]["authority"].ToString();
        }

        throw new Exception("error " + errorscode);

    }

    public async Task<string> VerifyPayment(string authority, decimal inAmount)
    {
        var merchantId = _config["Merchant"];

        var verifyData = new
        {
            merchant_id = merchantId,
            amount = inAmount,//* 10, // Convert to Rials
            authority
        };

        var response = await _httpClient.PostAsJsonAsync(URLs.verifyUrl, verifyData);
        var responseData = await response.Content.ReadAsStringAsync();
        var jObject = JObject.Parse(responseData);

        string errors = jObject["errors"].ToString();

        string data = jObject["data"].ToString();

        if (data != "[]")
        {
            return jObject["data"]["ref_id"].ToString();
        }

        if (errors != "[]")
        {
            string errorscode = jObject["errors"]["code"].ToString();
            throw new Exception(errorscode);
        }

        throw new Exception("بروز خطا در تائید پرداخت صورتحساب");
    }
}