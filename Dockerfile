FROM microsoft/dotnet:1.1.1-sdk

RUN mkdir -p /dotnetapp

COPY AuthWithIdentity /dotnetapp
WORKDIR /dotnetapp

EXPOSE 5001

WORKDIR /dotnetapp
RUN dotnet restore --no-cache

ENTRYPOINT ["dotnet", "run"]
