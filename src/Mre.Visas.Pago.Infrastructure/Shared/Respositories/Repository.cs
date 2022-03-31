﻿using Microsoft.EntityFrameworkCore;
using Mre.Visas.Pago.Application.Repositories;
using Mre.Visas.Pago.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mre.Visas.Pago.Infrastructure.Shared.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        #region Constructors

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion Constructors

        #region Attributes

        protected readonly ApplicationDbContext _context;

        #endregion Attributes

        #region Methods

        public async Task<(bool, string)> DeleteAsync(string id)
        {
            var entity = await GetByIdAsync(id).ConfigureAwait(false);
            if (entity is null)
            {
                return (false, null);
            }

            try
            {
                _context.Set<T>().Remove(entity);

                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.InnerException is null ? ex.Message : ex.InnerException.Message);
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .ToArrayAsync()
                .ConfigureAwait(false);
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _context.Set<T>()
                .FindAsync(id)
                .ConfigureAwait(false);
        }

        public async Task<(bool, string)> InsertAsync(T entity)
        {
            try
            {
                await _context.Set<T>()
                    .AddAsync(entity)
                    .ConfigureAwait(false);

                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.InnerException is null ? ex.Message : ex.InnerException.Message);
            }
        }

        public async Task<(bool, string)> InsertRangeAsync(IReadOnlyCollection<T> entities)
        {
            try
            {
                await _context.Set<T>()
                    .AddRangeAsync(entities)
                    .ConfigureAwait(false);

                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.InnerException is null ? ex.Message : ex.InnerException.Message);
            }
        }

        public (bool, string) Update(T entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;

                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.InnerException is null ? ex.Message : ex.InnerException.Message);
            }
        }

        public Task<(bool, string)> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}