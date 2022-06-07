using ClinicaSorriso.Controllers;
using ClinicaSorriso.Services;
using ClinicaSorriso.Repositories.InMemory;

namespace ClinicaSorriso
{
    class MainClass
    {
        //Instancia os objetos necessarios para rodar a aplicação e executa o Run() para iniciar
        public static void Main(string[] args)
        {
            PacientesRepositoryInMemory pacientesRepositoryInMemory = new PacientesRepositoryInMemory();
            ConsultaRepositoryInMemory consultaRepositoryInMemory = new ConsultaRepositoryInMemory();

            PacienteService pacienteService = new PacienteService(pacientesRepositoryInMemory); 
            ConsultaService consultaService = new ConsultaService(consultaRepositoryInMemory);

            PacienteController pacienteController = new PacienteController(pacienteService, consultaService);
            ConsultaController consultaController = new ConsultaController(consultaService, pacienteService);

            SeedRepositories populaRepositorio = new SeedRepositories(pacienteService, consultaService);
            populaRepositorio.PopularBanco();

            AppController app = new AppController(pacienteController, consultaController);

            app.Run();
        }
     
    }
}
