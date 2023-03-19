using AutoFixture;
using AutoMapper;
using Microsoft.AspNetCore.Http;
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
            //_documentService = new DocumentService(_repository, _mapper, _loggerMock.Object);
            _documentService = new DocumentService(_repository, _mapper);
        }

        [Fact]
        public async Task InsertDocumentTest()
        {
            //Given
            //var document = _fixture.Build<DocumentModel>()
            //    .With(x => x.UserId, "11")
            //    .With(x => x.WithdrawalAmount, 23)
            //    .With(x => x.ContractNumber, "45")
            //    .With(x => x.FirstName, "Andrey")
            //    .With(x => x.LastName, "Popov")
            //    .Create();
            var document = new DocumentModel
            {
                UserId = "11",
                WithdrawalAmount = 23,
                ContractNumber = "45",
                FirstName = "Andrey",
                LastName = "Popov"
            };
            List<DocumentModel> documents = new();
            documents.Add(document);

            _documentService.SaveToCollection(documents);

            List<DocumentModel> result = await _documentService.GetByUserIdAsync(document.UserId);
            //Then
            Assert.Equal(documents[0], result[0]);
        }

        public void GetDocumentByUserIdTest()
        {

        }

        [Fact]
        public void ShouldSuccessfullyInsertDocumentModelTest()
        {
            //Given
            var sut = GetSut();

            var documents = _fixture.Create<List<DocumentModel>>();
            var expectedResult = new List<DocumentModel>() { };

            //When
            List<DocumentModel> result = sut.GetByUserIdAsync(documents.First().UserId).Result;

            //Then
            Assert.Equal(expectedResult, result);
        }

        private DocumentService GetSut()
        {
            return _fixture.Create<DocumentService>();
        }
    }
}