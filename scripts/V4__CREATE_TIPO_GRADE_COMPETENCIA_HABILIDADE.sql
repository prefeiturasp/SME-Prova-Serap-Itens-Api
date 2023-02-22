create table if not exists public.tipo_grade (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	legado_id int8 NOT null,
	matriz_id int8 NOT null,
	descricao varchar(50) not null,
	ordem int8 NOT null,
	criado_em timestamptz  null,
    alterado_em timestamptz NULL,
    status int NOT NULL,    
	CONSTRAINT tipo_grade_pk PRIMARY KEY (id),
	CONSTRAINT matriz_fk FOREIGN KEY (matriz_id) REFERENCES public.matriz(id)
);

create table if not exists public.competencia (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	codigo varchar(100) not null,
	legado_id int8 NOT null,
	matriz_id int8 NOT null,
	descricao varchar(500) not null,
	criado_em timestamptz  null,
    alterado_em timestamptz NULL,
    status int NOT NULL,    
	CONSTRAINT competencia_pk PRIMARY KEY (id),
	CONSTRAINT matriz_fk FOREIGN KEY (matriz_id) REFERENCES public.matriz(id)
);

create table if not exists public.habilidade (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	codigo varchar(100) not null,
	legado_id int8 NOT null,
	competencia_id int8 NOT null,
	descricao varchar(500) not null,
	criado_em timestamptz  null,
    alterado_em timestamptz NULL,
    status int NOT NULL,    
	CONSTRAINT habilidade_pk PRIMARY KEY (id),
	CONSTRAINT competencia_fk FOREIGN KEY (competencia_id) REFERENCES public.competencia(id)
);