create table if not exists public.assunto (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	legado_id int8 NOT null,
	descricao varchar(50) not null,
	criado_em timestamptz  null,
    alterado_em timestamptz NULL,
    status int NOT NULL,    
	CONSTRAINT assunto_pk PRIMARY KEY (id)
);

create table if not exists public.subassunto (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	legado_id int8 NOT null,
	assunto_id int8 NOT NULL,
	descricao varchar(100) not null,
	criado_em timestamptz  null,
    alterado_em timestamptz NULL,
    status int NOT NULL,
	CONSTRAINT subassunto_pk PRIMARY KEY (id),
	CONSTRAINT assunto_fk FOREIGN KEY (assunto_id) REFERENCES public.assunto(id)
);

create table if not exists public.tipo_item and not exists public.quantidade_alternativa (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	legado_id int8 NOT null,
	eh_padrao boolean not null,
	qtde_alternativa int8 NOT null,
	descricao varchar(50) not null,
	criado_em timestamptz  null,
    alterado_em timestamptz NULL,
    status int NOT NULL,
	CONSTRAINT tipo_item_pk PRIMARY KEY (id)
);