using LegacyApp.Interfaces;
using System;

namespace LegacyApp
{
    public class User : IUser
    {
        const int creditLimitThresHold = 500;
        public int Id { get; set; }

        public string Firstname { get; set; }

        public string Surname { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string EmailAddress { get; set; }

        public bool HasCreditLimit { get; set; }

        public int CreditLimit { get; set; }

        public IClient Client { get; set; }
        public IUserService UserService { get; set; }

        public bool HasNotEnoughCredit => HasCreditLimit && CreditLimit < creditLimitThresHold;

        public void DoubleCreditLimit()
        {
            CreditLimit *= 2;
        }

        public void DoCreditCheck()
        {
            HasCreditLimit = true;
        }

        public void SkipCreditCheck()
        {
            HasCreditLimit = false;
        }
        #region 
        /// <summary>
        /// Add the user to the database
        /// </summary>
        public virtual void Save()
        {
            //UserService.AddUserToDatabase(this);
            UserService.AddUserToDataBase(this);
        }
        #endregion
    }
}
