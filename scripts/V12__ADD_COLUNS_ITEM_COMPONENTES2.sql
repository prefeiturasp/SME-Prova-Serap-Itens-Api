ALTER table if exists  item ADD column IF NOT EXISTS  assunto_id  int8; 
ALTER table if exists  item ADD column IF NOT EXISTS  subassunto_id  int8; 
ALTER table if exists  item ADD column IF NOT EXISTS  quantidade_alternativa_id  int8; 
ALTER table if exists  item ADD column IF NOT EXISTS  situacao  int8; 
ALTER table if exists  item ADD column IF NOT EXISTS  tipo  int8; 
ALTER table if exists  item ADD column IF NOT EXISTS  palavras_chave  varchar; 
ALTER table if exists  item ADD column IF NOT EXISTS  parametro_b_transformado  decimal;
ALTER table if exists  item ADD column IF NOT EXISTS  media_eh_desvio  decimal; 
ALTER table if exists  item ADD column IF NOT EXISTS  observacao  varchar(100); 
ALTER table if exists  item ADD column IF NOT EXISTS   criado_em timestamptz  null;
ALTER table if exists  item ADD column IF NOT EXISTS   alterado_em timestamptz NULL;

ALTER TABLE item  DROP CONSTRAINT IF EXISTS  item_assunto; 
ALTER TABLE item ADD CONSTRAINT item_assunto FOREIGN KEY (assunto_id) REFERENCES assunto(id) ON DELETE cascade;

ALTER TABLE item  DROP CONSTRAINT IF EXISTS  item_subassunto; 
ALTER TABLE item ADD CONSTRAINT item_subassunto FOREIGN KEY (subassunto_id) REFERENCES subassunto(id) ON DELETE cascade;

ALTER TABLE item  DROP CONSTRAINT IF EXISTS  item_quantidade_alternativa_id; 
ALTER TABLE item ADD CONSTRAINT item_quantidade_alternativa_id FOREIGN KEY (quantidade_alternativa_id) REFERENCES quantidade_alternativa(id) ON DELETE cascade;