using InstaDomain;

namespace SeleniumUtils.UiActions.Base
{
    public class UiActionResult //todo: replace with more elegant action result contract
    {
        public IList<InstaUser> AffectedUsers { get; init; } = new List<InstaUser>();

        public InstaUser? SingleResult { get; init; }

        public bool Success { get; init; }
    }
}
