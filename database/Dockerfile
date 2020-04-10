FROM mcr.microsoft.com/mssql/server:2019-latest
WORKDIR /db
COPY . .
ENV ACCEPT_EULA Y

USER root
RUN apt-get update -y && apt-get install -y dos2unix
RUN chmod +x ./preload.sh && dos2unix ./preload.sh

CMD ["/bin/bash", "-c", "./preload.sh & /opt/mssql/bin/sqlservr"]
