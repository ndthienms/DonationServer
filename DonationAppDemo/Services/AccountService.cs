using DonationAppDemo.DAL;
using DonationAppDemo.DTOs;
using DonationAppDemo.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DonationAppDemo.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountDal _accountDal;
        private readonly ITransactionDal _transactionDal;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountService(IAccountDal accountDal,
            ITransactionDal transactionDal,
            IHttpContextAccessor httpContextAccessor)
        {
            _accountDal = accountDal;
            _transactionDal = transactionDal;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> Delete(string phoneNum)
        {
            var result = await _accountDal.Delete(phoneNum);
            if (!result)
            {
                throw new Exception("Failed to delete account");
            }

            return result;
        }
        public async Task<bool> UpdateDisabledAccount(string phoneNum, bool disabled)
        {
            var result = await _accountDal.UpdateDisabledAccount(phoneNum, disabled);
            if (!result)
            {
                throw new Exception("Failed to do disabled account");
            }

            return result;
        }

        public async Task<bool> UpdateDisabledPersonalAccount(bool disabled)
        {
            // Get current user
            var handler = new JwtSecurityTokenHandler();
            string authHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            authHeader = authHeader.Replace("Bearer ", "");
            var jsonToken = handler.ReadToken(authHeader);
            var tokenS = handler.ReadJwtToken(authHeader) as JwtSecurityToken;
            var currentUserId = tokenS.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value.ToString();

            var result = await _accountDal.UpdateDisabledAccount(currentUserId, disabled);
            if (!result)
            {
                throw new Exception("Failed to do disabled account");
            }

            return result;
        }
    }
}
