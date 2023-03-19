using TestProject.Repositories.Entities;

namespace TestProject.Repositories.Interfaces;

public interface IDbDocumentRepository
{
    public ulong Add(DbDocument document);
    public List<DbDocument> GetByUserId(string userId);
    public List<DbDocument> GetByContractNumber(string contractNumber);
    public DbDocument GetById(ulong documentId);
    public ulong DeleteByUserIdOrContractNumber(string userId, string contractNumber);
}
