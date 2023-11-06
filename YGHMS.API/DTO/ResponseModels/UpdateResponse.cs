namespace YGHMS.API.DTO.ResponseModels;

public class UpdateResponse<T> : UpdateResponseBase
{
  public T? Data { get; set; }
}

public class UpdateResponseBase
{
  public bool Status { get; set; } = true;
  public string Message { get; set; } = "updates successfully";
}