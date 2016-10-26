using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwipeJob.Model
{
    public class Inbox
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid RecipientId { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public DateTime CreatedDateUtc { get; set; }

        public User User { get; set; }

        [ForeignKey("RecipientId")]
        public User Recipient { get; set; }
    }
}