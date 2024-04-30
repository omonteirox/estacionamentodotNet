using estacionamento.Utils;
using System.Text.Json.Serialization;

namespace estacionamento.DTOS
{

    public record CreateinOutEstablishmentDTO(int EstablishmentId, int VehicleId);

    public class DateEndinOutEstablishmentDTO {
        public int Id { get; set; }
        
        public string DateEnd { get; set;}
        public DateEndinOutEstablishmentDTO(DateTime dateEnd)
        {
            DateEnd = dateEnd.ToString("dd/MM/yyyy-HH:mm:ss.fff");
        }


    }

}
