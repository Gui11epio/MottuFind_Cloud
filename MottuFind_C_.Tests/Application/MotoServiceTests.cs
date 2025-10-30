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
using Sprint1_C_.Domain.Enums;

namespace MottuFind_C_.Tests.Application
{
    public class MotoServiceTests
    {
        private readonly Mock<IMotoRepository> _repoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly MotoService _service;

        public MotoServiceTests()
        {
            _repoMock = new Mock<IMotoRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new MotoService(_repoMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task ObterTodos_DeveRetornarListaDeMotos()
        {
            // Arrange
            var motos = new List<Moto>
            {
                new Moto { Placa = "ABC1234", Marca = "Honda", Modelo = MotoModelo.SPORT, Status = MotoStatus.DISPONIVEL },
                new Moto { Placa = "XYZ9876", Marca = "Yamaha", Modelo = MotoModelo.POP, Status = MotoStatus.MANUTENCAO }
            };

            _repoMock.Setup(r => r.ObterTodosAsync()).ReturnsAsync(motos);
            _mapperMock.Setup(m => m.Map<List<MotoResponse>>(It.IsAny<List<Moto>>()))
                       .Returns(new List<MotoResponse>
                       {
                           new MotoResponse { Placa = "ABC1234" },
                           new MotoResponse { Placa = "XYZ9876" }
                       });

            // Act
            var result = await _service.ObterTodos();

            // Assert
            Assert.Equal(2, result.Count);
            _repoMock.Verify(r => r.ObterTodosAsync(), Times.Once);
        }

        [Fact]
        public async Task ObterPorPlaca_DeveRetornarMotoQuandoEncontrada()
        {
            var moto = new Moto { Placa = "AAA1111", Marca = "Honda", Modelo = MotoModelo.SPORT, Status = MotoStatus.DISPONIVEL };
            var response = new MotoResponse { Placa = "AAA1111" };

            _repoMock.Setup(r => r.ObterPorPlacaAsync("AAA1111")).ReturnsAsync(moto);
            _mapperMock.Setup(m => m.Map<MotoResponse>(moto)).Returns(response);

            var result = await _service.ObterPorPlaca("AAA1111");

            Assert.NotNull(result);
            Assert.Equal("AAA1111", result.Placa);
        }

        [Fact]
        public async Task ObterPorPlaca_DeveRetornarNullQuandoNaoEncontrada()
        {
            _repoMock.Setup(r => r.ObterPorPlacaAsync("ZZZ9999")).ReturnsAsync((Moto?)null);

            var result = await _service.ObterPorPlaca("ZZZ9999");

            Assert.Null(result);
        }

        [Fact]
        public async Task Criar_DeveRetornarMotoResponse()
        {
            var request = new MotoRequest { Placa = "AAA0001", Marca = "Honda", Modelo = MotoModelo.POP, Status = MotoStatus.DISPONIVEL };
            var moto = new Moto { Placa = "AAA0001" };
            var tag = new TagRfid { CodigoIdentificacao = "TAG-AAA0001" };
            var response = new MotoResponse { Placa = "AAA0001" };

            _mapperMock.Setup(m => m.Map<Moto>(request)).Returns(moto);
            _mapperMock.Setup(m => m.Map<MotoResponse>(moto)).Returns(response);
            _repoMock.Setup(r => r.CriarAsync(It.IsAny<Moto>(), It.IsAny<TagRfid>())).ReturnsAsync(moto);

            var result = await _service.Criar(request);

            Assert.NotNull(result);
            Assert.Equal("AAA0001", result.Placa);
            _repoMock.Verify(r => r.CriarAsync(It.IsAny<Moto>(), It.IsAny<TagRfid>()), Times.Once);
        }

        [Fact]
        public async Task Atualizar_DeveRetornarTrueQuandoMotoExiste()
        {
            var request = new MotoRequest { Placa = "BBB2222", Marca = "Yamaha", Modelo = MotoModelo.SPORT, Status = MotoStatus.MANUTENCAO };
            var moto = new Moto { Placa = "BBB2222" };

            _repoMock.Setup(r => r.ObterPorPlacaAsync("BBB2222")).ReturnsAsync(moto);
            _repoMock.Setup(r => r.AtualizarAsync(moto)).ReturnsAsync(true);

            var result = await _service.Atualizar("BBB2222", request);

            Assert.True(result);
        }

        [Fact]
        public async Task Atualizar_DeveRetornarFalseQuandoMotoNaoExiste()
        {
            _repoMock.Setup(r => r.ObterPorPlacaAsync("ZZZ9999")).ReturnsAsync((Moto?)null);

            var request = new MotoRequest { Placa = "ZZZ9999" };
            var result = await _service.Atualizar("ZZZ9999", request);

            Assert.False(result);
        }

        [Fact]
        public async Task Remover_DeveRetornarTrueQuandoSucesso()
        {
            _repoMock.Setup(r => r.RemoverAsync("AAA0001")).ReturnsAsync(true);

            var result = await _service.Remover("AAA0001");

            Assert.True(result);
        }

        [Fact]
        public async Task Remover_DeveRetornarFalseQuandoFalha()
        {
            _repoMock.Setup(r => r.RemoverAsync("BBB1111")).ReturnsAsync(false);

            var result = await _service.Remover("BBB1111");

            Assert.False(result);
        }
    }
}

