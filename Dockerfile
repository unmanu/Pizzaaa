#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0.9-bullseye-slim-amd64 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0.306-bullseye-slim-amd64 AS build
WORKDIR /src
COPY ["src/Pizzaaa.UI.Blazor/Pizzaaa.UI.Blazor.csproj", "src/Pizzaaa.UI.Blazor/"]
RUN dotnet restore "src/Pizzaaa.UI.Blazor/Pizzaaa.UI.Blazor.csproj"
COPY . .
WORKDIR "/src/src/Pizzaaa.UI.Blazor"
RUN dotnet build "Pizzaaa.UI.Blazor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pizzaaa.UI.Blazor.csproj" -c Release -o /app/publish /p:UseAppHost=false --os linux

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pizzaaa.UI.Blazor.dll"]
