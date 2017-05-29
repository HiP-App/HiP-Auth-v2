# HiP-Auth-v2
Authorization service powered by IdentiyServer4

## Configuration

### Database

This service needs a MS SQL database for storing the client configurations, user credentials etc. The recommended approach is to use the [mssql-server-linux](https://hub.docker.com/r/microsoft/mssql-server-linux/) docker image. For Windows systems there also is a native MS SQL server solution.

### appsettings.json

The Auth-v2 service makes some assumptions about the environment in which it is run. Below are the keys of the `appsettings.json` file and their explanation / expected values. In some cases the default configuration is fine (e.g. for the `PORT`), but in most cases these default values should be overwritten via `appsettings.Development.json` / `appsettings.Production.json`.

- `PORT`: The port under which the server is run; has to be `5001`
- `ADMIN_USERNAME`: The username of the default admin user. Should be set to `admin@hipapp.de`
- `ADMIN_PASSWORD`: The password of the default admin user. Should be set to our default admin password.
- AspNetIdentity Database configuration: Host, username, password and name of the database containing the user logins etc. - the service does not care what the exact values are, as long as they enable it to connect to a database ;)
  - `ID_DB_HOST`
  - `ID_DB_USERNAME`
  - `ID_DB_PASSWORD`
  - `ID_DB_NAME`
- IdentityServer Database configuration: Same as above, but for the database containing client configurations and persisted grants - again, the exact values are not important, **but** the `IDS_DB_NAME` value should be **different** from the `ID_DB_NAME`
  - `IDS_DB_HOST`
  - `IDS_DB_USERNAME`
  - `IDS_DB_PASSWORD`
  - `IDS_DB_NAME`
- Addresses for TokenGeneratorClient, CmsAngularApp and CmsWebApi: Set these to the addresses that the respective service listens on. These are used for restricting CORS and for redirecting the user after their login.
  - `CMS_ADDRESS`
  - `TOKEN_GENERATOR_ADDRESS`
  - `WEB_API_ADDRESS`
- Secrets for the clients: Secrets that only the sourcecode of the calling application knows - KEEP THEM SECRET!
  - `SECRET_CMS` (this secret cannot be hidden 100% as it is a JS client)
  - `SECRET_GENERATOR` (same as above)
  - `SECRET_MOBILE`
- The server URL(s) are magically set via the `server.urls` env variable
  - a default value for this is set in `appsettings.json`
  - can be overriden via other environment variables
