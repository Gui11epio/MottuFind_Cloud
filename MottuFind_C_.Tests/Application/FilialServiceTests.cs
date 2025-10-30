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
    public class FilialServiceTests
    {
        private readonly Mock<IFilialRepository> _repoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly FilialService _service;

        public FilialServiceTests()
        {
            _repoMock = new Mock<IFilialRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new FilialService(_repoMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task ObterTodos_DeveRetornarListaDeFilialResponse()
        {
            // Arrange
            var filiais = new List<Filial>
            {
                new Filial { Id = 1, Cidade = "SP", Pais = "Brasil" },
                new Filial { Id = 2, Cidade = "Rio", Pais = "Brasil" }
            };
            var responses = new List<FilialResponse>
            {
                new FilialResponse { Id = 1, Cidade = "SP", Pais = "Brasil" },
                new FilialResponse { Id = 2, Cidade = "Rio", Pais = "Brasil" }
            };

            _repoMock.Setup(r => r.ObterTodosAsync()).ReturnsAsync(filiais);
            _mapperMock.Setup(m => m.Map<List<FilialResponse>>(filiais)).Returns(responses);

            // Act
            var result = await _service.ObterTodos();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("SP", result[0].Cidade);
        }

        [Fact]
        public async Task ObterPorId_DeveRetornarFilialResponse()
        {
            // Arrange
            var filial = new Filial { Id = 1, Cidade = "SP", Pais = "Brasil" };
            var response = new FilialResponse { Id = 1, Cidade = "SP", Pais = "Brasil" };

            _repoMock.Setup(r => r.ObterPorIdAsync(1)).ReturnsAsync(filial);
            _mapperMock.Setup(m => m.Map<FilialResponse>(filial)).Returns(response);

            // Act
            var result = await _service.ObterPorId(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("SP", result.Cidade);
        }

        [Fact]
        public async Task Criar_DeveRetornarFilialResponse()
        {
            // Arrange
            var request = new FilialRequest { Cidade = "Campinas", Pais = "Brasil" };
            var filial = new Filial { Id = 1, Cidade = "Campinas", Pais = "Brasil" };
            var response = new FilialResponse { Id = 1, Cidade = "Campinas", Pais = "Brasil" };

            _mapperMock.Setup(m => m.Map<Filial>(request)).Returns(filial);
            _repoMock.Setup(r => r.CriarAsync(filial)).ReturnsAsync(filial);
            _mapperMock.Setup(m => m.Map<FilialResponse>(filial)).Returns(response);

            // Act
            var result = await _service.Criar(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Campinas", result.Cidade);
            _repoMock.Verify(r => r.CriarAsync(It.IsAny<Filial>()), Times.Once);
        }

        [Fact]
        public async Task Atualizar_DeveRetornarFalse_QuandoFilialNaoExistir()
        {
            // Arrange
            _repoMock.Setup(r => r.ObterPorIdAsync(99)).ReturnsAsync((Filial)null);

            // Act
            var result = await _service.Atualizar(99, new FilialRequest());

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

