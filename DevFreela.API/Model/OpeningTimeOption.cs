namespace DevFreela.API.Model
{
    public class OpeningTimeOption
    {
        public const string OpeningTime = "OpeningTime";
        public TimeSpan StartAt { get; set; }
        public TimeSpan EndAt { get; set; }
    }

}
