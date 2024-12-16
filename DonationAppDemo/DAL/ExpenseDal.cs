using DonationAppDemo.DAL.Interfaces;
using DonationAppDemo.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonationAppDemo.DAL
{
    public class ExpenseDal : IExpenseDal
    {
        private readonly DonationDbContext _dbContext;

        public ExpenseDal(DonationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Expense expense)
        {
            await _dbContext.Expense.AddAsync(expense);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Expense expense)
        {
            _dbContext.Expense.Update(expense);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Expense expense)
        {
            _dbContext.Expense.Remove(expense);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Expense> GetByIdAsync(int id)
        {
            return await _dbContext.Expense.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Expense>> GetAllAsync()
        {
            return await _dbContext.Expense.ToListAsync();
        }

        public async Task<IEnumerable<Expense>> GetByCampaignIdAsync(int campaignId)
        {
            return await _dbContext.Expense.Where(e => e.CampaignId == campaignId).ToListAsync();
        }
    }
}