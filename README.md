# Desafio

A empresa está expandindo seus negócios e uma das missões do time de tecnologia é criar uma funcionalidade de cadastro de Clientes.
O Front deve ser desenvolvido em Asp.net com MVC, Razor, Javascript e Html(se necessário).
O Back deve ser uma API REST em C#.

## Tecnologias Utilizadas

- ASP.NET Core 7.0 (Front-end e Back-end)
- HTML, Razor, JavaScript (Front-end)
- SQL Server 2016
- Entity Framework

## Estrutura e Arquitetura do Projeto
A estrutura do projeto foi utilizado a arquitetura CQRS, com a seguinte organização:
### Web
- Models: Representa os modelos de dados usados nas operações da API.
- Views: Se estiver usando MVC, pode conter as visualizações associadas às operações.
- Controllers: Lida com as requisições HTTP, chamando os handlers correspondentes.
- Interfaces: Contratos dos serviços (Services).
- Services: Regras de negócios
- Program: Configuração inicial da aplicação.
### API 
- Controllers: Lida com as requisições HTTP, utilizando o Mediator e Queries
- DataBase: script do banco de dados
- DependencyMap: é o repositório das dependências que serão utilizados na aplicação
- Program: Configuração inicial da aplicação.
### Application
Separação das responsabilidades em pastas:
- Contracts: Os contratos dos serviços
- Core
- Crypto: Tipos de criptografias a serem utilizadas no sistema
- Handlers: Manipulação das consultas e comandos
- Libraries: Bibliotecas utilizadas, como cookie
- Mapper: Mapeamento das classes
- Queries: Operações de leitura e alteração
- Responses: Retorno das informações
- Services: Regras de negócios
### Core
- Events: Encapsulamento do resultado
### Domain
- Interfaces:  Os contratos das classes e repositórios
- Models: Entidades de domínio
### Infra
- Context: Contexto do banco de dados
- DataAccess: Faz a migração do script do banco para o Sql Server
- Repositories: Implementações de repositório

## Configurações
### Front-end

1. Dependências utilizadas:
   - Newtonsoft.Json
2. Configuração da conexão com a API no arquivo de configuração.
   
// appsettings.json

  {

    "ApiSettings": {  
     "BaseUrl": "https://localhost:7292/api/"   
     }
 
  }

### Back-end
1. Dependências utilizadas:
  - AutoMapper:
    
    Finalidade: Mapeamento de objetos. Facilita a transferência de dados entre diferentes tipos de objetos em uma aplicação.
  - Flunt.Br:

    Finalidade: Biblioteca para validações fluentes em objetos de domínio. Ajuda a melhorar a expressividade e a legibilidade do código em cenários de validação.
  - MediatR:

    Finalidade: Implementação do padrão Mediator para C#. Ajuda a separar as lógicas de negócios e a simplificar a comunicação entre componentes.
  - MediatR.Extensions.Microsoft.DependencyInjection:

    Finalidade: Integração do MediatR com o contêiner de injeção de dependência do ASP.NET Core.
  - Microsoft.AspNetCore.Authentication.JwtBearer:

     Finalidade: Provedor de autenticação JWT (JSON Web Token) para aplicativos ASP.NET Core.
  - Microsoft.AspNetCore.Authorization:

     Finalidade: Fornece infraestrutura para autorização em aplicativos ASP.NET Core. Ajuda a controlar o acesso a recursos da aplicação com base em regras definidas.
  - Microsoft.EntityFrameworkCore.Design:

    Finalidade: Fornece suporte ao design-time para o Entity Framework Core, permitindo a execução de comandos EF Core no console do Visual Studio.
  - Microsoft.EntityFrameworkCore.Proxies:
   
    Finalidade: Suporte a proxies para o Entity Framework Core, permitindo a criação de classes proxy para ativar a carga tardia (lazy loading) de propriedades de navegação.
  - Newtonsoft.Json:

     Finalidade: Biblioteca para serialização e desserialização de objetos JSON.
  - PartialResponse.AspNetCore.Mvc.Formatters.Json:

     Finalidade: Suporte para consulta de campos parciais em respostas JSON.
  - StringCipher:

     Finalidade: Biblioteca para criptografia de strings. Ajuda a proteger informações sensíveis por meio da criptografia.
  - Swashbuckle.AspNetCore:

     Finalidade: Integração do Swagger com aplicativos ASP.NET Core para geração automática de documentação interativa da API. Facilita o entendimento e o consumo da API por desenvolvedores.
   
2. Configuração da conexão com o banco de dados.
   
// appsettings.json

{

     "ConnectionStrings": {  
        "DefaultConnection": "Server=(local);Database=ThomasGreg;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"    
     }
  
}

## Modelagem de Dados
### Clientes
- Id (int, PK)
- Nome (varchar, 255)
- Email (varchar, 100)
- Logotipo (varchar,45)

### Logradouros
- Id (int, PK)
- ClienteId (int, FK)
- NomeRua (varchar, 100)
- Numero (varchar, 10)
- Bairro (varchar, 50)
- Cidade (varchar, 50)
- Estado (char, 2)
- Cep (char, 10)

### RefreshTokens
- Token (varchar,50, PK)
- UsuarioId (int, FK)
- ExpirationDate (datetime)

### Usuarios
- Id (int, PK)
- Nome (varchar, 100)
- Email (varchar, 50)
- Senha  (varchar, 60)
- Ativo (bit)

## Endpoint da API
[https://localhost:7292](https://localhost:7292/)

## Endpoint da Web
[https://localhost:7249](https://localhost:7249/)

## Autenticação
A API exige autenticação Bearer, sendo necessário incluir o token JWT no cabeçalho das requisições protegidas.

A aplicação também faz uso do Refresh Token, utilizado quando o JWT Token expira, o refreshToken é enviado para um endpoint de autorização para obter um novo JWT Token, sem a necessidade de o usuário fazer o login novamente.

Todas as validações e informações da aplicação Web é feito pela API, onde tem o total controle dos dados cadastrados.

Para poder utilizar o sistema, será necessário efetuar o cadastro do usuário, através da tela de login.
