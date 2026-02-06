## 📧 IdentityEmail

---
## 📋 Proje Hakkında
IdentityEmail, e-posta yönetimini kolaylaştıran ve yapay zeka ile desteklenen bir web uygulamasıdır. Kullanıcılar tek bir yerden gelen ve gönderilen mesajları yönetebilir, kategorilere göre filtreleyebilir ve dashboard üzerinden istatistiklerini takip edebilir. Uygulama, mesajları otomatik kategorilendirme, önceliklendirme ve kısa cevap önerileri sunma gibi AI özellikleriyle kullanıcı deneyimini iyileştirmeyi hedefler. Kayıt ve her girişte 6 haneli doğrulama kodu ile iki faktörlü doğrulama kullanılır; böylece hesap güvenliği güçlendirilir.

---

## 🛠️ Kullanılan Teknolojiler

### 📌 Backend: ASP.NET Core MVC 9.0
### 📌 ASP.NET Core Identity (iki faktörlü doğrulama ile birlikte)
### 📌 Veritabanı: SQL Server, Entity Framework Core
### 📌 E-posta gönderimi: MailKit / MimeKit, SMTP (Gmail)

### 🤖 Yapay zeka entegrasyonları

### 📌Google Gemini (Generative Language API): 

Gönderilen e-postaların konu ve içeriğine göre otomatik kategori (İş, İşletme, Aile, Arkadaşlar, Okul) ve öncelik (Yüksek, Orta, Düşük) atanması. Bu sayede gelen kutusu kategorilere göre filtrelenebilir ve önemli mesajlar öne çıkarılır.

### 📌OpenAI (Chat Completions API):

Dashboard’da kullanıcının gönderilen/gelen mesaj sayıları ve son mesaj içeriklerine göre kısa aktivite özeti üretilmesi.
Mesaj detay sayfasında, gelen mesajın içeriğine göre cevap önerisi üretilmesi; kullanıcı bu metni doğrudan yanıt olarak kullanabilir veya düzenleyebilir.

---

## 🖼️ Ekran Görüntüleri

### 🏠 Ana Sayfa

<div align="center">
  <img src="Images/AnaSayfa-1.png" alt="Admin Paneli-1" width="800" style="margin: 10px;">
  <img src="Images/AnaSayfa-2.png" alt="Admin Paneli-2" width="800" style="margin: 10px;">
  <img src="Images/Register-1.png" alt="Admin Paneli-3" width="800" style="margin: 10px;">
  <img src="Images/KullanimSartlari.png" alt="Admin Paneli-4" width="800" style="margin: 10px;">
  <img src="Images/KodDogrulama.png" alt="Admin Paneli-5" width="800" style="margin: 10px;">
  <img src="Images/Login-1.png" alt="Admin Paneli-6" width="800" style="margin: 10px;">
  <img src="Images/2FaktorDogrulama.png" alt="Admin Paneli-7" width="800" style="margin: 10px;">
</div>


### 🔐 Kullanıcı Paneli

<div align="center">
  <img src="Images/Dashboard-1.png" alt="Admin Paneli-1" width="800" style="margin: 10px;">
  <img src="Images/Dashboard-ProfileAnalysisWithOpenAI.png" alt="Admin Paneli-9" width="800" style="margin: 10px;">
  <img src="Images/Dashboard-2.png" alt="Admin Paneli-2" width="800" style="margin: 10px;">
  <img src="Images/Dashboard-3.png" alt="Admin Paneli-3" width="800" style="margin: 10px;">
  <img src="Images/Dashboard-MailDetailWithGeminiAI.png" alt="Admin Paneli-4" width="800" style="margin: 10px;">
  <img src="Images/Dashboard-RealMail.png" alt="Admin Paneli-5" width="800" style="margin: 10px;">
  <img src="Images/Dashboard-4.png" alt="Admin Paneli-6" width="800" style="margin: 10px;">
  <img src="Images/Dashboard-5.png" alt="Admin Paneli-7" width="800" style="margin: 10px;">
  <img src="Images/Dashboard-6.png" alt="Admin Paneli-8" width="800" style="margin: 10px;">
</div>
