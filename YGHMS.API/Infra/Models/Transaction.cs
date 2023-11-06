namespace YGHMS.API.Infra.Models;

public partial class Transaction : CommonModel
{
  public int Id { get; set; }
  public string? Name { get; set; }
  public int Type { get; set; }
  public double Amount { get; set; }
  public int? PostId { get; set; }
  public int UserId { get; set; }
  public int Status { get; set; }
  public string? Description { get; set; }
  public sbyte IsDelete { get; set; }
  public virtual Accommodation? Post { get; set; }
}