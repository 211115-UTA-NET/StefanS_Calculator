
--task 1
CREATE TABLE _Pokemon 
(
    _ID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    _Name VARCHAR(255) NOT NULL,
    _Height INT NOT NULL,
    _Weight INT NOT NULL
);

--task 2
CREATE TABLE _Type
(
    _ID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    _Name VARCHAR(255) UNIQUE NOT NULL,   
);

--task 3
CREATE TABLE _PokemonType
(
    _ID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    _PokemonID INT NOT NULL,
    _TypeID INT NOT NULL
);

ALTER TABLE _PokemonType ADD FOREIGN KEY (_PokemonID) REFERENCES _Pokemon(_ID);
ALTER TABLE _PokemonType ADD FOREIGN KEY (_TypeID) REFERENCES _Type(_ID);

--taks 4
INSERT _Pokemon 
    (_Name, _Height, _Weight)
VALUES
    ('Bulbasaur', 7, 70),
    ('Ditto', 3, 40);

--task 5
INSERT _Type
    (_Name)
VALUES
    ('Normal'),
    ('Grass');

--task 6
INSERT _PokemonType
    (_PokemonID,_TypeID)
VALUES
    (1,2),
    (2,1);

--task 7
Select 
    _Pokemon._ID,_Pokemon._Name,_Type._ID,_Type._Name
FROM _PokemonType
JOIN _Type ON _PokemonType._TypeID = _Type._ID
JOIN _Pokemon ON _PokemonType._PokemonID = _Pokemon._ID;