/*
 Navicat PostgreSQL Data Transfer

 Source Server         : localhost
 Source Server Type    : PostgreSQL
 Source Server Version : 100003
 Source Host           : localhost:32768
 Source Catalog        : kms
 Source Schema         : public

 Target Server Type    : PostgreSQL
 Target Server Version : 100003
 File Encoding         : 65001

 Date: 10/05/2018 22:43:49
*/


-- ----------------------------
-- Type structure for gtrgm
-- ----------------------------
DROP TYPE IF EXISTS "public"."gtrgm";
CREATE TYPE "public"."gtrgm" (
  INPUT = "public"."gtrgm_in",
  OUTPUT = "public"."gtrgm_out",
  INTERNALLENGTH = VARIABLE,
  CATEGORY = U,
  DELIMITER = ','
);

-- ----------------------------
-- Sequence structure for activity_activity_id_seq
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."activity_activity_id_seq";
CREATE SEQUENCE "public"."activity_activity_id_seq" 
INCREMENT 1
MINVALUE  1
MAXVALUE 2147483647
START 1
CACHE 1;

-- ----------------------------
-- Sequence structure for attachments_attachment_id_seq
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."attachments_attachment_id_seq";
CREATE SEQUENCE "public"."attachments_attachment_id_seq" 
INCREMENT 1
MINVALUE  1
MAXVALUE 2147483647
START 1
CACHE 1;

-- ----------------------------
-- Sequence structure for bookmarks_bookmark_id_seq
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."bookmarks_bookmark_id_seq";
CREATE SEQUENCE "public"."bookmarks_bookmark_id_seq" 
INCREMENT 1
MINVALUE  1
MAXVALUE 2147483647
START 1
CACHE 1;

-- ----------------------------
-- Sequence structure for comment_likes_comment_like_id_seq
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."comment_likes_comment_like_id_seq";
CREATE SEQUENCE "public"."comment_likes_comment_like_id_seq" 
INCREMENT 1
MINVALUE  1
MAXVALUE 2147483647
START 1
CACHE 1;

-- ----------------------------
-- Sequence structure for comments_comment_id_seq
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."comments_comment_id_seq";
CREATE SEQUENCE "public"."comments_comment_id_seq" 
INCREMENT 1
MINVALUE  1
MAXVALUE 2147483647
START 1
CACHE 1;

-- ----------------------------
-- Sequence structure for competences_competence_id_seq
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."competences_competence_id_seq";
CREATE SEQUENCE "public"."competences_competence_id_seq" 
INCREMENT 1
MINVALUE  1
MAXVALUE 2147483647
START 1
CACHE 1;

-- ----------------------------
-- Sequence structure for document_likes_document_like_id_seq
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."document_likes_document_like_id_seq";
CREATE SEQUENCE "public"."document_likes_document_like_id_seq" 
INCREMENT 1
MINVALUE  1
MAXVALUE 2147483647
START 1
CACHE 1;

-- ----------------------------
-- Sequence structure for document_text_document_text_id_seq
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."document_text_document_text_id_seq";
CREATE SEQUENCE "public"."document_text_document_text_id_seq" 
INCREMENT 1
MINVALUE  1
MAXVALUE 2147483647
START 1
CACHE 1;

-- ----------------------------
-- Sequence structure for documents_document_id_seq
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."documents_document_id_seq";
CREATE SEQUENCE "public"."documents_document_id_seq" 
INCREMENT 1
MINVALUE  1
MAXVALUE 2147483647
START 1
CACHE 1;

-- ----------------------------
-- Sequence structure for followed_projects_followed_projects_id_seq
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."followed_projects_followed_projects_id_seq";
CREATE SEQUENCE "public"."followed_projects_followed_projects_id_seq" 
INCREMENT 1
MINVALUE  1
MAXVALUE 2147483647
START 1
CACHE 1;

-- ----------------------------
-- Sequence structure for invite_tokens_invite_token_id_seq
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."invite_tokens_invite_token_id_seq";
CREATE SEQUENCE "public"."invite_tokens_invite_token_id_seq" 
INCREMENT 1
MINVALUE  1
MAXVALUE 2147483647
START 1
CACHE 1;

-- ----------------------------
-- Sequence structure for last_seen_documents_last_seen_documents_id_seq
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."last_seen_documents_last_seen_documents_id_seq";
CREATE SEQUENCE "public"."last_seen_documents_last_seen_documents_id_seq" 
INCREMENT 1
MINVALUE  1
MAXVALUE 2147483647
START 1
CACHE 1;

-- ----------------------------
-- Sequence structure for notifications_notification_id_seq
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."notifications_notification_id_seq";
CREATE SEQUENCE "public"."notifications_notification_id_seq" 
INCREMENT 1
MINVALUE  1
MAXVALUE 9223372036854775807
START 1
CACHE 1;

-- ----------------------------
-- Sequence structure for project_roles_project_role_id_seq
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."project_roles_project_role_id_seq";
CREATE SEQUENCE "public"."project_roles_project_role_id_seq" 
INCREMENT 1
MINVALUE  1
MAXVALUE 2147483647
START 1
CACHE 1;

-- ----------------------------
-- Sequence structure for projects_project_id_seq
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."projects_project_id_seq";
CREATE SEQUENCE "public"."projects_project_id_seq" 
INCREMENT 1
MINVALUE  1
MAXVALUE 2147483647
START 1
CACHE 1;

-- ----------------------------
-- Sequence structure for quick_links_quick_link_id_seq
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."quick_links_quick_link_id_seq";
CREATE SEQUENCE "public"."quick_links_quick_link_id_seq" 
INCREMENT 1
MINVALUE  1
MAXVALUE 2147483647
START 1
CACHE 1;

-- ----------------------------
-- Sequence structure for refresh_tokens_refresh_token_id_seq
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."refresh_tokens_refresh_token_id_seq";
CREATE SEQUENCE "public"."refresh_tokens_refresh_token_id_seq" 
INCREMENT 1
MINVALUE  1
MAXVALUE 2147483647
START 1
CACHE 1;

-- ----------------------------
-- Sequence structure for roles_role_id_seq
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."roles_role_id_seq";
CREATE SEQUENCE "public"."roles_role_id_seq" 
INCREMENT 1
MINVALUE  1
MAXVALUE 2147483647
START 1
CACHE 1;

-- ----------------------------
-- Sequence structure for template_text_template_text_id_seq
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."template_text_template_text_id_seq";
CREATE SEQUENCE "public"."template_text_template_text_id_seq" 
INCREMENT 1
MINVALUE  1
MAXVALUE 2147483647
START 1
CACHE 1;

-- ----------------------------
-- Sequence structure for templates_template_id_seq
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."templates_template_id_seq";
CREATE SEQUENCE "public"."templates_template_id_seq" 
INCREMENT 1
MINVALUE  1
MAXVALUE 2147483647
START 1
CACHE 1;

-- ----------------------------
-- Sequence structure for user_competences_competence_id_seq
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."user_competences_competence_id_seq";
CREATE SEQUENCE "public"."user_competences_competence_id_seq" 
INCREMENT 1
MINVALUE  1
MAXVALUE 2147483647
START 1
CACHE 1;

-- ----------------------------
-- Sequence structure for user_competences_user_id_seq
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."user_competences_user_id_seq";
CREATE SEQUENCE "public"."user_competences_user_id_seq" 
INCREMENT 1
MINVALUE  1
MAXVALUE 2147483647
START 1
CACHE 1;

-- ----------------------------
-- Sequence structure for users_user_id_seq
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."users_user_id_seq";
CREATE SEQUENCE "public"."users_user_id_seq" 
INCREMENT 1
MINVALUE  1
MAXVALUE 2147483647
START 1
CACHE 1;

-- ----------------------------
-- Table structure for activity
-- ----------------------------
DROP TABLE IF EXISTS "public"."activity";
CREATE TABLE "public"."activity" (
  "activity_id" int4 NOT NULL DEFAULT nextval('activity_activity_id_seq'::regclass),
  "project_id" int4 NOT NULL,
  "user_id" int4,
  "content" varchar(500) COLLATE "pg_catalog"."default",
  "time_fired" timestamp(6) NOT NULL,
  "meta" json
)
;

