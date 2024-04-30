using estacionamento.Data;
using estacionamento.Models;
using Microsoft.EntityFrameworkCore;

namespace estacionamento.Services
{
    public class EstablishmentService
    {
        private readonly DataContext _ctx;
        public EstablishmentService(DataContext dataContext)
        {
            _ctx = dataContext;
        }

        public Task<List<Establishment>> GetAllAsync()

        {
            var result = _ctx.Establishments.ToListAsync();
            if (result == null)
            {
                throw new Exception("EstablishmentException - Erro ao listar Estabelecimentos");
            }
            return result;
        }

        public async Task<Establishment> GetByIdAsync(int id)
        {
            var Establishment = await _ctx.Establishments.FirstOrDefaultAsync(x => x.Id == id);
            if (Establishment == null)
                throw new Exception("EstablishmentException - Id não encontrado");
            return Establishment;
        }

       

        public async Task<Establishment> CreateAsync(Establishment Establishment)
        {
            if (Establishment == null)
                throw new Exception("EstablishmentException - Estabelecimento está nulo");

                       
            var result = await _ctx.Establishments.AddAsync(Establishment);
            await _ctx.SaveChangesAsync();
            
            return result.Entity;

        }
        public async Task<Establishment> UpdateAsync(Establishment Establishment)
        {
            if (Establishment == null)
                throw new Exception("EstablishmentException - Estabelecimento está nulo");
            var existingEstablisment = await GetByIdAsync(Establishment.Id);
            existingEstablisment.Address = Establishment.Address;
            existingEstablisment.Phone = Establishment.Phone;
            existingEstablisment.CarSpotsAvailable = Establishment.CarSpotsAvailable;
            existingEstablisment.MotorcycleSpotsAvailable = Establishment.MotorcycleSpotsAvailable;
            existingEstablisment.Name = Establishment.Name;

            _ctx.Establishments.Update(existingEstablisment);

            //var result = await _ctx.Establishments.AddAsync(Establishment);
            await _ctx.SaveChangesAsync();

            return existingEstablisment;

        }

        public async Task RemoveAsync(int id)
        {
            var Establishment = await GetByIdAsync(id);
            if (Establishment == null)
                throw new Exception("EstablishmentException - Id não encontrado");

            _ctx.Establishments.Remove(Establishment);
            await _ctx.SaveChangesAsync();
        }
    }
}
