using LegacyApp.Enums;
using LegacyApp.Interfaces;
using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LegacyApp
{
    public class UserService : IUserService
    {
        #region local declares
        const int minimumServiceAge = 21;
        private readonly IClientRepository clientRepository;
        private readonly IUserCreditService userCreditService;
        private readonly Action<IUserService> userService;
        IUser user;
        #endregion 

        #region constructors
        public UserService(IClientRepository clientrepository, IUserCreditService usercreditserviceclient,  Action<IUserService> adduserdelegate)
        {
            clientRepository = clientrepository;
            userCreditService = usercreditserviceclient;
            userService = adduserdelegate;
        }
        public UserService()
        {
            var method = typeof(UserDataAccess).GetMethod(nameof(UserDataAccess.AddUser));
            var action = Delegate.CreateDelegate(typeof(Action<User>),null, method) as Action<IUser>;
            AddUserToDatabase = action;
            clientRepository = new ClientRepository();
            
        }
        #endregion

        #region GetByIdAsync
        public async Task<bool> AddUserAsync(string firname, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            var result = await Task.Run(() => AddUser(firname, surname, email, dateOfBirth, clientId));
            return result;
        }
        #endregion 

        #region AddUser
        public bool AddUser(string firname, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            const bool UserAddedSuccessfully = true;
            const bool UserAddingFailed = false;
            bool hasMinimumAge = ValidateUserDateOfBirth(dateOfBirth);
            bool hasValidEmailAddress = ValidateUserEmail(email);
            if (!hasMinimumAge) {/* pass the message to the logger -- "regarding minimum age of {minimumAge} requirement" */ }
            if (!hasValidEmailAddress) { /*  pass the message to the logger -- "regarding invalid emailaddress {email}" */ }

            bool minimumrequirements = hasMinimumAge && hasValidEmailAddress;

            if (!minimumrequirements) return UserAddingFailed;

            IClient client = default;
            if( clientRepository is not null) client = clientRepository.GetById(clientId);

            user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                Firstname = firname,
                Surname = surname, 
                UserService = this
            };

            if (client != null &&  client.IsVeryImportant)
            {
                user.SkipCreditCheck();
            }
            else
            {
                user.DoCreditCheck();
                if(userCreditService is not null) user.CreditLimit = userCreditService.GetCreditLimit(user.Firstname, user.Surname, user.DateOfBirth);
                if (client != null && client.IsImportant) user.DoubleCreditLimit();
            }
            if (user.HasNotEnoughCredit) return UserAddingFailed;
            user.Save();
            return UserAddedSuccessfully;
        }
        #endregion

        #region AddUserAsync
        //public Task<bool> AddUserAsync(string firname, string surname, string email, DateTime dateOfBirth, int clientId)
        //{
        //    var result = false;

        //    return Task.FromResult( result );
        //}
        #endregion

        #region AddUserToDatabase
        public Action<IUser> AddUserToDatabase { get; set; }
        #endregion

        #region AddUserToDataBase
        public void AddUserToDataBase(IUser user)
        {
            if(AddUserToDatabase is not null) AddUserToDatabase(user);
        }
        #endregion
        
        #region ValidateuserEmail
        /// <summary>
        /// Validate the users email address
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool ValidateUserEmail(string email)
        {
            bool result = !string.IsNullOrEmpty(email);
            result = result && Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
            return result;
        }
        #endregion

        #region ValidateUserDateOfBirth
        /// <summary>
        /// Validate the users minimum age
        /// </summary>
        /// <param name="dateofbirth"></param>
        /// <returns></returns>
        public bool ValidateUserDateOfBirth(DateTime dateofbirth)
        {
            var currentDate = DateTime.Now;
            int age = currentDate.Year - dateofbirth.Year;
            if (currentDate.Month < dateofbirth.Month || (currentDate.Month == dateofbirth.Month && currentDate.Day < dateofbirth.Day)) age--;
            return age > minimumServiceAge;
        }
        #endregion
    }
}
