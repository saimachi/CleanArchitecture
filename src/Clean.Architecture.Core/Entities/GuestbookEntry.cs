using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Architecture.Core.Entities;
public class GuestbookEntry : BaseEntity
{
  public string EmailAddress { get; set; }
  public string Message { get; set; }
  public DateTimeOffset DateTimeCreated { get; set; } = DateTimeOffset.Now;
}
