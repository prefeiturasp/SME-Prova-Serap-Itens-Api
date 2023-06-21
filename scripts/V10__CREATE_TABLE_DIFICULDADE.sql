CREATE TABLE if not exists public.dificuldade (
    id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
    legado_id int8 NOT NULL,
    descricao varchar(50) NOT NULL,
    ordem int4 NOT NULL,
    criado_em timestamptz NULL,
    alterado_em timestamptz NULL,
    status int4 NOT NULL,
    CONSTRAINT dificuldade_pk PRIMARY KEY (id)
);