using System.Text;
using System.Security.Cryptography;
using System.ComponentModel.DataAnnotations;
namespace ApiWorkbench.Models
{
    public class UserModel : IModel
    {
        [Key]
        public int UserId { set; get; }
        public string Name { set; get; }
        public string Username { set; get; }
        public string Password { set; get; }
        public int LastLogin { set; get; }

    }


}

