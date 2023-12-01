
-- Procedimento armazenado: InserirAtualizarLogradouro
CREATE PROCEDURE [dbo].[InserirAtualizarLogradouro]
    @Id INT,
    @ClienteId INT,
    @NomeRua VARCHAR(100),
    @Numero NVARCHAR(10),
    @Bairro VARCHAR(50),
    @Cidade VARCHAR(50),
    @Estado CHAR(2),
    @Cep char(9),
    @RowCount INT OUTPUT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Logradouros WHERE Id = @Id)
    BEGIN
        -- Atualizar o logradouro se já existir
        UPDATE Logradouros
        SET
            NomeRua = @NomeRua,
            Numero = @Numero,
            Bairro = @Bairro,
            Cidade = @Cidade,
            Estado = @Estado,
            Cep = @Cep
        WHERE 
            Id = @Id;
        SET @RowCount = @@ROWCOUNT; -- Número de linhas modificadas
    END
    ELSE
    BEGIN
        -- Inserir novo logradouro se não existir
        INSERT INTO Logradouros (ClienteId, NomeRua, Numero, Bairro, Cidade, Estado, Cep)
        VALUES (@ClienteId, @NomeRua, @Numero, @Bairro, @Cidade, @Estado, @Cep);
        SET @RowCount = @@ROWCOUNT; -- Número de linhas inseridas
    END
END;