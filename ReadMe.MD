# Langetronix Coding Assessment

A dotnet web api project that manages a collection of books and associated categories.

## Installion

Clone the project on your local computer using the command on your terminal

```
git clone https://github.com/Nwachukwu-Uzor/Lagetronix.Books.git
```

Ensure you have SQL SERVER installed on your machine.

Replace the default connection string in the Lagetronix.Books.Api/appsettings.json file with a connection string from your local sql server.

<Img src="./Lagetronix Web Api.png"/>

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
<img src="./Startup Project.png">
<br >
<br >
