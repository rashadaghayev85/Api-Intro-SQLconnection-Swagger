namespace API_intro_SQLconnection_Swagger.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
