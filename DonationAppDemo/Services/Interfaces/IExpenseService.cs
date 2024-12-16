using DonationAppDemo.DTOs;
using System.Threading.Tasks;

namespace DonationAppDemo.Services.Interfaces
{
    public interface IExpenseService
    {
        Task AddExpense(ExpenseDto expenseDto);
        Task UpdateExpense(int id, ExpenseDto expenseDto);
        Task DeleteExpense(int id);
        Task<object> GetAllExpenses(int campaignId, int page, int pageSize);
    }
}