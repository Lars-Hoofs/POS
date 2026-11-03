SET NOCOUNT ON;
SET QUOTED_IDENTIFIER ON;
GO

USE PekingGarden;
GO

CREATE TABLE DiningTable (
    Id        int IDENTITY PRIMARY KEY,
    Name      nvarchar(10) NOT NULL UNIQUE,
    SortOrder int NOT NULL
);

CREATE TABLE Category (
    Id   int IDENTITY PRIMARY KEY,
    Name nvarchar(40) NOT NULL UNIQUE
);

CREATE TABLE Product (
    Id             int IDENTITY PRIMARY KEY,
    Number         nvarchar(5) NULL,
    Name           nvarchar(80) NOT NULL,
    CategoryId     int NOT NULL REFERENCES Category(Id),
    Kind           tinyint NOT NULL CHECK (Kind IN (1, 2)),
    Course         tinyint NULL CHECK (Course BETWEEN 1 AND 3),
    Price          decimal(6,2) NOT NULL CHECK (Price >= 0),
    VatRate        tinyint NOT NULL CHECK (VatRate IN (9, 21)),
    OffersSideDish bit NOT NULL DEFAULT 0,
    CHECK ((Kind = 1 AND Course IS NOT NULL) OR (Kind = 2 AND Course IS NULL AND OffersSideDish = 0))
);

CREATE TABLE SideDish (
    Id        int IDENTITY PRIMARY KEY,
    Name      nvarchar(40) NOT NULL UNIQUE,
    Surcharge decimal(6,2) NOT NULL CHECK (Surcharge >= 0),
    IsDefault bit NOT NULL DEFAULT 0
);

CREATE TABLE StandardWish (
    Id        int IDENTITY PRIMARY KEY,
    Kind      tinyint NOT NULL CHECK (Kind IN (1, 2)),
    Text      nvarchar(50) NOT NULL,
    SortOrder int NOT NULL,
    UNIQUE (Kind, Text)
);

CREATE TABLE TableOrder (
    Id            int IDENTITY PRIMARY KEY,
    DiningTableId int NOT NULL REFERENCES DiningTable(Id),
    OpenedAt      datetime2 NOT NULL,
    ClosedAt      datetime2 NULL
);

CREATE UNIQUE INDEX UQ_TableOrder_OneOpenOrderPerTable
    ON TableOrder(DiningTableId)
    WHERE ClosedAt IS NULL;

CREATE TABLE Ticket (
    Id           int IDENTITY PRIMARY KEY,
    TableOrderId int NOT NULL REFERENCES TableOrder(Id),
    CreatedAt    datetime2 NOT NULL,
    DoneAt       datetime2 NULL
);

CREATE INDEX IX_Ticket_Open ON Ticket(CreatedAt) WHERE DoneAt IS NULL;

CREATE TABLE OrderLine (
    Id                int IDENTITY PRIMARY KEY,
    TableOrderId      int NOT NULL REFERENCES TableOrder(Id),
    ProductId         int NOT NULL REFERENCES Product(Id),
    LineNumber        int NOT NULL,
    Quantity          int NOT NULL CHECK (Quantity BETWEEN 1 AND 99),
    UnitPrice         decimal(6,2) NOT NULL,
    VatRate           tinyint NOT NULL CHECK (VatRate IN (9, 21)),
    SideDishId        int NULL REFERENCES SideDish(Id),
    SideDishSurcharge decimal(6,2) NOT NULL DEFAULT 0,
    Status            tinyint NOT NULL CHECK (Status IN (1, 2)),
    TicketId          int NULL REFERENCES Ticket(Id),
    AddedAt           datetime2 NOT NULL,
    UNIQUE (TableOrderId, LineNumber)
);

CREATE TABLE OrderLineWish (
    OrderLineId    int NOT NULL REFERENCES OrderLine(Id),
    StandardWishId int NOT NULL REFERENCES StandardWish(Id),
    PRIMARY KEY (OrderLineId, StandardWishId)
);
GO

CREATE VIEW LinePrice AS
SELECT l.Id, l.TableOrderId, l.VatRate,
       l.Quantity * (l.UnitPrice + l.SideDishSurcharge) AS Amount
FROM OrderLine l;
GO
