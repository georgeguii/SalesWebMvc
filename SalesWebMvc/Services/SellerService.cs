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

    public async Task Insert(Seller obj)
    {
        await _context.Seller.AddAsync(obj);
        _context.SaveChanges();
    }
}
