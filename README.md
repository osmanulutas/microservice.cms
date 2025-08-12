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

## 🐳 Docker ile Kurulum

### Gereksinimler

- Docker Desktop
- Docker Compose
- .NET 9.0 SDK (geliştirme için)

### Hızlı Başlangıç

1. **Projeyi klonlayın:**

```bash
git clone https://github.com/yourusername/microservice.cms.git
cd microservice.cms
```

2. **Docker Compose ile servisleri başlatın:**

```bash
docker-compose up -d
```

3. **Servis durumunu kontrol edin:**

```bash
docker-compose ps
```

### 🗄️ Veritabanı Yapılandırması

Proje, **CQRS prensibi** gereği okuma ve yazma işlemleri için ayrı veritabanı context'leri kullanır:

#### Account Service

- **Write Context**: `accountdb` (Port: 5433)
- **Read Context**: `accountdb` (Port: 5433)
- **API Port**: 5000

#### Content Service

- **Write Context**: `contentdb` (Port: 5434)
- **Read Context**: `contentdb` (Port: 5434)
- **API Port**: 5001

### 🔧 Environment Variables

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
│   ├── Microservice.Account.API/    # REST API endpoints
│   ├── Microservice.Account.Application/  # CQRS handlers
│   ├── Microservice.Account.Domain/       # Domain entities & business logic
│   ├── Microservice.Account.EFCore/       # Data access layer
│   ├── Microservice.Account.SharedKernel/ # Shared models
│   └── Microservice.Account.Test/         # Unit tests
├── Content/                          # 📝 Content Mikroservisi
│   ├── Microservice.Content.API/    # REST API endpoints
│   ├── Microservice.Content.Application/  # CQRS handlers
│   ├── Microservice.Content.Domain/       # Domain entities & business logic
│   ├── Microservice.Content.EFCore/       # Data access layer
│   ├── Microservice.Content.SharedKernel/ # Shared models
│   └── Microservice.Content.Tests/        # Unit tests
└── Shared/                           # 🔗 Ortak Bileşenler
    ├── Dockerfiles/                  # Container yapılandırmaları
    └── docker-compose.yml           # Orchestration
```

## 🚀 API Endpoints

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

### Lokal Geliştirme

1. **Veritabanı migration'ları:**

```bash
# Content Service
dotnet ef database update --project src/Content/Microservice.Content.EFCore --startup-project src/Content/Microservice.Content.API

# Account Service
dotnet ef database update --project src/Account/Microservice.Account.EFCore --startup-project src/Account/Microservice.Account.API
```

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
// Command Side (Write)
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

## 📊 Monitoring & Health Checks

- **Health Checks**: Her servis için `/health` endpoint'i
- **Logging**: Structured logging ile JSON format
- **Metrics**: Performance monitoring için hazır endpoint'ler

## 🔒 Güvenlik

- **Input Validation**: FluentValidation ile giriş doğrulama
- **Exception Handling**: Global exception handler
- **API Security**: CORS yapılandırması
- **Data Protection**: Hassas veri şifreleme

## 🚀 Deployment

### Production

```bash
# Docker image build
docker build -t cms-account:latest ./src/Account
docker build -t cms-content:latest ./src/Content

# Docker Compose production
docker-compose -f docker-compose.prod.yml up -d
```

### Kubernetes

```bash
# Helm chart deployment
helm install cms ./helm/cms
```

## 🤝 Katkıda Bulunma

1. Fork yapın
2. Feature branch oluşturun (`git checkout -b feature/amazing-feature`)
3. Commit yapın (`git commit -m 'Add amazing feature'`)
4. Push yapın (`git push origin feature/amazing-feature`)
5. Pull Request oluşturun

## 📝 Lisans

Bu proje MIT lisansı altında lisanslanmıştır. Detaylar için [LICENSE](LICENSE) dosyasına bakın.

## 📞 İletişim

- **Proje Linki**: [https://github.com/yourusername/microservice.cms](https://github.com/yourusername/microservice.cms)
- **Issues**: [GitHub Issues](https://github.com/yourusername/microservice.cms/issues)

## 🙏 Teşekkürler

Bu proje aşağıdaki teknolojiler ve kütüphaneler kullanılarak geliştirilmiştir:

- [.NET 9.0](https://dotnet.microsoft.com/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [MediatR](https://github.com/jbogard/MediatR)
- [FluentValidation](https://fluentvalidation.net/)
- [xUnit](https://xunit.net/)
- [Docker](https://www.docker.com/)

---

⭐ Bu projeyi beğendiyseniz yıldız vermeyi unutmayın!
