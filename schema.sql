CREATE TABLE Authors (
    ID INT PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL
);

CREATE TABLE Books (
    ID INT PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    AuthorID INT NOT NULL,
    PublicationYear INT NOT NULL,
    FOREIGN KEY (AuthorID) REFERENCES Authors(ID)
);

INSERT INTO Authors (ID, Name) VALUES (1, 'Dimitri Mumladze');

UPDATE Books SET PublicationYear = 2013 WHERE ID = 2;

DELETE FROM Books WHERE ID = 3;

SELECT b.Title, a.Name AS AuthorName
FROM Books b
INNER JOIN Authors a ON b.AuthorID = a.ID
WHERE b.PublicationYear > 2010;