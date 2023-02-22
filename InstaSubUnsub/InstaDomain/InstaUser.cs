using InstaDomain.Enums;

namespace InstaDomain
{
    public class InstaUser : BaseEntity
    {
        public string Name { get; set; }
        public int? Rank { get; set; } // Following/Followers && Last Post/Reel Date -- todo how to extract?
        public bool? IsFollower { get; set; }
        public int? FollowersNum { get; set; }
        public int? FollowingsNum { get; set; }

        /// <summary>
        /// Date when we followed the user
        /// </summary>
        public DateTime? FollowingDate { get; set; }
        public DateTime? UnfollowingDate { get; set; }
        public UserStatus Status { get; set; }
        public DateTime? LastPostDate { get; set; }
        public bool? HasRussianText { get; set; } //in description or last post

        public virtual IList<UserRelation>? Followees { get; set; }
        public virtual IList<UserRelation>? Followers { get; set; }

    }
}