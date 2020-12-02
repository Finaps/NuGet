using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Database.Core.Abstractions;

namespace Database.EF.Interfaces
{
  public interface IEntityFrameworkRepository<T> : IRepository<T> where T : class
  {
    ValueTask<T> GetByIdAsync(Guid id);
    void RemoveRange(IEnumerable<T> entities);
    void Remove(T entity);
  }
}