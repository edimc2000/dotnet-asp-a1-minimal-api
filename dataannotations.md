# Data Annotation Validation

*Note from instructor:*
My modification demonstrates C# data annotations to validate product data before adding it through the API. The validation ensures required fields, string length, and price range are enforced.

**Example: Add a valid product**

```
curl -X POST http://localhost:5000/product/add/ -H "Content-Type: application/json" -d "{\"ProductId\":16,\"Name\":\"Test Product\",\"Description\":\"A test product\",\"Price\":99.99}"
```

**Example: Add an invalid product (triggers validation errors)**

```
curl -X POST http://localhost:5000/product/add/ -H "Content-Type: application/json" -d "{\"ProductId\":17,\"Name\":\"A\",\"Description\":\"\",\"Price\":-5}"
```

**Sample output for invalid product:**

```
{
  "success": false,
  "message": "Validation failed",
  "errors": [
    "The field Name must be a string with a minimum length of 2 and a maximum length of 100.",
    "The Description field is required.",
    "The field Price must be between 0.01 and 10000."
  ]
}
```