using System.Text.Json.Serialization;

namespace estacionamento.Models
{
    public class Establishment
    {
        public int Id { get; init; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int MotorcycleSpotsAvailable { get; set; }
        public int CarSpotsAvailable { get; set; }
        [JsonIgnore]
        public List<Vehicle>? Vehicles{ get; set; }
        [JsonIgnore]
        public List<InOutEstablishment>? InOutEstablishments { get; set; }

        public int SubtractMotorcycleSpots()
        {
            if (MotorcycleSpotsAvailable > 0)
            {
                MotorcycleSpotsAvailable--;
            }
            else
            {
                throw new Exception("No motorcycle spots available"); 
            }

            return MotorcycleSpotsAvailable; 
        }
        public int SubtractCarSpots()
        {
            if (CarSpotsAvailable > 0)
            {
                CarSpotsAvailable--; 
            }
            else
            {
                throw new Exception("No car spots available"); 
            }

            return CarSpotsAvailable; 
        }

    }
}
