namespace IdentityEmail.Entities
{
    public class SentMessage
    {
        public int SentMessageId { get; set; }

        public string SenderEmail { get; set; }
        public string ReceiverEmail { get; set; }

        public string Subject { get; set; }
        public string MessageDetail { get; set; }

        public DateTime SendDate { get; set; }

        public bool IsRead { get; set; }
    }

}
