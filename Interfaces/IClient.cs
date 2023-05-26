
using LegacyApp.Enums;

namespace LegacyApp.Interfaces
{
    public interface IClient
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ClientStatus ClientStatus { get; set; }

        public bool IsVeryImportant { get; }
        public bool IsImportant { get; }
    }
}