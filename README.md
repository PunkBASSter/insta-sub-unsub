# insta-sub-unsub
A set of jobs for pumping subscribers for a specified Inst account (by following accounts which will likely follow it back).
4 different jobs do crawling, selection, following, unfollowing.
Entry point project for Windows: InstaCrawlerServiceWindows.csproj (add similar entry pount projects for other OS if needed).

Anti-bot protection countermeasures:
* Fuzzy scheduling adding random delays to job executions.
* Random delays in UI interactions.
* Pool of service accounts for crawling and data mining.
* Detection of implicit scraping block of the current account (excluding it from the pool for a while).

Drawbacks:
* Some of UI elements locators on Insta side may change breaking the jobs (in average once per 3 months).
* Many locators rely on strings in Russian localization (won't work with different cultures selected in the browser).
* Pumping goes rather slow: 30-50 new followers per day with default settings.
* Lack of reporting.

## Requirements
1. Dotnet 7
2. PostgreSQL
3. PgAdmin (optional)
4. Google Chrome or MS Edge
5. Dotnet EF tools CLI (dotnet tool install --global dotnet-ef)
   
## Setup
1. Download either chromedriver.exe of msedgedriver.exe and put the EXE-file to SeleniumUtils project folder next to the .scproj file.
2. Provide service Insta accounts credentials in appsettings.json "ServiceAccountsToImport" - will be written to the DB on the first launch (can be removed from appsettings.json after it). It's recommended to have 8-10 accounts for default crawling settings (see appsettings.json).
3. Provide the Insta account to be pumped credentials via CLI as: FollowUser:Username=<name> FollowUser:Password=<password>. The pumped account will follow other users expecting they follow it and than it will unfollow them after a while.
