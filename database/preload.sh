echo "Waiting for sql to start"
while [[ "$(/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Patata1. -Q "SELECT 'Hello world'" -h -1)" != *"Hello world"* ]]; do sleep 1; done

echo "Executing migration scripts"

for file in migrations/*
do
  /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P Patata1. -i $file
  echo "Executed migration: $file"
done

