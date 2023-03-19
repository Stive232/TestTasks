using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Text;
using TestProject.Core.Infrastructure.Exceptions;
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

    public async Task<List<ulong>> InsertAsync(IFormFile file)
    {
        string textFromFile = await ReadFromFileAsync(file);
        if (!string.IsNullOrWhiteSpace(textFromFile))
        {
            List<DocumentModel> documents = ConvertToListDocumentModels(textFromFile);

            return await Task.FromResult(SaveToCollection(documents));
        }
        else
        {
            throw new InvalidArgumentException("The file should not be empty");
        }
    }

    public List<ulong> SaveToCollection(List<DocumentModel> documents)
    {
        List<ulong> ids = new();

        foreach (DocumentModel documentModel in documents)
        {
            var id = _documentRepository.Add(_mapper.Map<DbDocument>(documentModel));
            ids.Add(id);
        }

        return ids;
    }

    public async Task<DocumentModel> GetByIdAsync(ulong documentId)
    {
        DbDocument document = _documentRepository.GetById(documentId);
        if(document == null)
            throw new NotFoundException(typeof(DocumentModel), documentId);

        return await Task.FromResult(_mapper.Map<DocumentModel>(document));
    }

    public async Task<List<DocumentModel>> GetByContractNumberAsync(string contractNumber)
    {
        if (string.IsNullOrWhiteSpace(contractNumber))
        {
            throw new InvalidArgumentException("ContractNumber is required.");
        }

        List<DbDocument> documents = _documentRepository.GetByContractNumber(contractNumber);
        if (documents.Count == 0)
            throw new NotFoundException(typeof(DocumentModel), contractNumber);

        List<DocumentModel> result = new();

        foreach(DbDocument document in documents)
        {
            result.Add(_mapper.Map<DocumentModel>(document));
        }

        return await Task.FromResult(result);
    }

    public async Task<List<DocumentModel>> GetByUserIdAsync(string userId)
    {
        if(string.IsNullOrWhiteSpace(userId))
        {
            throw new InvalidArgumentException("UserId is required.");
        }

        List<DbDocument> documents = _documentRepository.GetByUserId(userId);

        if (documents.Count == 0)
            throw new NotFoundException(typeof(DocumentModel), userId);

        List<DocumentModel> result = new();

        foreach (DbDocument document in documents)
        {
            result.Add(_mapper.Map<DocumentModel>(document));
        }

        return await Task.FromResult(result);
    }

    public Task<ulong> DeleteByUserIdOrContractNumberAsync(string userId, string contractNumber)
    {
        if (string.IsNullOrWhiteSpace(userId) && string.IsNullOrWhiteSpace(contractNumber))
            throw new InvalidArgumentException("One of the parameters must not equal an empty string and null");

        return Task.FromResult(_documentRepository.DeleteByUserIdOrContractNumber(userId, contractNumber));
    }

    private List<DocumentModel> ConvertToListDocumentModels(string text)
    {
        List<DocumentModel> documents = new();
        var array = text.Split("\r");
        foreach (var item in array)
        {
            DocumentModel model = new();
            var tmpArray = item.Split("--");

            try
            {
                for (int i = 0; i < tmpArray.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            model.UserId = tmpArray[0].Trim(' ').Replace("\n", "");
                            break;
                        case 1:
                            model.LastName = tmpArray[1].Trim(' ').Replace("\n", "");
                            break;
                        case 2:
                            model.FirstName = tmpArray[2].Trim(' ').Replace("\n", "");
                            break;
                        case 3:
                            model.ContractNumber = tmpArray[3].Trim(' ').Replace("\n", "");
                            break;
                        case 4:
                            model.WithdrawalAmount = Convert.ToDecimal(tmpArray[4].Trim(' ').Replace("\n", ""));
                            break;
                    }
                }
                documents.Add(model);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        return documents;
    }

    private async Task<string> ReadFromFileAsync(IFormFile file)
    {
        long length = file.Length;

        using var fileStream = file.OpenReadStream();
        byte[] buffer = new byte[length];
        await fileStream.ReadAsync(buffer, 0, (int)file.Length);

        return Encoding.Default.GetString(buffer);
    }

    
}