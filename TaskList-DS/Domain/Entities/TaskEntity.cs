namespace TaskList_DS.Domain.Entities
{
    public class TaskEntity
    {
        public int Id { get; set; }
        public string TaskTitle { get; set; }
        public string? TaskDescription { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DoneAt { get; set; }
        public TaskStatus Status { get; set; }
    }
}
