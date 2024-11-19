namespace TaskManagerAPI.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; } = "Icomplete";

        public string Priority { get; set; } = "Medium";

        public int? UserId { get; set; }
    }
}
