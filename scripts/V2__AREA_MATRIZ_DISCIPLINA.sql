create table if not exists public.area_conhecimento
(
    id int8 NOT NULL GENERATED ALWAYS AS IDENTITY ,
    legado_id int8  NULL,
    descricao varchar(50) NULL,
    status int NOT NULL,
    criado_em timestamptz  null,
    alterado_em timestamptz null

);

create table if not exists public.disciplina
(
    id int8 NOT NULL GENERATED ALWAYS AS IDENTITY ,
    legado_id int8  NOT NULL,
    descricao varchar(500) NULL,
    criado_em timestamptz  null,
    alterado_em timestamptz NULL,
    status int NOT NULL,
    area_conhecimento_id int  null, 
    CONSTRAINT disciplina_pk PRIMARY KEY (id)

);

 

create table if not exists public.matriz
(
    id int8 NOT NULL GENERATED ALWAYS AS IDENTITY ,
    legado_id int8  NOT NULL,
    descricao varchar(500) NULL,
    criado_em timestamptz  null,
    alterado_em timestamptz NULL,
    status int NOT NULL,
    disciplina_id int  NULL,
  CONSTRAINT matriz_pk PRIMARY KEY (id),
constraint  disciplina_fk FOREIGN KEY (disciplina_id)  REFERENCES  public.disciplina (id)
);