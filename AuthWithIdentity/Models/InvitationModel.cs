// Code generated by Microsoft (R) AutoRest Code Generator 1.0.1.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace PaderbornUniversity.SILab.Hip.Auth.Models
{
    using Microsoft.Rest;
    using Newtonsoft.Json;
    using PaderbornUniversity.SILab;
    using PaderbornUniversity.SILab.Hip;
    using PaderbornUniversity.SILab.Hip.Auth;
    using System.Linq;

    public partial class InvitationModel
    {
        /// <summary>
        /// Initializes a new instance of the InvitationModel class.
        /// </summary>
        public InvitationModel()
        {
          CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the InvitationModel class.
        /// </summary>
        public InvitationModel(string recipient, string subject)
        {
            Recipient = recipient;
            Subject = subject;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "recipient")]
        public string Recipient { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "subject")]
        public string Subject { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (Recipient == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Recipient");
            }
            if (Subject == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Subject");
            }
        }
    }
}
