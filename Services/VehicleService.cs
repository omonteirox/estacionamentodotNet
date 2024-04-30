using estacionamento.Data;
using estacionamento.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace estacionamento.Services
{
    public class VehicleService
    {
        private readonly DataContext _ctx;

        public VehicleService(DataContext ctx)
        {
            _ctx = ctx;
        }

        public Task<List<Vehicle>> GetAllAsync()
            
        {
            var result = _ctx.Vehicles.ToListAsync();
            if (result == null)
            {
                throw new Exception("GV - Erro ao listar veículos");
            }
            return result;
        }

        public async Task<Vehicle> GetByIdAsync(int id)
        {
            var vehicle = await _ctx.Vehicles.FirstOrDefaultAsync(x => x.Id == id);
            if (vehicle == null)
                throw new Exception("GVASYEX - Id não encontrado");
            return vehicle;
        }

        public async Task<Vehicle> GetByPlateAsync(string plate)
        {
            var vehicle = await _ctx.Vehicles.FirstOrDefaultAsync(x => x.Plate == plate);
            
            return vehicle;
        }

        public async Task<Vehicle> CreateAsync(Vehicle vehicle)
        {
            if (vehicle == null)
                throw new Exception("CVEX001 - Veículo está nulo");

            
            var existingVehicle = await GetByPlateAsync(vehicle.Plate);
            if (existingVehicle != null)
                throw new Exception("CVEX002 - Já existe veículo com essa placa cadastrada no sistema");
            
            await _ctx.Vehicles.AddAsync(vehicle);
                await _ctx.SaveChangesAsync();
            var result = await _ctx.Vehicles.FirstOrDefaultAsync(x => x.Plate == vehicle.Plate);
                return result;
            
        }

        public async Task RemoveAsync(int id)
        {
            var vehicle = await GetByIdAsync(id);
            if (vehicle == null)
                throw new Exception("GVASYEX - Id não encontrado");

            _ctx.Vehicles.Remove(vehicle);
            await _ctx.SaveChangesAsync();
        }
    }
}
