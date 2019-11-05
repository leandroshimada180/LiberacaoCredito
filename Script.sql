CREATE TABLE Cliente (
    IdCliente INT NOT NULL PRIMARY KEY,
	Nome VARCHAR(200),
	UF VARCHAR(2),
	Celular VARCHAR(20)
);

CREATE TABLE Financiamento(
	IdFinanciamento INT NOT NULL PRIMARY KEY,
	IdCliente INT FOREIGN KEY REFERENCES Cliente(IdCliente),
	TipoFinanciamento VARCHAR (2),
	ValorTotal FLOAT,
	DataVencimento DATETIME
);

CREATE TABLE Parcelas(
	IdParcela INT NOT NULL PRIMARY KEY,
	IdFinanciamento INT FOREIGN KEY REFERENCES Financiamento(IdFinanciamento),
	NumeroParcela INT,
	ValorParcela FLOAT,
	DataVencimento DATETIME,
	DataPagamento DATETIME
);
