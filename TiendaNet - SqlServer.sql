CREATE DATABASE TiendaNet;
GO

USE TiendaNet;
GO

-- Tabla de Roles
CREATE TABLE Roles (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(50) NOT NULL UNIQUE
);

-- Tabla de Usuarios
CREATE TABLE Usuarios (
    Id INT PRIMARY KEY IDENTITY(1,1),
    NombreUsuario NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
	Direccion NVARCHAR(100) NOT NULL,
	Municipio NVARCHAR(100) NOT NULL,
	CodPostal INT NOT NULL,
    Contraseña NVARCHAR(255) NOT NULL,
    RolId INT NOT NULL,
	EstadoUsuario Bit NOT NULL DEFAULT 1,
    FechaCreacion DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (RolId) REFERENCES Roles(Id)
);
-- Tabla de Unidades de Medida
CREATE TABLE UnidadesMedida (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(50) NOT NULL UNIQUE
);
-- Tabla de Ingredientes
CREATE TABLE Ingredientes (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Costo DECIMAL(10,2) NOT NULL,
    Stock DECIMAL(10,2) NOT NULL,
    DescripcionIngrediente TEXT,
    EstadoIngrediente BIT NOT NULL DEFAULT 1,
    UnidadMedidaId INT NOT NULL,
    FOREIGN KEY (UnidadMedidaId) REFERENCES UnidadesMedida(Id)
);
-- Tabla de Recetas
CREATE TABLE Recetas(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Nombre NVARCHAR(100) NOT NULL,
	DescripcionReceta TEXT,
	ImgUrl NVARCHAR(255) NOT NULL Default 'img/',
	PrecioReceta DECIMAL(10,2),
	EstadoReceta BIT NOT NULL DEFAULT 1
);
-- Tabla Ingrediente x Receta
CREATE TABLE IngredientexReceta(
	IngredienteId INT NOT NULL,
	RecetaId INT NOT NULL,
	Cantidad DECIMAL(10,2), -- Gramos,mililitros.
	
	Primary Key (IngredienteId,RecetaId),
	Foreign Key(IngredienteId) References Ingredientes(Id),
	Foreign key (RecetaId) References Recetas(Id)
);

-- Tabla Menu
CREATE TABLE Menu(
	Id INT PRIMARY KEY Identity(1,1),
	TituloMenu NVARCHAR(100),
	EstadoMenu BIT NOT NULL DEFAULT 1
);

-- Tabla de RecetasxMenu
CREATE TABLE RecetasXMenu(
	RecetaId INT NOT NULL,
	MenuId INT NOT NULL,

	Primary Key (RecetaId,MenuId),
	Foreign Key (RecetaId) References Recetas(Id),
	Foreign Key (MenuId) References Menu(Id)
);


Use TiendaNet
go

-- Insertar roles
INSERT INTO Roles (Nombre) 
select ('Admin') UNION
select ('Cliente');

-- Insertar usuarios
INSERT INTO Usuarios (NombreUsuario,Contraseña, Email, Direccion, Municipio, CodPostal, RolId)
SELECT 'admin', '1234','admin@gmail.com','Invento 123','Martinez',1640,1 UNION
SELECT 'cliente','1234','user@gmail.com','Invento 234','Martinez',1640,2;

--Insertar Medidas
INSERT INTO UnidadesMedida(Nombre)
VALUES ('Unidades'),('Gramos'),('Mililitros'),('Kg');

-- Carne picada: en Kg
-- Tomate: en gramos
-- Pan de hamburguesa: en Unidades
-- Lechuga: en gramos
-- Queso: en Kg
-- Cebolla: en gramos
-- Ketchup: en mililitros
-- Mostaza: en mililitros
-- Papas: en Kg
INSERT INTO Ingredientes (Nombre, Costo, Stock, DescripcionIngrediente, UnidadMedidaId)
VALUES 
('Carne picada', 0.6, 10.5, 'Carne vacuna molida, ideal para preparar hamburguesas caseras.', 4),
('Pan de hamburguesa', 0.4, 50, 'Pan suave y esponjoso, ideal para armar hamburguesas.', 1),
('Lechuga', 0.1, 500, 'Hojas frescas de lechuga, lavadas y listas para consumir.', 2 ),
('Tomate', 0.3, 20, 'Tomate fresco cortado en rodajas para acompañar las hamburguesas.', 4 ),
('Queso', 0.5, 10, 'Queso en fetas, ideal para derretir sobre la carne.', 4 ),
('Cebolla', 0.2, 400, 'Cebolla cortada en rodajas finas, puede usarse cruda o caramelizada.', 2 ),
('Ketchup', 0.1, 1000, 'Salsa de tomate dulce, clásica para acompañar hamburguesas y papas.', 3 ),
('Mostaza', 0.1, 1000, 'Salsa de mostaza con sabor intenso, ideal para hamburguesas.', 3 ),
('Papas', 0.2, 50, 'Papas frescas ideales para freír y servir como acompañamiento.', 4),
('PrePizza', 0.2, 50, 'Pre Pizza frescas ideales para honear y servir', 4);

-- Recetas
INSERT INTO Recetas (Nombre, DescripcionReceta, ImgUrl, PrecioReceta)
VALUES
('Hamburguesa completa', 'Hamburguesa casera con carne, queso, vegetales y salsas.', 'img/hamburguesa.jpg', 3.50),
('Papas fritas', 'Papas frescas fritas acompañadas con salsas.', 'img/papas.jpg', 1.80),
('Pizza individual', 'Pizza individual con queso, tomate y cebolla.', 'img/pizza.jpg', 4.20);


-- Hamburguesa completa
INSERT INTO IngredientexReceta (IngredienteId, RecetaId, Cantidad)
VALUES 
(1, 1, 0.200),  -- Carne picada (kg)
(2, 1, 1),      -- Pan de hamburguesa (unidad)
(5, 1, 0.050),  -- Queso (kg)
(3, 1, 20),     -- Lechuga (g)
(4, 1, 0.050),  -- Tomate (kg)
(6, 1, 10),     -- Cebolla (g)
(7, 1, 10),     -- Ketchup (ml)
(8, 1, 5);      -- Mostaza (ml)

-- Papas fritas
INSERT INTO IngredientexReceta (IngredienteId, RecetaId, Cantidad)
VALUES 
(9, 2, 0.250),  -- Papas (kg)
(7, 2, 10),     -- Ketchup (ml)
(8, 2, 5);      -- Mostaza (ml)

-- Pizza individual
INSERT INTO IngredientexReceta (IngredienteId, RecetaId, Cantidad)
VALUES 
(10, 3, 1),      -- PrePizza (unidad)
(5, 3, 0.100),   -- Queso (kg)
(4, 3, 0.050),   -- Tomate (kg)
(6, 3, 10);      -- Cebolla (g)

INSERT INTO Menu (TituloMenu)
VALUES ('Menú Clásico');

-- Recetas en Menù Clàsico
INSERT INTO RecetasXMenu (RecetaId, MenuId)
VALUES 
(1, 1), -- Hamburguesa
(2, 1), -- Papas fritas
(3, 1); -- Pizza