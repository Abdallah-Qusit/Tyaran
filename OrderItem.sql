CREATE TABLE OrderItem (
    OrderItemID INT PRIMARY KEY IDENTITY,

    OrderID INT,

    Quantity INT,
    UnitPrice DECIMAL(10,2),
    TotalPrice DECIMAL(10,2),

    ItemID INT,

    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
    FOREIGN KEY (ItemID) REFERENCES MenuItem(ItemID)
);
GO

CREATE PROCEDURE OrderItem_Create
    @OrderID INT,
    @ItemID INT,
    @Quantity INT,
    @UnitPrice DECIMAL(10,2)
AS
BEGIN
    INSERT INTO OrderItem (OrderID, Quantity, UnitPrice, TotalPrice, ItemID)
    VALUES (@OrderID, @Quantity, @UnitPrice, @Quantity * @UnitPrice, @ItemID);

    SELECT SCOPE_IDENTITY() AS OrderItemID;
END;
GO

CREATE PROCEDURE OrderItem_Read
    @OrderItemID INT
AS
BEGIN
    SELECT * FROM OrderItem WHERE OrderItemID = @OrderItemID;
END;
GO

CREATE PROCEDURE OrderItem_Delete
    @OrderItemID INT
AS
BEGIN
    DELETE FROM OrderItem WHERE OrderItemID = @OrderItemID;
END;
GO