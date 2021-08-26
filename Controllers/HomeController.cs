using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebYouToken.Controllers {
    public class HomeController : Controller {
        //[Route("")]
        public IActionResult Index() {
            return View();
        }

        [Route("code")]
        public async Task<IActionResult> Code(string error, string error_description, string code) {
            if (!string.IsNullOrEmpty(error)) {
                return Content(!string.IsNullOrEmpty(error_description) ? $"Ошибка {error}, {error_description}" : $"Ошибка {error}");
            }

            using var client = new HttpClient();
            var data = new FormUrlEncodedContent(new Dictionary<string, string> {
                ["code"] = code,
                ["client_id"] = Program.ClientId,
                ["grant_type"] = "authorization_code",
                ["redirect_uri"] = "http://youcode.local/code"
            });
            var res = await client.PostAsync("https://yoomoney.ru/oauth/token", data);
            if (!res.IsSuccessStatusCode) {
                return Content("Ошибка запроса " + res.StatusCode);
            }

            var obj = JObject.Parse(await res.Content.ReadAsStringAsync());
            return obj.ContainsKey("error") ? Content("Ошибка ответа " + obj["error"]) : Content($"Ваш токен:\n{obj["access_token"]}");
        }
    }
}
