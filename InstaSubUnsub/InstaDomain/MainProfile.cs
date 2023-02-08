using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaDomain
{
    public class MainProfileState
    {
        public long Id { get; set; }
        public int FollowersNumber { get; set; }
        public int FollowedNumber { get; set; }
        public IEnumerable<User> Followed { get; set; } = new List<User>();
    }
}
