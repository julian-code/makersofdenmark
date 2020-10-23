using FluentValidation;
using FluentValidation.Results;
using FluentValidation.Validators;
using MakersOfDenmark.Domain.Models;
using MakersOfDenmark.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MakersOfDenmark.Application.Commands.Validators
{
    /// <summary>
    /// A validator which checks if a given entity exists in database. It will be attached to the context, and therefore can b
    /// </summary>
    /// <typeparam name="TEntity">The entity type to check for</typeparam>
    /// <typeparam name="TId">The type of Id the entity type uses</typeparam>
	public class ExistsInDatabase<TEntity, TId> : AsyncValidatorBase where TEntity : Entity<TId> 
	{
        private readonly DbContext _context;
        private readonly string _toInclude;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="toInclude">Can be used to load a collection into the entity</param>
        public ExistsInDatabase(DbContext context, string toInclude = null)
			: base("{Type}:{Value} not found")
		{
            _context = context;
            _toInclude = toInclude;
        }

        protected override async Task<bool> IsValidAsync(PropertyValidatorContext context, CancellationToken cancellation)
        {
            if (typeof(TId) != context.PropertyValue.GetType())
            {
                throw new ApplicationException($"{typeof(TId).Name} does not match {context.PropertyValue.GetType()}");
            }

            var result = await _context.Set<TEntity>().FindAsync(context.PropertyValue);
            if (result is null)
            {
                context.MessageFormatter.AppendArgument("Type", typeof(TEntity).Name);
                context.MessageFormatter.AppendArgument("Value", context.PropertyValue);
                return false;
            }

            if (!string.IsNullOrEmpty(_toInclude))
            {
                await _context.Entry(result).Collection(_toInclude).LoadAsync();
            }

            return true;
        }
    }
}

