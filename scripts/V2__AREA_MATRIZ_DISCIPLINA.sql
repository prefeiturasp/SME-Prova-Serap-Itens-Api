create table if not exists public.area_conhecimento
(
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY ,
	area_conhecimento_legado_id int8  NULL,
	descricao varchar(50) NULL,
	status int NOT NULL,
	criado_em timestamptz  null,
	alterado_em timestamptz null

);


create table if not exists public.disciplina
(
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY ,
	disciplina_legado_id int8  NOT NULL,
	descricao varchar(500) NULL,
	disciplina_tipo_id int NOT NULL,
	criado_em timestamptz  null,
	alterado_em timestamptz NULL,
     status int NOT NULL,
     level_educacional_tipo_id int null,
     CONSTRAINT disciplina_pk PRIMARY KEY (id)
	

);

create table if not exists public.matriz
(
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY ,
	matriz_legado_id int8  NOT NULL,
	descricao varchar(500) NULL,
	edicao varchar(100) NULL,
	criado_em timestamptz  null,
	alterado_em timestamptz NULL,
    status int NOT NULL,
    disciplina_id int NOT NULL,
    modelo_matriz_id int  null, 
  CONSTRAINT matriz_pk PRIMARY KEY (id),

constraint  disciplina_fk FOREIGN KEY (disciplina_id)  REFERENCES  public.disciplina (id)

);