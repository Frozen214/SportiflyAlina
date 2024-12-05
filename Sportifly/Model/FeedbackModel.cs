namespace Sportifly.Model
{
    public class FeedbackModel
    {
        public int FeedbackId { get; set; }
        public int ClientId { get; set; }
        public string FeedbackComments { get; set; }
        public byte Ball { get; set; }
    }
}
