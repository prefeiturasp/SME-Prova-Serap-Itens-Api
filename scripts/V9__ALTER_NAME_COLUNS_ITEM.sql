DO $$
BEGIN
  IF EXISTS(SELECT *
    FROM information_schema.columns
    WHERE table_name='item' and column_name='are_conhecimento_legado_id')
  THEN
      ALTER TABLE "public"."item" RENAME COLUMN "are_conhecimento_legado_id" TO "area_conhecimento_id";
  END IF;
end $$ ;


DO $$
BEGIN
  IF EXISTS(SELECT *
    FROM information_schema.columns
    WHERE table_name='item' and column_name='disciplina_legado_id')
  THEN
      ALTER TABLE "public"."item" RENAME COLUMN "disciplina_legado_id" TO "disciplina_id";
  END IF;
end $$ ;


DO $$
BEGIN
  IF EXISTS(SELECT *
    FROM information_schema.columns
    WHERE table_name='item' and column_name='disciplina_legado_id')
  THEN
      ALTER TABLE "public"."item" RENAME COLUMN "matriz_legado_id" TO "matriz_id";
  END IF;
end $$ ;

