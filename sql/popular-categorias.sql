SELECT * FROM "Categoria" C

INSERT INTO "Categoria" VALUES
(md5(random()::text || clock_timestamp()::text)::uuid, 'Ação'),
(md5(random()::text || clock_timestamp()::text)::uuid, 'Drama'),
(md5(random()::text || clock_timestamp()::text)::uuid, 'Terror'),
(md5(random()::text || clock_timestamp()::text)::uuid, 'Comédia'),
(md5(random()::text || clock_timestamp()::text)::uuid, 'Romance'),
(md5(random()::text || clock_timestamp()::text)::uuid, 'Suspense'),
(md5(random()::text || clock_timestamp()::text)::uuid, 'Policial'),
(md5(random()::text || clock_timestamp()::text)::uuid, 'Fantasia'),
(md5(random()::text || clock_timestamp()::text)::uuid, 'Aventura'),
(md5(random()::text || clock_timestamp()::text)::uuid, 'Biografia'),
(md5(random()::text || clock_timestamp()::text)::uuid, 'Graphic Novel'),
(md5(random()::text || clock_timestamp()::text)::uuid, 'Ficção Científica'),
(md5(random()::text || clock_timestamp()::text)::uuid, 'História em Quadrinhos')