---
title: "Azure Data Tables"
date: 2024-06-17T16:36:46Z
draft: false
weight: 1001
type: "subpost"
---

Let's have a look at the code to see what I have provided.

I have already created all the files and added the code for you, so let's take a look.


### TableStorageContext

This file behaves like a DB context you might use for a relational database only we are using the `Azure.Data.Tables` nuget package.

This class will connect to the table store enabling read and write.

Azure table storage requires the use of class implementing `ITableEntity` and `PonyEntity` implements this.

### PonyService

This is a service that sits between the public facing API endpoints and the data store. It maps `PonyEntity` to `Pony` and back whilst implementing required logic such as sorting, filtering and null checking.

### TableStorageSeed

This class will read in the `PonyData.json` file and write it to a new table store.
