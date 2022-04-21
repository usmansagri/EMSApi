using System.ComponentModel.DataAnnotations;

namespace EMSApi.Modals.dto
{
    public class userLoginDTO
    {
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
    }
}
