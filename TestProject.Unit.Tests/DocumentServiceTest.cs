using Moq;
using AutoFixture;
using AutoMapper;
using Microsoft.Extensions.Logging;
using TestProject.Logic.Services.Document;
using TestProject.Logic.Services.Document.Models;
using TestProject.Repositories;
using TestProject.Repositories.Entities;

namespace TestProject.Unit.Tests;

public class DocumentServiceTest
{
    private IFixture _fixture;
    private readonly IMapper _mapper;
    private readonly DocumentService _documentService;
    private readonly DbDocumentRepositoryStub _repository;
    private Mock<ILogger<DocumentService>> _loggerMock;

    public DocumentServiceTest()
    {
        _fixture = new Fixture();
        _repository = new DbDocumentRepositoryStub();
        _loggerMock = _fixture.Freeze<Mock<ILogger<DocumentService>>>();

        var config = new MapperConfiguration(opts =>
        {
            opts.CreateMap<DocumentModel, DbDocument>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(c => c.UserId))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(c => c.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(c => c.LastName))
            .ForMember(dest => dest.ContractNumber, opt => opt.MapFrom(c => c.ContractNumber))
            .ForMember(dest => dest.WithdrawalAmount, opt => opt.MapFrom(c => c.WithdrawalAmount))
            .ReverseMap();
        });

        _mapper = new Mapper(config);
        //_documentService = new DocumentService(_repository, _mapper, _loggerMock.Object);
        _documentService = new DocumentService(_repository, _mapper);
    }

    [Fact]
    public async Task InsertDocumentTest()
    {
        //Given
        var document = _fixture.Build<DocumentModel>().Create();
        
        List<DocumentModel> documents = new();
        documents.Add(document);

        _documentService.SaveToCollection(documents);
        
        //When
        List<DocumentModel> result = await _documentService.GetByUserIdAsync(document.UserId);
        
        //Then
        Assert.Equal(document.FirstName, result.First().FirstName);
        Assert.Equal(document.LastName, result.First().LastName);
        Assert.Equal(document.WithdrawalAmount, result.First().WithdrawalAmount);
        Assert.Equal(document.ContractNumber, result.First().ContractNumber);
        Assert.Equal(document.UserId, result.First().UserId);
    }

    [Fact]
    public async Task GetDocumentByContractNumberTest()
    {
        //Given
        var document = _fixture.Build<DocumentModel>().Create();

        List<DocumentModel> documents = new();
        documents.Add(document);

        _documentService.SaveToCollection(documents);

        //When
        List<DocumentModel> result = await _documentService.GetByContractNumberAsync(document.ContractNumber);

        //Then
        Assert.Equal(document.FirstName, result.First().FirstName);
        Assert.Equal(document.LastName, result.First().LastName);
        Assert.Equal(document.WithdrawalAmount, result.First().WithdrawalAmount);
        Assert.Equal(document.ContractNumber, result.First().ContractNumber);
        Assert.Equal(document.UserId, result.First().UserId);
    }

    [Fact]
    public async Task GetDocumentGetByIdTest()
    {
        //Given
        var document = _fixture.Build<DocumentModel>().Create();

        List<DocumentModel> documents = new();
        documents.Add(document);

        _documentService.SaveToCollection(documents);

        //When
        DocumentModel result = await _documentService.GetByIdAsync(0);

        //Then
        Assert.Equal(document.FirstName, result.FirstName);
        Assert.Equal(document.LastName, result.LastName);
        Assert.Equal(document.WithdrawalAmount, result.WithdrawalAmount);
        Assert.Equal(document.ContractNumber, result.ContractNumber);
        Assert.Equal(document.UserId, result.UserId);
    }

    [Fact]
    public async Task DeleteByUserIdTest()
    {
        //Given
        var document = _fixture.Build<DocumentModel>().Create();

        List<DocumentModel> documents = new();
        documents.Add(document);

        _documentService.SaveToCollection(documents);

        //When
        var deletedRecordsCount =  await _documentService.DeleteByUserIdOrContractNumberAsync(document.UserId, "");

        ulong expected = 1;
        //Then
        Assert.Equal(deletedRecordsCount, expected);
    }

    [Fact]
    public async Task DeleteByontractNumberTest()
    {
        //Given
        var document = _fixture.Build<DocumentModel>().Create();

        List<DocumentModel> documents = new();
        documents.Add(document);

        _documentService.SaveToCollection(documents);

        //When
        var deletedRecordsCount = await _documentService.DeleteByUserIdOrContractNumberAsync("", document.ContractNumber);

        ulong expected = 1;

        //Then
        Assert.Equal(deletedRecordsCount, expected);
    }
}
