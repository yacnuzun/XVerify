# 🔍 XVerify

**XML, XSD, Schematron ve XSLT doğrulama ve dönüştürme işlemleri için tek nokta: Modern bir .NET 8 Blazor uygulaması.**

[![.NET 8](https://img.shields.io/badge/.NET-8-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![Blazor](https://img.shields.io/badge/Blazor-WebAssembly-purple?logo=blazor)](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)
[![Docker](https://img.shields.io/badge/Docker-Container-blue?logo=docker)](https://www.docker.com/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

XVerify, SOAP API'lerin ve XML tabanlı sistemlerin kalbinde yer alan XML, XSD, Schematron ve XSLT teknolojilerini görselleştirmenizi, doğrulamanızı ve anlamanızı sağlayan modern bir web uygulamasıdır.


## ✨ Öne Çıkan Özellikler

-   **🧪 Çoklu Doğrulama:** Tek seferde XSD şema doğrulaması ve Schematron kural kontrolü yapın.
-   **🔄 XSLT Dönüşümü:** XML dosyalarınızı anında HTML'ye veya başka bir XML formatına dönüştürün.
-   **📋 Detaylı Raporlama:** XSD hatalarını liste halinde ve Schematron sonuçlarını SVRL formatında görüntüleyin.
-   **⚡ Modern UI:** .NET 8'in gücüyle çalışan, interaktif ve hızlı bir Blazor Server arayüzü.
-   **🐳 Docker Desteği:** Tamamen konteynerize edilmiş yapı sayesinde tutarlı bir geliştirme ve dağıtım ortamı.
-   **🔩 Modüler Yapı:** Servis tabanlı mimarisi sayesinde kolayca genişletilebilir.

## 🏗️ Mimari ve Teknolojiler

**Backend:**
-   [.NET 8](https://dotnet.microsoft.com/) - Ana runtime ve framework
-   [Blazor Server](https://learn.microsoft.com/tr-tr/aspnet/core/blazor/) - Dinamik kullanıcı arayüzü
-   [System.Xml](https://learn.microsoft.com/tr-tr/dotnet/api/system.xml) - XML, XSD, XSLT işlemleri için .NET kütüphaneleri

**DevOps & Dağıtım:**
-   [Docker](https://www.docker.com/) - Konteynerizasyon
-   [Docker Compose](https://docs.docker.com/compose/) - Çoklu konteyner yönetimi

## 🚀 Hızlı Başlangıç

XVerify'i çalıştırmanın en kolay yolu Docker kullanmaktır.

### Docker ile Çalıştırma

1.  **Depoyu Klonlayın:**
    ```bash
    git clone https://github.com/kullanici_adiniz/XVerify.git
    cd XVerify
    ```

2.  **Docker Konteynerını Ayağa Kaldırın:**
    ```bash
    docker-compose up --build
    ```
    Bu komut, uygulamayı derleyecek ve `http://localhost:8080` adresinde yayına alacaktır.

3.  **Tarayıcınızı Açın:**
    `http://localhost:8080` adresine gidin ve XML dosyalarınızı test etmeye başlayın!

### Geliştirme Modunda Çalıştırma (Docker'sız)

1.  Gerekli [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)'sını yükleyin.
2.  Solution dizininde:
    ```bash
    dotnet restore
    dotnet run --project WebUI
    ```
3.  Uygulama `http://localhost:5000` (veya benzeri bir port) üzerinde çalışacaktır.

## 📖 Kullanım Kılavuzu

1.  **XML Yükleme:** Ana sayfadaki dosya seçiciyi kullanarak bir XML dosyası yükleyin.
2.  **Doğrulama:** Uygulama, önceden yapılandırılmış (`/Schemas/` dizinindeki) şema ve kuralları otomatik olarak kullanacaktır:
    -   **`invoice.xsd`**: XML yapısının şema kurallarını doğrular.
    -   **`rules.xslt`**: Derlenmiş Schematron kurallarını çalıştırarak iş mantığını kontrol eder.
    -   **`invoice-to-html.xslt`**: XML'i insanların okuyabileceği bir HTML formatına dönüştürür.
3.  **Sonuçları İnceleme:** Sonuçlar sayfasında üç ana bölüm görüntülenecektir:
    -   **XSD Hataları:** Herhangi bir şema ihlali listesi.
    -   **Schematron Raporu:** SVRL formatında ayrıntılı kural ihlal raporu.
    -   **Dönüştürülmüş HTML:** XML'inizin son kullanıcıya nasıl görüneceğinin önizlemesi.

## 🗂️ Proje Yapısı
XVerify/

├── Application/ # Core iş mantığı ve servisler

│ ├── Services/

│ │ ├── XsdValidatorService.cs

│ │ ├── SchematronValidatorService.cs

│ │ └── XsltTransformerService.cs

│ └── Dto/

│ └── XmlProcessingResult.cs

├── WebUI/ # Blazor UI Katmanı

│ ├── Pages/
│ │ └── upload-xml.razor # Ana bileşen

│ └── wwwroot/

├── Schemas/ # XSD, Schematron, XSLT dosyaları

│ ├── invoice.xsd

│ ├── rules.xslt

│ └── invoice-to-html.xslt

├── Dockerfile

└── docker-compose.yml


## 🔧 Servisler Nasıl Çalışır?

### SchematronValidatorService
Schematron doğrulaması üç aşamalı bir ISO işlemiyle gerçekleştirilir:
1.  **`iso_dsdl_include.xsl`**: `include` direktiflerini işler.
2.  **`iso_abstract_expand.xsl`**: Soyut kalıpları (abstract patterns) genişletir.
3.  **`iso_svrl_for_xslt2.xsl`**: Son olarak Schematron kurallarını, XML'i doğrulayacak bir XSLT dosyasına (SVRL) derler.

### XmlProcessingService
Yüklenen bir XML'i işlemek için tüm servisleri koordine eden orkestratör servistir.
```csharp
public async Task<XmlProcessingResult> ProcessAsync(string xmlPath, string xsdPath, string schematronXsltPath, string xsltPath)
{
    var xsdErrors = _xsdValidator.ValidateXmlWithXsd(xmlPath, xsdPath);
    var schematronResult = _schematronValidator.ValidateWithSchematron(xmlPath, schematronXsltPath);
    var transformedOutput = _xsltTransformer.TransformXmlWithXslt(xmlPath, xsltPath);

    return new XmlProcessingResult { ... };
}

# 💻 Docker container eklendi (Commit: fc8b6cfa, 2025-08-30)

# 🚀 Yeni Eklenenler 

- [2025-08-30] Dosya yapıları güncellendi.

# XVerify Project

Proje şu anda **uygulama kodu**, **testler** ve **dağıtım (Docker)** dosyaları arasında net bir ayrımla yapılandırılmıştır.

---

## 📂 Project Structure

XVerify/

├─ src/ # Application source code

│ ├─ WebApi/ # Web API project

│ ├─ ClassLib1/ # Example class library

│ └─ ClassLib2/ # Example class library

├─ test/ # Unit and integration tests

│ ├─ WebApi.Tests/

│ └─ ClassLib1.Tests/

├─ docker/ # Docker-related files

│ ├─ Dockerfile

│ ├─ docker-compose.yml

│ └─ .dockerignore

└─ XVerify.sln # Solution file


---

## 🚀 Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Docker](https://docs.docker.com/get-docker/)

### Build & Run (Local)

```bash
cd src/WebApi
dotnet build
dotnet run
