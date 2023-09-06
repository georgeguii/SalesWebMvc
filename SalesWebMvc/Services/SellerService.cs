using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Services;

public class SellerService
{
    private readonly SalesWebMvcContext _context;
    public SellerService(SalesWebMvcContext context)
    {
        _context = context;
    }

    public async Task<List<Seller>> FindAllAsync()
    {
        return await _context.Seller.ToListAsync();
    }

    public async Task<Seller> FindByIdAsync(int id)
    {
        return await _context.Seller.Include(s => s.Department).SingleOrDefaultAsync(s => s.Id == id);
    }

    public async Task InsertAsync(Seller obj)
    {
        await _context.Seller.AddAsync(obj);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(int id)
    {
        try
        {
            var obj = await _context.Seller.FindAsync(id);
            _context.Seller.Remove(obj);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new IntegrityException(ex.Message);
        }
    }

    public async Task UpdateAsync(Seller obj)
    {
        var hasAny = await _context.Seller.AnyAsync(s => s.Id == obj.Id);
        if (!hasAny)
        {
            throw new NotFoundException("Id not found");
        }

        try
        {
            _context.Seller.Update(obj);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new DbConcurrencyException(ex.Message);
        }
    }
}
