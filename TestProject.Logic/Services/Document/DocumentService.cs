using AutoMapper;
using Microsoft.Extensions.Logging;
using TestProject.Logic.Services.Document.Models;
using TestProject.Repositories.Entities;
using TestProject.Repositories.Interfaces;

namespace TestProject.Logic.Services.Document;

public class DocumentService : IDocumentService
{
    private readonly IMapper _mapper;
    private readonly ILogger<DocumentService> _logger;
    private readonly IDbDocumentRepository _documentRepository;

    public DocumentService(IDbDocumentRepository documentRepository, IMapper mapper, ILogger<DocumentService> logger)
    {
        _mapper = mapper;
        _logger = logger;
        _documentRepository = documentRepository;

    }

    public Task<List<ulong>> Insert(List<DocumentModel> documents)
    {
        List<ulong> ids = new();

        foreach(DocumentModel documentModel in documents)
        {
            var id = _documentRepository.Add(_mapper.Map<DbDocument>(documentModel));
            ids.Add(id);
        }

        return Task.FromResult(ids);
    }

    public async Task<List<DocumentModel>?> GetByContractNumber(string contractNumber)
    {
        var documents = _documentRepository.GetByContractNumber(contractNumber);

        List<DocumentModel> result = new();

        foreach(DbDocument document in documents)
        {
            result.Add(_mapper.Map<DocumentModel>(document));
        }

        return await Task.FromResult(result); //ToDo: прокинуть асинхронность везде
    }

    public async Task<List<DocumentModel>?> GetByUserId(string userId)
    {
        if(userId == null)
        {
            _logger.LogInformation(""); //ToDo: дописать лог
            return null;
        }
        var documents = _documentRepository.GetByUserId(userId);

        List<DocumentModel> result = new();

        foreach (DbDocument document in documents)
        {
            result.Add(_mapper.Map<DocumentModel>(document));
        }

        return await Task.FromResult(result);
    }

    public Task<ulong> DeleteByUserIdOrContractNumber(string? userId, string? contractNumber)
    {
        return Task.FromResult(_documentRepository.DeleteByUserIdOrContractNumber(userId, contractNumber));
    }
}