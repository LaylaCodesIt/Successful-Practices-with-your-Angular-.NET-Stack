+++
archetype = "chapter"
title = "Data Storage"
weight = 3
chapter= 3.3
+++

We'll be using Azure Table Storage for our data store.

Azure Table Storage is a NoSQL data storage service that provides a key/attribute store with a schema-less design. 
It is designed for storing large amounts of structured, non-relational data. Azure Table Storage is highly scalable, allowing for massive data storage and fast access times, making it ideal for applications requiring flexible data schemas, such as web applications, IoT, and logging systems.

Key Features:
- **Schema-less Design**: Supports a flexible data model, allowing each entity to have different properties.
- **Scalability**: Capable of handling large volumes of data and high transaction rates.
- **Cost-Effective**: Pay only for the storage you use, making it an economical choice for large datasets.
- **High Availability**: Built-in replication ensures data durability and availability.
- **Simple Query Model**: Supports OData-based queries for efficient data retrieval.

> What is "OData"? - OData (Open Data Protocol) is a standardized protocol used for building and consuming RESTful APIs. It allows for querying and manipulating data using simple HTTP requests. OData enables interoperability between data sources and clients by providing a common data access interface, supporting features like filtering, sorting, and pagination. Developed by Microsoft, OData aims to simplify data sharing across disparate systems and is widely adopted in enterprise environments for integrating various applications and services.