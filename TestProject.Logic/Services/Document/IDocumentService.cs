using TestProject.Logic.Services.Document.Models;

namespace TestProject.Logic.Services;
public interface IDocumentService
{
   void Insert(List<DocumentModel> document);
   List<DocumentModel> GetByLeadId(string leadId);
   List<DocumentModel> GetByContractNumber(string contractNumber);
   void DeleteByLeadIdOrContractNumber(string? leadId, string? contractNumber);
}
