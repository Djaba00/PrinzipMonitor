using System;
using System.Net;

namespace PrinzipMonitorService.Pll.Services.PriceCheckerService
{
    public class PriceChecker
    {
        string Url { get; set; }

        public PriceChecker(string setUrl)
        {
            Url = setUrl;
        }

        public int? GetPrice()
        {
            try
            {
                using (HttpClientHandler httpHandler = new HttpClientHandler
                {
                    AllowAutoRedirect = false,
                    CookieContainer = new CookieContainer()
                })
                {
                    using (HttpClient client = new HttpClient(httpHandler, false))
                    {
                        client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/116.0.0.0 Safari/537.36");
                        client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
                        client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9,ru;q=0.8");
                        //clnt.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
                        client.DefaultRequestHeaders.Add("Connection", "keep-alive");
                        client.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");

                        using (var response = client.GetAsync(Url).Result)
                        {
                            if (!response.IsSuccessStatusCode)
                            {
                                return null;
                            }
                        }
                    }

                    using (HttpClient client = new HttpClient(httpHandler, false))
                    {
                        client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:80.0) Gecko/20100101 Firefox/80.0");
                        client.DefaultRequestHeaders.Add("Accept", "*/*");
                        client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9,ru;q=0.8");
                        client.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
                        client.DefaultRequestHeaders.Add("Connection", "keep-alive");
                        client.DefaultRequestHeaders.Add("Referer", Url);

                        using (var response = client.GetAsync(Url).Result)
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                var json = response.Content.ReadAsStringAsync().Result;
                                if (!string.IsNullOrEmpty(json))
                                {
                                    PrinzipResponse result = Newtonsoft.Json.JsonConvert.DeserializeObject<PrinzipResponse>(json);
                                    return result.price;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return null;
        }
    }
}
