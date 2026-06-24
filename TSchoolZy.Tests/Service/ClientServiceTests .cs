using Application.Dto;
using Application.Service;
using AutoMapper;
using Core.Entity;
using Core.Interface;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSchoolZy.Tests.Service.Service
{
    public class ClientServiceTests
    {
        private readonly Mock<IGenericRepository<Client>> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ClientService _service;

        public ClientServiceTests()
        {
            _mockRepository = new Mock<IGenericRepository<Client>>();
            _mockMapper = new Mock<IMapper>();

            _service = new ClientService(
                _mockRepository.Object,
                _mockMapper.Object
            );
        }


        [Fact]
        public async Task GetAllAsync_ShouldReturnMapperClients()
        {
            //Arrange

            var clients = new List<Client>
            {
                new Client {Id=1},
                new Client {Id=2},
            };
            var ClientDto = new List<ClientDto>
            {
                new ClientDto {Id=1 },
                new ClientDto {Id=2 }
            };
            _mockRepository.Setup(x => x.GetAllAsync())
                .ReturnsAsync(clients);

            _mockMapper.Setup(x => x.Map<IEnumerable<ClientDto>>(clients))
                .Returns(ClientDto);

            //Act
            var result = await _service.GetAllAsync();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());

            _mockRepository.Verify(x => x.GetAllAsync(), Times.Once());

            _mockMapper.Verify(x => x.Map<IEnumerable<ClientDto>>(clients), Times.Once);


        }
        [Fact]
        public async Task GetByIdAsync_WhenClientExists_ShouldReturnMappedClient()
        {

            //Arrange 
            var clientId = 1;

            var client = new Client { Id = clientId };

            var clientDto = new ClientDto { Id = clientId };

            _mockRepository.Setup(x => x.GetByIdAsync(clientId))
                .ReturnsAsync(client);

            _mockMapper.Setup(x => x.Map<ClientDto>(client))
                .Returns(clientDto);

            //act
            var result = await _service.GetByIdAsync(clientId);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(clientId, result.Id);
            _mockRepository.Verify(x => x.GetByIdAsync(clientId), Times.Once());
            _mockMapper.Verify(x => x.Map<ClientDto>(client), Times.Once);


        }

        [Fact]

        public async Task AddAsync_WhenSaveSuccess_ShouldAddClientAndReturnTrue()
        {


            //Arrange
            var dto = new ClientDto { Name = "Test" };
            var entity = new Client { Name = "Test" };

            _mockMapper
                .Setup(x => x.Map<Client>(dto)).Returns(entity);

            _mockRepository
          .Setup(x => x.SaveChangesAsync())
          .ReturnsAsync(true);

            // Act
            var result = await _service.AddAsync(dto);

            // Assert
            Assert.True(result);

            _mockMapper.Verify(x => x.Map<Client>(dto), Times.Once);
            _mockRepository.Verify(x => x.AddAsync(entity), Times.Once);
            _mockRepository.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task AddAsync_WhenSaveFails_ShouldReturnFalse()
        {
            //Arrange
            var dto = new ClientDto { Name = "Test" };
            var entity = new Client { Name = "Test" };

            _mockMapper
                .Setup(x => x.Map<Client>(dto))
                .Returns(entity);

            _mockRepository
                .Setup(x => x.AddAsync(entity))
                .Returns(Task.CompletedTask);

            _mockRepository
                .Setup(x => x.SaveChangesAsync())
                .ReturnsAsync(false);

            // Act
            var result = await _service.AddAsync(dto);

            // Assert
            Assert.False(result);

            _mockMapper.Verify(x => x.Map<Client>(dto), Times.Once);
            _mockRepository.Verify(x => x.AddAsync(entity), Times.Once);
            _mockRepository.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_WhenClientExists_ShouldUpdateClientAndReturnTrue()
        {
            // Arrange
            var clientId = 1;

            var dto = new ClientDto
            {
                 Name = "Updated Client"
            };

            var existingClient = new Client
            {
                Id = clientId,
                 Name = "Old Client"
            };

            _mockRepository
                .Setup(x => x.GetByIdAsync(clientId))
                .ReturnsAsync(existingClient);

            _mockRepository
                .Setup(x => x.SaveChangesAsync())
                .ReturnsAsync(true);

            // Act
            var result = await _service.UpdateAsync(clientId, dto);

            // Assert
            Assert.True(result);

            _mockRepository.Verify(x => x.GetByIdAsync(clientId), Times.Once);

            _mockMapper.Verify(
                x => x.Map(dto, existingClient),
                Times.Once
            );

            _mockRepository.Verify(x => x.Update(existingClient), Times.Once);
            _mockRepository.Verify(x => x.SaveChangesAsync(), Times.Once);
        }


        [Fact]
        public async Task UpdateAsync_WhenClientNotFound_ShouldReturnFalse()
        {
            // Arrange
            var clientId = 1;

            var dto = new ClientDto
            {
                 Name = "Updated Client"
            };

            _mockRepository
                .Setup(x => x.GetByIdAsync(clientId))
                .ReturnsAsync((Client?)null);

            // Act
            var result = await _service.UpdateAsync(clientId, dto);

            // Assert
            Assert.False(result);

            _mockRepository.Verify(x => x.GetByIdAsync(clientId), Times.Once);

            _mockMapper.Verify(
                x => x.Map(It.IsAny<ClientDto>(), It.IsAny<Client>()),
                Times.Never
            );

            _mockRepository.Verify(
                x => x.Update(It.IsAny<Client>()),
                Times.Never
            );

            _mockRepository.Verify(
                x => x.SaveChangesAsync(),
                Times.Never
            );
        }


        [Fact]
        public async Task DeleteAsync_WhenClientExists_ShouldDeleteClientAndReturnTrue()
        {
            // Arrange
            var clientId = 1;

            var existingClient = new Client
            {
                Id = clientId
            };

            _mockRepository
                .Setup(x => x.GetByIdAsync(clientId))
                .ReturnsAsync(existingClient);

            _mockRepository
                .Setup(x => x.SaveChangesAsync())
                .ReturnsAsync(true);

            // Act
            var result = await _service.DeleteAsync(clientId);

            // Assert
            Assert.True(result);

            _mockRepository.Verify(x => x.GetByIdAsync(clientId), Times.Once);
            _mockRepository.Verify(x => x.Delete(existingClient), Times.Once);
            _mockRepository.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_WhenClientNotFound_ShouldReturnFalse()
        {
            // Arrange
            var clientId = 1;

            _mockRepository
                .Setup(x => x.GetByIdAsync(clientId))
                .ReturnsAsync((Client?)null);

            // Act
            var result = await _service.DeleteAsync(clientId);

            // Assert
            Assert.False(result);

            _mockRepository.Verify(x => x.GetByIdAsync(clientId), Times.Once);

            _mockRepository.Verify(
                x => x.Delete(It.IsAny<Client>()),
                Times.Never
            );

            _mockRepository.Verify(
                x => x.SaveChangesAsync(),
                Times.Never
            );
        }

    }
}