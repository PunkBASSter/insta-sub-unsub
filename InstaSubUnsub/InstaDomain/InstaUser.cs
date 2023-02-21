using InstaDomain.Enums;

namespace InstaDomain
{
    public class InstaUser : BaseEntity
    {
        public string? Name { get; set; }
        public int Rank { get; set; } // Following/Followers && Last Post/Reel Date -- todo how to extract?
        public bool? IsFollower { get; set; }

        /// <summary>
        /// Date when we followed the user
        /// </summary>
        public DateTime? FollowingDate { get; set; }
        public DateTime? UnfollowingDate { get; set; }
        public UserStatus Status { get; set; }
        public DateTime? LastPostDate { get; set; }
        public bool HasRussianText { get; set; } //in description or last post

        public virtual IList<UserSubscription> Followees { get; set; }
        public virtual IList<UserSubscription> Followers { get; set; }

    }
}