CREATE TABLE Coupons (
    CouponId INT PRIMARY KEY IDENTITY,
    DiscountType NVARCHAR(50),
    Code NVARCHAR(50) UNIQUE,
    MinimumOrder DECIMAL(10,2),
    DiscountValue DECIMAL(10,2),
    StartDate DATETIME,
    EndDate DATETIME,
    IsActive BIT
);
GO
CREATE PROCEDURE Coupon_Create
    @DiscountType NVARCHAR(50),
    @Code NVARCHAR(50),
    @MinimumOrder DECIMAL(10,2),
    @DiscountValue DECIMAL(10,2),
    @StartDate DATETIME,
    @EndDate DATETIME,
    @IsActive BIT
AS
BEGIN
    INSERT INTO Coupons
    (DiscountType, Code, MinimumOrder, DiscountValue, StartDate, EndDate, IsActive)
    VALUES
    (@DiscountType, @Code, @MinimumOrder, @DiscountValue, @StartDate, @EndDate, @IsActive);
    SELECT SCOPE_IDENTITY() AS CouponId;
END;
GO
CREATE PROCEDURE Coupon_Read
    @CouponId INT
AS
BEGIN
    SELECT *
    FROM Coupons
    WHERE CouponId = @CouponId;
END;
GO
CREATE PROCEDURE Coupon_ReadByCode
    @Code NVARCHAR(50)
AS
BEGIN
    SELECT *
    FROM Coupons
    WHERE Code = @Code;
END;
GO
CREATE PROCEDURE Coupon_GetActive
AS
BEGIN
    SELECT *
    FROM Coupons
    WHERE IsActive = 1
      AND GETDATE() BETWEEN StartDate AND EndDate;
END;
GO
CREATE PROCEDURE Coupon_Update
    @CouponId INT,
    @DiscountType NVARCHAR(50),
    @Code NVARCHAR(50),
    @MinimumOrder DECIMAL(10,2),
    @DiscountValue DECIMAL(10,2),
    @StartDate DATETIME,
    @EndDate DATETIME,
    @IsActive BIT
AS
BEGIN
    UPDATE Coupons
    SET DiscountType = @DiscountType,
        Code = @Code,
        MinimumOrder = @MinimumOrder,
        DiscountValue = @DiscountValue,
        StartDate = @StartDate,
        EndDate = @EndDate,
        IsActive = @IsActive
    WHERE CouponId = @CouponId;
END;
GO
CREATE PROCEDURE Coupon_Delete
    @CouponId INT
AS
BEGIN
    DELETE FROM Coupons
    WHERE CouponId = @CouponId;
END;
GO