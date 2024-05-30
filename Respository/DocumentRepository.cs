using AutoMapper;
using CarRental.DATA;
using CarRental.Entities;
using CarRental.Interface;
using CarRental.Repository;

namespace CarRental.Repository
{

    public class DocumentRepository : GenericRepository<Document , Guid> , IDocumentRepository
    {
        public DocumentRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
