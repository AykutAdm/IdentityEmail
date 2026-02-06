namespace IdentityEmail.Dtos
{
    public class MailRequestDto
    {
        public string ReceiverEmail { get; set; }
        public string Subject { get; set; }
        public string MessageDetail { get; set; }
        public string? Priority { get; set; }
        public string? Category { get; set; }
    }
}
