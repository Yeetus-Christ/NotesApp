# NotesApp

This is a simple app where users can anonymously view, add and edit notes.

## Architecture

The app is developed using Blazor Server from Microsoft.
Architecture has 4 main components:

- PostgreSQL database to store notes
- EntityFrameworkCore - allows interaction between app and database
- NotesService - service class with the main functionality of the app
- Blazor page - web page for users to interact with

![alt text](https://github.com/Yeetus-Christ/NotesApp/blob/1d45b264ead4def872ecedbe73165c9698393bbf/NotesApp%20Architecture.png)

The app also uses Serilog library, which allows for more extensive and convenient logging compared to built-in logger.
It is configured to write logs to ElasticSearch cluster. You can view data from ES cluster using Kibana. Both ES and Kibana are run as containers in Docker
