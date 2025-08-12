# 🚀 CMS Microservices Project

Modern ve ölçeklenebilir bir **İçerik Yönetim Sistemi (CMS)** için geliştirilmiş mikroservis mimarisi. Bu proje, **CQRS (Command Query Responsibility Segregation)**, **DDD (Domain-Driven Design)** ve **Factory Pattern** gibi modern yazılım mimari prensiplerini kullanarak geliştirilmiştir.

## 🏗️ Mimari Özellikler

### 📚 DDD (Domain-Driven Design)

- **Domain Layer**: İş mantığı ve domain kuralları
- **Application Layer**: Use case'ler ve command/query handler'lar
- **Infrastructure Layer**: Veritabanı, external service entegrasyonları
- **Shared Kernel**: Ortak kullanılan domain modelleri ve utility'ler

### 🔄 CQRS (Command Query Responsibility Segregation)

- **Command Side**: Veri yazma işlemleri (Create, Update, Delete)
- **Query Side**: Veri okuma işlemleri (Read, List, Search)
- **Database Separation**: Okuma ve yazma için ayrı veritabanı context'leri

### 🏭 Factory Pattern

- **Context Factory**: Read ve Write context'lerini dinamik olarak seçme
- **Repository Factory**: Uygun repository tipini context'e göre oluşturma
- **Dependency Injection**: IoC container ile loose coupling

### 🏭 Ardalist Spesfication Patern
- **Özellik ve Önemi**: Specification patern ile her bir proje içerisindeki sorguları kendine özel bir hale getirerek mapper kullanmadan select ile sadece ihtiyacımız olan veriyi çekmemizi sağlamış olduk. 
- **Özellik ve Önemi**: Ardalist'in özelliği sayesinde her bir model için Repository oluşturmamıza gerek kalmadı. bu da karmaşıklığı azaltarak daha temiz bir görünüm sağlamış oldu.

## 🐳 Docker ile Kurulum

### Gereksinimler

