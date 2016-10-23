using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SP.Model.Entities
{
    [DisplayName("用户表")]
    public class User
    {
        [Key]
        [DisplayName("主键")]
        public int ID { get; set; }
        [DisplayName("用户名")]
        [MaxLength(8)]
        public string UserName { get; set; }
        [DisplayName("用户密码")]
        [MaxLength(32)]
        public string UserPwd { get; set; }
        [DisplayName("用户邮箱")]
        [MaxLength(32)]
        public string Email { get; set; }
    }
}
