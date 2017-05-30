# HiP-Auth-v2
Authorization service powered by IdentiyServer4

## API

The most important API calls are manually documented here. The return values are not 100% to our satisfaction yet but if called correctly the methods work.

**Important**: There is no real login/logout here. You register at the service, and when you need a token you get it via `/connect/token`. If you want to log the user out in the frontend, just throw away the token. Revoking tokens is not supported at the moment. 

### Connect

#### Token

This is, sort of, the login. You give the service your `username` and `password` and it returns a token that can be used to call the services that were specified via the `scope` parameter.
POST: `https://docker-hip.cs.uni-paderborn.de/develop/authv2/connect/token`

Parameters as formdata:
- `client_id`: `HiP-CmsAngularApp` | `HiP-Mobile` | `HiP-TokenGenerator`
- `client_secret`: The client secret - can be taken from confluence, or take the default from `appsettings.json`
- `grant_type`: `password`
- `username`: Your username (for testing take our default HiP admin testing user)
- `password`: Your password (for testing take our default HiP admin testing password)
- `scope`: Always at least `openid`, add other scopes as needed (separate with a space): `HiP-CmsWebApi` `HiP-DataStore` `HiPCMS-OnlyOfficeIntegration` `HiP-FeatureToggle`

### Accounts

#### Register

POST: `https://docker-hip.cs.uni-paderborn.de/develop/authv2/Account/Register`

Parameters as formdata:
- `Email`: The email address of the account that wants to register. Currently not restricted to any domain.
- `Password`: The users password.
- `ConfirmPassword`: The users confirmed password.
- `returnUrl[optional]`: The URL that the service should redirect to.

Returns:
- `200` for every email address (otherwise an attacker could find the emails of existing accounts by calling this method)

#### ForgotPassword

POST: `https://docker-hip.cs.uni-paderborn.de/develop/authv2/Account/ForgotPassword`

Parameters as formdata:
- `Email`: The email address of the account that forgot its password.

Returns:
- `200` for every email address (otherwise an attacker could find the emails of existing accounts by calling this method)

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
