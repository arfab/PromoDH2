#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-bionic AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-bionic AS build
WORKDIR /src
COPY ["PromoDH.csproj", ""]
RUN dotnet restore "./PromoDH.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "PromoDH.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PromoDH.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PromoDH.dll"]