-- ----------------------------
-- Table structure for attachments
-- ----------------------------
DROP TABLE IF EXISTS "public"."attachments";
CREATE TABLE "public"."attachments" (
  "attachment_id" int4 NOT NULL DEFAULT nextval('attachments_attachment_id_seq'::regclass),
  "document_id" int4 NOT NULL,
  "user_id" int4,
  "name" varchar(255) COLLATE "pg_catalog"."default",
  "link" varchar(255) COLLATE "pg_catalog"."default",
  "type" varchar(50) COLLATE "pg_catalog"."default",
  "time_created" timestamp(6) NOT NULL
)
;

-- ----------------------------
-- Table structure for bookmarks
-- ----------------------------
DROP TABLE IF EXISTS "public"."bookmarks";
CREATE TABLE "public"."bookmarks" (
  "bookmark_id" int4 NOT NULL DEFAULT nextval('bookmarks_bookmark_id_seq'::regclass),
  "user_id" int4 NOT NULL,
  "document_id" int4 NOT NULL,
  "time_created" timestamp(6) NOT NULL
)
;

-- ----------------------------
-- Table structure for comment_likes
-- ----------------------------
DROP TABLE IF EXISTS "public"."comment_likes";
CREATE TABLE "public"."comment_likes" (
  "comment_like_id" int4 NOT NULL DEFAULT nextval('comment_likes_comment_like_id_seq'::regclass),
  "user_id" int4,
  "comment_id" int4 NOT NULL,
  "time_created" timestamp(6) NOT NULL
)
;

-- ----------------------------
-- Table structure for comments
-- ----------------------------
DROP TABLE IF EXISTS "public"."comments";
CREATE TABLE "public"."comments" (
  "comment_id" int4 NOT NULL DEFAULT nextval('comments_comment_id_seq'::regclass),
  "document_id" int4 NOT NULL,
  "user_id" int4,
  "content" text COLLATE "pg_catalog"."default",
  "time_created" timestamp(6) NOT NULL
)
;

-- ----------------------------
-- Table structure for competences
-- ----------------------------
DROP TABLE IF EXISTS "public"."competences";
CREATE TABLE "public"."competences" (
  "competence_id" int4 NOT NULL DEFAULT nextval('competences_competence_id_seq'::regclass),
  "name" varchar(255) COLLATE "pg_catalog"."default" NOT NULL
)
;

-- ----------------------------
-- Table structure for document_likes
-- ----------------------------
DROP TABLE IF EXISTS "public"."document_likes";
CREATE TABLE "public"."document_likes" (
  "document_like_id" int4 NOT NULL DEFAULT nextval('document_likes_document_like_id_seq'::regclass),
  "user_id" int4 NOT NULL,
  "document_id" int4 NOT NULL,
  "time_created" timestamp(6) NOT NULL
)
;

-- ----------------------------
-- Table structure for document_text
-- ----------------------------
DROP TABLE IF EXISTS "public"."document_text";
CREATE TABLE "public"."document_text" (
  "document_text_id" int4 NOT NULL DEFAULT nextval('document_text_document_text_id_seq'::regclass),
  "document_id" int4 NOT NULL,
  "editor_id" int4,
  "content" text COLLATE "pg_catalog"."default",
  "quill_delta" json,
  "time_updated" timestamp(6),
  "is_actual" bool NOT NULL
)
;

-- ----------------------------
-- Table structure for documents
-- ----------------------------
DROP TABLE IF EXISTS "public"."documents";
CREATE TABLE "public"."documents" (
  "document_id" int4 NOT NULL DEFAULT nextval('documents_document_id_seq'::regclass),
  "parent_document_id" int4,
  "project_id" int4 NOT NULL,
  "creator_id" int4,
  "slug" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "title" varchar(255) COLLATE "pg_catalog"."default",
  "subtitle" varchar(255) COLLATE "pg_catalog"."default",
  "date_created" timestamp(6) NOT NULL,
  "is_draft" bool NOT NULL,
  "document_tsv" tsvector
)
;

-- ----------------------------
-- Table structure for followed_projects
-- ----------------------------
DROP TABLE IF EXISTS "public"."followed_projects";
CREATE TABLE "public"."followed_projects" (
  "followed_projects_id" int4 NOT NULL DEFAULT nextval('followed_projects_followed_projects_id_seq'::regclass),
  "user_id" int4 NOT NULL,
  "project_id" int4 NOT NULL,
  "time_created" timestamp(6) NOT NULL
)
;

-- ----------------------------
-- Table structure for invite_tokens
-- ----------------------------
DROP TABLE IF EXISTS "public"."invite_tokens";
CREATE TABLE "public"."invite_tokens" (
  "invite_token_id" int4 NOT NULL DEFAULT nextval('invite_tokens_invite_token_id_seq'::regclass),
  "token" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "email" varchar(255) COLLATE "pg_catalog"."default",
  "time_created" timestamp(6) NOT NULL
)
;

-- ----------------------------
-- Table structure for last_seen_documents
-- ----------------------------
DROP TABLE IF EXISTS "public"."last_seen_documents";
CREATE TABLE "public"."last_seen_documents" (
  "last_seen_documents_id" int4 NOT NULL DEFAULT nextval('last_seen_documents_last_seen_documents_id_seq'::regclass),
  "user_id" int4 NOT NULL,
  "document_id" int4 NOT NULL,
  "time_created" date NOT NULL
)
;

-- ----------------------------
-- Table structure for notifications
-- ----------------------------
DROP TABLE IF EXISTS "public"."notifications";
CREATE TABLE "public"."notifications" (
  "notification_id" int8 NOT NULL DEFAULT nextval('notifications_notification_id_seq'::regclass),
  "user_id" int4 NOT NULL,
  "content" varchar(500) COLLATE "pg_catalog"."default",
  "time_fired" timestamp(6) NOT NULL,
  "meta" json
)
;

-- ----------------------------
-- Table structure for notifications_read
-- ----------------------------
DROP TABLE IF EXISTS "public"."notifications_read";
CREATE TABLE "public"."notifications_read" (
  "user_id" int4 NOT NULL,
  "time_read" timestamp(6) NOT NULL
)
;

-- ----------------------------
-- Table structure for permissions
-- ----------------------------
DROP TABLE IF EXISTS "public"."permissions";
CREATE TABLE "public"."permissions" (
  "permission_slug" varchar(100) COLLATE "pg_catalog"."default" NOT NULL,
  "name" varchar(255) COLLATE "pg_catalog"."default" NOT NULL
)
;

-- ----------------------------
-- Table structure for project_permissions
-- ----------------------------
DROP TABLE IF EXISTS "public"."project_permissions";
CREATE TABLE "public"."project_permissions" (
  "project_permission_slug" varchar(100) COLLATE "pg_catalog"."default" NOT NULL,
  "name" varchar(255) COLLATE "pg_catalog"."default" NOT NULL
)
;

-- ----------------------------
-- Table structure for project_role_permissions
-- ----------------------------
DROP TABLE IF EXISTS "public"."project_role_permissions";
CREATE TABLE "public"."project_role_permissions" (
  "project_role_id" int4 NOT NULL,
  "project_permission_slug" varchar(100) COLLATE "pg_catalog"."default" NOT NULL
)
;

-- ----------------------------
-- Table structure for project_roles
-- ----------------------------
DROP TABLE IF EXISTS "public"."project_roles";
CREATE TABLE "public"."project_roles" (
  "project_role_id" int4 NOT NULL DEFAULT nextval('project_roles_project_role_id_seq'::regclass),
  "name" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "system" bool NOT NULL
)
;

-- ----------------------------
-- Table structure for project_team
-- ----------------------------
DROP TABLE IF EXISTS "public"."project_team";
CREATE TABLE "public"."project_team" (
  "user_id" int4 NOT NULL,
  "project_id" int4 NOT NULL,
  "project_role_id" int4 NOT NULL,
  "date_joined" timestamp(6) NOT NULL,
  "position" varchar(255) COLLATE "pg_catalog"."default"
)
;

