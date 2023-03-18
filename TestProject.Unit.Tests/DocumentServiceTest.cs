using AutoFixture;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using TestProject.Logic.Services.Document;
using TestProject.Logic.Services.Document.Models;
using TestProject.Repositories;
using TestProject.Repositories.Entities;

namespace TestProject.Unit.Tests
{
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
            _documentService = new DocumentService(_repository, _mapper, _loggerMock.Object);
        }

        [Fact]
        public void InsertDocumentTest()
        {
            //Given
            var document = _fixture.Create<DocumentModel?>();
            List<DocumentModel>? documents = new();
            documents.Add(document);

            _documentService.Insert(documents);

            List<DocumentModel>? result = _documentService.GetByUserId(documents.First().UserId).Result;

            //Then
            Assert.Equal(documents, result);
        }

        [Fact]
        public void ShouldSuccessfullyInsertDocumentModelTest()
        {
            //Given
            var sut = GetSut();

            var documents = _fixture.Create<List<DocumentModel>>();
            var expectedResult = new List<DocumentModel>() { };

            //When
            List<DocumentModel>? result = sut.GetByUserId(documents.First().UserId).Result;

            //Then
            Assert.Equal(expectedResult, result);
        }

        private DocumentService GetSut()
        {
            return _fixture.Create<DocumentService>();
        }
    }
}