namespace NetMonkey.Model
{

    /// <summary>Represents the status of a the member of a list.</summary>
    public enum ListMemberStatus
    {
        /// <summary>The member has been subscribed.</summary>
        Subscribed,
        /// <summary>The member has been unsubscribed.</summary>
        Unsubscribed,
        /// <summary>The member has been cleaned.</summary>
        Cleaned,
        /// <summary>The member status is pending.</summary>
        Pending
    }
}
