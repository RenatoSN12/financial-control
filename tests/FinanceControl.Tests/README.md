# FinanceControl - Testes

## 📋 Arquitetura de Testes

Este projeto segue uma arquitetura de testes bem estruturada, alinhada com a **Clean Architecture** utilizada no projeto principal.

## 🏗️ Estrutura

```
FinanceControl.Tests/
├── Domain/
│   ├── Entities/          # Testes de entidades de domínio
│   └── ValueObjects/      # Testes de value objects
├── Application/
│   └── UseCases/         # Testes de handlers e casos de uso
└── README.md
```

## 🔧 Tecnologias Utilizadas

- **NUnit**: Framework de testes
- **FluentAssertions**: Assertions fluentes e legíveis
- **NSubstitute**: Framework de mocking
- **.NET 10.0**: Target framework

## 📚 Tipos de Testes

### 1. Testes de Value Objects
- **Localização**: `Domain/ValueObjects/`
- **Objetivo**: Validar regras de negócio, validações e comportamentos de value objects
- **Exemplos**: `EmailTests.cs`, `MoneyTests.cs`
- **Características**:
  - Sem dependências externas
  - Testes de validação
  - Testes de igualdade
  - Testes de operadores (quando aplicável)

### 2. Testes de Entidades
- **Localização**: `Domain/Entities/`
- **Objetivo**: Validar criação, comportamento e regras de negócio das entidades
- **Exemplos**: `UserTests.cs`, `CategoryTests.cs`, `TransactionTests.cs`
- **Características**:
  - Validação de construção
  - Validação de propriedades
  - Testes de relacionamentos
  - Validação de coleções

### 3. Testes de Handlers (Application)
- **Localização**: `Application/UseCases/`
- **Objetivo**: Validar a lógica de orquestração dos casos de uso
- **Exemplos**: `GetAllCategoriesByUserHandlerTests.cs`
- **Características**:
  - Uso de mocks (NSubstitute)
  - Validação de chamadas a dependências
  - Validação de retorno (Result pattern)
  - Testes de fluxo completo

## 🎯 Padrões de Nomenclatura

### Convenções de Nome de Testes

```csharp
[Test]
public void MethodName_Scenario_ExpectedBehavior()
{
    // Arrange
    // Act
    // Assert
}
```

**Exemplos:**
- `Create_WithValidEmail_ShouldReturnEmailInstance()`
- `Constructor_WithNegativeAmount_ShouldThrowArgumentException()`
- `HandleAsync_WithValidUserId_ShouldReturnCategories()`

### Padrão AAA (Arrange-Act-Assert)

Todos os testes seguem o padrão AAA:

```csharp
[Test]
public void Example_Test()
{
    // Arrange - Preparar dados e dependências
    var input = "test@example.com";
    
    // Act - Executar a ação sendo testada
    var result = Email.Create(input);
    
    // Assert - Verificar o resultado
    result.Address.Should().Be(input);
}
```

## 🚀 Executando os Testes

### Via CLI
```bash
dotnet test
```

### Via IDE (Visual Studio / Rider)
- Use o Test Explorer
- Execute todos os testes ou individualmente
- Visualize cobertura de código

### Com filtros
```bash
# Executar apenas testes de Value Objects
dotnet test --filter "FullyQualifiedName~ValueObjects"

# Executar apenas testes de Email
dotnet test --filter "FullyQualifiedName~EmailTests"

# Executar apenas testes de Application
dotnet test --filter "FullyQualifiedName~Application"
```

## 📊 Cobertura de Código

Para gerar relatório de cobertura:

```bash
dotnet test --collect:"XPlat Code Coverage"
```

## ✅ Checklist de Testes Implementados

### Domain - Value Objects
- ✅ Email
  - ✅ Criação com email válido
  - ✅ Normalização (lowercase, trim)
  - ✅ Validações (null, vazio, formato inválido)
  - ✅ Limite de caracteres
  - ✅ Igualdade
- ✅ Money
  - ✅ Criação com valor válido
  - ✅ Validação de valor negativo
  - ✅ Operador de adição
  - ✅ Operador de subtração
  - ✅ Igualdade

### Domain - Entities
- ✅ User
  - ✅ Criação com dados válidos
  - ✅ Validação de email
  - ✅ Inicialização de coleções
  - ✅ Estado inicial (IsActive)
- ✅ Category
  - ✅ Criação com dados válidos
  - ✅ Inicialização de coleções
- ✅ Transaction
  - ✅ Criação com dados válidos
  - ✅ Tipos de transação (Income/Expense)
  - ✅ Datas (passado/futuro)

### Application - Use Cases
- ✅ GetAllCategoriesByUserHandler
  - ✅ Retorno com dados válidos
  - ✅ Retorno sem categorias
  - ✅ Verificação de chamadas a dependências
  - ✅ Passagem de CancellationToken

## 🔜 Próximos Passos

### Testes a Implementar
1. **Domain - Entities**
   - Card
   - CardTransaction
   - Invoice
   - InvoiceItem

2. **Application - Use Cases**
   - Commands (Create, Update, Delete)
   - Outras Queries

3. **Testes de Integração**
   - Repositórios
   - Queries
   - Database Context

4. **Testes de API**
   - Controllers
   - Middleware
   - Filters

## 💡 Boas Práticas

1. **Isolamento**: Cada teste deve ser independente
2. **Nomenclatura Clara**: Use nomes descritivos que explicam o cenário
3. **Um Assert por Conceito**: Foque em validar um comportamento por teste
4. **Mocks Mínimos**: Use mocks apenas quando necessário
5. **Dados Realistas**: Use dados que fazem sentido no domínio
6. **Fast, Isolated, Repeatable, Self-checking, Timely (FIRST)**

## 📖 Referências

- [NUnit Documentation](https://docs.nunit.org/)
- [FluentAssertions Documentation](https://fluentassertions.com/)
- [NSubstitute Documentation](https://nsubstitute.github.io/NSubstitute/)
