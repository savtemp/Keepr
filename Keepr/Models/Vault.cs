namespace Keepr.Models
{
  public class Vault : RepoItem<int>
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public bool? isPrivate { get; set; }
    public string CreatorId { get; set; }
    public Profile Creator { get; set; }
  }
}