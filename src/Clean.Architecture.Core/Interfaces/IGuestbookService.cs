using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Architecture.Core.Entities;

namespace Clean.Architecture.Core.Interfaces;
public interface IGuestbookService
{
  Task RecordEntry(Guestbook guestbook, GuestbookEntry newEntry);
}