-- ----------------------------
-- Table structure for projects
-- ----------------------------
DROP TABLE IF EXISTS "public"."projects";
CREATE TABLE "public"."projects" (
  "project_id" int4 NOT NULL DEFAULT nextval('projects_project_id_seq'::regclass),
  "slug" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "name" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "description" text COLLATE "pg_catalog"."default",
  "goal" varchar(255) COLLATE "pg_catalog"."default",
  "date_start" date,
  "date_end" date,
  "avatar" varchar(255) COLLATE "pg_catalog"."default",
  "is_open" bool NOT NULL,
  "is_active" bool NOT NULL
)
;

-- ----------------------------
-- Table structure for quick_links
-- ----------------------------
DROP TABLE IF EXISTS "public"."quick_links";
CREATE TABLE "public"."quick_links" (
  "quick_link_id" int4 NOT NULL DEFAULT nextval('quick_links_quick_link_id_seq'::regclass),
  "housing_project_id" int4,
  "project_id" int4,
  "document_id" int4,
  "user_id" int4,
  "external_link" varchar(255) COLLATE "pg_catalog"."default",
  "name" varchar(255) COLLATE "pg_catalog"."default"
)
;

-- ----------------------------
-- Table structure for refresh_tokens
-- ----------------------------
DROP TABLE IF EXISTS "public"."refresh_tokens";
CREATE TABLE "public"."refresh_tokens" (
  "refresh_token_id" int4 NOT NULL DEFAULT nextval('refresh_tokens_refresh_token_id_seq'::regclass),
  "user_id" int4 NOT NULL,
  "token" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "revoked" bool NOT NULL,
  "time_created" timestamp(6) NOT NULL
)
;

-- ----------------------------
-- Table structure for role_permissions
-- ----------------------------
DROP TABLE IF EXISTS "public"."role_permissions";
CREATE TABLE "public"."role_permissions" (
  "role_id" int4 NOT NULL,
  "permission_slug" varchar(100) COLLATE "pg_catalog"."default" NOT NULL
)
;

-- ----------------------------
-- Table structure for roles
-- ----------------------------
DROP TABLE IF EXISTS "public"."roles";
CREATE TABLE "public"."roles" (
  "role_id" int4 NOT NULL DEFAULT nextval('roles_role_id_seq'::regclass),
  "name" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "system" bool NOT NULL
)
;

-- ----------------------------
-- Table structure for template_text
-- ----------------------------
DROP TABLE IF EXISTS "public"."template_text";
CREATE TABLE "public"."template_text" (
  "template_text_id" int4 NOT NULL DEFAULT nextval('template_text_template_text_id_seq'::regclass),
  "template_id" int4 NOT NULL,
  "editor_id" int4,
  "content" text COLLATE "pg_catalog"."default",
  "quill_delta" json,
  "time_updated" timestamp(6),
  "is_actual" bool NOT NULL
)
;

-- ----------------------------
-- Table structure for template_types
-- ----------------------------
DROP TABLE IF EXISTS "public"."template_types";
CREATE TABLE "public"."template_types" (
  "template_type_slug" varchar(100) COLLATE "pg_catalog"."default" NOT NULL,
  "name" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "system" bool NOT NULL
)
;

-- ----------------------------
-- Table structure for templates
-- ----------------------------
DROP TABLE IF EXISTS "public"."templates";
CREATE TABLE "public"."templates" (
  "template_id" int4 NOT NULL DEFAULT nextval('templates_template_id_seq'::regclass),
  "template_type_slug" varchar(100) COLLATE "pg_catalog"."default" NOT NULL,
  "project_id" int4,
  "creator_id" int4,
  "slug" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "title" varchar(255) COLLATE "pg_catalog"."default",
  "description" text COLLATE "pg_catalog"."default",
  "date_created" timestamp(6) NOT NULL,
  "template_tsv" tsvector
)
;

-- ----------------------------
-- Table structure for user_competences
-- ----------------------------
DROP TABLE IF EXISTS "public"."user_competences";
CREATE TABLE "public"."user_competences" (
  "competence_id" int4 NOT NULL DEFAULT nextval('user_competences_competence_id_seq'::regclass),
  "user_id" int4 NOT NULL DEFAULT nextval('user_competences_user_id_seq'::regclass)
)
;

-- ----------------------------
-- Table structure for user_roles
-- ----------------------------
DROP TABLE IF EXISTS "public"."user_roles";
CREATE TABLE "public"."user_roles" (
  "user_id" int4 NOT NULL,
  "role_id" int4 NOT NULL
)
;

-- ----------------------------
-- Table structure for users
-- ----------------------------
DROP TABLE IF EXISTS "public"."users";
CREATE TABLE "public"."users" (
  "user_id" int4 NOT NULL DEFAULT nextval('users_user_id_seq'::regclass),
  "name" varchar(255) COLLATE "pg_catalog"."default",
  "surname" varchar(255) COLLATE "pg_catalog"."default",
  "email" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "password" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "avatar" varchar(255) COLLATE "pg_catalog"."default" DEFAULT NULL::character varying,
  "date_registered" timestamp(6)
)
;

-- ----------------------------
-- Function structure for get_gin_tsvector
-- ----------------------------
DROP FUNCTION IF EXISTS "public"."get_gin_tsvector"("title" text, "subtitle" text, "content" text);
CREATE OR REPLACE FUNCTION "public"."get_gin_tsvector"("title" text, "subtitle" text, "content" text)
  RETURNS "pg_catalog"."tsvector" AS $BODY$
    SELECT setweight(to_tsvector(coalesce($1, '')), 'A') || setweight(to_tsvector(coalesce($2, '')), 'C') || setweight(to_tsvector(coalesce($3, '')), 'B');
$BODY$
  LANGUAGE sql IMMUTABLE
  COST 100;

-- ----------------------------
-- Function structure for gin_extract_query_trgm
-- ----------------------------
DROP FUNCTION IF EXISTS "public"."gin_extract_query_trgm"(text, internal, int2, internal, internal, internal, internal);
CREATE OR REPLACE FUNCTION "public"."gin_extract_query_trgm"(text, internal, int2, internal, internal, internal, internal)
  RETURNS "pg_catalog"."internal" AS '$libdir/pg_trgm', 'gin_extract_query_trgm'
  LANGUAGE c IMMUTABLE STRICT
  COST 1;

-- ----------------------------
-- Function structure for gin_extract_value_trgm
-- ----------------------------
DROP FUNCTION IF EXISTS "public"."gin_extract_value_trgm"(text, internal);
CREATE OR REPLACE FUNCTION "public"."gin_extract_value_trgm"(text, internal)
  RETURNS "pg_catalog"."internal" AS '$libdir/pg_trgm', 'gin_extract_value_trgm'
  LANGUAGE c IMMUTABLE STRICT
  COST 1;

-- ----------------------------
-- Function structure for gin_trgm_consistent
-- ----------------------------
DROP FUNCTION IF EXISTS "public"."gin_trgm_consistent"(internal, int2, text, int4, internal, internal, internal, internal);
CREATE OR REPLACE FUNCTION "public"."gin_trgm_consistent"(internal, int2, text, int4, internal, internal, internal, internal)
  RETURNS "pg_catalog"."bool" AS '$libdir/pg_trgm', 'gin_trgm_consistent'
  LANGUAGE c IMMUTABLE STRICT
  COST 1;

-- ----------------------------
-- Function structure for gin_trgm_triconsistent
-- ----------------------------
DROP FUNCTION IF EXISTS "public"."gin_trgm_triconsistent"(internal, int2, text, int4, internal, internal, internal);
CREATE OR REPLACE FUNCTION "public"."gin_trgm_triconsistent"(internal, int2, text, int4, internal, internal, internal)
  RETURNS "pg_catalog"."char" AS '$libdir/pg_trgm', 'gin_trgm_triconsistent'
  LANGUAGE c IMMUTABLE STRICT
  COST 1;

