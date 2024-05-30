using CarRental.Entities;

namespace CarRental.Entities
{
    public class Document : BaseEntity<Guid>
    {
        public Car? Car { get; set; }
        public ICollection<Document> Documents { get; set; }

    }
}
