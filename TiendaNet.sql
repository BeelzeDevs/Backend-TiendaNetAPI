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
VALUES ('Unidades'),('Gramos'),('Mililitros');