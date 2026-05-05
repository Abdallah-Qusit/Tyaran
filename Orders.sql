CREATE TABLE Orders (
    OrderID INT PRIMARY KEY IDENTITY,

    OrderStatus NVARCHAR(50),

    Subtotal DECIMAL(10,2),
    CreatedAt DATETIME DEFAULT GETDATE(),
    DeliveredAt DATETIME,

    TotalAmount DECIMAL(10,2),
    DeliveryFee DECIMAL(10,2),
    Tax DECIMAL(10,2),
    Discount DECIMAL(10,2),

    RestaurantID INT,
    UserID INT,
    DriverID INT,
    AddressID INT,

    PaymentID INT,

    FOREIGN KEY (RestaurantID) REFERENCES Restaurant(RestaurantID),
    FOREIGN KEY (UserID) REFERENCES [User](UserID),
    FOREIGN KEY (DriverID) REFERENCES DeliveryMan(DriverID),
    FOREIGN KEY (AddressID) REFERENCES Addresses(AddressID),
    FOREIGN KEY (PaymentID) REFERENCES Payment(PaymentID)
);
GO

CREATE PROCEDURE Order_Create
    @RestaurantID INT,
    @UserID INT,
    @AddressID INT
AS
BEGIN
    INSERT INTO Orders (RestaurantID, UserID, AddressID, OrderStatus)
    VALUES (@RestaurantID, @UserID, @AddressID, 'Pending');

    SELECT SCOPE_IDENTITY() AS OrderID;
END;
GO

CREATE PROCEDURE Order_Read
    @OrderID INT
AS
BEGIN
    SELECT * FROM Orders WHERE OrderID = @OrderID;
END;
GO

CREATE PROCEDURE Order_UpdateStatus
    @OrderID INT,
    @OrderStatus NVARCHAR(50)
AS
BEGIN
    UPDATE Orders
    SET OrderStatus = @OrderStatus
    WHERE OrderID = @OrderID;
END;
GO

CREATE PROCEDURE Order_Delete
    @OrderID INT
AS
BEGIN
    DELETE FROM Orders WHERE OrderID = @OrderID;
END;
GO