# API Specification

We have been tasked with creating an API whereby users can search for and retrieve information on books for a search engine applications. Users are not required to authenticate with any identity provider.

The API portion of the application should be an ASP .NET Core Web API supporting RESTful endpoints. Data storage technology to be decided as and when required.

Users will have the following options:

- GET a single book by ID
  - Returned data should include:
    - Title
    - Author
    - ISBN
    - Cover Description / Summary
- GET a page of book records
  - Implies paging
  - Pages can be sorted by arbitrary fields
    - TBC
  - Paged data should include:
    - Title
    - Author
    - ISBN
    - ID
- POST a new book into storage
  - Essential data required:
    - Title
    - ISBN
    - Author name
    - All other fields optional
- PUT an update to a book
  - Essential fields are:
    - ID
    - Title
    - Author
    - Description
- DELETE a book record
  - The only required field is the ID of the book

## Default Response Codes

All endpoints should return these HTTP Status codes, along with any others that are listed in the API descriptions

- 400 Bad request
  - Returned when the request is badly formed
- 404 Not found
  - Returned when the book record cannot be found
- 500 Internal server
  - Returned when the server is unable to deal with the request
  - Perhaps an exception was raised

## Example Requests

### GET a single book

#### Request

A user can GET a book and return it's details with a request similar to the following:

``` http
GET /Books/{ID}
```

#### Response Codes

Along with the default response codes, the following response codes should be returned from this endpoint:

- 200 OK

#### Response Data

A successful GET request for a single book will return data similar to the following:

``` json
{
  "title" : "Lorem Ipsum: The Revenge",
  "author" : "A. N Author",
  "isbn" : "9780385602648",
  "Description" : "Lorem Ipsum: The Revenge is set in Germany in 1440. Johannes Gutenberg has invented the printing press, however a rash of murders happens at his workshop and he has to find and bring the murderer to the authorities",
  "coverImage" : "{Base64 encoded image}",
  "id": "{GUID}"
}
```

### GET a page of books

#### Request

A user can GET a filtered page of book records with a request similar to the following:

``` http
GET /Books/?pageNumber=1&perPage=10
```
Paged data is passed as part of a query string and contains the following fields:

- `pageNumber` [Required]
  - The page number to return
- `perPage` [Required]
  - The number of records to return per page
- `sortColumn` [Optional]
  - Which column (if any) to sort the returned data by
- `sortDirection` [Optional]
  - Which direction (ASC or DESC) to sort by
  - If not present DESC is asumed
- `searchQuery` [Optional]
  - An arbitrary string to all data fields by

#### Response Codes

Along with the default response codes, the following response codes should be returned from this endpoint:

- 200 OK

#### Response Data

A GET request for a page of book records will return data similar to the following:

```json
{
  "pageNumber" : 1,
  "perPage" : 10,
  "records" : [
    {
      "title" : "Lorem Ipsum: The Revenge",
      "author" : "A. N Author",
      "isbn" : "9780385602648",
      "Description" : "Lorem Ipsum: The Revenge is set in Germany in 1440. Johannes Gutenberg has invented the printing press, however a rash of murders happens at his workshop and he has to find and bring the murderer to the authorities",
      "coverImage" : "{Base64 encoded image}",
      "id": "{GUID}"
    },
    {
      "title" : "Lorem Ipsum: The Revenge",
      "author" : "A. N Author",
      "isbn" : "9780385602648",
      "Description" : "Lorem Ipsum: The Revenge is set in Germany in 1440. Johannes Gutenberg has invented the printing press, however a rash of murders happens at his workshop and he has to find and bring the murderer to the authorities",
      "coverImage" : "{Base64 encoded image}",
      "id": "{GUID}"
    },
    ...etc
  ]
}
```

### POST a new book into storage

#### Request

A new book record can be placed into storage using a request similar to the following:

``` http
POST /Books

Body:
{
  "title" : "Lorem Ipsum: The Revenge",
  "author" : "A. N Author",
  "isbn" : "9780385602648",
  "Description" : "Lorem Ipsum: The Revenge is set in Germany in 1440. Johannes Gutenberg has invented the printing press, however a rash of murders happens at his workshop and he has to find and bring the murderer to the authorities",
  "coverImage" : "{Base64 encoded image}"
}
```

Of the fields in the above request body, the following are mandatory:

- `title`
- `author`
- `isbn`

All other fields are optional during creation.

#### Response Codes

Along with the default response codes, the following response codes should be returned from this endpoint:

- 201 Created

#### Response Data

A POST request for creating a new book resource will return data similar to the following:

```json
{
  "id" : "{GUID}"
}
```

## PUT an update to a book

### Request

A book record can be updated with a request similar to:

``` http
PUT /Books

Body:
{
  "id" : "{GUID}",
  "title" : "Lorem Ipsum: The Revenge",
  "author" : "A. N Author",
  "isbn" : "9780385602648",
  "Description" : "Lorem Ipsum: The Revenge is set in Germany in 1440. Johannes Gutenberg has invented the printing press, however a rash of murders happens at his workshop and he has to find and bring the murderer to the authorities",
  "coverImage" : "{Base64 encoded image}"
}
```

All values from any supplied fields will be used to update the relevant record. If a value is not supplied, then it's value will not be updated.

As an example: if only the `id` and `title` fields are supplied with non-null or empty values, then the `title` value of the matching book record will be updated.

### Response Codes

- 204 Resource updated
  - Returned when the book record is successfully updated

### Response Data

The response from a PUT request to update a book record will return data similar to the following:

```json
{
  "id" : "{GUID}"
}
```

## DELETE a book record

### Request

A book record can be updated with a request similar to:

``` http
DELETE /Books

Body:
{
  "id" : "{GUID}"
}
```

If a matching record can be found, it will be deleted

### Response Codes

- 204 Resource updated
  - Returned when the book record is successfully updated

### Response Data

A DELETE request should have no response body.
