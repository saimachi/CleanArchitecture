using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Architecture.SharedKernel.Interfaces;

namespace Clean.Architecture.Core.Entities;
public class Guestbook : BaseEntity, IAggregateRoot
{
  public string Name { get; set; }
  public List<GuestbookEntry> Entries { get; } = new List<GuestbookEntry>();
}
