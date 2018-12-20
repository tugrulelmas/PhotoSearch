using System;

namespace PhotoSearch.Api.Entities
{
    /// <summary>
    /// Photo
    /// </summary>
    /// <seealso cref="Object" />
    public class Photo
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>The owner.</value>
        public Owner Owner { get; set; }

        /// <summary>
        /// Gets or sets the secret.
        /// </summary>
        /// <value>The secret.</value>
        public string Secret { get; set; }

        /// <summary>
        /// Gets or sets the server.
        /// </summary>
        /// <value>The server.</value>
        public string Server { get; set; }

        /// <summary>
        /// Gets or sets the farm.
        /// </summary>
        /// <value>The farm.</value>
        public int Farm { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is favorite.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is favorite; otherwise, <c>false</c>.
        /// </value>
        public bool IsFavorite { get; set; }

        /// <summary>
        /// Gets or sets the license.
        /// </summary>
        /// <value>The license.</value>
        public int License { get; set; }

        /// <summary>
        /// Gets or sets the safety level.
        /// </summary>
        /// <value>The safety level.</value>
        public int SafetyLevel { get; set; }

        /// <summary>
        /// Gets or sets the rotation.
        /// </summary>
        /// <value>The rotation.</value>
        public int Rotation { get; set; }

        /// <summary>
        /// Gets or sets the uploaded date.
        /// </summary>
        /// <value>
        /// The uploaded date.
        /// </value>
        public DateTime UploadedDate { get; set; }
    }
}
