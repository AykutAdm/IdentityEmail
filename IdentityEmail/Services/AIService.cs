using System.Text;
using System.Text.Json;

namespace IdentityEmail.Services
{
    public class AIService
    {
        private readonly string _apiKey = "your-api-key";
        private readonly string _model = "gemini-2.5-pro";

        public async Task<string> PredictCategoryAsync(string messageText)
        {
            var url = $"https://generativelanguage.googleapis.com/v1/models/{_model}:generateContent?key={_apiKey}";

            using var http = new HttpClient();

            var requestBody = new
            {
                contents = new[]
                {
                new
                {
                    parts = new[]
                    {
                        new { text = $"Aşağıdaki kullanıcı Emailini içeriğini değerlendirerek kategorize et. Sadece kategori adı döndür.\n\nMesaj: {messageText}\n\nOlası kategoriler:\n- İş\n- İşletme\n- Aile\n- Arkadaşlar\n- Okul\n" }
                    }
                }
            }
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await http.PostAsync(url, content);
            var result = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(result);
            var text = doc.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text").GetString();

            return text.Trim();
        }

        public async Task<string> PredictPriorityAsync(string messageText)
        {
            var url = $"https://generativelanguage.googleapis.com/v1/models/{_model}:generateContent?key={_apiKey}";

            using var http = new HttpClient();

            var requestBody = new
            {
                contents = new[]
                {
            new
            {
                parts = new[]
                {
                    new { text = $@"
Aşağıdaki e-posta mesajının öncelik seviyesini belirle.
Sadece şu üç değerden birini döndür: Yüksek, Orta, Düşük.

Kurallar:
- Acil durum, hata, kritik problem, güvenlik, ödeme sorunu → Yüksek
- İşle ilgili normal talepler, bilgi güncelleme, teklif/yenileme istekleri → Orta
- Genel sohbet, selamlaşma, düşük öneme sahip bilgi paylaşımları → Düşük

Mesaj:
{messageText}
" }
                }
            }
        }
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await http.PostAsync(url, content);
            var result = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(result);
            var text = doc.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text").GetString();

            return text.Trim();
        }
    }
}
