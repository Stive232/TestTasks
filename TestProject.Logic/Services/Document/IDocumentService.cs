using TestProject.Logic.Services.Document.Models;

namespace TestProject.Logic.Services;
public interface IDocumentService
{
   void Insert(List<DocumentModel> document);
   List<DocumentModel> GetByUserId(string userId);
   List<DocumentModel> GetByContractNumber(string contractNumber);
   void DeleteByUserIdOrContractNumber(string? userId, string? contractNumber);
}
