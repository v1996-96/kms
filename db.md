### Trigram extension
CREATE EXTENSION pg_trgm;

### Unaccent extension
CREATE EXTENSION unaccent;


### First approach: materialized views with indexed fields
Проблема: необходимость одновременно переиндексировать всю таблицу - долго

#### Materialized view for doceuments
select
	d.document_id,
	setweight(to_tsvector(coalesce(d.title, '')), 'A') ||
	setweight(to_tsvector(coalesce(d.subtitle, '')), 'C') ||
	setweight(to_tsvector(coalesce(dt.content, '')), 'B') as document
from documents as d
left join document_text as dt on dt.document_id = d.document_id
-- where dt.is_actual = true
group by d.document_id, dt.content

#### Index on materialized view
CREATE INDEX idx_document_search ON document_search USING gin(document);

#### Searching query
select ds.*, ts_rank(document, to_tsquery('culpa')) as score from document_search as ds
where document @@ to_tsquery('culpa')
order by ts_rank(document, to_tsquery('culpa')) desc




#### Trigram index
CREATE INDEX document_text_trigram_idx ON document_text USING gist(content gist_trgm_ops)
