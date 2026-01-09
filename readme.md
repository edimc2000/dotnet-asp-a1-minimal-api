﻿﻿﻿_This README.md file was created with assistance from AI - DeepSeek. 
The remaining code was developed without AI support, except where explicitly 
indicated in the code comments._

# Minimal API - using ASP.NET
This is a minimal API implementation using ASP.Net demonstrating API endpoints basic operations.
The basic operations include 
- **creating a new product** (Create using Post method),  
- **retrieving all the stored products** (Retrieve using Get method), 
- **searching for product using the ProductId** (search using Get Method) and 
- **deleting a product using the ProductId** (Delete using Delete method) 


## Requirements

- .NET SDK: Version 10.0.100
- No external libraries
- ANSI-capable terminal (optional, for colors)

# Files and Folders
```
│   .gitignore
│   appsettings.Development.json
│   appsettings.json
│   MinimalApi.csproj
│   MinimalApi.csproj.user
│   MinimalApi.slnx
│   ProductEndpoint.cs
│   Program.cs
│   readme.md

├───support
│       Helper.cs		//
│       JsonHelper.cs	//
│       ProductSeed.cs		// default products for querying without needing to add products first 

├───screenshots			// stores images for this readme file
```
