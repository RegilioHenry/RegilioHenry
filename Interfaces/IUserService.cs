namespace LegacyApp.Interfaces
{
    /// <summary>
    /// interface to the user services
    /// </summary>
    public interface IUserService
    {
        public bool AddUser(string firname, string surname, string email, DateTime dateOfBirth, int clientId);
        public Action<IUser> AddUserToDatabase { get; set; }
        public void AddUserToDataBase(IUser user);
        public Task<bool> AddUserAsync(string firname, string surname, string email, DateTime dateOfBirth, int clientId);
    }
}