-- ----------------------------
-- Function structure for gtrgm_compress
-- ----------------------------
DROP FUNCTION IF EXISTS "public"."gtrgm_compress"(internal);
CREATE OR REPLACE FUNCTION "public"."gtrgm_compress"(internal)
  RETURNS "pg_catalog"."internal" AS '$libdir/pg_trgm', 'gtrgm_compress'
  LANGUAGE c IMMUTABLE STRICT
  COST 1;

-- ----------------------------
-- Function structure for gtrgm_consistent
-- ----------------------------
DROP FUNCTION IF EXISTS "public"."gtrgm_consistent"(internal, text, int2, oid, internal);
CREATE OR REPLACE FUNCTION "public"."gtrgm_consistent"(internal, text, int2, oid, internal)
  RETURNS "pg_catalog"."bool" AS '$libdir/pg_trgm', 'gtrgm_consistent'
  LANGUAGE c IMMUTABLE STRICT
  COST 1;

-- ----------------------------
-- Function structure for gtrgm_decompress
-- ----------------------------
DROP FUNCTION IF EXISTS "public"."gtrgm_decompress"(internal);
CREATE OR REPLACE FUNCTION "public"."gtrgm_decompress"(internal)
  RETURNS "pg_catalog"."internal" AS '$libdir/pg_trgm', 'gtrgm_decompress'
  LANGUAGE c IMMUTABLE STRICT
  COST 1;

-- ----------------------------
-- Function structure for gtrgm_distance
-- ----------------------------
DROP FUNCTION IF EXISTS "public"."gtrgm_distance"(internal, text, int2, oid, internal);
CREATE OR REPLACE FUNCTION "public"."gtrgm_distance"(internal, text, int2, oid, internal)
  RETURNS "pg_catalog"."float8" AS '$libdir/pg_trgm', 'gtrgm_distance'
  LANGUAGE c IMMUTABLE STRICT
  COST 1;

-- ----------------------------
-- Function structure for gtrgm_in
-- ----------------------------
DROP FUNCTION IF EXISTS "public"."gtrgm_in"(cstring);
CREATE OR REPLACE FUNCTION "public"."gtrgm_in"(cstring)
  RETURNS "public"."gtrgm" AS '$libdir/pg_trgm', 'gtrgm_in'
  LANGUAGE c IMMUTABLE STRICT
  COST 1;

-- ----------------------------
-- Function structure for gtrgm_out
-- ----------------------------
DROP FUNCTION IF EXISTS "public"."gtrgm_out"("public"."gtrgm");
CREATE OR REPLACE FUNCTION "public"."gtrgm_out"("public"."gtrgm")
  RETURNS "pg_catalog"."cstring" AS '$libdir/pg_trgm', 'gtrgm_out'
  LANGUAGE c IMMUTABLE STRICT
  COST 1;

-- ----------------------------
-- Function structure for gtrgm_penalty
-- ----------------------------
DROP FUNCTION IF EXISTS "public"."gtrgm_penalty"(internal, internal, internal);
CREATE OR REPLACE FUNCTION "public"."gtrgm_penalty"(internal, internal, internal)
  RETURNS "pg_catalog"."internal" AS '$libdir/pg_trgm', 'gtrgm_penalty'
  LANGUAGE c IMMUTABLE STRICT
  COST 1;

-- ----------------------------
-- Function structure for gtrgm_picksplit
-- ----------------------------
DROP FUNCTION IF EXISTS "public"."gtrgm_picksplit"(internal, internal);
CREATE OR REPLACE FUNCTION "public"."gtrgm_picksplit"(internal, internal)
  RETURNS "pg_catalog"."internal" AS '$libdir/pg_trgm', 'gtrgm_picksplit'
  LANGUAGE c IMMUTABLE STRICT
  COST 1;

-- ----------------------------
-- Function structure for gtrgm_same
-- ----------------------------
DROP FUNCTION IF EXISTS "public"."gtrgm_same"("public"."gtrgm", "public"."gtrgm", internal);
CREATE OR REPLACE FUNCTION "public"."gtrgm_same"("public"."gtrgm", "public"."gtrgm", internal)
  RETURNS "pg_catalog"."internal" AS '$libdir/pg_trgm', 'gtrgm_same'
  LANGUAGE c IMMUTABLE STRICT
  COST 1;

-- ----------------------------
-- Function structure for gtrgm_union
-- ----------------------------
DROP FUNCTION IF EXISTS "public"."gtrgm_union"(internal, internal);
CREATE OR REPLACE FUNCTION "public"."gtrgm_union"(internal, internal)
  RETURNS "public"."gtrgm" AS '$libdir/pg_trgm', 'gtrgm_union'
  LANGUAGE c IMMUTABLE STRICT
  COST 1;

-- ----------------------------
-- Function structure for set_limit
-- ----------------------------
DROP FUNCTION IF EXISTS "public"."set_limit"(float4);
CREATE OR REPLACE FUNCTION "public"."set_limit"(float4)
  RETURNS "pg_catalog"."float4" AS '$libdir/pg_trgm', 'set_limit'
  LANGUAGE c VOLATILE STRICT
  COST 1;

-- ----------------------------
-- Function structure for show_limit
-- ----------------------------
DROP FUNCTION IF EXISTS "public"."show_limit"();
CREATE OR REPLACE FUNCTION "public"."show_limit"()
  RETURNS "pg_catalog"."float4" AS '$libdir/pg_trgm', 'show_limit'
  LANGUAGE c STABLE STRICT
  COST 1;

-- ----------------------------
-- Function structure for show_trgm
-- ----------------------------
DROP FUNCTION IF EXISTS "public"."show_trgm"(text);
CREATE OR REPLACE FUNCTION "public"."show_trgm"(text)
  RETURNS "pg_catalog"."_text" AS '$libdir/pg_trgm', 'show_trgm'
  LANGUAGE c IMMUTABLE STRICT
  COST 1;

-- ----------------------------
-- Function structure for similarity
-- ----------------------------
DROP FUNCTION IF EXISTS "public"."similarity"(text, text);
CREATE OR REPLACE FUNCTION "public"."similarity"(text, text)
  RETURNS "pg_catalog"."float4" AS '$libdir/pg_trgm', 'similarity'
  LANGUAGE c IMMUTABLE STRICT
  COST 1;

-- ----------------------------
-- Function structure for similarity_dist
-- ----------------------------
DROP FUNCTION IF EXISTS "public"."similarity_dist"(text, text);
CREATE OR REPLACE FUNCTION "public"."similarity_dist"(text, text)
  RETURNS "pg_catalog"."float4" AS '$libdir/pg_trgm', 'similarity_dist'
  LANGUAGE c IMMUTABLE STRICT
  COST 1;

-- ----------------------------
-- Function structure for similarity_op
-- ----------------------------
DROP FUNCTION IF EXISTS "public"."similarity_op"(text, text);
CREATE OR REPLACE FUNCTION "public"."similarity_op"(text, text)
  RETURNS "pg_catalog"."bool" AS '$libdir/pg_trgm', 'similarity_op'
  LANGUAGE c STABLE STRICT
  COST 1;

-- ----------------------------
-- Function structure for trigger_document_text_before_insert
-- ----------------------------
DROP FUNCTION IF EXISTS "public"."trigger_document_text_before_insert"();
CREATE OR REPLACE FUNCTION "public"."trigger_document_text_before_insert"()
  RETURNS "pg_catalog"."trigger" AS $BODY$
begin

update document_text set is_actual = false where document_id = new.document_id;
new.is_actual = true;
	
return new;
end;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;

-- ----------------------------
-- Function structure for trigger_documents_update_search
-- ----------------------------
DROP FUNCTION IF EXISTS "public"."trigger_documents_update_search"();
CREATE OR REPLACE FUNCTION "public"."trigger_documents_update_search"()
  RETURNS "pg_catalog"."trigger" AS $BODY$
declare
content_var text;
begin
	
content_var = (select content from document_text where document_id = new.document_id and is_actual = true limit 1);
	
update documents
set document_tsv = setweight(to_tsvector(coalesce(title, '')), 'A') || setweight(to_tsvector(coalesce(subtitle, '')), 'C') || setweight(to_tsvector(coalesce(content_var, '')), 'B')
where document_id = new.document_id;
	
return new;
end;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;

