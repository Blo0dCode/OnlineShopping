using OnlineShopping.Domain.Enum;

namespace OnlineShopping.Domain.Response;

public class BaseResponse<T> : IBaseResponse<T>
{
    public string Description { get; init; }
    public StatusCode StatusCode { get; init; }
    public T Data { get; set; }
}

public interface IBaseResponse<T>
{
    T Data { get; set; }
    public StatusCode StatusCode { get; }
    public string Description { get; }
}