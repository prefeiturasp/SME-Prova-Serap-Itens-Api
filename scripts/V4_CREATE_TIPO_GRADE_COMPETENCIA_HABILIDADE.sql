create table if not exists public.tipo_grade (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	legado_id int8 NOT null,
	matriz_id int8 NOT null,
	descricao varchar not null,
	ordem int8 NOT null,
	criado_em timestamptz  null,
    alterado_em timestamptz NULL,
    status int NOT NULL,    
	CONSTRAINT tipo_grade_pk PRIMARY KEY (id)
);
CREATE index if not exists tipo_grade_matriz_id_idx ON public.tipo_grade USING btree (matriz_id);
ALTER TABLE public.tipo_grade ADD constraint matriz_fk FOREIGN KEY (matriz_id) REFERENCES public.matriz(id);

create table if not exists public.competencia (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	codigo varchar(100) not null,
	legado_id int8 NOT null,
	matriz_id int8 NOT null,
	descricao varchar(500) not null,
	criado_em timestamptz  null,
    alterado_em timestamptz NULL,
    status int NOT NULL,    
	CONSTRAINT competencia_pk PRIMARY KEY (id)
);
CREATE index if not exists competencia_matriz_id_idx ON public.competencia USING btree (matriz_id);
ALTER TABLE public.competencia add constraint matriz_fk FOREIGN KEY (matriz_id) REFERENCES public.matriz(id);

create table if not exists public.habilidade (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	codigo varchar(100) not null,
	legado_id int8 NOT null,
	competencia_id int8 NOT null,
	descricao varchar(500) not null,
	criado_em timestamptz  null,
    alterado_em timestamptz NULL,
    status int NOT NULL,    
	CONSTRAINT habilidade_pk PRIMARY KEY (id)
);
CREATE index if not exists habilidade_competencia_id_idx ON public.habilidade USING btree (competencia_id);
ALTER TABLE public.habilidade ADD constraint competencia_fk FOREIGN KEY (competencia_id) REFERENCES public.competencia(id);


