create table if not exists public.grupo (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	legado_id uuid NOT null,
	nome varchar(50) not null,
	permite_consultar bool not null,
	permite_inserir bool not null,
	permite_alterar bool not null,
	permite_excluir bool not null,
	criado_em timestamptz  null,
    alterado_em timestamptz NULL,
    status int NOT NULL,    
	CONSTRAINT grupo_pk PRIMARY KEY (id)
);

create table if not exists public.usuario (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	legado_id uuid NOT null,
	login varchar(500) not null,
	nome varchar(200) not null,
	criado_em timestamptz  null,
    alterado_em timestamptz NULL,
    status int NOT NULL,    
	CONSTRAINT usuario_pk PRIMARY KEY (id)
);

create table if not exists public.usuario_grupo (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	usuario_id int8 NOT null,
	grupo_id int8 NOT null,
	criado_em timestamptz  null,
    alterado_em timestamptz NULL,
    status int NOT NULL,    
	CONSTRAINT usuario_grupo_pk PRIMARY KEY (id)
);

ALTER TABLE public.usuario_grupo add constraint usuario_fk FOREIGN KEY (usuario_id) REFERENCES public.usuario(id);
ALTER TABLE public.usuario_grupo add constraint grupo_fk FOREIGN KEY (grupo_id) REFERENCES public.grupo(id);

create index if not exists grupo_legado_id_idx ON public.grupo (legado_id);
create index if not exists usuario_legado_id_idx ON public.usuario (legado_id);