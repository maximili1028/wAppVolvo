using System;
using Xunit;
using wAppVolvo.Controllers;
using wAppVolvo.Models;
using wAppVolvo.Context;
using System.Threading.Tasks;
using System.Linq;

namespace tAppVolvoTest
{
    public class UnitTest1
    {
        #region CREATE
        [Theory]
        [InlineData(2022, 2022, "FM")]
        [InlineData(2022, 2025, "FH")]
        public async void TestCreateCamioaoValOK(int anoFabricacao, int anoModelo, string modelo)
        {
            var dbContext = new DbContext();
            CaminhaoDbContext camDbContext = dbContext.CreateDbContext();

            //1
            var caminhao = new Caminhao() { AnoFabricacao = anoFabricacao, AnoModelo = anoModelo, Modelo = modelo };
            CaminhaosController cont = new CaminhaosController(camDbContext);

            int countBeforeCamiones = cont.caminhaos().Count;

            //2  ejecutar operacion           
            await cont.Create(caminhao);

            //3 assertions resultado esperado, resultado obtenido            
            Assert.Equal(countBeforeCamiones + 1, cont.caminhaos().Count);
        }

        [Theory]
        [InlineData(2021, 2021, null)]
        [InlineData(2021, 2021, "FS")]
        public async void TestCreateCamioaoValErrModelo(int anoFabricacao, int anoModelo, string modelo)
        {
            var dbContext = new DbContext();
            CaminhaoDbContext camDbContext = dbContext.CreateDbContext();

            var caminhao = new Caminhao() { AnoFabricacao = anoFabricacao, AnoModelo = anoModelo, Modelo = modelo };
            CaminhaosController cont = new CaminhaosController(camDbContext);

            int countBeforeCamiones = cont.caminhaos().Count;
            await cont.Create(caminhao);

            Assert.Equal(countBeforeCamiones, cont.caminhaos().Count);
        }

        //Ano Modelo(Poderá ser o atual ou o ano subsequente)
        [Theory]
        [InlineData(2022, 2021, "FH")]
        [InlineData(2022, 1999, "FM")]
        public async void TestCreateCamioaoValErrAnoModelo(int anoFabricacao, int anoModelo, string modelo)
        {
            var dbContext = new DbContext();
            CaminhaoDbContext camDbContext = dbContext.CreateDbContext();

            var caminhao = new Caminhao() { AnoFabricacao = anoFabricacao, AnoModelo = anoModelo, Modelo = modelo };
            CaminhaosController cont = new CaminhaosController(camDbContext);

            int countBeforeCamiones = cont.caminhaos().Count;
            await cont.Create(caminhao);

            Assert.Equal(countBeforeCamiones, cont.caminhaos().Count);
        }

        //Ano de Fabricação(Ano deverá ser o atual)
        [Theory]
        [InlineData(2021, 2022, "FM")]
        [InlineData(2023, 2025, "FH")]
        public async void TestCreateCamioaoValErrAnoFabricacao(int anoFabricacao, int anoModelo, string modelo)
        {
            var dbContext = new DbContext();
            CaminhaoDbContext camDbContext = dbContext.CreateDbContext();

            var caminhao = new Caminhao() { AnoFabricacao = anoFabricacao, AnoModelo = anoModelo, Modelo = modelo };
            CaminhaosController cont = new CaminhaosController(camDbContext);

            int countBeforeCamiones = cont.caminhaos().Count;
            await cont.Create(caminhao);

            Assert.Equal(countBeforeCamiones, cont.caminhaos().Count);
        }
        #endregion CREATE

