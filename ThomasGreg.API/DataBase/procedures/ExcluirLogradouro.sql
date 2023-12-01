-- Procedimento armazenado: ExcluirLogradouro
CREATE PROCEDURE [dbo].[ExcluirLogradouro]
    @Id INT,
    @ClienteId INT,
    @RowCount INT OUTPUT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Logradouros WHERE Id = @Id AND ClienteId = @ClienteId)
    BEGIN
        DELETE FROM Logradouros WHERE Id = @Id AND ClienteId = @ClienteId;
        SET @RowCount = @@ROWCOUNT; -- Número de linhas excluídas
    END
    ELSE
    BEGIN
        SET @RowCount = 0;
    END
END;
