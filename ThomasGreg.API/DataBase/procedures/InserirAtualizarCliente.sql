
-- Procedimento armazenado: InserirAtualizarCliente
CREATE PROCEDURE [dbo].[InserirAtualizarCliente]
    @Id INT,
    @Nome VARCHAR(255),
    @Email VARCHAR(100),
    @Logotipo text,
    @RowCount INT OUTPUT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Clientes WHERE Id = @Id)
    BEGIN
        -- Atualizar se já existir
        UPDATE Clientes
        SET
            Nome = @Nome,
            Email = @Email,
            Logotipo = @Logotipo
        WHERE 
            Id = @Id;
        SET @RowCount = @@ROWCOUNT; -- Número de linhas modificadas
    END
    ELSE
    BEGIN
        -- Inserir novo se não existir
        INSERT INTO Clientes (Nome, Email, Logotipo)
        VALUES (@Nome, @Email, @Logotipo);
        SET @RowCount = @@ROWCOUNT; -- Número de linhas inseridas
    END
END;
