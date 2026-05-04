CREATE TABLE Payment (
    PaymentId INT PRIMARY KEY IDENTITY,
    PaymentMethod NVARCHAR(50),
    Status NVARCHAR(50),
    TransactionId NVARCHAR(100),
    PaidAt DATETIME,
    Amount DECIMAL(10,2)
);
GO
CREATE PROCEDURE Payment_Create
    @PaymentMethod NVARCHAR(50),   
    @Status NVARCHAR(50),         
    @TransactionId NVARCHAR(100) = NULL,
    @PaidAt DATETIME = NULL,
    @Amount DECIMAL(10,2)
AS
BEGIN
    INSERT INTO Payment
    (PaymentMethod, Status, TransactionId, PaidAt, Amount)
    VALUES
    (@PaymentMethod, @Status, @TransactionId, @PaidAt, @Amount);
    SELECT SCOPE_IDENTITY() AS PaymentId;
END;
GO
CREATE PROCEDURE Payment_Read
    @PaymentId INT
AS
BEGIN
    SELECT *
    FROM Payment
    WHERE PaymentId = @PaymentId;
END;
GO
CREATE PROCEDURE Payment_Update
    @PaymentId INT,
    @PaymentMethod NVARCHAR(50),
    @Status NVARCHAR(50),
    @TransactionId NVARCHAR(100),
    @PaidAt DATETIME,
    @Amount DECIMAL(10,2)
AS
BEGIN
    UPDATE Payment
    SET PaymentMethod = @PaymentMethod,
        Status = @Status,
        TransactionId = @TransactionId,
        PaidAt = @PaidAt,
        Amount = @Amount
    WHERE PaymentId = @PaymentId;
END;
GO
CREATE PROCEDURE Payment_Delete
    @PaymentId INT
AS
BEGIN
    DELETE FROM Payment
    WHERE PaymentId = @PaymentId;
END;
GO