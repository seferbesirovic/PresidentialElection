
# USA Presidential Elections Application

## About developing
### Database model
There are 3 tables. Candidates, States and Results which represent many to many relationship between Candidates and States.
Structure like this allow us to create Candidates and States separately, so it can be reused for next elections.
We are using .Net local database for now.

### Data transfer objects
Using of Dtos for communication with client is reason why our database schema is not exposed.
Also we used it for returning formatted response to client. Client gets all states with all results and necessary calculations.

## Demo
Here you can see short usage of App:
https://gifyu.com/image/S2PaA

## Backend .Net 5.0

### Endpoints documentation
After running Web Api project (F5) all endpoints and its description can be seen at: https://localhost:44340/swagger/index.html

### Dependencies
1. Microsoft.AspNetCore.Mvc.NewtonsoftJson
2. Microsoft.EntityFrameworkCore
3. Microsoft.EntityFrameworkCore.SqlServer
4. Microsoft.EntityFrameworkCore.Tools
5. Swashbuckle.AspNetCore
6. System.ComponentModel.Annotations

## Frontend Angular  9

### Installing dependencies

1. Run `npm install` in project folder

### Development server

2. Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`.


