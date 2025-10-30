using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using MottuFind_C_.Domain.Repositories;
using Sprint1_C_.Application.DTOs.Requests;
using Sprint1_C_.Application.DTOs.Response;
using Sprint1_C_.Application.Services;
using Sprint1_C_.Domain.Entities;

namespace MottuFind_C_.Tests.Application
{
    public class PatioServiceTests
    {
        private readonly Mock<IPatioRepository> _repoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly PatioService _service;

        public PatioServiceTests()
        {
            _repoMock = new Mock<IPatioRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new PatioService(_repoMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task ObterTodos_DeveRetornarListaDePatioResponse()
        {
            // Arrange
            var patios = new List<Patio>
            {
                new Patio { Id = 1, Nome = "Pátio 1" },
                new Patio { Id = 2, Nome = "Pátio 2" }
            };

            var responses = new List<PatioResponse>
            {
                new PatioResponse { Id = 1, Nome = "Pátio 1" },
                new PatioResponse { Id = 2, Nome = "Pátio 2" }
            };

            _repoMock.Setup(r => r.ObterTodosAsync()).ReturnsAsync(patios);
            _mapperMock.Setup(m => m.Map<List<PatioResponse>>(patios)).Returns(responses);

            // Act
            var result = await _service.ObterTodos();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Pátio 1", result[0].Nome);
        }

        [Fact]
        public async Task ObterPorId_DeveRetornarPatioResponse()
        {
            // Arrange
            var patio = new Patio { Id = 10, Nome = "Pátio Norte" };
            var response = new PatioResponse { Id = 10, Nome = "Pátio Norte" };

            _repoMock.Setup(r => r.ObterPorIdAsync(10)).ReturnsAsync(patio);
            _mapperMock.Setup(m => m.Map<PatioResponse>(patio)).Returns(response);

            // Act
            var result = await _service.ObterPorId(10);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Pátio Norte", result.Nome);
        }

        [Fact]
        public async Task Criar_DeveRetornarPatioResponse()
        {
            // Arrange
            var request = new PatioRequest { Nome = "Novo Pátio", FilialId = 1 };
            var patio = new Patio { Id = 5, Nome = "Novo Pátio", FilialId = 1 };
            var response = new PatioResponse { Id = 5, Nome = "Novo Pátio", FilialId = 1 };

            _mapperMock.Setup(m => m.Map<Patio>(request)).Returns(patio);
            _repoMock.Setup(r => r.CriarAsync(patio)).ReturnsAsync(patio);
            _mapperMock.Setup(m => m.Map<PatioResponse>(patio)).Returns(response);

            // Act
            var result = await _service.Criar(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Novo Pátio", result.Nome);
            _repoMock.Verify(r => r.CriarAsync(It.IsAny<Patio>()), Times.Once);
        }

        [Fact]
        public async Task Atualizar_DeveRetornarFalse_QuandoPatioNaoExistir()
        {
            // Arrange
            _repoMock.Setup(r => r.ObterPorIdAsync(99)).ReturnsAsync((Patio)null);

            // Act
            var result = await _service.Atualizar(99, new PatioRequest());

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task Remover_DeveChamarRepositorioERetornarTrue()
        {
            // Arrange
            _repoMock.Setup(r => r.RemoverAsync(1)).ReturnsAsync(true);

            // Act
            var result = await _service.Remover(1);

            // Assert
            Assert.True(result);
            _repoMock.Verify(r => r.RemoverAsync(1), Times.Once);
        }
    }
}

