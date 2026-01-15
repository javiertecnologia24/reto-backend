
INSERT INTO Usuario(Email, [Password], Rol) values('javier@hotmail.com', '123', 'Admin')
INSERT INTO Usuario(Email, [Password], Rol) values('jose@hotmail.com', '123', 'Supervidor')
INSERT INTO Pedido(NumeroPedido, Cliente, Fecha, Total, Estado) VALUES ('PED0001', 'Jose Martines',GETDATE(), 1500, 'R')
INSERT INTO Pedido(NumeroPedido, Cliente, Fecha, Total, Estado) VALUES ('PED0004', 'Maria Bendezu',GETDATE(), 2400, 'R')
INSERT INTO Pedido(NumeroPedido, Cliente, Fecha, Total, Estado) VALUES ('PED0005', 'Juan Vargas',GETDATE(), 500, 'E')