        #region EDIT
        [Theory]
        [InlineData(1, 2022, 2025, "FM")]
        [InlineData(2, 2022, 2025, "FH")]
        public async void TestEditCamioaoValOK(int id, int anoFabricacao, int anoModelo, string modelo)
        {
            var dbContext = new DbContext();
            CaminhaoDbContext camDbContext = dbContext.CreateDbContext();

            //1
            var caminhao = new Caminhao() { CaminhaoId = id, AnoFabricacao = anoFabricacao, AnoModelo = anoModelo, Modelo = modelo };
            CaminhaosController cont = new CaminhaosController(camDbContext);

            //2  ejecutar operacion                       
            await cont.Edit(caminhao.CaminhaoId, caminhao);
            Caminhao camiaoEdit = cont.caminhao(id);

            //3 assertions resultado esperado, resultado obtenido            
            Assert.Equal(caminhao, camiaoEdit);
        }

        [Theory]
        [InlineData(1, 2022, 2023, null)]
        [InlineData(2, 2022, 2025, "FS")]
        public async void TestEditCamioaoValErrModelo(int id, int anoFabricacao, int anoModelo, string modelo)
        {
            var dbContext = new DbContext();
            CaminhaoDbContext camDbContext = dbContext.CreateDbContext();

            //1
            var caminhao = new Caminhao() { CaminhaoId = id, AnoFabricacao = anoFabricacao, AnoModelo = anoModelo, Modelo = modelo };
            CaminhaosController cont = new CaminhaosController(camDbContext);

            //2  ejecutar operacion                       
            await cont.Edit(caminhao.CaminhaoId, caminhao);
            Caminhao camiaoEdit = cont.caminhao(id);

            //3 assertions resultado esperado, resultado obtenido            
            Assert.NotEqual(camiaoEdit, caminhao);
        }

        //Ano Modelo(Poderá ser o atual ou o ano subsequente)
        [Theory]
        [InlineData(1, 2022, 2021, "FH")]
        [InlineData(2, 2022, 1999, "FM")]
        public async void TestEditCamioaoValErrAnoModelo(int id, int anoFabricacao, int anoModelo, string modelo)
        {
            var dbContext = new DbContext();
            CaminhaoDbContext camDbContext = dbContext.CreateDbContext();

            //1
            var caminhao = new Caminhao() { CaminhaoId = id, AnoFabricacao = anoFabricacao, AnoModelo = anoModelo, Modelo = modelo };
            CaminhaosController cont = new CaminhaosController(camDbContext);

            //2  ejecutar operacion                       
            await cont.Edit(caminhao.CaminhaoId, caminhao);
            Caminhao camiaoEdit = cont.caminhao(id);

            //3 assertions resultado esperado, resultado obtenido            
            Assert.NotEqual(camiaoEdit, caminhao);
        }

        //Ano de Fabricação(Ano deverá ser o atual)
        [Theory]
        [InlineData(1, 2021, 2023, "FM")]
        [InlineData(2, 2023, 2025, "FH")]
        public async void TestEditCamioaoValErrAnoFabricacao(int id, int anoFabricacao, int anoModelo, string modelo)
        {
            var dbContext = new DbContext();
            CaminhaoDbContext camDbContext = dbContext.CreateDbContext();

            //1
            var caminhao = new Caminhao() { CaminhaoId = id, AnoFabricacao = anoFabricacao, AnoModelo = anoModelo, Modelo = modelo };
            CaminhaosController cont = new CaminhaosController(camDbContext);

            //2  ejecutar operacion                       
            await cont.Edit(caminhao.CaminhaoId, caminhao);
            Caminhao camiaoEdit = cont.caminhao(id);

            //3 assertions resultado esperado, resultado obtenido            
            Assert.NotEqual(camiaoEdit, caminhao);
        }
        #endregion EDIT

        #region DELETE
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async void TestDeleteValOK(int id)
        {
            var dbContext = new DbContext();
            CaminhaoDbContext camDbContext = dbContext.CreateDbContext();

            //1
            CaminhaosController cont = new CaminhaosController(camDbContext);

            //2  ejecutar operacion
            await cont.Delete(id);
            int cant = 0;
            if (cont.caminhaos() != null)
                cant = cont.caminhaos().Where(x => x.CaminhaoId == id).ToList().Count();

            //3 assertions resultado esperado, resultado obtenido            
            Assert.Equal(0, cant);
        }
        #endregion DELETE
    }
}
