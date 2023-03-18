using System.Collections.Concurrent;
using TestProject.Repositories.Entities;

namespace TestProject.Repositories.Interfaces
{
    public interface IDbDocumentRepository
    {
        ulong Add(DbDocument document);
        List<DbDocument> GetByUserId(string userId);
        List<DbDocument> GetByContractNumber(string contractNumber);
        ulong DeleteByUserIdOrContractNumber(string? userId, string? contractNumber);
    }
}
