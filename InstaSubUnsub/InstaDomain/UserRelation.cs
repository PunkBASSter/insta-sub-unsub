namespace InstaDomain
{
    public class UserRelation : BaseEntity
    {
        public long FollowerId { get; set; }
        public virtual InstaUser? Follower { get; set; }

        public long FolloweeId { get; set; }
        public InstaUser? Followee { get; set; }
        
        public DateTime LastUpdate { get; set; }
    }
}
