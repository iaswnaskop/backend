namespace backend.Entities
{
    public class Schedule
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan OpeningTime { get; set; }
        public TimeSpan ClosingTime { get; set; }
        public bool IsClosed { get; set; } = false;
        public virtual Store Store { get; set; } = null!;
    }
}
