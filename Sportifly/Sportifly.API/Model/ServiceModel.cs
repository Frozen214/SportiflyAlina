namespace Sportifly.API.Model
{
    public class ServiceModel
    {
        public int ServiesId { get; set; }
        public string ServiesName { get; set; }
        public string Discription { get; set; }
        public double Cost { get; set; }
        public int MaxCountPeople { get; set; }
        public int CoachId { get; set; }
    }
}
