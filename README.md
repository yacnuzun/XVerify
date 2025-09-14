# 🔍 XVerify

**Proje Tanımı**  
XVerify, faturalandırma sürecinin ve üyelerin süreç takibinin yönetilebildiği bir sistemdir.  
Üyelik sistemi sayesinde firmalar kendi üzerindeki faturaları takip edebilir ve XSD şeması ile bu faturaların çıktısını görebilir.

---

## 🛠 Mevcut Teknoloji ve Stack

Projede kullanılan ve kullanılacak teknolojiler:  
- **Backend:** .NET 8, Dapper, UnitOfWork, IoC (Autofac)  
- **Frontend:** Blazor  
- **Security:** JWT, Identity  
- **Diğer:** Docker Container, TestUnit  
- **Veri Yapıları:** XML, XSD  

---

## 🚀 Roadmap

### ✅ Completed
- **Milestone 1: Dapper + Unit of Work Yapısı**  
  - Repository ve Unit of Work implementasyonu tamamlandı.  
  - Database operasyonları için temel yapı oturtuldu.

### 🛠 In Progress
- **Milestone 2: IoC Katmanı (Autofac vb.)**  
  - Servislerin dependency injection ile yönetilebilir hale getirilmesi üzerinde çalışılıyor.  
  - Bu aşama tamamlandığında, servislerin loosely coupled yapısı garanti altına alınacak.

### 📅 Upcoming
- **Milestone 3: DTO Yapılanması + Fluent Validation**  
  - Request/response modelleri DTO’lar üzerinden yönetilecek.  
  - FluentValidation ile business kuralları ve veri doğrulama katmanı eklenecek.

- **Milestone 4: Identity Model (JWT + User & Claims)**  
  - Authentication & Authorization mekanizması oluşturulacak.  
  - Hashing & Security helper’ları entegre edilecek.  
  - Kullanıcı yönetimi (register, login, role, claim) hazırlanacak.

- **Milestone 5: Blazor Frontend**  
  - Backend API’leri ile haberleşecek login/register sayfaları geliştirilecek.  
  - UI tarafında temel layout ve component yapısı kurulacak.

- **Milestone 6: XSD/XML Yapılarının UI’da Kullanımı**  
  - Daha önce hazırlanmış XSD/XML yapıları frontend üzerinde işlenecek.  
  - Fatura görselleştirme ve kullanıcıya sunma özellikleri eklenecek.

---

## 📌 Milestone Timeline (Son Durum)

### Milestone 1: Dapper + UnitOfWork
- 2025-09-14 / 813d86e4 : Repository ve UnitOfWork implementasyonu tamamlandı, testler eklendi.
