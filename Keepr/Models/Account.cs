namespace Keepr.Models
{
  public class Profile : RepoItem<string>
  {
    public new string Id { get; set; }
    public string Name { get; set; }
    public string Picture { get; set; }
  }

  public class Account : Profile
  {
    public string Email { get; set; }
  }

  public class VaultKeepViewModel : Profile
  {
    public int VaultKeepId { get; set; }
  }
}