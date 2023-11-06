namespace YGHMS.API.Infra.Models;

public partial class UserHighlight : CommonModel
{
  /// <summary>
  /// id
  /// </summary>
  public int Id { get; set; }

  public int? UserId { get; set; }
  public int? PublicationId { get; set; }
  public int? Title { get; set; }
  public string? Description { get; set; }
  public sbyte IsDelete { get; set; }
  public virtual Publication? Publication { get; set; }
  public virtual User? User { get; set; }
}