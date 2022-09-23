CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';


-- STUB Keeps 
CREATE TABLE IF NOT EXISTS keeps(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  creatorId VARCHAR(255) NOT NULL,
  name VARCHAR(255) NOT NULL,
  description TEXT NOT NULL, 
  img VARCHAR(500) NOT NULL,
  views INT NOT NULL DEFAULT 0, 
  kept INT NOT NULL DEFAULT 0,
  shares INT NOT NULL DEFAULT 0,

  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

DROP TABLE keeps;

INSERT INTO keeps
(creatorId, name, description, img)
VALUES
('63237a6e2d055737269fbc1a', 'Spooky Skeleton', 'A spooky scary skeleton for spooky season!', 'https://images.unsplash.com/photo-1588959634980-945c7f339dca?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxzZWFyY2h8Mnx8c2tlbGV0b258ZW58MHx8MHx8&auto=format&fit=crop&w=600&q=60');

INSERT INTO keeps
(creatorId, name, description, img)
VALUES
('632de1a3bcc159f396782a2b', 'Corns', 'A field of corn', 'https://images.unsplash.com/photo-1567547921486-f280c2f53b5d?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxzZWFyY2h8NXx8Y29ybnxlbnwwfHwwfHw%3D&auto=format&fit=crop&w=600&q=60');

SELECT * FROM keeps k 
JOIN accounts a ON a.id = k.creatorId;


-- STUB Vaults 
CREATE TABLE IF NOT EXISTS vaults(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  creatorId VARCHAR(255) NOT NULL,
  name VARCHAR(255) NOT NULL,
  description TEXT NOT NULL,
  isPrivate BOOLEAN NOT NULL DEFAULT false,

  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE 
) default charset utf8 COMMENT '';

DROP TABLE vaults;

INSERT INTO vaults
(creatorId, name, description)
VALUES
('63237a6e2d055737269fbc1a', 'Halloween Vault', 'A sick vault for all things spooky this halloween');

INSERT INTO vaults
(creatorId, name, description)
VALUES
('632de1a3bcc159f396782a2b', 'Corns Vault', 'All my hopes and dreams');

SELECT * FROM vaults v
JOIN accounts a ON a.id = v.creatorId;

-- STUB VaultKeeps
CREATE TABLE IF  NOT EXISTS vaultkeeps(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  creatorId VARCHAR(255) NOT NULL,
  vaultId INT NOT NULL,
  keepId INT NOT NULL,

  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN KEY (vaultId) REFERENCES vaults(id) ON DELETE CASCADE,
  FOREIGN KEY (keepId) REFERENCES keeps(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

DROP TABLE vaultkeeps;

INSERT INTO vaultkeeps
(creatorId, vaultId, keepId)
VALUES
('63237a6e2d055737269fbc1a', 1, 1);


SELECT 
vk.*,
COUNT(k.id) AS keepCount,
v.*,
a.*
FROM vaultkeeps vk 
LEFT JOIN keeps k ON vk.keepId = k.id
INNER JOIN vaults v ON vk.vaultId = v.id
INNER JOIN accounts a ON vk.creatorId = a.id
GROUP BY(vk.id);