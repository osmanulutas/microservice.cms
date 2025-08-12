# Test Documentation for Microservice.CMS

Bu dokümantasyon, Account ve Content projeleri için yazılan testleri açıklamaktadır.

## 📋 Test Projeleri

### 1. Account Projesi (`src/Account/Microservice.Account.Test/`)
### 2. Content Projesi (`src/Content/Microservice.Content.Tests/`)

## 🧪 Test Kategorileri

### Domain Entity Tests
- **AccountTests.cs**: Account domain entity'nin tüm metodları ve edge case'leri için testler
- **ContentTests.cs**: Content domain entity'nin tüm metodları ve edge case'leri için testler

### Command Handler Tests
- **AddAccountCommandHandlerTests.cs**: Account ekleme işlemi için testler
- **UpdateAccountCommandHandlerTests.cs**: Account güncelleme işlemi için testler
- **AddContentCommandHandlerTests.cs**: Content ekleme işlemi için testler
- **UpdateContentCommandHandlerTests.cs**: Content güncelleme işlemi için testler

### Validator Tests
- **AddAccountCommandValidatorTests.cs**: Account ekleme validasyon kuralları için testler

## 🚀 Testleri Çalıştırma

### PowerShell Script ile (Önerilen)
```powershell
.\run-tests.ps1
```

### Manuel olarak
```bash
# Account tests
cd src/Account/Microservice.Account.Test
dotnet test

# Content tests
cd src/Content/Microservice.Content.Tests
dotnet test
```

### Visual Studio/Rider
- Test Explorer'da testleri görüntüleyin
- "Run All Tests" butonuna tıklayın

## 📊 Test Coverage

### Account Domain Tests
- ✅ Constructor validation
- ✅ Property updates
- ✅ Business logic methods
- ✅ Edge cases (null values, empty strings)
- ✅ Timestamp management

### Account Command Handler Tests
- ✅ Success scenarios
- ✅ Error handling
- ✅ Repository interaction
- ✅ Method call order verification
- ✅ Data validation

### Account Validator Tests
- ✅ Required field validation
- ✅ Length validation
- ✅ Email format validation
- ✅ Date validation
- ✅ Multiple error scenarios

### Content Domain Tests
- ✅ Constructor validation
- ✅ Content updates
- ✅ Publishing/unpublishing
- ✅ Business logic methods
- ✅ Timestamp management

### Content Command Handler Tests
- ✅ Success scenarios
- ✅ Error handling
- ✅ Repository interaction
- ✅ Method call order verification
- ✅ Data validation

## 🔧 Test Dependencies

### Account Test Project
```xml
<PackageReference Include="Moq" Version="4.20.70" />
<PackageReference Include="FluentValidation.AspNetCore" Version="11.3.0" />
<PackageReference Include="xunit" Version="2.9.2" />
<PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.12.0" />
```

### Content Test Project
```xml
<PackageReference Include="Moq" Version="4.20.70" />
<PackageReference Include="xunit" Version="2.9.2" />
<PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.12.0" />
```

## 📝 Test Naming Convention

Test metodları şu formatta adlandırılmıştır:
```
[MethodName]_[Condition]_[ExpectedResult]
```

Örnekler:
- `Constructor_WithValidParameters_ShouldCreateAccount`
- `Handle_WhenAccountNotFound_ShouldReturnNotFoundResponse`
- `UpdateAccount_WithEmptyEmail_ShouldThrowArgumentException`

## 🎯 Test Best Practices

1. **Arrange-Act-Assert Pattern**: Her test 3 bölümden oluşur
2. **Descriptive Names**: Test metodları ne test edildiğini açıkça belirtir
3. **Single Responsibility**: Her test tek bir senaryoyu test eder
4. **Mocking**: External dependencies mock'lanır
5. **Edge Cases**: Null values, empty strings, invalid data test edilir
6. **Error Scenarios**: Exception handling test edilir

## 🔍 Test Scenarios

### Account Tests
- Valid account creation
- Null parameter handling
- Email validation
- Date validation
- Phone number validation
- Update operations
- Delete operations
- Timestamp management

### Content Tests
- Valid content creation
- Content updates
- Publishing workflow
- Unpublishing workflow
- Author ID management
- Category and tag updates
- Timestamp management

## 📈 Test Metrics

- **Total Tests**: 50+ test metodu
- **Coverage Areas**: Domain, Application, Validation layers
- **Test Types**: Unit tests, Integration tests (via mocking)
- **Framework**: xUnit
- **Mocking**: Moq
- **Validation**: FluentValidation

## 🚨 Troubleshooting

### Common Issues
1. **Package Restore**: `dotnet restore` komutunu çalıştırın
2. **Build Errors**: `dotnet build` ile projeyi derleyin
3. **Test Discovery**: Test projelerinin doğru referanslara sahip olduğundan emin olun

### Test Failures
- Test output'unda detaylı hata mesajlarını kontrol edin
- Mock setup'larının doğru olduğundan emin olun
- Test data'nın beklenen formatta olduğunu kontrol edin

## 📚 Additional Resources

- [xUnit Documentation](https://xunit.net/)
- [Moq Documentation](https://github.com/moq/moq4)
- [FluentValidation Documentation](https://docs.fluentvalidation.net/)
- [.NET Testing Best Practices](https://docs.microsoft.com/en-us/dotnet/core/testing/)

## 🤝 Contributing

Yeni testler eklerken:
1. Mevcut naming convention'ı takip edin
2. Test coverage'ı artırın
3. Edge case'leri test edin
4. Documentation'ı güncelleyin

---

**Not**: Bu testler, projenin kalitesini artırmak ve regression'ları önlemek için tasarlanmıştır. Düzenli olarak çalıştırılmalı ve yeni özellikler eklendikçe güncellenmelidir.
