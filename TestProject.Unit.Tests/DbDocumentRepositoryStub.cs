using System.Collections.Concurrent;
using TestProject.Repositories.Entities;
using TestProject.Repositories.Interfaces;

namespace TestProject.Repositories
{
    public class DbDocumentRepositoryStub : IDbDocumentRepository
    {
        private ulong _count = 0;
        private ConcurrentDictionary<ulong, DbDocument> _data = new();

        public ulong Add(DbDocument document)
        {
            if (_data.TryAdd(_count, document))
            { 
                return _count++; //ToDo: Проверить работоспособность
            }
            else
            {
                throw new Exception("Запись данных в базу прошла не успешно."); //ToDO: Повторно добавить данные в коллекцию.
            }
        }

        public ulong DeleteByUserIdOrContractNumber(string userId, string contractNumber)
        {
            ulong count = 0;

            foreach (var item in _data)
            {
                if (item.Value.UserId == userId || item.Value.ContractNumber == contractNumber)
                {
                    item.Value.IsDeleted = true;
                    count++;
                }
            }

            return count;
        }

        public List<DbDocument> GetByContractNumber(string contractNumber) =>
            _data
            .Where(x => contractNumber.Equals(x.Value.ContractNumber) && x.Value.IsDeleted == false)
            .Select(x => x.Value)
            .ToList();

        public List<DbDocument> GetByUserId(string userId) =>
            _data
            .Where(x => x.Value.UserId == userId && x.Value.IsDeleted == false)
            .Select(x => x.Value)
            .ToList();

        public DbDocument GetById(ulong documentId)
        {
            _data.TryGetValue(documentId, out DbDocument value);

            return value;
        }
    }
}