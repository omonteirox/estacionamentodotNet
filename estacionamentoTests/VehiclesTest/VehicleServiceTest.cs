using estacionamento.Data;
using estacionamento.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using estacionamento.Models.Enums;
using estacionamento.Services;

using MockQueryable.Moq;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace estacionamentoTests.VehiclesTest
{
    public class VehicleServiceTest
    {

        private readonly Mock<DataContext> mockContext;
        private readonly VehicleService service;
        public VehicleServiceTest()
        {
            var data = new List<Vehicle>
        {
            new Vehicle { Id = 1, Brand = "Brand1", Color = "Color1", Plate = "123", Type = VehicleEnum.CAR },
            new Vehicle { Id = 2, Brand = "Brand1", Color = "Color1", Plate = "12310", Type = VehicleEnum.CAR },
        }.AsQueryable();


            var mock = data.BuildMockDbSet();

            mockContext = new Mock<DataContext>();

            mockContext.Setup(p => p.Vehicles).Returns(mock.Object);
            service = new VehicleService(mockContext.Object);
        }

        [Fact]
        public async Task Delete_VehicleExists_RemovesVehicleAndSavesChanges()
        {

            // Act
            await service.RemoveAsync(1);

            // Assert
            mockContext.Verify(x => x.Vehicles.Remove(It.IsAny<Vehicle>()), Times.Once()); ;

            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async Task Delete_VehicleNotFound_ThrowsException()
        {
            // Act & Assert
            var exception = Assert.ThrowsAsync<Exception>(async () => await service.RemoveAsync(3));

            Assert.Equal("GVASYEX - Id não encontrado", exception.Result.Message);
        }


        [Fact]
        public async Task Create_Vehicle_ShouldReturnVeichle()
        {

            var car = new Vehicle { Id = 100, Brand = "Brand1", Color = "Color1", Plate = "123555555", Type = VehicleEnum.CAR };
            mockContext.Setup(x => x.AddAsync(car, It.IsAny<CancellationToken>()))
            .Returns(new ValueTask<EntityEntry<Vehicle>>(Task.FromResult((EntityEntry<Vehicle>)null)));

            var result = await service.CreateAsync(car);
            mockContext.Verify(x => x.Vehicles.AddAsync(car, It.IsAny<CancellationToken>()), Times.Once());
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
            Assert.IsType<Vehicle>(result);


        }
        [Fact]
        public async Task Create_VehicleWithExistentPlate_ThrowsException()
        {
            Vehicle car = new Vehicle
            {
                Brand = "123",
                Color = "123",
                Plate = "123",
                Type = VehicleEnum.CAR
            };
            var exception = Assert.ThrowsAsync<Exception>(async () => await service.CreateAsync(car));
            Assert.Equal("CVEX002 - Já existe veículo com essa placa cadastrada no sistema", exception.Result.Message);
        }
        [Fact]
        public async Task Create_VehicleIsNull_ThrowsException()
        {
            Vehicle car = null;
            var exception = Assert.ThrowsAsync<Exception>(async () => await service.CreateAsync(car));
            Assert.Equal("CVEX001 - Veículo está nulo", exception.Result.Message);
        }
        [Fact]
        public async Task GetByIdAsync_WithIdCorrect_ReturnsVeichle()
        {
            var car = await service.GetByIdAsync(1);

            Assert.IsType<Vehicle>(car);
        }
        [Fact]
        public async Task GetByIdAsync_WithIdNotFound_ThrowsException()
        {

            var exception = Assert.ThrowsAsync<Exception>(async () => await service.GetByIdAsync(3));
            Assert.Equal("GVASYEX - Id não encontrado", exception.Result.Message);
        }
        [Fact]
        public async Task GetByPlateAsync_WithPlateExisting_ReturnsVehicle()
        {
            var result = await service.GetByPlateAsync("123");
            Assert.IsType<Vehicle>(result);
        }

        [Fact]
        public async Task GetAll_ShouldReturn_ListOfElements()
        {
            var car = await service.GetAllAsync();
            Assert.Equal(2, car.Count);
        }
    }


}
