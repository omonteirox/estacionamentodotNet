using estacionamento.Utils;
using System.Text.Json.Serialization;

namespace estacionamento.Models
{
    public class InOutEstablishment
    {
        public int Id { get; init; }
        [JsonConverter(typeof(CustomDateTimeConverter))]
        
        public DateTime DateStart { get; set; } = DateTime.UtcNow;
        [JsonConverter(typeof(CustomDateTimeConverter))]

        public DateTime? DateEnd { get; set; } = null;
        public int  EstablishmentId{ get; set; }
        [JsonIgnore]
        public Establishment? Establishment { get; set; }
        public int VehicleId { get; set; }
        [JsonIgnore]
        public Vehicle? Vehicle { get; set; }

        

    }
}
