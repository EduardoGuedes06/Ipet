-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
-- -----------------------------------------------------
-- Schema ipet
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema ipet
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `ipet` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci ;
USE `ipet` ;

-- -----------------------------------------------------
-- Table `ipet`.`__efmigrationshistory`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ipet`.`__efmigrationshistory` (
  `MigrationId` VARCHAR(150) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `ProductVersion` VARCHAR(32) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  PRIMARY KEY (`MigrationId`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `ipet`.`aspnetroles`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ipet`.`aspnetroles` (
  `Id` VARCHAR(255) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `Name` VARCHAR(256) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL,
  `NormalizedName` VARCHAR(256) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL,
  `ConcurrencyStamp` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE INDEX `RoleNameIndex` (`NormalizedName` ASC) VISIBLE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `ipet`.`aspnetroleclaims`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ipet`.`aspnetroleclaims` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `RoleId` VARCHAR(255) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `ClaimType` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL,
  `ClaimValue` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL,
  PRIMARY KEY (`Id`),
  INDEX `IX_AspNetRoleClaims_RoleId` (`RoleId` ASC) VISIBLE,
  CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId`
    FOREIGN KEY (`RoleId`)
    REFERENCES `ipet`.`aspnetroles` (`Id`)
    ON DELETE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `ipet`.`aspnetusers`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ipet`.`aspnetusers` (
  `Id` VARCHAR(255) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `Nome` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL,
  `Imagem` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL,
  `Cep` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL,
  `Numero` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL,
  `Password` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL,
  `ConfirmPassord` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL,
  `Documento` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL,
  `UserName` VARCHAR(256) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL,
  `NormalizedUserName` VARCHAR(256) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL,
  `Email` VARCHAR(256) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL,
  `NormalizedEmail` VARCHAR(256) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL,
  `EmailConfirmed` TINYINT(1) NOT NULL,
  `PasswordHash` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL,
  `SecurityStamp` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL,
  `ConcurrencyStamp` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL,
  `PhoneNumber` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL,
  `PhoneNumberConfirmed` TINYINT(1) NOT NULL,
  `TwoFactorEnabled` TINYINT(1) NOT NULL,
  `LockoutEnd` DATETIME(6) NULL DEFAULT NULL,
  `LockoutEnabled` TINYINT(1) NOT NULL,
  `AccessFailedCount` INT NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE INDEX `UserNameIndex` (`NormalizedUserName` ASC) VISIBLE,
  INDEX `EmailIndex` (`NormalizedEmail` ASC) VISIBLE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `ipet`.`aspnetuserclaims`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ipet`.`aspnetuserclaims` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `UserId` VARCHAR(255) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `ClaimType` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL,
  `ClaimValue` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL,
  PRIMARY KEY (`Id`),
  INDEX `IX_AspNetUserClaims_UserId` (`UserId` ASC) VISIBLE,
  CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId`
    FOREIGN KEY (`UserId`)
    REFERENCES `ipet`.`aspnetusers` (`Id`)
    ON DELETE CASCADE)
ENGINE = InnoDB
AUTO_INCREMENT = 3
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `ipet`.`aspnetuserlogins`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ipet`.`aspnetuserlogins` (
  `LoginProvider` VARCHAR(128) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `ProviderKey` VARCHAR(128) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `ProviderDisplayName` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL,
  `UserId` VARCHAR(255) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  PRIMARY KEY (`LoginProvider`, `ProviderKey`),
  INDEX `IX_AspNetUserLogins_UserId` (`UserId` ASC) VISIBLE,
  CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId`
    FOREIGN KEY (`UserId`)
    REFERENCES `ipet`.`aspnetusers` (`Id`)
    ON DELETE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `ipet`.`aspnetuserroles`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ipet`.`aspnetuserroles` (
  `UserId` VARCHAR(255) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `RoleId` VARCHAR(255) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  PRIMARY KEY (`UserId`, `RoleId`),
  INDEX `IX_AspNetUserRoles_RoleId` (`RoleId` ASC) VISIBLE,
  CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId`
    FOREIGN KEY (`RoleId`)
    REFERENCES `ipet`.`aspnetroles` (`Id`)
    ON DELETE CASCADE,
  CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId`
    FOREIGN KEY (`UserId`)
    REFERENCES `ipet`.`aspnetusers` (`Id`)
    ON DELETE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `ipet`.`aspnetusertokens`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ipet`.`aspnetusertokens` (
  `UserId` VARCHAR(255) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `LoginProvider` VARCHAR(128) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `Name` VARCHAR(128) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `Value` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL,
  PRIMARY KEY (`UserId`, `LoginProvider`, `Name`),
  CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId`
    FOREIGN KEY (`UserId`)
    REFERENCES `ipet`.`aspnetusers` (`Id`)
    ON DELETE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `ipet`.`produtos`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ipet`.`produtos` (
  `Id` CHAR(36) CHARACTER SET 'ascii' NOT NULL,
  `EstabelecimentoId` CHAR(36) CHARACTER SET 'ascii' NOT NULL,
  `Nome` VARCHAR(200) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `Estabelecimento` VARCHAR(100) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `Descricao` VARCHAR(1000) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `Imagem` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `Valor` DECIMAL(65,30) NOT NULL,
  `Ativo` TINYINT(1) NOT NULL,
  `DataCadastro` DATETIME(6) NOT NULL,
  PRIMARY KEY (`Id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `ipet`.`avaliação`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ipet`.`avaliação` (
  `Id` CHAR(36) CHARACTER SET 'ascii' NOT NULL,
  `ProdutoId` CHAR(36) CHARACTER SET 'ascii' NOT NULL,
  `Estrelas` DOUBLE NOT NULL,
  `DataCadastro` DATETIME(6) NOT NULL,
  `UserId` CHAR(36) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`),
  INDEX `IX_Avaliação_ProdutoId` (`ProdutoId` ASC) VISIBLE,
  CONSTRAINT `FK_Avaliação_Produtos_ProdutoId`
    FOREIGN KEY (`ProdutoId`)
    REFERENCES `ipet`.`produtos` (`Id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `ipet`.`carrinho`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ipet`.`carrinho` (
  `Id` CHAR(36) CHARACTER SET 'ascii' NOT NULL,
  `UsuarioId` CHAR(36) CHARACTER SET 'ascii' NOT NULL,
  `DataCadastro` DATETIME(6) NOT NULL,
  PRIMARY KEY (`Id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `ipet`.`compras`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ipet`.`compras` (
  `Id` CHAR(36) CHARACTER SET 'ascii' NOT NULL,
  `UsuarioId` CHAR(36) CHARACTER SET 'ascii' NOT NULL,
  `Valor` FLOAT NOT NULL,
  `Status` VARCHAR(100) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `DataCadastro` DATETIME(6) NOT NULL,
  `DataCompra` DATETIME(6) NULL DEFAULT NULL,
  `ListaProdutos` VARCHAR(5000) NOT NULL,
  PRIMARY KEY (`Id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `ipet`.`carrinhoproduto`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ipet`.`carrinhoproduto` (
  `Id` CHAR(36) CHARACTER SET 'ascii' NOT NULL,
  `CarrinhoId` CHAR(36) CHARACTER SET 'ascii' NOT NULL,
  `ProdutoId` CHAR(36) CHARACTER SET 'ascii' NOT NULL,
  `Quantidade` INT NOT NULL,
  `DataCadastro` DATETIME(6) NOT NULL,
  `CompraId` CHAR(36) CHARACTER SET 'ascii' NULL DEFAULT NULL,
  PRIMARY KEY (`Id`),
  INDEX `IX_CarrinhoProduto_CarrinhoId` (`CarrinhoId` ASC) VISIBLE,
  INDEX `IX_CarrinhoProduto_ProdutoId` (`ProdutoId` ASC) VISIBLE,
  INDEX `IX_CarrinhoProduto_CompraId` (`CompraId` ASC) VISIBLE,
  CONSTRAINT `FK_CarrinhoProduto_Carrinho_CarrinhoId`
    FOREIGN KEY (`CarrinhoId`)
    REFERENCES `ipet`.`carrinho` (`Id`),
  CONSTRAINT `FK_CarrinhoProduto_Compras_CompraId`
    FOREIGN KEY (`CompraId`)
    REFERENCES `ipet`.`compras` (`Id`),
  CONSTRAINT `FK_CarrinhoProduto_Produtos_ProdutoId`
    FOREIGN KEY (`ProdutoId`)
    REFERENCES `ipet`.`produtos` (`Id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `ipet`.`favoritos`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ipet`.`favoritos` (
  `Id` CHAR(36) CHARACTER SET 'ascii' NOT NULL,
  `UsuarioId` CHAR(36) CHARACTER SET 'ascii' NOT NULL,
  `ProdutoId` CHAR(36) CHARACTER SET 'ascii' NOT NULL,
  `DataCadastro` DATETIME(6) NOT NULL,
  PRIMARY KEY (`Id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `ipet`.`perfilpet`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ipet`.`perfilpet` (
  `Id` CHAR(36) CHARACTER SET 'ascii' NOT NULL,
  `IdUsuario` CHAR(36) CHARACTER SET 'ascii' NOT NULL,
  `TipoAnimal` VARCHAR(100) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `Nome` VARCHAR(200) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `Raca` VARCHAR(100) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `Idade` INT NOT NULL,
  `Porte` VARCHAR(100) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `Observacao` VARCHAR(1000) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `Ativo` TINYINT(1) NOT NULL,
  `DataCadastro` DATETIME(6) NOT NULL,
  PRIMARY KEY (`Id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `ipet`.`produtohashtag`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ipet`.`produtohashtag` (
  `Id` CHAR(36) CHARACTER SET 'ascii' NOT NULL,
  `IdProduto` CHAR(36) CHARACTER SET 'ascii' NOT NULL,
  `Tag` VARCHAR(100) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `DataCadastro` DATETIME(6) NOT NULL,
  PRIMARY KEY (`Id`),
  INDEX `IX_ProdutoHashtag_IdProduto` (`IdProduto` ASC) VISIBLE,
  CONSTRAINT `FK_ProdutoHashtag_Produtos_IdProduto`
    FOREIGN KEY (`IdProduto`)
    REFERENCES `ipet`.`produtos` (`Id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
