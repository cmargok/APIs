using RecursosHumanos.API.Models.Security;

namespace RecursosHumanos.API.Services
{
    public class UserService : IUserService
    {

        //como usamos Basic Auth, quemamos los usuarios en el codigo
        List<User> users = new()
        {
            new User() { Name = "kevin" ,Email = "kev1@gmail.com", Password = "123456" },
            new User() { Name = "luisa" ,Email = "luf2@gmail.com", Password = "987654" },
        };

        public string GetName(string email) { 
        
            var name=  users.FirstOrDefault(u => u.Email == email);

            return name!.Name;    

        }
            



        public bool IsUser(string email, string pass) =>
            users.Where( u=> u.Email == email && u.Password == pass).Count() > 0 ;
    }
}
