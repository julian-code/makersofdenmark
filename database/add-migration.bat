@ECHO off
IF "%1"=="" GOTO Error
dotnet ef migrations add %1 -s ..\src\MakersOfDenmark.WebAPI\ -p ..\src\MakersOfDenmark.Infrastructure\
GOTO End
:Error
ECHO You nedd to specify a name for the migration

:End