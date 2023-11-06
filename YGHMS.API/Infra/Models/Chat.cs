namespace YGHMS.API.Infra.Models;

public partial class Chat : CommonModel
{
  public int Id { get; set; }
  public int SendedId { get; set; }
  public int ReceivedId { get; set; }
  public string Content { get; set; } = null!;
  public bool IsDeleted { get; set; }
  public virtual User Received { get; set; } = null!;
  public virtual User Sended { get; set; } = null!;
}