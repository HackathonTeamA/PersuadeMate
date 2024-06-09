# PersuadeMate API project

## how to run

1. install [dotnet cli](https://learn.microsoft.com/en-us/dotnet/core/install/) (requirement .NET8)
2. edit appsettings.json's advisor name
   - when use stub advisor, set "AdvisorName": "Stub"
   - when use OpenAI API advisor, set "AdvisorName": "AI"
3. configure environment variables, when use Open AI API advisor
   - OpenAIServiceOptions__ApiKey
   - OpenAIServiceOptions__Organization
4. `cd Api`
5. execute `dotnet run`

## run with frontend (test only on linux, not working on windows.)

1. install [bun](https://bun.sh/)
2. install [just](https://github.com/casey/just) - command runner like make
3. execute `just watch`

If you want to see all tasks, execute `just run tasks`.

## endpoint for development

- https://localhost:7114/
- http://localhost:8080

## endpoint for production

- frontend
  - https://persuademate-frontend-ovkyr72pqq-an.a.run.app/
- backend
  - https://persuademate-ovkyr72pqq-an.a.run.app/api
