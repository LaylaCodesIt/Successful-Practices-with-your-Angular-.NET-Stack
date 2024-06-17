---
title: "HTTP Verbs explained"
date: 2024-01-05T16:36:46Z
draft: false
weight: 1001
type: "subpost"
---


HTTP verbs, also known as HTTP methods, are a set of request methods used by HTTP protocols to perform actions on resources.
 The most common HTTP verbs include GET, POST, PUT, DELETE, PATCH, OPTIONS, and HEAD.


- **GET**: Retrieve data from the server (safe and idempotent).
- **POST**: Submit data to the server to create a new resource (not idempotent).
- **PUT**: Update or create a resource at the specified URI (idempotent).
- **DELETE**: Remove a resource at the specified URI (idempotent).
- **PATCH**: Apply partial modifications to a resource (not necessarily idempotent).
- **OPTIONS**: Describe the communication options for the target resource.
- **HEAD**: Same as GET but only retrieves the headers, not the body.


>***Idempotent*** is a term used in computing and mathematics to describe an operation that can be applied multiple times without changing the result beyond the initial application. In the context of HTTP methods, an idempotent method means that making multiple identical requests will have the same effect as making a single request.