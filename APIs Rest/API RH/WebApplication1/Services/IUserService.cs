namespace RecursosHumanos.API.Services
{
    public interface IUserService
    {

        public bool IsUser(string email, string pass);

        public string GetName(string email); 


    }
}
