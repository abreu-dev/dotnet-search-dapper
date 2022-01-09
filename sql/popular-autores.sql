SELECT * FROM "Autor" A

INSERT INTO "Autor" VALUES
(md5(random()::text || clock_timestamp()::text)::uuid, 'Stephen King', '1947-09-21T00:00:00'),
(md5(random()::text || clock_timestamp()::text)::uuid, 'Rick Riordan', '1964-06-05T00:00:00'),
(md5(random()::text || clock_timestamp()::text)::uuid, 'George R. R. Martin', '1948-09-20T00:00:00'),
(md5(random()::text || clock_timestamp()::text)::uuid, 'H. P. Lovecraft ', '1890-08-20T00:00:00'),
(md5(random()::text || clock_timestamp()::text)::uuid, 'J. R. R. Tolkien', '1892-01-03T00:00:00'),
(md5(random()::text || clock_timestamp()::text)::uuid, 'William Shakespeare', '1564-04-26T00:00:00'),
(md5(random()::text || clock_timestamp()::text)::uuid, 'Akira Toriyama', '1955-04-05T00:00:00'),
(md5(random()::text || clock_timestamp()::text)::uuid, 'J. K. Rowling', '1965-07-31T00:00:00'),
(md5(random()::text || clock_timestamp()::text)::uuid, 'Paulo Coelho', '1947-08-24T00:00:00'),
(md5(random()::text || clock_timestamp()::text)::uuid, 'Masashi Kishimoto', '1974-11-08T00:00:00'),
(md5(random()::text || clock_timestamp()::text)::uuid, 'Arthur Conan Doyle', '1859-05-22T00:00:00')
