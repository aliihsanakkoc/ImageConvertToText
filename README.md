# 🧠 WPF OCR Uygulaması (VB.NET + Tesseract)

Bu proje, **VB.NET WPF** kullanılarak geliştirilen basit ama güçlü bir **OCR (Optical Character Recognition)** uygulamasıdır.  
Uygulama, kullanıcıdan alınan ekran görüntülerini veya dosyadan seçilen görselleri **Tesseract OCR** motoru ile analiz eder ve içindeki metinleri çözümler.

---

## 🚀 Özellikler

- 📋 **Clipboard’tan resim yapıştırma:**  
  `Windows + Shift + S` tuş kombinasyonu ile ekranın bir bölümünü seçip, ardından **“Yapıştır”** butonuyla görüntüyü uygulamaya aktarabilirsin.

- 📁 **Dosyadan görsel seçme:**  
  Bilgisayarındaki `.jpg`, `.png`, `.bmp`, `.tif` gibi formatlardaki dosyaları açabilirsin.

- 🔤 **Dil seçimi:**  
  OCR işlemini **Türkçe (tur)** veya **İngilizce (eng)** dillerinde gerçekleştirebilirsin.

- 🧾 **OCR çıktısı:**  
  Görüntüden çıkarılan metin `TextBox` içinde gösterilir ve istenirse dışa aktarılabilir.

- ✅ **Tesseract 5** destekli, tam yerel çalışır (internet gerekmez).

---

## 🖼️ Ekran Görüntüsü

> 📸 Uygulama arayüzü örneği:

![Uygulama Arayüzü](docs/screenshot.png)

---

## 🧩 Gereksinimler

| Bileşen | Açıklama |
|----------|-----------|
| **.NET Framework / .NET 6+** | Projeye uygun sürüm |
| **Tesseract OCR Engine** | [Tesseract 5](https://github.com/tesseract-ocr/tesseract) |
| **Tesseract .NET Wrapper (Tesseract.dll)** | .NET için Tesseract NuGet paketi |
| **VB.NET WPF** | Visual Studio 2022 veya daha yeni |

---

## 📦 Kurulum

1. Repozitoyu klonla:
   ```bash
   git clone https://github.com/aliihsanakkoc/ImageConvertToText.git
2. Visual Studio ile aç.
3. Gerekli NuGet paketlerini yükle:
   ```bash
   Install-Package Tesseract
5. Proje dizininde tessdata klasörü oluştur.
6. tessdata klasörüne aşağıdaki dil dosyalarını ekle:
    tur.traineddata
    eng.traineddata
    Bu dosyaları linkten: https://github.com/tesseract-ocr/tessdata_best indirebilirsin.

🧠 Kullanım

  -Uygulamayı aç.
  
  -Dil seçimini yap: Türkçe veya İngilizce.
  
  -Görseli yüklemek için: Windows + Shift + S ile bir alan seç → sonra ClipBoarddan Yapıştır butonuna tıkla veya Dosyadan Resim Seç butonuyla bir görsel aç.
  
  -Metin Ayıkla butonuna tıkla.
  
  -Çıkarılan metin txtOutput kutusunda görüntülenecektir.
