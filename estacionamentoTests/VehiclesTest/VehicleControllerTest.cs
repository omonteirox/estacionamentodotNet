using estacionamento.Models;
using estacionamento.Services;
using estacionamento.Controllers;
using MockQueryable.Moq;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using estacionamento.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using estacionamento.Data;
using estacionamento.DTOS;

namespace estacionamentoTests.VehiclesTest
{
    public class VehicleControllerTest
    {
        private readonly Mock<VehicleService> mockService;
        private readonly VehicleController controller;
        private readonly Mock<DataContext> mockContext;

        public VehicleControllerTest()
        {
            // Dados simulados
            var data = new List<Vehicle>
            {
                new Vehicle { Id = 1, Brand = "Brand1", Color = "Color1", Plate = "123", Type = VehicleEnum.CAR },
                new Vehicle { Id = 2, Brand = "Brand1", Color = "Color1", Plate = "12310", Type = VehicleEnum.CAR },
            }.AsQueryable();

            var mockDbSet = data.BuildMockDbSet();

            // Mockando o DataContext
            mockContext = new Mock<DataContext>();
            mockContext.Setup(ctx => ctx.Vehicles).Returns(mockDbSet.Object);

            // Mockando a VehicleService com uma instância do DataContext mockada
            mockService = new Mock<VehicleService>(mockContext.Object);

            // Inicializando o controlador
            controller = new VehicleController(mockService.Object);
        }

        
        [Fact]
        public async Task getAllShouldReturnAllCars()
        {

            // Chamando o método do controlador
            var response = await controller.GetAllAsync();

            // Verificando se o retorno é uma resposta bem-sucedida
            Assert.IsType<OkObjectResult>(response);

            // Extraindo o conteúdo da resposta
            var content = (response as OkObjectResult).Value as ResponseModel<List<Vehicle>>;

            Assert.NotNull(content);
            Assert.Equal(2, content.Data.Count);
        }

        [Fact]
        public async Task getByIdShouldReturnVehicle()
        {
            // Dados simulados
            var vehicle = new Vehicle { Id = 1, Brand = "Brand1", Color = "Color1", Plate = "123", Type = VehicleEnum.CAR };

            
            // Chamando o método do controlador
            var response = await controller.GetByIdAsync(1);

            // Verificando se o retorno é uma resposta bem-sucedida
            Assert.IsType<OkObjectResult>(response);

            // Extraindo o conteúdo da resposta
            var content = (response as OkObjectResult).Value as ResponseModel<Vehicle>;

            Assert.NotNull(content);
            Assert.Equal<Vehicle>(vehicle, content.Data);
            Assert.IsType <Vehicle>(content.Data);
        }
        [Fact]
        public async Task getByIdShouldExceptionVehicle()
        {
            // Dados simulados
            var vehicle = new Vehicle { Id = 1, Brand = "Brand1", Color = "Color1", Plate = "123", Type = VehicleEnum.CAR };


            // Chamando o método do controlador
            var response = await controller.GetByIdAsync(3);

            // Verificando se o retorno é uma resposta bem-sucedida
            Assert.IsType<BadRequestObjectResult>(response);

            // Extraindo o conteúdo da resposta
            var content = (response as BadRequestObjectResult).Value as ResponseModel<Exception>;
            
            Assert.NotNull(content);
            Assert.Contains("GVASYEX - Id não encontrado", content.Errors) ;

        }
        //[Fact]
        //public async Task createShouldCreateVehicle()
        //{
        //    // Dados simulados
            
            


        //    // Chamando o método do controlador
        //    var response = await controller.CreateAsync(vehicleDto);

        //    // Verificando se o retorno é uma resposta bem-sucedida
        //    Assert.IsType<OkObjectResult>(response);

        //    // Extraindo o conteúdo da resposta
        //    var content = (response as OkObjectResult).Value as ResponseModel<Vehicle>;

        //    Assert.NotNull(content);
        //    Assert.Equal(vehicle, content.Data);
        //}

        [Fact]
        public async Task getByPlateShouldReturnVehicle()
        {
            // Dados simulados
            var vehicle = new Vehicle { Id = 1, Brand = "Brand1", Color = "Color1", Plate = "123", Type = VehicleEnum.CAR };
      

            // Chamando o método do controlador
            var response = await controller.GetByPlateAsync("123");

            // Verificando se o retorno é uma resposta bem-sucedida
            Assert.IsType<OkObjectResult>(response);

            // Extraindo o conteúdo da resposta
            var content = (response as OkObjectResult).Value as ResponseModel<Vehicle>;

            Assert.NotNull(content);
            Assert.IsType<Vehicle>(content.Data);
            Assert.Equal<Vehicle>(vehicle, content.Data);
        }
        [Fact]
        public async Task getByPlateShouldThrowExceptionVehicle()
        {
            // Dados simulados
            var vehicle = new Vehicle { Id = 1, Brand = "Brand1", Color = "Color1", Plate = "123", Type = VehicleEnum.CAR };


            // Chamando o método do controlador
            var response = await controller.GetByPlateAsync("121233");

            // Verificando se o retorno é uma resposta bem-sucedida
            Assert.IsType<BadRequestObjectResult>(response);

            // Extraindo o conteúdo da resposta
            var content = (response as BadRequestObjectResult).Value as ResponseModel<Exception>;

            Assert.NotNull(content.Errors);
            Assert.Contains("GVASYEX002 - Placa não encontrada", content.Errors);
        }
        [Fact]
        public async Task deleteShouldRemoveVehicle()
        {
                       
            // Chamando o método do controlador
            var response = await controller.DeleteAsync(1);

            // Verificando se o retorno é uma resposta bem-sucedida
            Assert.IsType<NoContentResult>(response);
        }
    }
}
