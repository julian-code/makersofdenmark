@echo off
echo Is this first run? Give script 90 seconds to finish
docker run -p 1433:1433 -v dbdata:/var/opt/mssql makers-db