using TestProject.Logic.Services.Document.Models;

namespace TestProject.Logic.Services;
public interface IDocumentService
{
   Task<List<ulong>> Insert(List<DocumentModel> document);
   Task<List<DocumentModel>?> GetByUserId(string userId);
   Task<List<DocumentModel>> GetByContractNumber(string contractNumber);
   Task<ulong> DeleteByUserIdOrContractNumber(string? userId, string? contractNumber);
}
