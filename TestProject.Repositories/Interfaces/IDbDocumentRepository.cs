using System.Collections.Concurrent;
using TestProject.Repositories.Entities;

namespace TestProject.Repositories.Interfaces
{
    public interface IDbDocumentRepository
    {
        void Insert(DbDocument document);
        List<DbDocument> GetByUserId(string userId);
        List<DbDocument> GetByContractNumber(string contractNumber);
        void DeleteByUserIdOrContractNumber(string? userId, string? contractNumber);
    }
}
