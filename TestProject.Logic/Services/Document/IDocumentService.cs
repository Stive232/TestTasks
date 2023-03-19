using Microsoft.AspNetCore.Http;
using TestProject.Logic.Services.Document.Models;

namespace TestProject.Logic.Services;
public interface IDocumentService
{
    public Task<List<ulong>> InsertAsync(IFormFile file);
    public Task<List<DocumentModel>> GetByUserIdAsync(string userId);
    public Task<DocumentModel> GetById(ulong documentId);
    public Task<List<DocumentModel>> GetByContractNumberAsync(string contractNumber);
    public Task<ulong> DeleteByUserIdOrContractNumberAsync(string userId, string contractNumber);
}
