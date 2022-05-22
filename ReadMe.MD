# LAGETRONIX CODING ASSESSMENT

A dotnet web api project that manages a collection of books and associated categories.

## DOCUMENTATION

## Installation

Clone the project on your local computer using the command on your terminal

```
git clone https://github.com/Nwachukwu-Uzor/Lagetronix.Books.git
```

Ensure you have SQL SERVER installed on your machine.

Replace the default connection string in the Lagetronix.Books.Api/appsettings.json file with a connection string from your local sql server.

<Img src="Lagetronix Web Api.png"/>

Open the project on Visual Studio IDE and right click on the project and select "Clean Solution"

Right again and select "Rebuild Solution"

## Setting up the database

Set the startup project of the solution to the "Lagetronix.Books.Api" Project
<br >
<br >
<img src="./Startup Project.png">
<br >
<br >

Open the package manager console (on the toolbar in Visual Studio navigate to **Tools > Nugget Package Manager > Package Manager Console**) and set the default project in the console to "Lagetronix.Books.Data"
<br >
<br >
<img src="Default Project setup.png" >
<br >
<br >
On the package manager console, run the command

```
add-migration "initial migration"
```

Once the migration is successful, run the following command on the package manager console

```
update-database
```

The database is now setup and seeded with dummy data.

Run the project by clicking on the run button in Visual Studio
<br >
<br >
<img src="Startup Project.png">
<br >
<br >

## Usage

The project runs on locally on **https://localhost:5001/**

### Categories

### Create a Category

```
Endpoint: https://localhost:5001/api/categories

Method: POST

Payload:
{
    "name"*:  "name of category to create",
    "description": "description of of category to create"
}

Response
    Success:
        Status: 201
        Body:
            {
                "Success": true,
                "Errors": null,
                "data": {
                    "name": "category name",
                    "description": "category description",
                    "createdAt": "date of creation",
                    "modifiedOn": "date of creation"
                }
            }

    Failure:
        Status: 400

```

**Note** fields marker with <strong>\*</strong> are required

### Get all Categories

```
Endpoint: https://localhost:5001/api/categories

Method: GET
```

### Get a Category by Id

```

Endpoint: https://localhost:5001/api/categories/{categoryId}

Method: GET

```

### Update a Category

- PUT

```

Endpoint: https://localhost:5001/api/categories/{categoryId}

Method: PUT

Payload
{
    "name"\*: "name of category to create",
    "description": "description of of category to create"
}

```

- PATCH

```

Endpoint: https://localhost:5001/api/categories/{categoryId}

Method: PATCH

Payload
[
    {
        "op": "/replace",
        "path": "property to update",
        "value": "value to update the property to"
    }
]

```

### Delete a Category

```

Endpoint: https://localhost:5001/api/categories/{categoryId}

Method: DELETE

```

### Books

### Create a book

```

Endpoint: https://localhost:5001/api/books

Method: POST

Payload:
{
    "title"_: "title of the book to create",
    "description": "description of of book to create",
    "author"_: "author of the book",
    "categoryId"\*: "category id of the book"
}

```

### Get all books

```

Endpoint: https://localhost:5001/api/books

Method: GET

```

## Get favorite books

```

Endpoint: https://localhost:5001/api/books/favorite

Method: GET

```

### Get a book

```

Endpoint: https://localhost:5001/api/books/{bookId}

Method: GET

```

### Get books for a given categories

```

Endpoint:  https://localhost:5001/api/books/categories/{categoryId}

Method: GET

```

### Update a book

- PUT

```

Endpoint: https://localhost:5001/api/books/{bookId}

Method: PUT

Payload
{
    "title"\*: "title of book to update",
    "description": "description of book to update",
    "author"*: "author of book to update"
}

```

- PATCH

```

Endpoint: https://localhost:5001/api/categories/{categoryId}

Method: PATCH

Payload
[
    {
        "op": "/replace",
        "path": "property to update",
        "value": "value to update the property to"
    }
]

```

### Update a book favorite status

```

Endpoint: https://localhost:5001/api/books/favorite/{bookId}

Method: PUT
```

### Delete a book

```

Endpoint: https://localhost:5001/api/books/{bookId}

Method: DELETE

```
