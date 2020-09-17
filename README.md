# calculator
A simple calculator API service.

This application was bult using ASP.NET CORE 3.1. The logic was published into a Lambda function on AWS. The Lambda function is triggered by API Gateway. Being that the request was to showcase my ability to program in C#, I did not add a front-end although I could have easily hosted a front-end webpage in an S3 bucket that would call the API Gateway endpoints via AJAX calls. I also could have added authorization/authentication & privilege-level security to the application but it seemed like gold-plating. I tried to avoid gold-plating as much as possible while still showcasing my knowledge of key OOP concepts like abstraction (see abstract class `BaseModel`), encapsulation, inheritance (see interface `IBaseModel`), and polymorphism (see the use of generics in the instantiation of the base model/interface.) I also added validation and unit tests (written in XUnit).

While I don't think that it was necessary to use Lambda and API Gateway, I wanted to emphasize that I am experienced with AWS technologies and I am constantly considering ways to streamline the applications that I write and make the robust (so they can handle one user at a time or 10,000 users at a time.)

This application does not showcase my knowledge in other areas, but I want to state that I also spend a lot of time working with SQL and many AWS technologies that were not applicable to this project.

I chose to host this code in GitHub purely because I can do so for free. In an enterprise climate, if we were using AWS, I would use either TFS or AWS's code management tools.

## Endpoints
### [GET] /api/calculator/decimal/{op}
#### Route parameters:
* op: the mathematical operator. ("add", "subtract", "multiply", or "divide")
#### Query parameters:
* a: the first number to be considered in the equation
* b: the second number to be considered in the equation
#### Response model:
```json
{
  "a": decimal,
  "b": decimal,
  "operator": string,
  "result": decimal,
}
```
#### Example:
* request: /api/calculator/decimal/divide?a=3.25&b=4
* response:
  ```json
  {
    "a": 3.25,
    "b": 4,
    "operator": "divide",
    "result": 0.8125,
  }
  ```
### [GET] /api/calculator/integer/{op}
#### Route parameters:
* op: the mathematical operator. ("add", "subtract", "multiply", or "divide")
#### Query parameters:
* a: the first number to be considered in the equation
* b: the second number to be considered in the equation
#### Response model:
```json
{
  "a": int,
  "b": int,
  "operator": string,
  "result": decimal,
}
```
#### Example:
* request: /api/calculator/integer/add?a=3&b=7
* response:
    ```json
    {
      "a": 3,
      "b": 7,
      "operator": "add",
      "result": 10,
    }
    ```
