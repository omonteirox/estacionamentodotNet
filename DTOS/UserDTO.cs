using estacionamento.Utils;
using System.Text.Json.Serialization;

namespace estacionamento.DTOS
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime ExpireDate { get; set; }
        public UserDTO(int Id, string Username, string Password, string Token, DateTime ExpireDates)
        {
            this.Id = Id;
            this.Username = Username;
            this.Password = Password;
            this.Token = Token;
            this.ExpireDate = ExpireDates;
        }
    }
    
}
