-- Database: netflix

DROP DATABASE IF EXISTS netflix;

CREATE DATABASE netflix
    WITH 
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'en_US.utf8'
    LC_CTYPE = 'en_US.utf8'
    TABLESPACE = pg_default
	CONNECTION LIMIT = -1;

\c netflix

DROP TABLE IF EXISTS profiles;
DROP TABLE IF EXISTS accounts;

CREATE TABLE IF NOT EXISTS accounts (
	account_id INT GENERATED ALWAYS AS IDENTITY,
   	email varchar(255) UNIQUE NOT NULL,
   	password varchar(200) NOT NULL,
   	firstname varchar(50) NOT NULL,
   	lastname varchar(50) NOT NULL,
	PRIMARY KEY(account_id)
);

CREATE TABLE IF NOT EXISTS profiles (
   	profile_id INT GENERATED ALWAYS AS IDENTITY,
   	account_id INT NOT NULL,
   	name varchar(50) NOT NULL,
   	age int NOT NULL,
	PRIMARY KEY(profile_id),
	CONSTRAINT fk_account
		FOREIGN KEY(account_id)
			REFERENCES accounts(account_id)
);

INSERT INTO accounts(email, password, firstname, lastname)
VALUES('test@test.dk', '1234', 'Andreas', 'Vikke'),
      ('test2@test.dk', '1234', 'Martin', 'Frederiksen'),
      ('test3@test.dk', '1234', 'William', 'Hussfeldt')
      ('test4@test.dk', '1234', 'Asger', 'Sørensen');	   
	   
INSERT INTO profiles(account_id, name, age)
VALUES(1,'Andreas',22),
      (2,'Martin',28),
      (2,'Tut',27),
	  (3,'William',35),
	  (4,'Asger',23);