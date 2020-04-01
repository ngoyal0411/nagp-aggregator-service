FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["OrderDetails/OrderDetails.csproj", "OrderDetails/"]
RUN dotnet restore "OrderDetails/OrderDetails.csproj"
COPY . .
WORKDIR "/src/OrderDetails"
RUN dotnet build "OrderDetails.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "OrderDetails.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "OrderDetails.dll"]