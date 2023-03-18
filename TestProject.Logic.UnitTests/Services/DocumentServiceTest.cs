using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using FluentAssertions;
using TestProject.Logic.Services.Document;
using TestProject.Logic.Services.Document.Models;
using Xunit;

namespace TestProject.Logic.UnitTests
{
    public class DocumentServiceTest
    {
        private IFixture _fixture;
        private readonly IMapper _mapper;

        public DocumentServiceTest(IMapper mapper)
        {
            _mapper = mapper;
        }

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
        }

        [Fact]
        public void ShouldSuccessfullyInsertDocumentModelTest()
        {
            //Given
            var sut = GetSut();

            var documents = _fixture.Create<List<DocumentModel>>();
            var expectedResult = new List<DocumentModel>() { };

            //When
            var result = sut.GetByUserId(documents.First().UserId);

            //Then
            result.Should().BeEquivalentTo(expectedResult);  
        }

        private DocumentService GetSut()
        {
            return _fixture.Create<DocumentService>();
        }
    }
}