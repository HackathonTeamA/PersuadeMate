# PersuadeMate API project

## how to run

1. install dotnet cli (requirement .NET8)
2. edit appsettings.json's advisor name
   - when use stub advisor, set "AdvisorName": "Stub"
   - when use OpenAI API advisor, set "AdvisorName": "AI"
3. configure environment variables, when use Open AI API advisor
   - OpenAIServiceOptions__ApiKey
   - OpenAIServiceOptions__Organization
4. cd Api
5. dotnet run

## endpoint for development

- https://localhost:7114/
- http://localhost:5157
