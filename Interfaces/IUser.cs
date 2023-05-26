using System;

namespace LegacyApp.Interfaces
{
    public interface IUser
    {
        public int Id { get; set; }

        public string Firstname { get; set; }

        public string Surname { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string EmailAddress { get; set; }

        public bool HasCreditLimit { get; set; }

        public int CreditLimit { get; set; }
        public bool HasNotEnoughCredit { get; }
        public void DoubleCreditLimit();
        public void DoCreditCheck();
        public void SkipCreditCheck();


        public IClient Client { get; set; }

        public IUserService UserService { get; set; }
        public void Save();
    }
}