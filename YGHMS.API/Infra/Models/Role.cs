namespace YGHMS.API.Infra.Models;

public partial class Role : CommonModel
{
  public int Id { get; set; }
  public string Name { get; set; } = null!;
  public string? Description { get; set; }
  public bool IsDeleted { get; set; }
  public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
  public virtual ICollection<User> Users { get; set; } = new List<User>();
}