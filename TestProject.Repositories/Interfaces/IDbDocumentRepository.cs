using System.Collections.Concurrent;
using TestProject.Repositories.Entities;

namespace TestProject.Repositories.Interfaces
{
    public interface IDbDocumentRepository
    {
        void Insert(DbDocument document);
        List<DbDocument> GetByLeadId(string leadId);
        List<DbDocument> GetByContractNumber(string contractNumber);
        void DeleteByLeadIdOrContractNumber(Guid? leadId, string? contractNumber);
    }
}
