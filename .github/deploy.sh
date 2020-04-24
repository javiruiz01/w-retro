#! /bin/bash

docker-compose down

if [ -f ".env" ]
then
  rm .env
fi

SA_PASSWORD=$1
DOCKER_TAG=$2

echo "SA_PASSWORD=$SA_PASSWORD" >> .env
echo "IMAGE_TAG=$DOCKER_TAG" >> .env

docker-compose up -d