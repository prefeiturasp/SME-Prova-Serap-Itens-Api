-- ARQUIVO
  
  CREATE table if not exists public.arquivo (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	legado_id int8  null,
	nome varchar(500) NOT NULL,
	caminho varchar(500) NOT NULL,
    content_type varchar(500) NOT NULL,
	situacao int4 NOT NULL,
	criado_em timestamptz NULL,
	alterado_em timestamptz NULL,
	CONSTRAINT arquivo_pk PRIMARY KEY (id)
);
  
-- ITEM_VIDEO  

CREATE table if not exists public.item_video (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	arquivo_id int8 NOT NULL,
	item_id int8 NOT NULL,
	situacao int4 NOT NULL,
	criado_em timestamptz NULL,
	alterado_em timestamptz NULL,
	constraint video_pk PRIMARY KEY (id)
);

ALTER TABLE public.item_video ADD CONSTRAINT video_item FOREIGN KEY (item_id) REFERENCES public.item(id);
ALTER TABLE public.item_video ADD CONSTRAINT arquivo_item FOREIGN KEY (arquivo_id) REFERENCES public.arquivo(id);

-- ITEM_AUDIO

CREATE table if not exists public.item_audio (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	arquivo_id int8 NOT NULL,
	item_id int8 NOT NULL,
	situacao int4 NOT NULL,
	criado_em timestamptz NULL,
	alterado_em timestamptz NULL,
	constraint audio_pk PRIMARY KEY (id)
);

ALTER TABLE public.item_audio ADD CONSTRAINT audio_item FOREIGN KEY (item_id) REFERENCES public.item(id);
ALTER TABLE public.item_audio ADD CONSTRAINT arquivo_audio_item FOREIGN KEY (arquivo_id) REFERENCES public.arquivo(id);
