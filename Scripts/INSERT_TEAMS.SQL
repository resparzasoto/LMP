DELETE FROM LMP.dbo.Teams;
GO
INSERT INTO LMP.dbo.Teams(Id, Name, Color, Logo)
SELECT 1, 'Águilas de Mexicali', '#FF0000', BulkColumn
FROM OPENROWSET (BULK 'D:\Repositorios\LMP\Imagenes\aguilas.png', SINGLE_BLOB) AS IMAGE

INSERT INTO LMP.dbo.Teams(Id, Name, Color, Logo)
SELECT 2, 'Algodoneros de Guasave', '#0000FF', BulkColumn
FROM OPENROWSET (BULK 'D:\Repositorios\LMP\Imagenes\algodoneros.png', SINGLE_BLOB) AS IMAGE

INSERT INTO LMP.dbo.Teams(Id, Name, Color, Logo)
SELECT 3, 'Cañeros de Los Mochis', '#FF006400', BulkColumn
FROM OPENROWSET (BULK 'D:\Repositorios\LMP\Imagenes\cañeros.png', SINGLE_BLOB) AS IMAGE

INSERT INTO LMP.dbo.Teams(Id, Name, Color, Logo)
SELECT 4, 'Charros de Jalisco', '#FF00BFFF', BulkColumn
FROM OPENROWSET (BULK 'D:\Repositorios\LMP\Imagenes\charros.png', SINGLE_BLOB) AS IMAGE

INSERT INTO LMP.dbo.Teams(Id, Name, Color, Logo)
SELECT 5, 'Mayos de Navojoa', '#FFB8860B', BulkColumn
FROM OPENROWSET (BULK 'D:\Repositorios\LMP\Imagenes\mayos.png', SINGLE_BLOB) AS IMAGE

INSERT INTO LMP.dbo.Teams(Id, Name, Color, Logo)
SELECT 6, 'Naranjeros de Hermosillo', '#FFA500', BulkColumn
FROM OPENROWSET (BULK 'D:\Repositorios\LMP\Imagenes\naranjeros.png', SINGLE_BLOB) AS IMAGE

INSERT INTO LMP.dbo.Teams(Id, Name, Color, Logo)
SELECT 7, 'Sultanes de Monterrey', '#FF1E90FF', BulkColumn
FROM OPENROWSET (BULK 'D:\Repositorios\LMP\Imagenes\sultanes.png', SINGLE_BLOB) AS IMAGE

INSERT INTO LMP.dbo.Teams(Id, Name, Color, Logo)
SELECT 8, 'Tomateros de Culiacán', '#FFCD5C5C', BulkColumn
FROM OPENROWSET (BULK 'D:\Repositorios\LMP\Imagenes\tomateros.png', SINGLE_BLOB) AS IMAGE

INSERT INTO LMP.dbo.Teams(Id, Name, Color, Logo)
SELECT 9, 'Venados de Mazatlán', '#FFDB7093', BulkColumn
FROM OPENROWSET (BULK 'D:\Repositorios\LMP\Imagenes\venados.png', SINGLE_BLOB) AS IMAGE

INSERT INTO LMP.dbo.Teams(Id, Name, Color, Logo)
SELECT 10, 'Yaquis de Ciudad Obregón', '#000000', BulkColumn
FROM OPENROWSET (BULK 'D:\Repositorios\LMP\Imagenes\yaquis.png', SINGLE_BLOB) AS IMAGE
