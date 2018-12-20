namespace PhotoSearch.Api.Entities
{
    /// <summary>
    /// PhotoSummary
    /// </summary>
    /// <seealso cref="Object" />
    public class PhotoSummary
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        public string Owner { get; set; }

        /// <summary>
        /// Gets or sets the secret.
        /// </summary>
        /// <value>
        /// The secret.
        /// </value>
        public string Secret { get; set; }

        /// <summary>
        /// Gets or sets the server.
        /// </summary>
        /// <value>
        /// The server.
        /// </value>
        public string Server { get; set; }

        /// <summary>
        /// Gets or sets the farm.
        /// </summary>
        /// <value>
        /// The farm.
        /// </value>
        public int Farm { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is public.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is public; otherwise, <c>false</c>.
        /// </value>
        public bool IsPublic { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is friend.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is friend; otherwise, <c>false</c>.
        /// </value>
        public bool IsFriend { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is family.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is family; otherwise, <c>false</c>.
        /// </value>
        public bool IsFamily { get; set; }
    }
}
