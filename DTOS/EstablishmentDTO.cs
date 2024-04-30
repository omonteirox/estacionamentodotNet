using estacionamento.Models;

namespace estacionamento.DTOS
{
    public record EstablishmentDTO(string Name, string Address, string Phone, int MotorCycleSpots, int carSpots);
   
}
