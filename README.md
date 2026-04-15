# 📦 Sistema de Almoxarifado

Sistema de controle de estoque desenvolvido em C#, com foco em simular operações reais de um almoxarifado empresarial. O sistema realiza o gerenciamento completo de entrada, saída e devolução de materiais.

Este projeto foi desenvolvido como base de estudo, priorizando o entendimento dos fundamentos de desenvolvimento de sistemas, sem utilização de frameworks.

---

## 🚀 Funcionalidades

* Cadastro de funcionários
* Cadastro de fornecedores
* Registro de entrada de produtos (nota fiscal)
* Listagem de notas fiscais
* Criação de requisições (saída de materiais)
* Registro de devoluções
* Consulta de estoque

---

## 🧭 Fluxo do sistema

O sistema segue um fluxo semelhante ao de um ambiente real:

1. Cadastro de funcionários e fornecedores
2. Registro de entrada de materiais via nota fiscal
3. Consulta de notas fiscais cadastradas
4. Criação de requisições para saída de materiais
5. Registro de devoluções
6. Consulta e controle do estoque

---

## 🖥️ Interface

Sistema desenvolvido em console, com navegação por menus:

```
=== MENU PRINCIPAL ===
1 - Cadastros de Funcionario
2 - Cadastros de Fornecedor
3 - Entradas (Notas Fiscais)
4 - Listar Notas Fiscais
5 - Criar Requisições
6 - Devoluções
7 - Estoque
0 - Sair
```

---

## 🧠 Arquitetura do projeto

O sistema foi estruturado com separação de responsabilidades:

* **Models** → Representação das entidades do sistema
* **DAO (Data Access Object)** → Acesso e manipulação dos dados no banco
* **Menus** → Interação com o usuário via console
* **Data** → Conexão com o banco de dados

A implementação foi realizada sem uso de frameworks, visando reforçar os conceitos fundamentais.

---

## 📁 Estrutura do projeto

```
/Almoxerife
├── Data
├── DAO
├── Models
├── Menus
├── database
├── Program.cs
├── App.config
```

---

## 🗄️ Banco de dados

O projeto utiliza um banco de dados relacional MySQL, estruturado para simular um ambiente real de almoxarifado.

O script de criação do banco está disponível no diretório:

```
/database
```

Para executar o banco:

1. Acesse o MySQL
2. Execute o script SQL localizado na pasta `/database`
3. O banco será criado com todas as tabelas e relacionamentos

---

## 🛠️ Tecnologias utilizadas

* Linguagem: C#
* Banco de Dados: MySQL
* Paradigma: Programação orientada a objetos
* Acesso a dados: Implementação própria (DAO)

---

## 📊 Controle de estoque

Através da view `vw_estoque`, o sistema permite:

* Visualizar quantidade total em estoque
* Controlar quantidade empenhada
* Calcular automaticamente a quantidade disponível

---

## ⚙️ Como executar o projeto

```bash
# Clonar o repositório
git clone https://github.com/DevRochaAndre/Almoxarife_andre.git

# Acessar a pasta do projeto
cd projeto-almoxarife

### 1. Configurar o banco de dados

* Executar o script localizado em `/database`

### 2. Abrir o projeto

* Abrir o arquivo `.sln` no Visual Studio

### 3. Executar

* Pressionar F5

---

## 📚 Objetivo do projeto

* Praticar desenvolvimento em C#
* Compreender arquitetura em camadas sem frameworks
* Modelar banco de dados relacional
* Simular um sistema corporativo real

---

## 🔄 Próximos passos

Este projeto serve como base para futuras evoluções, como:

* Desenvolvimento de interface gráfica (desktop ou web)
* Criação de API
* Implementação de autenticação
* Evolução da arquitetura

---

## 👨‍💻 Autor

Desenvolvido por André Rocha
