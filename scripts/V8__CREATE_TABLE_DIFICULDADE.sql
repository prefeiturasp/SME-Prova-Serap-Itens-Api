create table if not exists public.dificuldade (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	legado_id int8 NOT null,
	descricao varchar(50) not null,
	ordem int not null,
	criado_em timestamptz  null,
    alterado_em timestamptz NULL,
    status int NOT NULL,    
	CONSTRAINT dificuldade_pk PRIMARY KEY (id)
);