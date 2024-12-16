using DonationAppDemo.Services.Interfaces;
using DonationAppDemo.DAL.Interfaces;
using DonationAppDemo.DTOs;
using DonationAppDemo.Models;
using System.Threading.Tasks;
using System.Linq;

namespace DonationAppDemo.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseDal _expenseDal;
        private readonly ICampaignStatisticsDal _campaignStatisticsDal;

        public ExpenseService(IExpenseDal expenseDal, ICampaignStatisticsDal campaignStatisticsDal)
        {
            _expenseDal = expenseDal;
            _campaignStatisticsDal = campaignStatisticsDal;
        }

        public async Task AddExpense(ExpenseDto expenseDto)
        {
            var expense = new Expense
            {
                Description = expenseDto.Description,
                ExpenseDate = expenseDto.ExpenseDate,
                Amount = expenseDto.Amount,
                OrganiserId = expenseDto.OrganiserId,
                CampaignId = expenseDto.CampaignId
            };

            await _expenseDal.AddAsync(expense);

            // Update Campaign Statistics
            await UpdateCampaignStatistics(expense.CampaignId);
        }

        public async Task UpdateExpense(int id, ExpenseDto expenseDto)
        {
            var expense = await _expenseDal.GetByIdAsync(id);
            if (expense == null)
            {
                throw new KeyNotFoundException("Expense not found");
            }

            expense.Description = expenseDto.Description;
            expense.ExpenseDate = expenseDto.ExpenseDate;
            expense.Amount = expenseDto.Amount;
            expense.OrganiserId = expenseDto.OrganiserId;

            await _expenseDal.UpdateAsync(expense);

            // Update Campaign Statistics
            await UpdateCampaignStatistics(expense.CampaignId);
        }

        public async Task DeleteExpense(int id)
        {
            var expense = await _expenseDal.GetByIdAsync(id);
            if (expense == null)
            {
                throw new KeyNotFoundException("Expense not found");
            }

            await _expenseDal.DeleteAsync(expense);

            // Update Campaign Statistics
            await UpdateCampaignStatistics(expense.CampaignId);
        }

        public async Task<object> GetAllExpenses(int campaignId, int page, int pageSize)
        {
            var expenses = await _expenseDal.GetByCampaignIdAsync(campaignId);
            var paginatedExpenses = expenses.Skip(page * pageSize).Take(pageSize).ToList();
            return new
            {
                expenses = paginatedExpenses,
                hasMore = expenses.Count() > (page + 1) * pageSize
            };
        }

        private async Task UpdateCampaignStatistics(int campaignId)
        {
            var totalAmount = (await _expenseDal.GetByCampaignIdAsync(campaignId))
                                .Sum(e => e.Amount);

            var campaignStatistics = await _campaignStatisticsDal.GetByCampaignIdAsync(campaignId);
            if (campaignStatistics != null)
            {
                campaignStatistics.TotalExpendedAmount = totalAmount;
                await _campaignStatisticsDal.UpdateAsync(campaignStatistics);
            }
        }
    }
}