-- ----------------------------
-- Function structure for trigger_template_text_before_insert
-- ----------------------------
DROP FUNCTION IF EXISTS "public"."trigger_template_text_before_insert"();
CREATE OR REPLACE FUNCTION "public"."trigger_template_text_before_insert"()
  RETURNS "pg_catalog"."trigger" AS $BODY$
begin

update template_text set is_actual = false where template_id = new.template_id;
new.is_actual = true;
	
return new;
end;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;

-- ----------------------------
-- Function structure for trigger_templates_update_search
-- ----------------------------
DROP FUNCTION IF EXISTS "public"."trigger_templates_update_search"();
CREATE OR REPLACE FUNCTION "public"."trigger_templates_update_search"()
  RETURNS "pg_catalog"."trigger" AS $BODY$
declare
content_var text;
begin
	
content_var = (select content from template_text where template_id = new.template_id and is_actual = true limit 1);
	
update templates
set template_tsv = setweight(to_tsvector(coalesce(title, '')), 'A') || setweight(to_tsvector(coalesce(description, '')), 'C') || setweight(to_tsvector(coalesce(content_var, '')), 'B')
where template_id = new.template_id;
	
return new;
end;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;

-- ----------------------------
-- Function structure for unaccent
-- ----------------------------
DROP FUNCTION IF EXISTS "public"."unaccent"(text);
CREATE OR REPLACE FUNCTION "public"."unaccent"(text)
  RETURNS "pg_catalog"."text" AS '$libdir/unaccent', 'unaccent_dict'
  LANGUAGE c STABLE STRICT
  COST 1;

-- ----------------------------
-- Function structure for unaccent
-- ----------------------------
DROP FUNCTION IF EXISTS "public"."unaccent"(regdictionary, text);
CREATE OR REPLACE FUNCTION "public"."unaccent"(regdictionary, text)
  RETURNS "pg_catalog"."text" AS '$libdir/unaccent', 'unaccent_dict'
  LANGUAGE c STABLE STRICT
  COST 1;

-- ----------------------------
-- Function structure for unaccent_init
-- ----------------------------
DROP FUNCTION IF EXISTS "public"."unaccent_init"(internal);
CREATE OR REPLACE FUNCTION "public"."unaccent_init"(internal)
  RETURNS "pg_catalog"."internal" AS '$libdir/unaccent', 'unaccent_init'
  LANGUAGE c VOLATILE
  COST 1;

-- ----------------------------
-- Function structure for unaccent_lexize
-- ----------------------------
DROP FUNCTION IF EXISTS "public"."unaccent_lexize"(internal, internal, internal, internal);
CREATE OR REPLACE FUNCTION "public"."unaccent_lexize"(internal, internal, internal, internal)
  RETURNS "pg_catalog"."internal" AS '$libdir/unaccent', 'unaccent_lexize'
  LANGUAGE c VOLATILE
  COST 1;

-- ----------------------------
-- Function structure for word_similarity
-- ----------------------------
DROP FUNCTION IF EXISTS "public"."word_similarity"(text, text);
CREATE OR REPLACE FUNCTION "public"."word_similarity"(text, text)
  RETURNS "pg_catalog"."float4" AS '$libdir/pg_trgm', 'word_similarity'
  LANGUAGE c IMMUTABLE STRICT
  COST 1;

-- ----------------------------
-- Function structure for word_similarity_commutator_op
-- ----------------------------
DROP FUNCTION IF EXISTS "public"."word_similarity_commutator_op"(text, text);
CREATE OR REPLACE FUNCTION "public"."word_similarity_commutator_op"(text, text)
  RETURNS "pg_catalog"."bool" AS '$libdir/pg_trgm', 'word_similarity_commutator_op'
  LANGUAGE c STABLE STRICT
  COST 1;

-- ----------------------------
-- Function structure for word_similarity_dist_commutator_op
-- ----------------------------
DROP FUNCTION IF EXISTS "public"."word_similarity_dist_commutator_op"(text, text);
CREATE OR REPLACE FUNCTION "public"."word_similarity_dist_commutator_op"(text, text)
  RETURNS "pg_catalog"."float4" AS '$libdir/pg_trgm', 'word_similarity_dist_commutator_op'
  LANGUAGE c IMMUTABLE STRICT
  COST 1;

-- ----------------------------
-- Function structure for word_similarity_dist_op
-- ----------------------------
DROP FUNCTION IF EXISTS "public"."word_similarity_dist_op"(text, text);
CREATE OR REPLACE FUNCTION "public"."word_similarity_dist_op"(text, text)
  RETURNS "pg_catalog"."float4" AS '$libdir/pg_trgm', 'word_similarity_dist_op'
  LANGUAGE c IMMUTABLE STRICT
  COST 1;

-- ----------------------------
-- Function structure for word_similarity_op
-- ----------------------------
DROP FUNCTION IF EXISTS "public"."word_similarity_op"(text, text);
CREATE OR REPLACE FUNCTION "public"."word_similarity_op"(text, text)
  RETURNS "pg_catalog"."bool" AS '$libdir/pg_trgm', 'word_similarity_op'
  LANGUAGE c STABLE STRICT
  COST 1;

-- ----------------------------
-- Alter sequences owned by
-- ----------------------------
ALTER SEQUENCE "public"."activity_activity_id_seq"
OWNED BY "public"."activity"."activity_id";
SELECT setval('"public"."activity_activity_id_seq"', 2, false);
ALTER SEQUENCE "public"."attachments_attachment_id_seq"
OWNED BY "public"."attachments"."attachment_id";
SELECT setval('"public"."attachments_attachment_id_seq"', 8, true);
ALTER SEQUENCE "public"."bookmarks_bookmark_id_seq"
OWNED BY "public"."bookmarks"."bookmark_id";
SELECT setval('"public"."bookmarks_bookmark_id_seq"', 2, false);
ALTER SEQUENCE "public"."comment_likes_comment_like_id_seq"
OWNED BY "public"."comment_likes"."comment_like_id";
SELECT setval('"public"."comment_likes_comment_like_id_seq"', 2, false);
ALTER SEQUENCE "public"."comments_comment_id_seq"
OWNED BY "public"."comments"."comment_id";
SELECT setval('"public"."comments_comment_id_seq"', 2, false);
ALTER SEQUENCE "public"."competences_competence_id_seq"
OWNED BY "public"."competences"."competence_id";
SELECT setval('"public"."competences_competence_id_seq"', 1002, true);
ALTER SEQUENCE "public"."document_likes_document_like_id_seq"
OWNED BY "public"."document_likes"."document_like_id";
SELECT setval('"public"."document_likes_document_like_id_seq"', 2, false);
ALTER SEQUENCE "public"."document_text_document_text_id_seq"
OWNED BY "public"."document_text"."document_text_id";
SELECT setval('"public"."document_text_document_text_id_seq"', 6, true);
ALTER SEQUENCE "public"."documents_document_id_seq"
OWNED BY "public"."documents"."document_id";
SELECT setval('"public"."documents_document_id_seq"', 3, true);
ALTER SEQUENCE "public"."followed_projects_followed_projects_id_seq"
OWNED BY "public"."followed_projects"."followed_projects_id";
SELECT setval('"public"."followed_projects_followed_projects_id_seq"', 2, true);
ALTER SEQUENCE "public"."invite_tokens_invite_token_id_seq"
OWNED BY "public"."invite_tokens"."invite_token_id";
SELECT setval('"public"."invite_tokens_invite_token_id_seq"', 3, true);
ALTER SEQUENCE "public"."last_seen_documents_last_seen_documents_id_seq"
OWNED BY "public"."last_seen_documents"."last_seen_documents_id";
SELECT setval('"public"."last_seen_documents_last_seen_documents_id_seq"', 2, false);
ALTER SEQUENCE "public"."notifications_notification_id_seq"
OWNED BY "public"."notifications"."notification_id";
SELECT setval('"public"."notifications_notification_id_seq"', 10001, true);
ALTER SEQUENCE "public"."project_roles_project_role_id_seq"
OWNED BY "public"."project_roles"."project_role_id";
SELECT setval('"public"."project_roles_project_role_id_seq"', 3, true);
ALTER SEQUENCE "public"."projects_project_id_seq"
OWNED BY "public"."projects"."project_id";
SELECT setval('"public"."projects_project_id_seq"', 2, true);
ALTER SEQUENCE "public"."quick_links_quick_link_id_seq"
OWNED BY "public"."quick_links"."quick_link_id";
SELECT setval('"public"."quick_links_quick_link_id_seq"', 2, false);
ALTER SEQUENCE "public"."refresh_tokens_refresh_token_id_seq"
OWNED BY "public"."refresh_tokens"."refresh_token_id";
SELECT setval('"public"."refresh_tokens_refresh_token_id_seq"', 96, true);
ALTER SEQUENCE "public"."roles_role_id_seq"
OWNED BY "public"."roles"."role_id";
SELECT setval('"public"."roles_role_id_seq"', 5, true);
ALTER SEQUENCE "public"."template_text_template_text_id_seq"
OWNED BY "public"."template_text"."template_text_id";
SELECT setval('"public"."template_text_template_text_id_seq"', 11, true);
ALTER SEQUENCE "public"."templates_template_id_seq"
OWNED BY "public"."templates"."template_id";
SELECT setval('"public"."templates_template_id_seq"', 7, true);
ALTER SEQUENCE "public"."user_competences_competence_id_seq"
OWNED BY "public"."user_competences"."competence_id";
SELECT setval('"public"."user_competences_competence_id_seq"', 2, false);
ALTER SEQUENCE "public"."user_competences_user_id_seq"
OWNED BY "public"."user_competences"."user_id";
SELECT setval('"public"."user_competences_user_id_seq"', 2, false);
ALTER SEQUENCE "public"."users_user_id_seq"
OWNED BY "public"."users"."user_id";
SELECT setval('"public"."users_user_id_seq"', 101205, true);

