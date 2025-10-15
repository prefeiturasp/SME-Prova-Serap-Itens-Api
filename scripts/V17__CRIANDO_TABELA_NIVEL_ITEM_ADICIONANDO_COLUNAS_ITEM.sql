ALTER TABLE IF EXISTS public.item
ADD COLUMN IF NOT EXISTS nivelItem_id INTEGER;

ALTER TABLE IF EXISTS public.item
ADD COLUMN IF NOT EXISTS sentencaDescritora VARCHAR(100);

CREATE TABLE IF NOT EXISTS public.nivel_item (
    id INTEGER PRIMARY KEY,
    descricao VARCHAR(50) NOT NULL,
    ordem INTEGER NOT NULL,
    status INTEGER NOT NULL DEFAULT 1,
    criado_em TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    alterado_em TIMESTAMPTZ NULL
);

INSERT INTO public.nivel_item (id, descricao, ordem, status)
VALUES
    (1, 'Abaixo do Básico', 1, 1),
    (2, 'Básico',          2, 1),
    (3, 'Adequado',        3, 1),
    (4, 'Avançado',        4, 1)
ON CONFLICT (id) DO NOTHING;






