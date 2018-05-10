#!/bin/bash
set -e

psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" <<-EOSQL
    CREATE USER docker;
    CREATE DATABASE kms;
    GRANT ALL PRIVILEGES ON DATABASE kms TO docker;
EOSQL

psql --username "$POSTGRES_USER" "kms" < "./dump.sql"
