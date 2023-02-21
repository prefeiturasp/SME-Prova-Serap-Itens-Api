CREATE table if not exists  public.item (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	codigo_item int8 NOT NULL,
	are_conhecimento_legado_id int8 NOT NULL,
	disciplina_legado_id int8 NOT null,
	matriz_legado_id int8 default NULL,
	CONSTRAINT item_pk PRIMARY KEY (id)
);

CREATE table if not exists public.sequencial_item (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	codigo_area_conhecimento int8 NOT NULL,
	codigo_disciplina int8 NOT NULL,
	sequencial int8 NOT null,
	 criado_em timestamp null,
	 alterado_em timestamp null,
	CONSTRAINT sequencial_item_pk PRIMARY KEY (id)
); 