using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;

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
        return await _context.Seller.SingleOrDefaultAsync(s => s.Id == id) ?? throw new NullReferenceException($"Not found a seller with {id} id.");
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
}
