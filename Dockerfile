FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80


FROM microsoft/dotnet:2.2-sdk AS build-env
WORKDIR /src


COPY ./jwt.Shared/jwt.Shared.csproj ./jwt.Shared/
COPY ./PlanGRDataAccess/PlanGRDataAccess.csproj ./PlanGRDataAccess/
COPY ./PlanGRBusiness/PlanGRBusiness.csproj ./PlanGRBusiness/
COPY ./PlanGRAPI/PlanGRAPI.csproj ./PlanGRAPI/
RUN dotnet restore ./PlanGRAPI/


COPY . .
WORKDIR /src/PlanGRAPI
RUN dotnet build PlanGRAPI.csproj


FROM build-env AS publish
RUN dotnet publish . -o /publish -c Release


FROM base AS final
ENV ConnectionStrings:DefaultConnection="Server=kascoit.ddns.me,22017;Database=WMSDB_Inbound;Trusted_Connection=False;Integrated Security=False;user id=sa;password=K@sc0db12345;"
WORKDIR /app
COPY --from=publish /publish .
ENTRYPOINT ["dotnet", "PlanGRAPI.dll"]