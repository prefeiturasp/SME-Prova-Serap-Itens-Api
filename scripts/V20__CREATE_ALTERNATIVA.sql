CREATE TABLE public.alternativa (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	descricao varchar NULL,
	ordem int4 NOT NULL,
	numeracao varchar NULL,
	justificativa varchar NULL,
	correta bool NULL DEFAULT false,
	criado_em timestamptz NULL,
	alterado_em timestamptz NULL,
	item_id int8 not null,
	CONSTRAINT alternativa_pk PRIMARY KEY (id)
);


ALTER TABLE public.alternativa ADD CONSTRAINT alternativa_item FOREIGN KEY (item_id) REFERENCES public.item (id);