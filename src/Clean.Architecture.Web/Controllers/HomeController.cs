using System.Diagnostics;
using Clean.Architecture.Core.Entities;
using Clean.Architecture.Core.ProjectAggregate.Specifications;
using Clean.Architecture.SharedKernel.Interfaces;
using Clean.Architecture.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Clean.Architecture.Web.Controllers;

/// <summary>
/// A sample MVC controller that uses views.
/// Razor Pages provides a better way to manage view-based content, since the behavior, viewmodel, and view are all in one place,
/// rather than spread between 3 different folders in your Web project. Look in /Pages to see examples.
/// See: https://ardalis.com/aspnet-core-razor-pages-%E2%80%93-worth-checking-out/
/// </summary>
public class HomeController : Controller
{
  private readonly IRepository<Guestbook> _projectRepository;
  public HomeController(IRepository<Guestbook> projectRepository) => _projectRepository = projectRepository;
  public async Task<IActionResult> Index()
  {
    if (!await _projectRepository.AnyAsync())
    {
      Guestbook newGuestBook = new Guestbook { Name = "My Guestbook" };
      newGuestBook.Entries.Add(new GuestbookEntry
      {
        EmailAddress = "demo@example.com",
        Message = "Great hotel!",
        DateTimeCreated = DateTime.UtcNow.AddHours(-1)
      });
      newGuestBook.Entries.Add(new GuestbookEntry
      {
        EmailAddress = "demo2@example.com",
        Message = "Vacation",
        DateTimeCreated = DateTime.UtcNow.AddHours(-2)
      });
      newGuestBook.Entries.Add(new GuestbookEntry
      {
        EmailAddress = "demo3@example.com",
        Message = "Work event",
        DateTimeCreated = DateTime.UtcNow.AddHours(-3)
      });
      await _projectRepository.AddAsync(newGuestBook);
    }

    Guestbook guestBook = await _projectRepository.FirstOrDefaultAsync(
      new GuestbookByIdWithInclude(1, "Entries")
    );

    return View(new HomePageViewModel
    {
      GuestbookName = guestBook.Name,
      PreviousEntries = guestBook.Entries
    });
  }

  [HttpPost]
  public async Task<IActionResult> Index(HomePageViewModel model)
  {
    if (ModelState.IsValid)
    {
      var guestBook = await _projectRepository.FirstOrDefaultAsync(
        new GuestbookByIdWithInclude(1, "Entries")
      );
      guestBook.Entries.Add(model.NewEntry);
      await _projectRepository.UpdateAsync(guestBook);

      model = new HomePageViewModel { GuestbookName = guestBook.Name, PreviousEntries = guestBook.Entries };
    }

    return View(model);
  }

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
