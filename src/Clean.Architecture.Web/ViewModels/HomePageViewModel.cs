using Clean.Architecture.Core.Entities;

namespace Clean.Architecture.Web.ViewModels;

public class HomePageViewModel
{
  public string GuestbookName { get; set; }
  public List<GuestbookEntry> PreviousEntries { get; set; }
  public GuestbookEntry NewEntry { get; set; }
}
