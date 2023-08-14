﻿using App.Core.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace App.Core.DataAccess.EntityFramework;

public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
    where TEntity : class, IEntity, new()
    where TContext : DbContext, new()
{
    public void Add(TEntity entity)
    {
        using (var context = new TContext())
        {
            EntityEntry addedEntity = context.Entry(entity);
            addedEntity.State = EntityState.Added;
            context.SaveChanges();
        }
    }

    public void Delete(TEntity entity)
    {
        using (var context = new TContext())
        {
            EntityEntry addedEntity = context.Entry(entity);
            addedEntity.State = EntityState.Deleted;
            context.SaveChanges();
        }
    }

    public TEntity Get(Expression<Func<TEntity, bool>> filter = null!)
    {
        using var context = new TContext();
        return context.Set<TEntity>().SingleOrDefault(filter)!;
    }

    public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null!)
    {
        using var context = new TContext();
        return filter == null
            ? context.Set<TEntity>().ToList()
            : context.Set<TEntity>().Where(filter).ToList();
    }

    public void Update(TEntity entity)
    {
        using (var context = new TContext())
        {
            EntityEntry addedEntity = context.Entry(entity);
            addedEntity.State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}

