using TestProject.Repositories.Entities;
using TestProject.Repositories.Interfaces;

namespace TestProject.Repositories
{
    public class DbDocumentRepository : IDbDocumentRepository
    {
        private DocumentsData _documentDataStorage;

        public DbDocumentRepository(DocumentsData document)
        {
            _documentDataStorage = document;
        }

        public void Insert(DbDocument document)
        {
            _documentDataStorage.Add(document);
        }

        public List<DbDocument> GetByLeadId(string leadId)
        {
            return _documentDataStorage.Data.Where(x => x.LeadId == leadId).ToList<DbDocument>();
        }

        public List<DbDocument> GetByContractNumber(string contractNumber)
        {
            return _documentDataStorage.Data.Where(x => contractNumber.Equals(x.ContractNumber)).ToList<DbDocument>();
        }

        public void DeleteByLeadIdOrContractNumber(Guid? leadId, string? contractNumber)
        {
            //ToDo:
        }
    }
}
