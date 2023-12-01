
-- Procedimento armazenado: ExcluirCliente
CREATE PROCEDURE [dbo].[ExcluirCliente]
    @Id INT,
    @RowCount INT OUTPUT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Clientes WHERE Id = @Id)
    BEGIN
        DELETE FROM Clientes WHERE Id = @Id;
        SET @RowCount = @@ROWCOUNT; -- Número de linhas excluídas
    END
    ELSE
    BEGIN
        SET @RowCount = 0;
    END
END;

