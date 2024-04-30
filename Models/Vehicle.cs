using estacionamento.Models.Enums;
using System.Text.Json.Serialization;

namespace estacionamento.Models
{
    public class Vehicle : IEquatable<Vehicle>
    {
        public int Id { get; init; }
        public string Brand { get; set; }
        public string Color { get; set; }
        public string Plate { get; set; }
        public VehicleEnum Type { get; set; }
        public int EstablishmentId { get; set; }
        [JsonIgnore]
        public Establishment Establishment { get; set; }
        [JsonIgnore]
        public List<InOutEstablishment> InOutEstablishments { get; set; }
        public bool Equals(Vehicle? other)
        {
            return Id == other.Id &&
                     Brand == other.Brand &&
                     Color == other.Color &&
                     Plate == other.Plate &&
                     Type == other.Type;
        }
    }
}