-- ----------------------------
-- Primary Key structure for table activity
-- ----------------------------
ALTER TABLE "public"."activity" ADD CONSTRAINT "activity_pkey" PRIMARY KEY ("activity_id");

-- ----------------------------
-- Primary Key structure for table attachments
-- ----------------------------
ALTER TABLE "public"."attachments" ADD CONSTRAINT "attachments_pkey" PRIMARY KEY ("attachment_id");

-- ----------------------------
-- Primary Key structure for table bookmarks
-- ----------------------------
ALTER TABLE "public"."bookmarks" ADD CONSTRAINT "bookmarks_pkey" PRIMARY KEY ("bookmark_id");

-- ----------------------------
-- Primary Key structure for table comment_likes
-- ----------------------------
ALTER TABLE "public"."comment_likes" ADD CONSTRAINT "comment_likes_pkey" PRIMARY KEY ("comment_like_id");

-- ----------------------------
-- Indexes structure for table comments
-- ----------------------------
CREATE INDEX "comment_content_idx" ON "public"."comments" USING gin (
  "content" COLLATE "pg_catalog"."default" "public"."gin_trgm_ops"
);

-- ----------------------------
-- Primary Key structure for table comments
-- ----------------------------
ALTER TABLE "public"."comments" ADD CONSTRAINT "comments_pkey" PRIMARY KEY ("comment_id");

-- ----------------------------
-- Indexes structure for table competences
-- ----------------------------
CREATE INDEX "competence_name_idx" ON "public"."competences" USING gin (
  "name" COLLATE "pg_catalog"."default" "public"."gin_trgm_ops"
);

-- ----------------------------
-- Primary Key structure for table competences
-- ----------------------------
ALTER TABLE "public"."competences" ADD CONSTRAINT "competences_pkey" PRIMARY KEY ("competence_id");

-- ----------------------------
-- Primary Key structure for table document_likes
-- ----------------------------
ALTER TABLE "public"."document_likes" ADD CONSTRAINT "document_likes_pkey" PRIMARY KEY ("document_like_id");

-- ----------------------------
-- Triggers structure for table document_text
-- ----------------------------
CREATE TRIGGER "document_before_insert_trigger" BEFORE INSERT ON "public"."document_text"
FOR EACH ROW
EXECUTE PROCEDURE "public"."trigger_document_text_before_insert"();
CREATE TRIGGER "document_update_content_trigger" AFTER INSERT OR UPDATE OF "content" ON "public"."document_text"
FOR EACH ROW
EXECUTE PROCEDURE "public"."trigger_documents_update_search"();

-- ----------------------------
-- Primary Key structure for table document_text
-- ----------------------------
ALTER TABLE "public"."document_text" ADD CONSTRAINT "document_text_pkey" PRIMARY KEY ("document_text_id");

-- ----------------------------
-- Indexes structure for table documents
-- ----------------------------
CREATE INDEX "document_searching_idx" ON "public"."documents" USING gin (
  "document_tsv" "pg_catalog"."tsvector_ops"
);

-- ----------------------------
-- Triggers structure for table documents
-- ----------------------------
CREATE TRIGGER "document_update_trigger" AFTER INSERT OR UPDATE OF "title", "subtitle" ON "public"."documents"
FOR EACH ROW
EXECUTE PROCEDURE "public"."trigger_documents_update_search"();

-- ----------------------------
-- Uniques structure for table documents
-- ----------------------------
ALTER TABLE "public"."documents" ADD CONSTRAINT "document_slug" UNIQUE ("slug");

-- ----------------------------
-- Primary Key structure for table documents
-- ----------------------------
ALTER TABLE "public"."documents" ADD CONSTRAINT "documents_pkey" PRIMARY KEY ("document_id");

-- ----------------------------
-- Primary Key structure for table followed_projects
-- ----------------------------
ALTER TABLE "public"."followed_projects" ADD CONSTRAINT "followed_projects_pkey" PRIMARY KEY ("followed_projects_id");

-- ----------------------------
-- Primary Key structure for table invite_tokens
-- ----------------------------
ALTER TABLE "public"."invite_tokens" ADD CONSTRAINT "invite_tokens_pkey" PRIMARY KEY ("invite_token_id");

-- ----------------------------
-- Primary Key structure for table last_seen_documents
-- ----------------------------
ALTER TABLE "public"."last_seen_documents" ADD CONSTRAINT "last_seen_documents_pkey" PRIMARY KEY ("last_seen_documents_id");

-- ----------------------------
-- Primary Key structure for table notifications
-- ----------------------------
ALTER TABLE "public"."notifications" ADD CONSTRAINT "notifications_pkey" PRIMARY KEY ("notification_id");

-- ----------------------------
-- Primary Key structure for table notifications_read
-- ----------------------------
ALTER TABLE "public"."notifications_read" ADD CONSTRAINT "notifications_read_pkey" PRIMARY KEY ("user_id");

-- ----------------------------
-- Primary Key structure for table permissions
-- ----------------------------
ALTER TABLE "public"."permissions" ADD CONSTRAINT "permissions_pkey" PRIMARY KEY ("permission_slug");

-- ----------------------------
-- Primary Key structure for table project_permissions
-- ----------------------------
ALTER TABLE "public"."project_permissions" ADD CONSTRAINT "project_permissions_pkey" PRIMARY KEY ("project_permission_slug");

-- ----------------------------
-- Primary Key structure for table project_role_permissions
-- ----------------------------
ALTER TABLE "public"."project_role_permissions" ADD CONSTRAINT "project_role_permissions_pkey" PRIMARY KEY ("project_role_id", "project_permission_slug");

-- ----------------------------
-- Primary Key structure for table project_roles
-- ----------------------------
ALTER TABLE "public"."project_roles" ADD CONSTRAINT "project_roles_pkey" PRIMARY KEY ("project_role_id");

-- ----------------------------
-- Primary Key structure for table project_team
-- ----------------------------
ALTER TABLE "public"."project_team" ADD CONSTRAINT "project_team_pkey" PRIMARY KEY ("user_id", "project_id");

-- ----------------------------
-- Indexes structure for table projects
-- ----------------------------
CREATE INDEX "projects_name_idx" ON "public"."projects" USING gin (
  "name" COLLATE "pg_catalog"."default" "public"."gin_trgm_ops"
);

