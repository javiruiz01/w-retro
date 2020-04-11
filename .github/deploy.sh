#! /bin/bash

cd projects/w-retro/
git pull
docker-compose down

if [ -f ".env" ]
then
  rm .env
fi

PG_PASSWORD=$1
DOCKER_TAG=$2

echo "$PG_PASSWORD" >> .env
echo "$DOCKER_TAG" >> .env

docker-compose up -d