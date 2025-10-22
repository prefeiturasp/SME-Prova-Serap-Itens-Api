-- V18__ADICIONA_COLUNA_VERSAO_ITEM.sql

-- A coluna 'versao_item' será adicionada à tabela 'public.item'.
-- Tipo: int8 (BIGINT)
-- Restrição: NOT NULL
-- Valor inicial/default: 1
--
-- O bloco DO/PLpgSQL garante que o script seja idempotente (não falhe se executado mais de uma vez).

DO $$
BEGIN
    -- Verifica se a coluna 'versao_item' JÁ EXISTE na tabela 'public.item'
    IF NOT EXISTS (
        SELECT 1
        FROM information_schema.columns
        WHERE table_schema = 'public'
        AND table_name = 'item'
        AND column_name = 'versao_item'
    ) THEN
        -- Adiciona a coluna se ela NÃO existir
        ALTER TABLE public.item
        ADD COLUMN versao_item int8 NOT NULL DEFAULT 1;

        RAISE NOTICE 'Flyway: Coluna versao_item (int8 NOT NULL DEFAULT 1) adicionada com sucesso à tabela public.item.';
    ELSE
        -- Mensagem informativa, caso o script seja executado fora do Flyway ou manualmente
        RAISE NOTICE 'Flyway: Coluna versao_item JÁ EXISTE na tabela public.item. Nenhuma alteração realizada.';
    END IF;
END
$$ LANGUAGE plpgsql;