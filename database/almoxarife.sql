-- Criar banco
CREATE DATABASE almoxarife;
USE almoxarife;

-- Tabela fornecedor
CREATE TABLE fornecedor (
  Id int NOT NULL AUTO_INCREMENT,
  RazaoSocial varchar(150) NOT NULL,
  Fantasia varchar(150) DEFAULT NULL,
  CNPJ varchar(18) NOT NULL,
  Contato varchar(60) DEFAULT NULL,
  Logradouro varchar(200) DEFAULT NULL,
  PRIMARY KEY (Id),
  UNIQUE KEY CNPJ (CNPJ)
);

-- Tabela material
CREATE TABLE material (
  Id int NOT NULL AUTO_INCREMENT,
  Codigo int NOT NULL,
  Descricao varchar(150) NOT NULL,
  Categoria varchar(80) DEFAULT NULL,
  Marca varchar(80) DEFAULT NULL,
  Modelo varchar(80) DEFAULT NULL,
  Validade date DEFAULT NULL,
  TipoProduto int NOT NULL DEFAULT '1',
  PRIMARY KEY (Id),
  UNIQUE KEY Codigo (Codigo)
);

-- Tabela estoque
CREATE TABLE estoque (
  Id int NOT NULL AUTO_INCREMENT,
  MaterialCodigo int NOT NULL,
  Descricao varchar(255) NOT NULL,
  QuantidadeEstoque int NOT NULL,
  QuantidadeEmpenhada int NOT NULL DEFAULT '0',
  PRIMARY KEY (Id),
  KEY fk_estoque_material_codigo (MaterialCodigo),
  CONSTRAINT fk_estoque_material_codigo FOREIGN KEY (MaterialCodigo) REFERENCES material (Codigo)
);

-- Tabela funcionario
CREATE TABLE funcionario (
  Id int NOT NULL AUTO_INCREMENT,
  Nome varchar(100) NOT NULL,
  CPF varchar(14) NOT NULL,
  Matricula int NOT NULL,
  Departamento varchar(60) DEFAULT NULL,
  Cargo varchar(60) DEFAULT NULL,
  PRIMARY KEY (Id),
  UNIQUE KEY CPF (CPF),
  UNIQUE KEY Matricula (Matricula)
);

-- Tabela notafiscal
CREATE TABLE notafiscal (
  Id int NOT NULL AUTO_INCREMENT,
  NumeroNF int NOT NULL,
  DataEntrada date NOT NULL,
  FornecedorId int NOT NULL,
  PRIMARY KEY (Id),
  KEY fk_nf_fornecedor (FornecedorId),
  CONSTRAINT fk_nf_fornecedor FOREIGN KEY (FornecedorId) REFERENCES fornecedor (Id)
);

-- Tabela requisicao
CREATE TABLE requisicao (
  Id int NOT NULL AUTO_INCREMENT,
  NumeroRequisicao int NOT NULL,
  DataSaida date NOT NULL,
  FuncionarioId int NOT NULL,
  PRIMARY KEY (Id),
  KEY fk_req_func (FuncionarioId),
  CONSTRAINT fk_req_func FOREIGN KEY (FuncionarioId) REFERENCES funcionario (Id)
);

-- Tabela devolucao
CREATE TABLE devolucao (
  Id int NOT NULL AUTO_INCREMENT,
  RequisicaoId int NOT NULL,
  DataDevolucao date NOT NULL,
  PRIMARY KEY (Id),
  KEY fk_dev_req (RequisicaoId),
  CONSTRAINT fk_dev_req FOREIGN KEY (RequisicaoId) REFERENCES requisicao (Id)
);

-- Tabela itemrequisicao
CREATE TABLE itemrequisicao (
  Id int NOT NULL AUTO_INCREMENT,
  RequisicaoId int NOT NULL,
  MaterialId int NOT NULL,
  Quantidade int NOT NULL,
  PRIMARY KEY (Id),
  KEY fk_itemreq_req (RequisicaoId),
  KEY fk_itemreq_material (MaterialId),
  CONSTRAINT fk_itemreq_material FOREIGN KEY (MaterialId) REFERENCES material (Id),
  CONSTRAINT fk_itemreq_req FOREIGN KEY (RequisicaoId) REFERENCES requisicao (Id)
);

-- Tabela itemdevolucao
CREATE TABLE itemdevolucao (
  Id INT NOT NULL AUTO_INCREMENT,
  DevolucaoId INT NOT NULL,
  MaterialCodigo INT NOT NULL,
  Quantidade INT NOT NULL,
  PRIMARY KEY (Id),
  KEY fk_itemdev_dev (DevolucaoId),
  KEY fk_itemdev_material (MaterialCodigo),
  CONSTRAINT fk_itemdev_dev FOREIGN KEY (DevolucaoId) REFERENCES devolucao (Id),
  CONSTRAINT fk_itemdev_material FOREIGN KEY (MaterialCodigo) REFERENCES material (Codigo)
);

-- Tabela itemnf
CREATE TABLE itemnf (
  Id int NOT NULL AUTO_INCREMENT,
  NotaFiscalId int NOT NULL,
  MaterialCodigo int NOT NULL,
  Quantidade int NOT NULL,
  Descricao varchar(255) DEFAULT NULL,
  DataEntrada date NOT NULL DEFAULT '2000-01-01',
  PRIMARY KEY (Id),
  KEY fk_item_material (MaterialCodigo),
  KEY fk_item_nota (NotaFiscalId),
  CONSTRAINT fk_itemnf_nf FOREIGN KEY (NotaFiscalId) REFERENCES notafiscal (Id)
);

-- View vw_estoque
CREATE OR REPLACE VIEW vw_estoque AS
SELECT 
    m.Codigo AS Codigo,
    m.Descricao AS Descricao,
    e.QuantidadeEstoque AS QuantidadeEstoque,
    e.QuantidadeEmpenhada AS QuantidadeEmpenhada,
    (e.QuantidadeEstoque - e.QuantidadeEmpenhada) AS QuantidadeDisponivel
FROM estoque e
JOIN material m ON m.Id = e.MaterialId;
