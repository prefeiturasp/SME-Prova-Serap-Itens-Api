ALTER table if exists  assunto ADD column IF NOT EXISTS  disciplina_id  int8; 

ALTER TABLE assunto  DROP CONSTRAINT IF EXISTS  assunto_disciplina; 
ALTER TABLE assunto ADD CONSTRAINT assunto_disciplina FOREIGN KEY (disciplina_id) REFERENCES disciplina(id) ON DELETE cascade;