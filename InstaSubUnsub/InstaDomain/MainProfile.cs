using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaDomain
{
    public class MainProfileState : BaseEntity
    {
        public int FollowersNumber { get; set; }
        public int FollowedNumber { get; set; }
        public IEnumerable<InstaUser> Followed { get; set; } = new List<InstaUser>();
    }
}
