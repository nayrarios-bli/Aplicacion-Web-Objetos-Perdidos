namespace ObjetosPerdidosUCB.Api.Controllers.Response
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public ApiResponse(T data)
        {
            Data = data;
        }
    }
}
