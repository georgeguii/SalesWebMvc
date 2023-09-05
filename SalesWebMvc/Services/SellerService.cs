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

    public async Task<List<Seller>> FindAll()
    {
        return await _context.Seller.ToListAsync();
    }

    public async Task<Seller> FindById(int id)
    {
        return await _context.Seller.Include(s => s.Department).SingleOrDefaultAsync(s => s.Id == id);
    }

    public async Task Insert(Seller obj)
    {
        await _context.Seller.AddAsync(obj);
        _context.SaveChanges();
    }

    public async Task Remove(int id)
    {
        var obj = await _context.Seller.SingleOrDefaultAsync(s => s.Id == id);
        if (obj != null)
        {
            _context.Seller.Remove(obj);
            await _context.SaveChangesAsync();
        } 
    }

    public async Task Update(Seller obj)
    {
        if(!_context.Seller.Any(s => s.Id == obj.Id))
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
