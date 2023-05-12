namespace OnlineShopping.Domain.Enum;

public enum StatusCode
{
    OK = 200,
    Created = 201,
    NoContent = 204,
    BadRequest = 400,
    Unauthorized = 401,
    Forbidden = 403,
    NotFound = 404,
    InternalServerError = 500,

    ProductsNotFound = 1,
    ProductNotFound = 2
}