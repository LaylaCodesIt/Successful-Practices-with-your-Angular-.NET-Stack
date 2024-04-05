+++
archetype = "chapter"
title = "Data"
weight = 3
+++

### Welcome to the database setup module of the workshop practical 🎉

Data storage is always up for debate and many of us feel very strongly about it. You are probably used to having a single relational database and you probably feel comfortable with it - and there is nothing wrong with that.

However, as you start to modularise your codebase, you'll want to modularise your data also. The biggest mistake is to tightly couple your modules and services via the data storage - this can lead to the highly undesirable *distributed monolith*.

But, on the other hand, you don't need to rip apart your data storage all in one go. Just as you incrementally tease out your code, you can incrementally separate your data.

Going fully distributed brings in a whole host of problems that are often managed by *sagas* - this is beyond the scope of this workshop.

## Tacky Tacos Data

The current database for Tacky Tacos is a SQL DB (a relational database) that looks like this:

![diagram](current-db-diagram.PNG)

I decided to extract the *Orders* data out into a document store - and I used MongoDB.


#### SQL Server and MongoDB in Docker 

Running SQL Server in Docker is great! It's cross-platform and doesn't litter your machine with all its bits and pieces!

Checkout either the `data-one` branch if we are covering entity framework, or the `data-two` branch if not. Open `docker-compose.yml`, found in the "SolutionItems" directory, in a text editor. We are saving our data entries against a volume so you'll need to update line `13` to match a file structure on your machine.

Next, go down to line `24` and update the volume path for MongoDB.

Example on Windows:
```
C:\Users\Layla\DockerVolumes\mongo:/var/opt/mongo/data
```
Example on MacOS/Linux:
```
/Users/layla/Dockerfiles/Volumes/MongoDb:/var/opt/mongo/data
```

Once that is done, run `docker compose up` from the CLI from within the "SolutionItems" directory, to start SQL server.

Make sure Docker is running!

## Setting up the data stores

#### Using SQL Server Management Studio (SSMS)

Open SSMS - you will be prompted to login:

- "Server name" => `localhost`
- "Login" => `sa`
- "Password" => `Password123`

Next we need to import the `bacpac` file, found in this repo.
Right-click on *Databases* and select `Import Data-tier Application` - follow the wizard and import the DB found in *SolutionItems*.

#### Using Azure Data Studio

Select the *Create a Connection* option and enter the following:

- "Server" => `localhost`
- "Authentication type" => SQL Login
- "Login" => `sa`
- "Password" => `Password123`

Click connect and then, when prompted, "Enable Trust server certificate".
To restore from a `.bacpac` you'll need to install the extension *Admin Pack for SQL Server*.
Once this is installed and enabled - right click on the server ("localhost"), select "Data-tier Application Wizard" and follow the instructions to import the DB found in this repo.

> If you struggle with any of this for "reasons", then use the SQL volume in this repo and drop it into your Docker volume directory.

### Setting up Azurite

The table storage will run in an emulator called Azurite which will host a blob storage.

In the CLI, run the following command:

```bash
npm install -g azurite
```

> Azurite does come included with Visual Studio.

To start Azurite run the following command in the CLI:

```bash
azurite
```

