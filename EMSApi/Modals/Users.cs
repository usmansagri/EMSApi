using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMSApi.Modals
{
    public class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmailID { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string UserType { get; set; }
        [NotMapped]
        public string Token { get; set; }

    }
}
