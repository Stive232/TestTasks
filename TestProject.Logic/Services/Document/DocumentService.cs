using AutoMapper;
using TestProject.Logic.Services.Document.Models;
using TestProject.Repositories.Entities;
using TestProject.Repositories.Interfaces;

namespace TestProject.Logic.Services.Document;

public class DocumentService : IDocumentService
{
    private readonly IMapper _mapper;
    private readonly IDbDocumentRepository _documentRepository;

    public DocumentService(IDbDocumentRepository documentRepository, IMapper mapper)
    {
        _mapper = mapper;
        _documentRepository = documentRepository;

    }

    public void Insert(List<DocumentModel> documents)
    {
        foreach(DocumentModel documentModel in documents)
        {
            _documentRepository.Insert(_mapper.Map<DbDocument>(documentModel));
        }
    }

    public List<DocumentModel> GetByContractNumber(string contractNumber)
    {
        var documents = _documentRepository.GetByContractNumber(contractNumber);

        List<DocumentModel> result = new();

        foreach(DbDocument document in documents)
        {
            result.Add(_mapper.Map<DocumentModel>(document));
        }

        return result;
    }

    public List<DocumentModel> GetByUserId(string userId)
    {
        var documents = _documentRepository.GetByUserId(userId);

        List<DocumentModel> result = new();

        foreach (DbDocument document in documents)
        {
            result.Add(_mapper.Map<DocumentModel>(document));
        }

        return result;
    }

    public void DeleteByUserIdOrContractNumber(string? userId, string? contractNumber)
    {
        _documentRepository.DeleteByUserIdOrContractNumber(userId, contractNumber);
    }
}