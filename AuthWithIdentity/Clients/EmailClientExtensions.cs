// Code generated by Microsoft (R) AutoRest Code Generator 1.0.1.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace PaderbornUniversity.SILab.Hip.Auth.Clients
{
    using Models;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Extension methods for EmailClient.
    /// </summary>
    public static partial class EmailClientExtensions
    {
            /// <summary>
            /// Send a email defined in the EmailModel.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='email'>
            /// EmailModel to send
            /// </param>
            public static void EmailPost(this IEmailClient operations, EmailModel email = default(EmailModel))
            {
                operations.EmailPostAsync(email).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Send a email defined in the EmailModel.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='email'>
            /// EmailModel to send
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task EmailPostAsync(this IEmailClient operations, EmailModel email = default(EmailModel), CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.EmailPostWithHttpMessagesAsync(email, null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

            /// <summary>
            /// Send a Notification.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='notificationModel'>
            /// Notification to send
            /// </param>
            public static void EmailNotificationPost(this IEmailClient operations, NotificationModel notificationModel = default(NotificationModel))
            {
                operations.EmailNotificationPostAsync(notificationModel).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Send a Notification.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='notificationModel'>
            /// Notification to send
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task EmailNotificationPostAsync(this IEmailClient operations, NotificationModel notificationModel = default(NotificationModel), CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.EmailNotificationPostWithHttpMessagesAsync(notificationModel, null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

            /// <summary>
            /// Send a Invitation defined in the InvitationModel.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='invitation'>
            /// Invitation to send
            /// </param>
            public static void EmailInvitationPost(this IEmailClient operations, InvitationModel invitation = default(InvitationModel))
            {
                operations.EmailInvitationPostAsync(invitation).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Send a Invitation defined in the InvitationModel.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='invitation'>
            /// Invitation to send
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task EmailInvitationPostAsync(this IEmailClient operations, InvitationModel invitation = default(InvitationModel), CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.EmailInvitationPostWithHttpMessagesAsync(invitation, null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

    }
}
