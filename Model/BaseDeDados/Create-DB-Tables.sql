CREATE DATABASE videolocadora;
USE videolocadora;

CREATE TABLE `classificacao_indicativa` (
  `id` int NOT NULL AUTO_INCREMENT,
  `indicacao` varchar(2) NOT NULL,
  `descricao` varchar(50) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `genero_cinematografico` (
  `id` int NOT NULL AUTO_INCREMENT,
  `genero` varchar(45) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `endereco` (
  `id` int NOT NULL AUTO_INCREMENT,
  `logradouro` varchar(150) NOT NULL,
  `bairro` varchar(50) NOT NULL,
  `cidade` varchar(50) NOT NULL,
  `uf` varchar(2) NOT NULL,
  `cep` varchar(8) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `telefone` (
  `id` int NOT NULL AUTO_INCREMENT,
  `residencial` varchar(10) DEFAULT NULL,
  `celular` varchar(11) NOT NULL,
  `comercial` varchar(10) DEFAULT NULL,
  `recado` varchar(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `cliente` (
  `id` int NOT NULL AUTO_INCREMENT,
  `nome` varchar(100) NOT NULL,
  `cpf` varchar(11) NOT NULL,
  `email` varchar(50) NOT NULL,
  `telefone_id` int NOT NULL,
  `endereco_id` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_cliente_telefone1_idx` (`telefone_id`),
  KEY `fk_cliente_endereco1_idx` (`endereco_id`),
  CONSTRAINT `fk_cliente_endereco1` FOREIGN KEY (`endereco_id`) REFERENCES `endereco` (`id`),
  CONSTRAINT `fk_cliente_telefone1` FOREIGN KEY (`telefone_id`) REFERENCES `telefone` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `dependente` (
  `id` int NOT NULL AUTO_INCREMENT,
  `nome` varchar(50) NOT NULL,
  `cliente_id` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_dependente_cliente1_idx` (`cliente_id`),
  CONSTRAINT `fk_dependente_cliente1` FOREIGN KEY (`cliente_id`) REFERENCES `cliente` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `filme` (
  `id` int NOT NULL AUTO_INCREMENT,
  `titulo` varchar(150) NOT NULL,
  `lancamento` year(4) NOT NULL,
  `sinopse` varchar(250) DEFAULT NULL,
  `genero_cinematografico_id` int NOT NULL,
  `classificacao_indicativa_id` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_filme_genero_cinematografico_idx` (`genero_cinematografico_id`),
  KEY `fk_filme_classificacao_indicativa1_idx` (`classificacao_indicativa_id`),
  CONSTRAINT `fk_filme_classificacao_indicativa1` FOREIGN KEY (`classificacao_indicativa_id`) REFERENCES `classificacao_indicativa` (`id`),
  CONSTRAINT `fk_filme_genero_cinematografico` FOREIGN KEY (`genero_cinematografico_id`) REFERENCES `genero_cinematografico` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `estoque` (
  `id` int NOT NULL AUTO_INCREMENT,
  `quantidade` int NOT NULL,
  `filme_id` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_estoque_filme1_idx` (`filme_id`),
  CONSTRAINT `fk_estoque_filme1` FOREIGN KEY (`filme_id`) REFERENCES `filme` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `locacao` (
  `id` int NOT NULL AUTO_INCREMENT,
  `data_locacao` datetime NOT NULL,
  `data_devolucao` datetime NOT NULL,
  `cliente_id` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_locacao_cliente1_idx` (`cliente_id`),
  CONSTRAINT `fk_locacao_cliente1` FOREIGN KEY (`cliente_id`) REFERENCES `cliente` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `rel_locacao_filme` (
  `id` int NOT NULL AUTO_INCREMENT,
  `locacao_id` int NOT NULL,
  `filme_id` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_rel_locacao_filme_locacao1_idx` (`locacao_id`),
  KEY `fk_rel_locacao_filme_filme1_idx` (`filme_id`),
  CONSTRAINT `fk_rel_locacao_filme_filme1` FOREIGN KEY (`filme_id`) REFERENCES `filme` (`id`),
  CONSTRAINT `fk_rel_locacao_filme_locacao1` FOREIGN KEY (`locacao_id`) REFERENCES `locacao` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `pagamento` (
  `id` int NOT NULL AUTO_INCREMENT,
  `valor` decimal(6,2) NOT NULL,
  `locacao_id` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_pagamento_locacao1_idx` (`locacao_id`),
  CONSTRAINT `fk_pagamento_locacao1` FOREIGN KEY (`locacao_id`) REFERENCES `locacao` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `historico` (
  `id` int NOT NULL AUTO_INCREMENT,
  `data_inclusao` datetime NOT NULL,
  `pagamento_id` int NOT NULL,
  `cliente_id` int NOT NULL,
  `locacao_id` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_historico_pagamento1_idx` (`pagamento_id`),
  KEY `fk_historico_cliente1_idx` (`cliente_id`),
  KEY `fk_historico_locacao1_idx` (`locacao_id`),
  CONSTRAINT `fk_historico_cliente1` FOREIGN KEY (`cliente_id`) REFERENCES `cliente` (`id`),
  CONSTRAINT `fk_historico_locacao1` FOREIGN KEY (`locacao_id`) REFERENCES `locacao` (`id`),
  CONSTRAINT `fk_historico_pagamento1` FOREIGN KEY (`pagamento_id`) REFERENCES `pagamento` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;