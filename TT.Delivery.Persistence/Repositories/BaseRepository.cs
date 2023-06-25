using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TT.Delieveries.Application;
using TT.Delieveries.Persistence;
using Microsoft.EntityFrameworkCore;
using TT.Delieveries.Persistence.Context;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    protected readonly DataContext Context;

    public BaseRepository(DataContext context)
    {
        Context = context;
    }
    
    public void Create(T entity)
    {
        Context.Add(entity);
    }

    public void Update(T entity)
    {
        Context.Update(entity);
    }

    public void Delete(T entity)
    {
        entity.DateCreated = DateTimeOffset.UtcNow;
        Context.Update(entity);
    }

    public Task<T> Get(int id, CancellationToken cancellationToken)
    {
        return Context.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public Task<List<T>> GetAll(CancellationToken cancellationToken)
    {
        return Context.Set<T>().ToListAsync(cancellationToken);
    }

    public Task<T> Get(string id, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}