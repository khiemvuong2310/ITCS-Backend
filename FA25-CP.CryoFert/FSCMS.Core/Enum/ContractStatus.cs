namespace FSCMS.Core.Enums
{
    /// <summary>
    /// Represents the possible statuses of a cryo storage contract.
    /// </summary>
    public enum ContractStatus
    {
        /// <summary>
        /// The contract is currently active and valid.
        /// </summary>
        Active = 1,

        /// <summary>
        /// The contract has expired and is no longer valid.
        /// </summary>
        Expired = 2,

        /// <summary>
        /// The contract has been terminated before its expiration.
        /// </summary>
        Terminated = 3,

        /// <summary>
        /// The contract has been renewed for an extended duration.
        /// </summary>
        Renewed = 4,
        Draft = 5,
        Pending = 6,
        Cancel = 7,
    }
}
