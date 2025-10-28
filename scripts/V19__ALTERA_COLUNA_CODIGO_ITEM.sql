 ALTER TABLE public.item
ALTER COLUMN codigo_item TYPE varchar(32)
USING codigo_item::varchar(32);

DROP INDEX IF EXISTS item_codigo_idx;

CREATE INDEX item_codigo_idx 
ON public.item (codigo_item);