-- ----------------------------
-- Uniques structure for table projects
-- ----------------------------
ALTER TABLE "public"."projects" ADD CONSTRAINT "project_slug" UNIQUE ("slug");

-- ----------------------------
-- Primary Key structure for table projects
-- ----------------------------
ALTER TABLE "public"."projects" ADD CONSTRAINT "projects_pkey" PRIMARY KEY ("project_id");

-- ----------------------------
-- Indexes structure for table quick_links
-- ----------------------------
CREATE INDEX "quick_link_name_idx" ON "public"."quick_links" USING gin (
  "name" COLLATE "pg_catalog"."default" "public"."gin_trgm_ops"
);

-- ----------------------------
-- Primary Key structure for table quick_links
-- ----------------------------
ALTER TABLE "public"."quick_links" ADD CONSTRAINT "quick_links_pkey" PRIMARY KEY ("quick_link_id");

-- ----------------------------
-- Primary Key structure for table refresh_tokens
-- ----------------------------
ALTER TABLE "public"."refresh_tokens" ADD CONSTRAINT "refresh_tokens_pkey" PRIMARY KEY ("refresh_token_id");

-- ----------------------------
-- Primary Key structure for table role_permissions
-- ----------------------------
ALTER TABLE "public"."role_permissions" ADD CONSTRAINT "role_permissions_pkey" PRIMARY KEY ("role_id", "permission_slug");

-- ----------------------------
-- Primary Key structure for table roles
-- ----------------------------
ALTER TABLE "public"."roles" ADD CONSTRAINT "roles_pkey" PRIMARY KEY ("role_id");

-- ----------------------------
-- Triggers structure for table template_text
-- ----------------------------
CREATE TRIGGER "template_text_before_insert_trigger" BEFORE INSERT ON "public"."template_text"
FOR EACH ROW
EXECUTE PROCEDURE "public"."trigger_template_text_before_insert"();
CREATE TRIGGER "template_update_content_trigger" AFTER INSERT OR UPDATE OF "content" ON "public"."template_text"
FOR EACH ROW
EXECUTE PROCEDURE "public"."trigger_templates_update_search"();

-- ----------------------------
-- Primary Key structure for table template_text
-- ----------------------------
ALTER TABLE "public"."template_text" ADD CONSTRAINT "template_text_pkey" PRIMARY KEY ("template_text_id");

-- ----------------------------
-- Indexes structure for table template_types
-- ----------------------------
CREATE INDEX "template_type_idx" ON "public"."template_types" USING gin (
  "name" COLLATE "pg_catalog"."default" "public"."gin_trgm_ops"
);

-- ----------------------------
-- Primary Key structure for table template_types
-- ----------------------------
ALTER TABLE "public"."template_types" ADD CONSTRAINT "template_types_pkey" PRIMARY KEY ("template_type_slug");

-- ----------------------------
-- Indexes structure for table templates
-- ----------------------------
CREATE INDEX "template_searching_idx" ON "public"."templates" USING gin (
  "template_tsv" "pg_catalog"."tsvector_ops"
);

-- ----------------------------
-- Triggers structure for table templates
-- ----------------------------
CREATE TRIGGER "template_update_trigger" AFTER INSERT OR UPDATE OF "title", "description" ON "public"."templates"
FOR EACH ROW
EXECUTE PROCEDURE "public"."trigger_templates_update_search"();

-- ----------------------------
-- Uniques structure for table templates
-- ----------------------------
ALTER TABLE "public"."templates" ADD CONSTRAINT "template_slug" UNIQUE ("slug");

-- ----------------------------
-- Primary Key structure for table templates
-- ----------------------------
ALTER TABLE "public"."templates" ADD CONSTRAINT "templates_pkey" PRIMARY KEY ("template_id");

-- ----------------------------
-- Primary Key structure for table user_competences
-- ----------------------------
ALTER TABLE "public"."user_competences" ADD CONSTRAINT "user_competences_pkey" PRIMARY KEY ("competence_id", "user_id");

-- ----------------------------
-- Primary Key structure for table user_roles
-- ----------------------------
ALTER TABLE "public"."user_roles" ADD CONSTRAINT "user_roles_pkey" PRIMARY KEY ("user_id", "role_id");

-- ----------------------------
-- Indexes structure for table users
-- ----------------------------
CREATE INDEX "user_name_idx" ON "public"."users" USING gin (
  "name" COLLATE "pg_catalog"."default" "public"."gin_trgm_ops"
);
CREATE INDEX "user_surname_idx" ON "public"."users" USING gin (
  "surname" COLLATE "pg_catalog"."default" "public"."gin_trgm_ops"
);

-- ----------------------------
-- Uniques structure for table users
-- ----------------------------
ALTER TABLE "public"."users" ADD CONSTRAINT "user_email" UNIQUE ("email");

-- ----------------------------
-- Primary Key structure for table users
-- ----------------------------
ALTER TABLE "public"."users" ADD CONSTRAINT "users_pkey" PRIMARY KEY ("user_id");

