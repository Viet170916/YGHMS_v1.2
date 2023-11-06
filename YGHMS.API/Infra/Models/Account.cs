namespace YGHMS.API.Infra.Models;

public partial class Account: CommonModel
{
  public int Id { get; set; }
  public int UserId { get; set; }
  public string UserName { get; set; } = null!;
  public string? Emai { get; set; }
  public string Password { get; set; } = null!;
  public int Status { get; set; }
  public int RoleId { get; set; }
  public virtual Role Role { get; set; }
  public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
  public virtual User User { get; set; } = null!;
}