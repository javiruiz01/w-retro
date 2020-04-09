FROM mcr.microsoft.com/mssql/server:2019-latest
WORKDIR /db
COPY . .
ENV ACCEPT_EULA Y
ENV SA_PASSWORD Patata1.
CMD ["/bin/bash", "-c", "./preload.sh & /opt/mssql/bin/sqlservr"]
