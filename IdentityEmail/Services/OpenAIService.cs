using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace IdentityEmail.Services
{
    public class OpenAIService
    {
        private readonly string _apiKey;
        private readonly string _model = "gpt-4o-mini";

        public OpenAIService(IConfiguration configuration)
        {
            _apiKey = configuration["OpenAI:ApiKey"];
        }

        public async Task<string> GenerateUserSummaryAsync(int sentCount, int receivedCount, string recentMessages)
        {
            var url = "https://api.openai.com/v1/chat/completions";

            using var http = new HttpClient();
            http.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

            var prompt = $@"Kullanıcının mesaj istatistikleri:
- Gönderilen mesaj sayısı: {sentCount}
- Gelen mesaj sayısı: {receivedCount}

Son mesaj içerikleri:
{recentMessages}

Bu bilgilere göre kullanıcının mesaj aktivitesi hakkında kısa, öz ve Türkçe bir yorum yap. 3-4 cümle yeterli. Doğrudan yorumu yaz, ek başlık veya format kullanma.";

            var requestBody = new
            {
                model = _model,
                messages = new[]
                {
                    new { role = "system", content = "Sen yardımcı bir asistansın. Kullanıcının mail istatistiklerini analiz edip kısa özetler sunuyorsun." },
                    new { role = "user", content = prompt }
                },
                temperature = 0.7
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await http.PostAsync(url, content);
            var result = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(result);
            var text = doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content").GetString();

            return text.Trim();
        }


        public async Task<string> GenerateReplySuggestionAsync(string messageContent, string subject)
        {
            var url = "https://api.openai.com/v1/chat/completions";

            using var http = new HttpClient();
            http.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

            var prompt = $@"Aşağıdaki e-posta mesajına kısa bir Türkçe yanıt önerisi yaz.

Konu: {subject}
Mesaj: {messageContent}

Sadece yanıt metnini yaz, ek açıklama veya format kullanma. 2-3 cümle yeterli.";

            var requestBody = new
            {
                model = _model,
                messages = new[]
                {
                    new { role = "system", content = "Sen e-posta yanıtları yazan bir asistansın." },
                    new { role = "user", content = prompt }
                },
                temperature = 0.7
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await http.PostAsync(url, content);
            var result = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(result);
            var text = doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content").GetString();

            return text.Trim();
        }
    }
}
