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

        public List<DbDocument> GetByUserId(string userId)
        {
            return _documentDataStorage.Data.Where(x => x.Value.UserId == userId && x.Value.IsDeleted == false).Select(x => x.Value).ToList<DbDocument>();
        }

        public List<DbDocument> GetByContractNumber(string contractNumber)
        {
            return _documentDataStorage.Data.Where(x => contractNumber.Equals(x.Value.ContractNumber) && x.Value.IsDeleted == false).Select(x => x.Value).ToList<DbDocument>();
        }

        public void DeleteByUserIdOrContractNumber(string? userId, string? contractNumber)
        { 
            foreach(var item in _documentDataStorage.Data)
            {
                if(item.Value.UserId == userId || item.Value.ContractNumber == contractNumber)
                {
                    item.Value.IsDeleted = true;
                }
            }
        }
    }
}