-- ----------------------------
-- Foreign Keys structure for table activity
-- ----------------------------
ALTER TABLE "public"."activity" ADD CONSTRAINT "fk_activity_projects_1" FOREIGN KEY ("project_id") REFERENCES "public"."projects" ("project_id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "public"."activity" ADD CONSTRAINT "fk_activity_users_1" FOREIGN KEY ("user_id") REFERENCES "public"."users" ("user_id") ON DELETE SET NULL ON UPDATE NO ACTION;

-- ----------------------------
-- Foreign Keys structure for table attachments
-- ----------------------------
ALTER TABLE "public"."attachments" ADD CONSTRAINT "fk_attachments_documents_1" FOREIGN KEY ("document_id") REFERENCES "public"."documents" ("document_id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "public"."attachments" ADD CONSTRAINT "fk_attachments_users_1" FOREIGN KEY ("user_id") REFERENCES "public"."users" ("user_id") ON DELETE SET NULL ON UPDATE NO ACTION;

-- ----------------------------
-- Foreign Keys structure for table bookmarks
-- ----------------------------
ALTER TABLE "public"."bookmarks" ADD CONSTRAINT "fk_bookmarks_documents_1" FOREIGN KEY ("document_id") REFERENCES "public"."documents" ("document_id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "public"."bookmarks" ADD CONSTRAINT "fk_bookmarks_users_1" FOREIGN KEY ("user_id") REFERENCES "public"."users" ("user_id") ON DELETE CASCADE ON UPDATE NO ACTION;

-- ----------------------------
-- Foreign Keys structure for table comment_likes
-- ----------------------------
ALTER TABLE "public"."comment_likes" ADD CONSTRAINT "fk_comment_likes_comments_1" FOREIGN KEY ("comment_id") REFERENCES "public"."comments" ("comment_id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "public"."comment_likes" ADD CONSTRAINT "fk_comment_likes_users_1" FOREIGN KEY ("user_id") REFERENCES "public"."users" ("user_id") ON DELETE SET NULL ON UPDATE NO ACTION;

-- ----------------------------
-- Foreign Keys structure for table comments
-- ----------------------------
ALTER TABLE "public"."comments" ADD CONSTRAINT "fk_comments_documents_1" FOREIGN KEY ("document_id") REFERENCES "public"."documents" ("document_id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "public"."comments" ADD CONSTRAINT "fk_comments_users_1" FOREIGN KEY ("user_id") REFERENCES "public"."users" ("user_id") ON DELETE SET NULL ON UPDATE NO ACTION;

-- ----------------------------
-- Foreign Keys structure for table document_likes
-- ----------------------------
ALTER TABLE "public"."document_likes" ADD CONSTRAINT "fk_document_likes_documents_1" FOREIGN KEY ("document_id") REFERENCES "public"."documents" ("document_id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "public"."document_likes" ADD CONSTRAINT "fk_document_likes_users_1" FOREIGN KEY ("user_id") REFERENCES "public"."users" ("user_id") ON DELETE CASCADE ON UPDATE NO ACTION;

-- ----------------------------
-- Foreign Keys structure for table document_text
-- ----------------------------
ALTER TABLE "public"."document_text" ADD CONSTRAINT "fk_document_text_documents_1" FOREIGN KEY ("document_id") REFERENCES "public"."documents" ("document_id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "public"."document_text" ADD CONSTRAINT "fk_document_text_users_1" FOREIGN KEY ("editor_id") REFERENCES "public"."users" ("user_id") ON DELETE SET NULL ON UPDATE NO ACTION;

-- ----------------------------
-- Foreign Keys structure for table documents
-- ----------------------------
ALTER TABLE "public"."documents" ADD CONSTRAINT "fk_documents_documents_1" FOREIGN KEY ("parent_document_id") REFERENCES "public"."documents" ("document_id") ON DELETE SET NULL ON UPDATE NO ACTION;
ALTER TABLE "public"."documents" ADD CONSTRAINT "fk_documents_projects_1" FOREIGN KEY ("project_id") REFERENCES "public"."projects" ("project_id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "public"."documents" ADD CONSTRAINT "fk_documents_users_1" FOREIGN KEY ("creator_id") REFERENCES "public"."users" ("user_id") ON DELETE SET NULL ON UPDATE NO ACTION;

-- ----------------------------
-- Foreign Keys structure for table followed_projects
-- ----------------------------
ALTER TABLE "public"."followed_projects" ADD CONSTRAINT "fk_followed_projects_projects_1" FOREIGN KEY ("project_id") REFERENCES "public"."projects" ("project_id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "public"."followed_projects" ADD CONSTRAINT "fk_followed_projects_users_1" FOREIGN KEY ("user_id") REFERENCES "public"."users" ("user_id") ON DELETE CASCADE ON UPDATE NO ACTION;

-- ----------------------------
-- Foreign Keys structure for table last_seen_documents
-- ----------------------------
ALTER TABLE "public"."last_seen_documents" ADD CONSTRAINT "fk_last_seen_documents_documents_1" FOREIGN KEY ("document_id") REFERENCES "public"."documents" ("document_id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "public"."last_seen_documents" ADD CONSTRAINT "fk_last_seen_documents_users_1" FOREIGN KEY ("user_id") REFERENCES "public"."users" ("user_id") ON DELETE CASCADE ON UPDATE NO ACTION;

-- ----------------------------
-- Foreign Keys structure for table notifications
-- ----------------------------
ALTER TABLE "public"."notifications" ADD CONSTRAINT "fk_notifications_users_1" FOREIGN KEY ("user_id") REFERENCES "public"."users" ("user_id") ON DELETE CASCADE ON UPDATE NO ACTION;

-- ----------------------------
-- Foreign Keys structure for table notifications_read
-- ----------------------------
ALTER TABLE "public"."notifications_read" ADD CONSTRAINT "fk_notifications_read_users_1" FOREIGN KEY ("user_id") REFERENCES "public"."users" ("user_id") ON DELETE CASCADE ON UPDATE NO ACTION;

-- ----------------------------
-- Foreign Keys structure for table project_role_permissions
-- ----------------------------
ALTER TABLE "public"."project_role_permissions" ADD CONSTRAINT "fk_role_permissions_permissions_1" FOREIGN KEY ("project_permission_slug") REFERENCES "public"."project_permissions" ("project_permission_slug") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "public"."project_role_permissions" ADD CONSTRAINT "fk_role_permissions_roles_1" FOREIGN KEY ("project_role_id") REFERENCES "public"."project_roles" ("project_role_id") ON DELETE CASCADE ON UPDATE NO ACTION;

-- ----------------------------
-- Foreign Keys structure for table project_team
-- ----------------------------
ALTER TABLE "public"."project_team" ADD CONSTRAINT "fk_project_team_project_roles_1" FOREIGN KEY ("project_role_id") REFERENCES "public"."project_roles" ("project_role_id") ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE "public"."project_team" ADD CONSTRAINT "fk_project_team_projects_1" FOREIGN KEY ("project_id") REFERENCES "public"."projects" ("project_id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "public"."project_team" ADD CONSTRAINT "fk_project_team_users_1" FOREIGN KEY ("user_id") REFERENCES "public"."users" ("user_id") ON DELETE CASCADE ON UPDATE NO ACTION;

-- ----------------------------
-- Foreign Keys structure for table quick_links
-- ----------------------------
ALTER TABLE "public"."quick_links" ADD CONSTRAINT "fk_quick_links_documents_1" FOREIGN KEY ("document_id") REFERENCES "public"."documents" ("document_id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "public"."quick_links" ADD CONSTRAINT "fk_quick_links_projects_1" FOREIGN KEY ("project_id") REFERENCES "public"."projects" ("project_id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "public"."quick_links" ADD CONSTRAINT "fk_quick_links_projects_2" FOREIGN KEY ("housing_project_id") REFERENCES "public"."projects" ("project_id") ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE "public"."quick_links" ADD CONSTRAINT "fk_quick_links_users_1" FOREIGN KEY ("user_id") REFERENCES "public"."users" ("user_id") ON DELETE CASCADE ON UPDATE NO ACTION;

-- ----------------------------
-- Foreign Keys structure for table refresh_tokens
-- ----------------------------
ALTER TABLE "public"."refresh_tokens" ADD CONSTRAINT "fk_refresh_tokens_users_1" FOREIGN KEY ("user_id") REFERENCES "public"."users" ("user_id") ON DELETE CASCADE ON UPDATE NO ACTION;

-- ----------------------------
-- Foreign Keys structure for table role_permissions
-- ----------------------------
ALTER TABLE "public"."role_permissions" ADD CONSTRAINT "fk_role_permissions_permissions_1" FOREIGN KEY ("permission_slug") REFERENCES "public"."permissions" ("permission_slug") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "public"."role_permissions" ADD CONSTRAINT "fk_role_permissions_roles_1" FOREIGN KEY ("role_id") REFERENCES "public"."roles" ("role_id") ON DELETE CASCADE ON UPDATE NO ACTION;

-- ----------------------------
-- Foreign Keys structure for table template_text
-- ----------------------------
ALTER TABLE "public"."template_text" ADD CONSTRAINT "fk_template_text_templates_1" FOREIGN KEY ("template_id") REFERENCES "public"."templates" ("template_id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "public"."template_text" ADD CONSTRAINT "fk_template_text_users_1" FOREIGN KEY ("editor_id") REFERENCES "public"."users" ("user_id") ON DELETE SET NULL ON UPDATE NO ACTION;

-- ----------------------------
-- Foreign Keys structure for table templates
-- ----------------------------
ALTER TABLE "public"."templates" ADD CONSTRAINT "fk_templates_projects_1" FOREIGN KEY ("project_id") REFERENCES "public"."projects" ("project_id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "public"."templates" ADD CONSTRAINT "fk_templates_template_types_1" FOREIGN KEY ("template_type_slug") REFERENCES "public"."template_types" ("template_type_slug") ON DELETE SET NULL ON UPDATE NO ACTION;
ALTER TABLE "public"."templates" ADD CONSTRAINT "fk_templates_users_1" FOREIGN KEY ("creator_id") REFERENCES "public"."users" ("user_id") ON DELETE SET NULL ON UPDATE NO ACTION;

-- ----------------------------
-- Foreign Keys structure for table user_competences
-- ----------------------------
ALTER TABLE "public"."user_competences" ADD CONSTRAINT "fk_user_competences_competences_1" FOREIGN KEY ("competence_id") REFERENCES "public"."competences" ("competence_id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "public"."user_competences" ADD CONSTRAINT "fk_user_competences_users_1" FOREIGN KEY ("user_id") REFERENCES "public"."users" ("user_id") ON DELETE CASCADE ON UPDATE NO ACTION;

-- ----------------------------
-- Foreign Keys structure for table user_roles
-- ----------------------------
ALTER TABLE "public"."user_roles" ADD CONSTRAINT "fk_user_roles_roles_1" FOREIGN KEY ("role_id") REFERENCES "public"."roles" ("role_id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "public"."user_roles" ADD CONSTRAINT "fk_user_roles_users_1" FOREIGN KEY ("user_id") REFERENCES "public"."users" ("user_id") ON DELETE CASCADE ON UPDATE NO ACTION;
