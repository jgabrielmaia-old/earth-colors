#!/bin/bash

echo '===== ====================='
echo '===== Starting script bash'
echo '===== ====================='

DB_NAME="earth_colors"
DBSCRIPTS_PATH="/docker-entrypoint-initdb.d/DBScripts/"

# initialize single node replica set to support transactions
mongo --eval "rs.initiate({ _id: 'rs0', version: 1, members: [{ _id: 0, host : 'localhost:27017' }] });"

echo '===== <> Load Deploy DBScripts ====================='
echo '.'
for entry in `ls $DBSCRIPTS_PATH`; do
    echo "$DBSCRIPTS_PATH/$entry"
    mongo $DB_NAME $DBSCRIPTS_PATH/$entry
done
echo '..'
echo '===== <> Load Deploy DBScripts ====================='

echo '===== =================='
echo '===== Ending script bash'
echo '===== =================='