
# UrlShortener

## Run the solution

Navigate to the project folder e.g.: `\UrlShortener-master` and run from the command line:
### To create/update the database:
```Batchfile
dotnet ef database update --project UrlShortener.Infrastructure --startup-project UrlShortener.API
```


### To run the application:
```Batchfile
dotnet run --project UrlShortener.API
```


The application should start in localhost:PORT URL.

### SwaggerUI
`http://localhost:xxxx/swagger`

### URL redirect
`http://localhost:xxxx/uniqueID`

![Capture](https://user-images.githubusercontent.com/7429247/131897066-4a47e373-ec78-4fdc-9178-1a8cbfac9f8b.PNG)

### Completed Workflow (MVP project)
- Create the URL model;
- Create the DTO's and AutoMapper configuration;
- Create the method to generate an random value;
- Create the method to return the current hosted URL + random value;
- Create the database project (Code First / Relational database);
- Create the method that will check for existing long URLs;
- Create the method that save the URL;
- Create the method that will return the long URL based on the short URL;
- Create a controller to create the URL;
- Create a controller to redirect the short URL;
- Create the controller to clean expired URL's;
- Create the error handling class;
- Update the save method to retry if the short URL is not unique; 
- Add "short URL size" and "max attempts to retry" to web.config;
- Create basic unit testing;


### Pending Workflow
- Create a SPA with Angular;
- Create unit testing for all the services with Moq;
- Increase the model validation;
- Move to a NoSQL database;
- Add docker;
