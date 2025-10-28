CREATE TABLE Authors (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL
);

CREATE TABLE Books (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(200) NOT NULL,
    AuthorID INT NOT NULL,
    PublicationYear INT NOT NULL,
    CONSTRAINT FK_Books_Authors FOREIGN KEY (AuthorID) 
        REFERENCES Authors(ID) ON DELETE CASCADE
);

INSERT INTO Authors (Name) VALUES ('J.K. Rowling');
INSERT INTO Authors (Name) VALUES ('George Orwell');
INSERT INTO Authors (Name) VALUES ('J.R.R. Tolkien');

INSERT INTO Books (Title, AuthorID, PublicationYear) 
VALUES ('Harry Potter and the Philosopher Stone', 1, 1997);

INSERT INTO Books (Title, AuthorID, PublicationYear) 
VALUES ('Harry Potter and the Chamber of Secrets', 1, 1998);

INSERT INTO Books (Title, AuthorID, PublicationYear) 
VALUES ('Animal Farm', 2, 1945);

INSERT INTO Books (Title, AuthorID, PublicationYear) 
VALUES ('The Hobbit', 3, 1937);

UPDATE Books 
SET PublicationYear = 2013 
WHERE ID = 2;

DELETE FROM Books 
WHERE ID = 3;

SELECT 
    b.Title AS BookTitle,
    a.Name AS AuthorName,
    b.PublicationYear
FROM Books b
INNER JOIN Authors a ON b.AuthorID = a.ID
WHERE b.PublicationYear > 2010
ORDER BY b.PublicationYear DESC;

