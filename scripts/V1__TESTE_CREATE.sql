create table if not exists public.teste
(
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	descricao varchar(60),
	CONSTRAINT teste_pk PRIMARY KEY (id)
);