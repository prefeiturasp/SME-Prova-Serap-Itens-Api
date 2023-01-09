create table if not exists public.assunto (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	legado_id int8 NOT null,
	descricao varchar not null,
	criado_em timestamptz  null,
    alterado_em timestamptz NULL,
    status int NOT NULL,    
	CONSTRAINT assunto_pk PRIMARY KEY (id)
);


create table if not exists public.subassunto (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	legado_id int8 NOT null,
	assunto_id int8 NOT NULL,
	descricao varchar not null,
	criado_em timestamptz  null,
    alterado_em timestamptz NULL,
    status int NOT NULL,
	CONSTRAINT subassunto_pk PRIMARY KEY (id)
);
CREATE index if not exists subassunto_assunto_id_idx ON public.subassunto USING btree (assunto_id);
ALTER TABLE public.subassunto ADD constraint assunto_fk FOREIGN KEY (assunto_id) REFERENCES public.assunto(id);


create table if not exists public.tipo_item (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	legado_id int8 NOT null,
	eh_padrao boolean not null,
	qtde_alternativa int8 NOT null,
	descricao varchar not null,
	criado_em timestamptz  null,
    alterado_em timestamptz NULL,
    status int NOT NULL,
	CONSTRAINT tipo_item_pk PRIMARY KEY (id)
);