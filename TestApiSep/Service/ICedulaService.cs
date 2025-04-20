namespace TestApiSep.Service
{
    public interface ICedulaService
    {
        Task<string> BuscarCedulaAsync(string idCedula);
    }
}
