using estacionamento.Models.Enums;
using System.Text;

namespace estacionamento.DTOS
{
    public record VehicleDTO(string Brand, string Color, string Plate, VehicleEnum Type, int EstablishmentId)
    {
    }
}
