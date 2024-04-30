using estacionamento.Data;
using estacionamento.Models;
using Microsoft.EntityFrameworkCore;

namespace estacionamento.Services
{
    public class InOutEstablishmentService
    {
        private readonly DataContext _ctx;
        public InOutEstablishmentService(DataContext ctx)
        {
            _ctx = ctx;
        }

        public Task<List<InOutEstablishment>> GetAllAsync()

        {
            var result = _ctx.InOutEstablishments.ToListAsync();
            if (result == null)
            {
                throw new Exception("InOutException - Erro ao listar InOuts");
            }
            return result;
        }

        public async Task<InOutEstablishment> GetByIdAsync(int id)
        {
            var InOutEstablishment = await _ctx.InOutEstablishments.FirstOrDefaultAsync(x => x.Id == id);
            if (InOutEstablishment == null)
                throw new Exception("InOutException - Id não encontrado");
            return InOutEstablishment;
        }



        public async Task<InOutEstablishment> CreateAsync(InOutEstablishment inOutEstablishment)
        {
            if (inOutEstablishment == null)
                throw new Exception("InOutException - Informações nulas");

            using (var transaction = await _ctx.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await _ctx.InOutEstablishments.AddAsync(inOutEstablishment);

                    await _ctx.Entry(result.Entity).Reference(e => e.Vehicle).LoadAsync();
                    await _ctx.Entry(result.Entity).Reference(e => e.Establishment).LoadAsync();

                    var vehicle = result.Entity.Vehicle;
                    var establishment = result.Entity.Establishment;

                    if(vehicle == null || establishment == null)
                    {
                        throw new Exception("InOutException - Veículo ou Estabelecimento estão nulos.");
                    }
                    if (vehicle.Type == Models.Enums.VehicleEnum.CAR)
                    {
                        establishment.CarSpotsAvailable = establishment.SubtractCarSpots();
                    }
                    else
                    {
                        establishment.MotorcycleSpotsAvailable = establishment.SubtractMotorcycleSpots();
                    }

                    _ctx.Establishments.Update(establishment);

                    await _ctx.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return result.Entity;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception($"InOutException - Algo deu errado, por favor entre em contato com nossa equipe de suporte");
                }
            }
        }


        public async Task RemoveAsync(int id)
        {
            var InOutEstablishment = await GetByIdAsync(id);
            if (InOutEstablishment == null)
                throw new Exception("InOutException - Id não encontrado");

            _ctx.InOutEstablishments.Remove(InOutEstablishment);
            await _ctx.SaveChangesAsync();
        }

        public async Task<InOutEstablishment> closeEstablishmentAsync(InOutEstablishment inOutEstablishment)
        {
            if (inOutEstablishment == null)
            {
                throw new Exception("InOutException - Informações estão nulas");
            }
            

            using (var transaction = await _ctx.Database.BeginTransactionAsync())
            {
                try
                {
                    
                    var result = await GetByIdAsync(inOutEstablishment.Id);
                    if(result.DateEnd != null)
                    {
                        throw new Exception($"InOutException - InOut Já foi cancelado");
                    }

                    result.DateEnd = inOutEstablishment.DateEnd;
                    await _ctx.Entry(result).Reference(e=> e.Vehicle).LoadAsync();
                    await _ctx.Entry(result).Reference(e => e.Establishment).LoadAsync();
                    var vehicle = result.Vehicle;
                    var establishment = result.Establishment;
                    if (vehicle.Type == Models.Enums.VehicleEnum.CAR)
                    {
                        establishment.CarSpotsAvailable++;
                    }
                    else
                    {
                        establishment.MotorcycleSpotsAvailable++;
                    }

                    _ctx.Establishments.Update(establishment);

                    await _ctx.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return result;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception($"InOutException - Algo deu errado, por favor entre em contato com nossa equipe de suporte");
                }
            }
           
        }
    }
}
