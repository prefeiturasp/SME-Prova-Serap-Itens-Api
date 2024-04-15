ALTER table if exists  item ADD column IF NOT EXISTS  competencia_id  int8; 
ALTER table if exists  item ADD column IF NOT EXISTS  habilidade_id  int8; 
ALTER table if exists  item ADD column IF NOT EXISTS  tipo_grade_id  int8; 
ALTER table if exists  item ADD column IF NOT EXISTS  dificuldade_sugerida_id  int8 ;
ALTER table if exists  item ADD column IF NOT EXISTS  matriz_id  int8 ;
ALTER table if exists  item ADD column IF NOT EXISTS  discriminacao  decimal; 
ALTER table if exists  item ADD column IF NOT EXISTS  acerto_casual  decimal; 
ALTER table if exists  item ADD column IF NOT EXISTS  dificuldade  decimal; 

create index if not exists item_codigo_idx ON public.item (codigo_item);

ALTER TABLE item  DROP CONSTRAINT IF EXISTS  item_area_conhecimento; 
ALTER TABLE item ADD CONSTRAINT item_area_conhecimento FOREIGN KEY (area_conhecimento_id) REFERENCES area_conhecimento(id) ON DELETE cascade;

ALTER TABLE item  DROP CONSTRAINT IF EXISTS  item_disciplina; 
ALTER TABLE item ADD CONSTRAINT item_disciplina FOREIGN KEY (disciplina_id) REFERENCES disciplina(id) ON DELETE cascade;

ALTER TABLE item  DROP CONSTRAINT IF EXISTS  item_matriz;
ALTER TABLE item ADD CONSTRAINT item_matriz FOREIGN KEY (matriz_id) REFERENCES matriz(id) ON DELETE cascade;

ALTER TABLE item DROP CONSTRAINT IF EXISTS item_competencia;
ALTER TABLE item ADD CONSTRAINT item_competencia FOREIGN KEY (competencia_id) REFERENCES competencia(id) ON DELETE cascade;

ALTER TABLE item DROP CONSTRAINT IF EXISTS  item_habilidade; 
ALTER TABLE item ADD CONSTRAINT item_habilidade FOREIGN KEY (habilidade_id) REFERENCES habilidade(id) ON DELETE cascade;

ALTER TABLE item DROP CONSTRAINT IF EXISTS  item_tipo_grade; 
ALTER TABLE item ADD CONSTRAINT item_tipo_grade FOREIGN KEY (tipo_grade_id) REFERENCES tipo_grade(id) ON DELETE cascade;

ALTER TABLE item DROP CONSTRAINT IF EXISTS  item_dificuldade_sugerida; 
ALTER TABLE item ADD CONSTRAINT item_dificuldade_sugerida FOREIGN KEY (dificuldade_sugerida_id) REFERENCES dificuldade(id) ON DELETE cascade;