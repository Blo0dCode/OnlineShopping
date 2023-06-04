namespace OnlineShopping.Domain.Enum;

public enum StatusCode
{
    //общие
    OK = 200,
    Created = 201,
    NoContent = 204,
    BadRequest = 400,
    Unauthorized = 401,
    Forbidden = 403,
    NotFound = 404,
    InternalServerError = 500,
    Deleted = 3,
    Updated = 5,

    
    //Product
    ProductsNotFound = 1,
    ProductNotFound = 2,
    
    //Category
    CategoryNotFound = 4,
    CategoriesNotFound = 5
}