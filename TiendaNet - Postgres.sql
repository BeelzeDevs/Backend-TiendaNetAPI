
-- Tabla de Roles
CREATE TABLE Roles (
    Id SERIAL PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL UNIQUE
);

-- Tabla de Usuarios
CREATE TABLE Usuarios (
    Id SERIAL PRIMARY KEY,
    NombreUsuario VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    Direccion VARCHAR(100) NOT NULL,
    Municipio VARCHAR(100) NOT NULL,
    CodPostal INTEGER NOT NULL,
    Contraseña VARCHAR(255) NOT NULL,
    RolId INTEGER NOT NULL,
    EstadoUsuario BOOLEAN NOT NULL DEFAULT TRUE,
    FechaCreacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (RolId) REFERENCES Roles(Id)
);

-- Tabla de Unidades de Medida
CREATE TABLE UnidadesMedida (
    Id SERIAL PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL UNIQUE
);

-- Tabla de Ingredientes
CREATE TABLE Ingredientes (
    Id SERIAL PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Costo DECIMAL(10,2) NOT NULL,
    Stock DECIMAL(10,2) NOT NULL,
    DescripcionIngrediente TEXT,
    EstadoIngrediente BOOLEAN NOT NULL DEFAULT TRUE,
    UnidadMedidaId INTEGER NOT NULL,
    FOREIGN KEY (UnidadMedidaId) REFERENCES UnidadesMedida(Id)
);

-- Tabla de Recetas
CREATE TABLE Recetas (
    Id SERIAL PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    DescripcionReceta TEXT,
    ImgUrl VARCHAR(255) NOT NULL DEFAULT 'img/',
    PrecioReceta DECIMAL(10,2),
    EstadoReceta BOOLEAN NOT NULL DEFAULT TRUE
);

-- Tabla Ingrediente x Receta
CREATE TABLE IngredientexReceta (
    IngredienteId INTEGER NOT NULL,
    RecetaId INTEGER NOT NULL,
    Cantidad DECIMAL(10,2),
    PRIMARY KEY (IngredienteId, RecetaId),
    FOREIGN KEY (IngredienteId) REFERENCES Ingredientes(Id),
    FOREIGN KEY (RecetaId) REFERENCES Recetas(Id)
);

-- Tabla Menu
CREATE TABLE Menu (
    Id SERIAL PRIMARY KEY,
    TituloMenu VARCHAR(100),
    EstadoMenu BOOLEAN NOT NULL DEFAULT TRUE
);

-- Tabla de Recetas x Menu
CREATE TABLE RecetasXMenu (
    RecetaId INTEGER NOT NULL,
    MenuId INTEGER NOT NULL,
    PRIMARY KEY (RecetaId, MenuId),
    FOREIGN KEY (RecetaId) REFERENCES Recetas(Id),
    FOREIGN KEY (MenuId) REFERENCES Menu(Id)
);

-- Insertar Roles
INSERT INTO Roles (Nombre)
VALUES ('Admin'), ('Cliente');

-- Insertar Usuarios
INSERT INTO Usuarios (NombreUsuario, Contraseña, Email, Direccion, Municipio, CodPostal, RolId)
VALUES 
('admin', '1234', 'admin@gmail.com', 'Invento 123', 'Martinez', 1640, 1),
('cliente', '1234', 'user@gmail.com', 'Invento 234', 'Martinez', 1640, 2);

-- Insertar Unidades de Medida
INSERT INTO UnidadesMedida (Nombre)
VALUES ('Unidades'), ('Gramos'), ('Mililitros'), ('Kg');

-- Insertar Ingredientes
INSERT INTO Ingredientes (Nombre, Costo, Stock, DescripcionIngrediente, UnidadMedidaId)
VALUES 
('Carne picada', 0.6, 10.5, 'Carne vacuna molida, ideal para preparar hamburguesas caseras.', 4),
('Pan de hamburguesa', 0.4, 50, 'Pan suave y esponjoso, ideal para armar hamburguesas.', 1),
('Lechuga', 0.1, 500, 'Hojas frescas de lechuga, lavadas y listas para consumir.', 2),
('Tomate', 0.3, 20, 'Tomate fresco cortado en rodajas para acompañar las hamburguesas.', 4),
('Queso', 0.5, 10, 'Queso en fetas, ideal para derretir sobre la carne.', 4),
('Cebolla', 0.2, 400, 'Cebolla cortada en rodajas finas, puede usarse cruda o caramelizada.', 2),
('Ketchup', 0.1, 1000, 'Salsa de tomate dulce, clásica para acompañar hamburguesas y papas.', 3),
('Mostaza', 0.1, 1000, 'Salsa de mostaza con sabor intenso, ideal para hamburguesas.', 3),
('Papas', 0.2, 50, 'Papas frescas ideales para freír y servir como acompañamiento.', 4),
('PrePizza', 0.2, 50, 'Pre Pizza frescas ideales para hornear y servir', 4);

-- Insertar Recetas
INSERT INTO Recetas (Nombre, DescripcionReceta, ImgUrl, PrecioReceta)
VALUES
('Hamburguesa completa', 'Hamburguesa casera con carne, queso, vegetales y salsas.', 'img/hamburguesa.jpg', 3.50),
('Papas fritas', 'Papas frescas fritas acompañadas con salsas.', 'img/papas.jpg', 1.80),
('Pizza individual', 'Pizza individual con queso, tomate y cebolla.', 'img/pizza.jpg', 4.20);

-- Ingredientes x Recetas
INSERT INTO IngredientexReceta (IngredienteId, RecetaId, Cantidad)
VALUES
(1, 1, 0.200),  -- Carne picada (kg)
(2, 1, 1),      -- Pan de hamburguesa (unidad)
(5, 1, 0.050),  -- Queso (kg)
(3, 1, 20),     -- Lechuga (g)
(4, 1, 0.050),  -- Tomate (kg)
(6, 1, 10),     -- Cebolla (g)
(7, 1, 10),     -- Ketchup (ml)
(8, 1, 5),      -- Mostaza (ml)
(9, 2, 0.250),  -- Papas (kg)
(7, 2, 10),     -- Ketchup (ml)
(8, 2, 5),      -- Mostaza (ml)
(10, 3, 1),     -- PrePizza (unidad)
(5, 3, 0.100),  -- Queso (kg)
(4, 3, 0.050),  -- Tomate (kg)
(6, 3, 10);     -- Cebolla (g)

-- Insertar Menú
INSERT INTO Menu (TituloMenu)
VALUES ('Menú Clásico');

-- Recetas x Menú
INSERT INTO RecetasXMenu (RecetaId, MenuId)
VALUES 
(1, 1),
(2, 1),
(3, 1);
