using PetAPI.Repositories.Interfaces;
using PetAPI.Services.Interfaces;
using PetAPI.Tools;

namespace PetAPI.Services.Implementations
{
    public class UsersService : IUsersService
    {
        //Declaración de dependencias
        private readonly IUsersRepository _usersRepository;
        private readonly Encrypter _encrypter;

        //Constructor que utiliza inyección de dependencias
        public UsersService(
            IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
            _encrypter = new Encrypter();
        }


    }
}
