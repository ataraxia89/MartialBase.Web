## MartialBase.Web

### Introduction
MartialBase® is currently a work in progress, and will eventually be released as a commercial product to manage martial arts clubs. This web solution has been used experimentally to practice functional, clean coding as well as familiarising with Blazor design principles and structuring the code in a way that will be fully testable at a later date.

### Update nuget.config to access required packages
The [nuget.config](https://github.com/ataraxia89/MartialBase.Web/blob/main/nuget.config) file contains a placeholder for GitHub credentials, to enable access to GitHub-hosted packages. The packages are public, so any GitHub account can access them. Official GitHub documentation can be found [here](https://docs.github.com/en/authentication/keeping-your-account-and-data-secure/managing-your-personal-access-tokens), please follow the below steps to obtain a PAT token:

- Click the profile icon in the top right of any GitHub page, then click Settings
- Scroll down and select "Developer settings" in the sidebar to the left
- Go to "Personal access tokens -> Tokens (classic)"
- Any previously-created tokens will appear on this page, from the dropdown at the top select "Generate new token -> Generate new token (classic)"
- Give the token a name, select an expiry time and ensure that it has the `read:packages` permission selected
- Click "Generate token" at the bottom
- The newly-generated token will appear on the list in plaintext, copy this and paste into the `GITHUB_PAT` placeholder in the config file
- Replace the `GITHUB_USER` placeholder with your GitHub username

### Mock Data
The MartialBase® web solution is currently disconnected from the API, as the API depends on authentication tokens and the former identity provider (Microsoft Azure Front Door/B2C service) has been temporarily removed along with all authorization throughout the web controllers.

As the web solution depends on interfaces to implement data services which would usually contact the live API, there is instead a set of tools to provide a mock implementation and ensure that the data services return sample data to the UI, whilst the system is being designed.

Once a new authentication mechanism is implemented (or Front Door/B2C is re-instated), the live data services can be used again alongside the API.