using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Architecture.Core.Entities;
using Clean.Architecture.Core.Interfaces;
using Clean.Architecture.Core.ProjectAggregate.Specifications;
using Clean.Architecture.SharedKernel.Interfaces;

namespace Clean.Architecture.Core.Services;
public class GuestbookService : IGuestbookService
{
  private readonly IRepository<Guestbook> _repository;
  private readonly IMessageSender _messageSender;

  public GuestbookService(IRepository<Guestbook> repository, IMessageSender messageSender)
  {
    _repository = repository;
    _messageSender = messageSender;
  }
  public async Task RecordEntry(Guestbook guestbook, GuestbookEntry newEntry)
  {
    foreach (var entry in guestbook.Entries)
    {
      _messageSender.SendGuestbookNotificationEmail(entry.EmailAddress, newEntry.Message);
    }

    guestbook.Entries.Add(newEntry);
    await _repository.UpdateAsync(guestbook);
  }
}
