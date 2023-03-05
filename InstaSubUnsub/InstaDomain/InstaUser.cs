using InstaDomain.Enums;

namespace InstaDomain
{
    public class InstaUser : BaseEntity
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private string _name;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Name 
        {
            get { return _name; } 
            set { if (!string.IsNullOrWhiteSpace(value)) _name = value; } 
        }
        public double Rank { get; set; }
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

        private DateTime? _lastPostDate;
        public DateTime? LastPostDate
        { 
            get 
            {
                return _lastPostDate;
            }
            set
            {
                if (value > (_lastPostDate ?? default))
                    _lastPostDate = value;
            }
        }

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