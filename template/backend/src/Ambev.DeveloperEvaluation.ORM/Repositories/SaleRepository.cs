using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    /// <summary>
    /// Implementation of ISaleRepository using Entity Framework Core
    /// </summary>
    public class SaleRepository : ISaleRepository
    {
        private readonly DefaultContext _context;

        /// <summary>
        /// Initializes a new instance of UserRepository
        /// </summary>
        /// <param name="context">The database context</param>
        public SaleRepository(DefaultContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new sale in the repository
        /// </summary>
        /// <param name="sale">The sale to create</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created sale</returns>
        public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            await _context.Sale.AddAsync(sale, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return sale;
        }
            
        /// <summary>
        /// Retrieves a sale by their unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the sale</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The sale if found, null otherwise</returns>
        public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Sale.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }

        /// <summary>
        /// Retrieves all sales in the repository
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>All sales if found, null otherwise</returns>
        public async Task<List<Sale?>> GetAllbyStatusAsync(Status status, CancellationToken cancellationToken = default)
        {
            return _context.Sale.Where(x => x.Status == status).ToList()!;
        }

        /// <summary>
        /// Deletes a sale from the repository
        /// </summary>
        /// <param name="id">The unique identifier of the sale to delete</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>True if the sale was deleted, false if not found</returns>
        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var sale = await GetByIdAsync(id, cancellationToken);
            if (sale == null)
                return false;
            _context.Sale.Remove(sale);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        /// <summary>
        /// Update a new sale in the repository
        /// </summary>
        /// <param name="sale">The sale to update</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The updated sale</returns>
        public async Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            var existing = await _context.Sale
                .Include(s => s.Items)
                .FirstOrDefaultAsync(s => s.Id == sale.Id, cancellationToken);

            if (existing is null)
                throw new KeyNotFoundException($"Sale with ID {sale.Id} not found.");

            _context.Entry(existing).CurrentValues.SetValues(sale);

            var itemsToRemove = existing.Items
                .Where(ei => !sale.Items.Any(i => i.Id == ei.Id))
                .ToList();

            _context.Set<SaleItem>().RemoveRange(itemsToRemove);

            foreach (var item in sale.Items)
            {
                var existingItem = existing.Items.FirstOrDefault(i => i.Id == item.Id);
                if (existingItem is null)
                    _context.Set<SaleItem>().Add(item);
                else
                    _context.Entry(existingItem).CurrentValues.SetValues(item);
            }

            await _context.SaveChangesAsync(cancellationToken);
            return existing;
        }
    }
}
