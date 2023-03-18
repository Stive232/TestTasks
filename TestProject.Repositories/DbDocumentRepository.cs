using TestProject.Repositories.Entities;
using TestProject.Repositories.Interfaces;

namespace TestProject.Repositories
{
    public class DbDocumentRepository : IDbDocumentRepository
    {
        private static ulong _count;
        private static object _locker = new();
        private DocumentsData _documentDataStorage;

        public DbDocumentRepository(DocumentsData document)
        {
            _documentDataStorage = document;
        }

        public ulong Add(DbDocument document)
        {
            lock (_locker)
            {
                if (_documentDataStorage.Data.TryAdd(_count, document))
                {
                    return _count++;
                }
                else
                {
                    throw new Exception("Запись данных в базу прошла не успешно.");
                }
            }
        }

        public List<DbDocument> GetByUserId(string userId) => 
            _documentDataStorage.Data
            .Where(x => x.Value.UserId == userId && x.Value.IsDeleted == false)
            .Select(x => x.Value)
            .ToList();

        public List<DbDocument> GetByContractNumber(string contractNumber) =>
            _documentDataStorage.Data
            .Where(x => contractNumber.Equals(x.Value.ContractNumber) && x.Value.IsDeleted == false)
            .Select(x => x.Value)
            .ToList();

        public ulong DeleteByUserIdOrContractNumber(string? userId, string? contractNumber)
        {
            ulong count = 0;

            foreach(var item in _documentDataStorage.Data)
            {            
                if(item.Value.UserId == userId || item.Value.ContractNumber == contractNumber)
                {
                    item.Value.IsDeleted = true;
                    count++;
                }
            }

            return count;
        }
    }
}
