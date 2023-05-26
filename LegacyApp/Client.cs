using LegacyApp.Enums;
using LegacyApp.Interfaces;

namespace LegacyApp
{
    public class Client : IClient
    {
        public const string veryImportantString = "VeryImportantClient";
        public const string importantString = "ImportantClient";
        public int Id { get; set; }

        public string Name { get; set; }

        public ClientStatus ClientStatus { get; set; }

        public bool IsVeryImportant
        {
            get
            {
                return Name == veryImportantString;
            }
        }
        public bool IsImportant
        {
            get
            {
                return Name == importantString;
            }
        }
    }
}
