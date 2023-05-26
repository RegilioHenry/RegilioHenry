using System;
using System.Threading.Tasks;

namespace LegacyApp
{
    public class UserCreditService : IUserCreditService, ICreditService
    {
        public int GetCreditLimit(string firstname, string surname, DateTime dateOfBirth)
        {
            return 500;

        }
        public async Task<int> GetCreditLimitAsync(string firstname, string surname, DateTime dateOfBirth)
        {
            var result = await Task<int>.Run(()=> GetCreditLimit(firstname, surname, dateOfBirth));
            return result;
        }
    }
}