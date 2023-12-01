USE [master];
USE [ThomasGreg];

-- Criação da tabela Clientes
CREATE TABLE [dbo].[Clientes](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Nome] [varchar](255) NOT NULL,
    [Email] [varchar](100) NOT NULL,
    [Logotipo] [text] NOT NULL,
    CONSTRAINT [PK_Clientes] PRIMARY KEY CLUSTERED ([Id] ASC)
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];

-- Criação da tabela Logradouros
CREATE TABLE [dbo].[Logradouros](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [ClienteId] [int] NOT NULL,
    [NomeRua] [varchar](100) NOT NULL,
    [Numero] [varchar](10) NOT NULL,
    [Bairro] [varchar](50) NOT NULL,
    [Cidade] [varchar](50) NOT NULL,
    [Estado] [char](2) NOT NULL,
    [Cep] [char](10) NOT NULL,
    CONSTRAINT [PK_Logradouros] PRIMARY KEY CLUSTERED ([Id] ASC)
) ON [PRIMARY];

-- Criação da tabela RefreshTokens
CREATE TABLE [dbo].[RefreshTokens](
    [Token] [varchar](50) NOT NULL,
    [UsuarioId] [int] NOT NULL,
    [ExpirationDate] [datetime] NOT NULL,
    CONSTRAINT [PK_RefreshToken] PRIMARY KEY CLUSTERED ([Token] ASC)
) ON [PRIMARY];

-- Criação da tabela Usuarios
CREATE TABLE [dbo].[Usuarios](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Nome] [varchar](100) NOT NULL,
    [Email] [varchar](50) NOT NULL,
    [Senha] [varchar](60) NOT NULL,
    [Ativo] [bit] NOT NULL,
    CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED ([Id] ASC)
) ON [PRIMARY];

-- Adicionar chave estrangeira à tabela Logradouros
ALTER TABLE [dbo].[Logradouros] WITH CHECK ADD CONSTRAINT [FK_Logradouros_Clientes] FOREIGN KEY([ClienteId])
REFERENCES [dbo].[Clientes] ([Id]) ON DELETE CASCADE;
ALTER TABLE [dbo].[Logradouros] CHECK CONSTRAINT [FK_Logradouros_Clientes];

