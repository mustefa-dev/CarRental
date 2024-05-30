using CarRental.Entities;
using CarRental.Interface;

namespace CarRental.Interface
{
    public interface IDocumentRepository : IGenericRepository<Document , Guid>
    {
         
    }
}
