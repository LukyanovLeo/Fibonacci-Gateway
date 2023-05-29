FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Fibonacci.Gateway/Fibonacci.Gateway.csproj", "Fibonacci.Gateway/"]
COPY ["Fibonacci.Gateway.Infrastructure/Fibonacci.Gateway.Infrastructure.csproj", "Fibonacci.Gateway.Infrastructure/"]
COPY ["Fibonacci.Gateway.Domain/Fibonacci.Gateway.Domain.csproj", "Fibonacci.Gateway.Domain/"]
RUN dotnet restore "Fibonacci.Gateway/Fibonacci.Gateway.csproj"
COPY . .
WORKDIR "/src/Fibonacci.Gateway"
RUN dotnet build "Fibonacci.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Fibonacci.Gateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fibonacci.Gateway.dll"]