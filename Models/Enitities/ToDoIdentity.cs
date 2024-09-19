using System.ComponentModel.DataAnnotations;

namespace ToDoTask.Web.Models.Enitities
{
    
    public class ToDoIdentity
    {
        [Key]
        public Guid ID { get; set; }
        public string? Title {  get; set; }
        public int Status {  get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime? CompletedOn {  get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