- Docker Desktop
- Docker Compose
- .NET 9.0 SDK (Kodu local'de Test için)

### Hızlı Başlangıç

1. **Projeyi klonlayın:**

```bash
git clone https://github.com/yourusername/microservice.cms.git
cd microservice.cms
```

2. **Docker Compose ile servisleri başlatmak için aşağıdaki kodu proje Root klasörü içerisinde çalıştırabilirsiniz.**

```bash
docker-compose up -d
```

3. **Servis durumunu kontrol edin:**

```bash
docker-compose ps
```

### 🗄️ Veritabanı Yapılandırması

Proje, **CQRS prensibi** gereği okuma ve yazma işlemleri için ayrı veritabanı context'leri kullanır: Bir çok CQRS projesi gerçekten Write ve Read olarak db'ler bölünmediği için aslında tam olarak CQRS'i kullanmamaktadır. Burada CQRS'i tam anlamı ile kullandık.

- **Not**: Kaynak tüketimi çok olduğundan dolayı appsettings.json'da write ve read contextleri aynı db'ye bakmaktadır.
#### Account Service Postgresql Portları

- **Write Context**: `accountdb` (Port: 5433)
- **Read Context**: `accountdb` (Port: 5433)
- **API Port**: 5000

#### Content Service Postgresql Portları

- **Write Context**: `contentdb` (Port: 5434)
- **Read Context**: `contentdb` (Port: 5434)
- **API Port**: 5001

### 🔧 Postgresql Environment Variables

```yaml
# Account Service
POSTGRES_USER: postgres
POSTGRES_PASSWORD: postgres
POSTGRES_DB: accountdb
ASPNETCORE_ENVIRONMENT: Production

# Content Service
POSTGRES_USER: postgres
POSTGRES_PASSWORD: postgres
POSTGRES_DB: contentdb
ASPNETCORE_ENVIRONMENT: Production
ASPNETCORE_URLS: http://+:8080
```

## 🏛️ Proje Yapısı

```
src/
├── Account/                          # 👤 Account Mikroservisi
│   ├── Microservice.Account.API/    # REST API endpoints Web Ara yüzü
│   ├── Microservice.Account.Application/  # CQRS handlers
│   ├── Microservice.Account.Domain/       # Domain entities & business logic
│   ├── Microservice.Account.EFCore/       # Data access layer
│   ├── Microservice.Account.SharedKernel/ # Shared models
│   └── Microservice.Account.Test/         # Unit tests
├── Content/                          # 📝 Content Mikroservisi
│   ├── Microservice.Content.API/    # REST API endpoints Web Ara yüzü
│   ├── Microservice.Content.Application/  # CQRS handlers
│   ├── Microservice.Content.Domain/       # Domain entities & business logic
│   ├── Microservice.Content.EFCore/       # Data access layer
│   ├── Microservice.Content.SharedKernel/ # Shared models
│   └── Microservice.Content.Tests/        # Unit tests
└── Shared/                           # 🔗 Ortak Bileşenler
    ├── Dockerfiles/                  # Container yapılandırmaları
    └── docker-compose.yml           # Orchestration
```

##  API Endpoints

### Account Service (Port: 5000)

- `GET /api/accounts` - Tüm hesapları listele
- `POST /api/accounts` - Yeni hesap oluştur
- `GET /api/accounts/{id}` - Hesap detayı
- `PUT /api/accounts/{id}` - Hesap güncelle
- `DELETE /api/accounts/{id}` - Hesap sil

### Content Service (Port: 5001)

- `GET /api/contents` - Tüm içerikleri listele
- `POST /api/contents` - Yeni içerik ekle
- `GET /api/contents/{id}` - İçerik detayı
- `PUT /api/contents/{id}` - İçerik güncelle
- `DELETE /api/contents/{id}` - İçerik sil

## 📖 Swagger Dokümantasyonu

Her mikroservis için detaylı API dokümantasyonu:

- **Account Service**: http://localhost:5000/swagger
- **Content Service**: http://localhost:5001/swagger

### Gereksinimler

- .NET 9.0 SDK
- Visual Studio 2022 / VS Code
- Docker Desktop
- PostgreSQL (opsiyonel, Docker kullanıyorsanız gerekmez)

2. **Projeyi çalıştır:**

```bash
# Content Service
cd src/Content/Microservice.Content.API
dotnet run

# Account Service
cd src/Account/Microservice.Account.API
dotnet run
```

## 🏗️ Mimari Detayları

### CQRS Implementation

```csharp
// Command Side, Controller'den AddcontentCommand'ı çağırarak sonucunda bool bir değer döneceğini gösterir.
public class AddContentCommand : IRequest<ApiResponse<bool>> 
public class AddContentCommandHandler : IRequestHandler<AddContentCommand, ApiResponse<bool>>

// Query Side (Read)
public class GetContentByIdQuery : IRequest<ApiResponse<ContentDto>>
public class GetContentByIdQueryHandler : IRequestHandler<GetContentByIdQuery, ApiResponse<ContentDto>>
```

### Repository Pattern

```csharp
// Write Repository
public interface IWriteRepository<T> : IRepositoryBase<T>
{
    IUnitOfWork UnitOfWork { get; }
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    Task<int> UpdateAsync(T entity, CancellationToken cancellationToken = default);
}

// Read Repository
public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IEntityBase
{
    Task<T> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
}
```

### Factory Pattern

```csharp
// Context Factory 
public class ContextFactory : IContextFactory
{
    public DbContext CreateContext(ContextType contextType)
    {
        return contextType switch
        {
            ContextType.Read => _readContext,
            ContextType.Write => _writeContext,
            _ => throw new ArgumentException("Invalid context type")
        };
    }
}
```

## 🚀 Deployment

### Production

```bash
# Docker image build
docker build -t cms-account:latest ./src/Account
docker build -t cms-content:latest ./src/Content

# Docker Compose production
docker-compose -f docker-compose.prod.yml up -d
```

Bu proje aşağıdaki teknolojiler ve kütüphaneler kullanılarak geliştirilmiştir:

- [.NET 9.0](https://dotnet.microsoft.com/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [MediatR](https://github.com/jbogard/MediatR)
- [FluentValidation](https://fluentvalidation.net/)
- [Ardalist](https://specification.ardalis.com/)
- [xUnit](https://xunit.net/)
- [Docker](https://www.docker.com/)
