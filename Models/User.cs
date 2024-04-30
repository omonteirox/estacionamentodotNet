using estacionamento.Models.Enums;
using System.Text.Json.Serialization;

namespace estacionamento.Models
{
    public class User
    {
        [JsonIgnore]
        public int Id { get; init; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public TypeUserEnum TypeUserEnum { get; set; }

    }
}
