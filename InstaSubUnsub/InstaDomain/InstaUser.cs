using InstaDomain.Enums;

namespace InstaDomain
{
    public class InstaUser : BaseEntity
    {
        public string Name { get; set; }
        public int? Rank { get; set; }
        public bool? IsFollower { get; set; }
        public int? FollowersNum { get; set; }
        public int? FollowingsNum { get; set; }

        /// <summary>
        /// Date when we followed the user
        /// </summary>
        public DateTime? FollowingDate { get; set; }
        public DateTime? UnfollowingDate { get; set; }

        private UserStatus _status;
        public UserStatus Status 
        { 
            get 
            {
                return _status;
            }
            set
            {
                if (_status != UserStatus.Protected)
                    _status = value;
            }
        }
        public DateTime? LastPostDate { get; set; }

        private bool? _hasRussianText;

        public bool? HasRussianText //in description or last posts (cannot be set to false once true)
        {
            get { return _hasRussianText; }
            set { _hasRussianText = (_hasRussianText == true) || (value == true); }
        }

        public virtual IList<UserRelation>? Followees { get; set; }
        public virtual IList<UserRelation>? Followers { get; set; }

    }
}