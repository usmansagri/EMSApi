using EMSApi.Modals;

namespace EMSApi.Repository
{
    public interface IUsersRepository
    {
        Users Authenticate(string EmailID, string password);
        Users Register(Users user);
        Users DeleteUser(int id);
        Users findUser(int id);
        List<Users> getUserList();

    }
}
