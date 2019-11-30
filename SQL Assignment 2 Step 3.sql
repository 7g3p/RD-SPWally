USE wallysworld;

INSERT INTO orders(`CustomerID`, `ProductID`, `BranchID`, `OrderDate`, `Status`, `ItemQuantity`) VALUES 
(4, 6, 1, '2019-07-20', 'PAID', 4),
(4, 5, 1, '2019-07-20', 'PAID', 1),
(4, 3, 1, '2019-07-20', 'PAID', 3),
(3, 2, 3, '2019-10-06', 'PAID', 10),
(2, 1, 2, '2019-11-02', 'PAID', 12),
(2, 3, 2, '2019-11-02', 'PAID', 3),
(2, 3, 2, '2019-11-02', 'RFND', 12),
(2, 3, 2, '2019-11-02', 'RFND', 3);

SELECT * FROM Orders;