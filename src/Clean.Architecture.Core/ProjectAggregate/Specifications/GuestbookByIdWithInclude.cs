using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using Clean.Architecture.Core.Entities;

namespace Clean.Architecture.Core.ProjectAggregate.Specifications;
public class GuestbookByIdWithInclude : Specification<Guestbook>, ISingleResultSpecification
{
  public GuestbookByIdWithInclude(int id, string include)
  {
    Query.Include(include).Where(e => e.Id == id);
  }
}
