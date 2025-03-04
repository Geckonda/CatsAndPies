using CatsAndPies.Domain.Entities.PiesTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public RoleEntity? Role { get; set; }
        public QuestionnaireEntity? Questionnaire { get; set; }
        public CatEntity? Cat { get; set; }
        public WalletEntity? Wallet { get; set; }
        public List<PieEntity> Pies { get; set; }
    }
}
