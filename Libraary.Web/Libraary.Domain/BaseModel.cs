namespace Libraary.Domain
{
    using System.ComponentModel.DataAnnotations;

    public class BaseModel<T>
    {
        [Key]
        public T Id { get; set; }
    }
}
