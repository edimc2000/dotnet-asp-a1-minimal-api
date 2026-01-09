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
│   MinimalApi.slnx
│   ProductEndpoint.cs	// stores all the methods directly used by the main program
│   Program.cs			// main program detailing the endpoint routes
│   readme.md			// this readme file

├───support
│       Helper.cs		// helpers for the endpoint, stores response formats etc
│       JsonHelper.cs	// AI generated helper to detect malformed JSON 
│       ProductSeed.cs	// default products for querying without needing to add products first 

├───screenshots			// stores images for this readme file
```

## Build and Run (powershell)

- Restore dependencies 
	```
	dotnet restore
	```

- Build
	```
	dotnet build
	```

- Run
	```
	dotnet run
	```

## API Endpoints
- Create a new product
	- `Endpoint URL `			```		http://localhost:5220/product/show/all
		```
	- `Endpoint HTTP Method `			```		GET		
		```
	- `Authentication:`
	
		```		NONE		
		```
	- `Parameters:`
	
		```		NONE		
		```
	- `Request Examples:`
	
		```		NONE		
		```
	- `Response status codes:`
	
		```		Response Status: 200		Response Status Text: OK			Response Status: 405		Response Status Text: Method Not Allowed
		```
	- `Sample JSON Response:`
	
		```	
		Response Body: {
		  success: true,
		  message: 'Total of 15 products retrieved successfully',
		  data: [
			{
			  productId: 1,
			  name: 'Classic White T-Shirt',
			  description: '100% cotton crew neck t-shirt, perfect for everyday wear',
			  price: 19.99
			},
			{
			  productId: 2,
			  name: 'Slim Fit Jeans',
			  description: 'Dark wash denim jeans with stretch for comfort',
			  price: 59.99
			}	
		```
		

- Retrieving all the stored products
- Searching for product using the ProductId
- Deleting a product using the ProductId
