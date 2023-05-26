using System.Threading.Tasks;
using System;

namespace LegacyApp
{
    public interface ICreditService
    {
        public  Task<int> GetCreditLimitAsync(string firstname, string surname, DateTime dateOfBirth);
        
    }
}