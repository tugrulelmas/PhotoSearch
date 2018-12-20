FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /app

COPY *.sln .
COPY PhotoSearch.Api/*.csproj ./PhotoSearch.Api/
COPY PhotoSearch.Api.Tests/*.csproj ./PhotoSearch.Api.Tests/
RUN dotnet restore

COPY . .
WORKDIR /app/PhotoSearch.Api
RUN dotnet build

FROM build AS test
WORKDIR /app/PhotoSearch.Api.Tests
RUN dotnet test --verbosity:normal

FROM build AS publish
WORKDIR /app/PhotoSearch.Api
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:2.2.0-aspnetcore-runtime-alpine
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:80
COPY --from=publish /app/PhotoSearch.Api/out ./
ENTRYPOINT ["dotnet", "PhotoSearch.Api.dll"]