CREATE TABLE DeliveryMan (
    DriverID INT PRIMARY KEY IDENTITY,

    Location NVARCHAR(255),
    VehiclePlate NVARCHAR(50),

    IsOnline BIT,
    IsAvailable BIT,

    CreatedAt DATETIME DEFAULT GETDATE(),

    VehicleType NVARCHAR(50)
);
GO

CREATE PROCEDURE DeliveryMan_Create
    @Location NVARCHAR(255),
    @VehiclePlate NVARCHAR(50),
    @VehicleType NVARCHAR(50)
AS
BEGIN
    INSERT INTO DeliveryMan (Location, VehiclePlate, VehicleType)
    VALUES (@Location, @VehiclePlate, @VehicleType);

    SELECT SCOPE_IDENTITY() AS DriverID;
END;
GO

CREATE PROCEDURE DeliveryMan_Read
    @DriverID INT
AS
BEGIN
    SELECT * FROM DeliveryMan WHERE DriverID = @DriverID;
END;
GO

CREATE PROCEDURE DeliveryMan_Update
    @DriverID INT,
    @Location NVARCHAR(255),
    @IsOnline BIT,
    @IsAvailable BIT
AS
BEGIN
    UPDATE DeliveryMan
    SET Location = @Location,
        IsOnline = @IsOnline,
        IsAvailable = @IsAvailable
    WHERE DriverID = @DriverID;
END;
GO

CREATE PROCEDURE DeliveryMan_Delete
    @DriverID INT
AS
BEGIN
    DELETE FROM DeliveryMan WHERE DriverID = @DriverID;
END;
GO