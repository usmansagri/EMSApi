using EMSApi.Modals;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EMSApi.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly AppSettings _settings;

        //Dependency injection
        public UsersRepository(ApplicationDbContext context,IOptions<AppSettings> appsettings)
        {
            _context = context;
            _settings = appsettings.Value;
        }

        public Users findUser(int id)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id==id);
            if (user == null)
            {
                return null;
            }
            return user;

        }

        public Users Authenticate(string EmailID, string Password)
        {
            var user = _context.Users.SingleOrDefault(u => u.EmailID == EmailID && u.Password == Password);
            if (user == null)
            {
                return null;
            }

            //Generate token after successful login
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role,user.UserType)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token=tokenHandler.WriteToken(token);
            return user; //return user object with token
        }

        public Users DeleteUser(int id)
        {
            var user=_context.Users.SingleOrDefault(u => u.Id == id);
            if(user == null)
            {
                return null;
            }
            _context.Users.Remove(user);
            _context.SaveChanges();
            return user;
        }

        public Users Register(Users user)
        {
            Users newUser = new Users()
            {
                EmailID = user.EmailID,
                Password = user.Password,
                Name=user.Name,
                Address=user.Address,
                UserType=user.UserType,
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
           return newUser;
        }

        public List<Users> getUserList()
        {
            var allUsers= _context.Users.ToList();
            return allUsers;
        }
    }
}
