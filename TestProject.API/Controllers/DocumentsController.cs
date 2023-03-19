using Microsoft.AspNetCore.Mvc;
using System.Text;
using TestProject.API.Models;
using TestProject.Logic.Services;

namespace TestProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentController : ControllerBase
{
    private readonly IDocumentService _documentService;

    public DocumentController(IDocumentService documentService)
    {
        _documentService = documentService;
    }

    /// <summary>
    /// Add data from a document to the repository.
    /// </summary>
    /// <param name="file">File</param>
    /// <returns>List of IDs of added entities</returns>
    [HttpPost()]
    public async Task<IActionResult> Insert(IFormFile file)
    {
        if (file == null || file.Length < 0)
            return BadRequest();

        var ids = await _documentService.InsertAsync(file);

        return Ok(ids);
    }

    /// <summary>
    /// Get data by User ID.
    /// </summary>
    /// <param name="userId">UserId</param>
    /// <returns>List DocumentModel</returns>
    [HttpGet("user-id")]
    public IActionResult GetDocumentsByUserId(string userId)
    {
        if (string.IsNullOrEmpty(userId))
            return BadRequest("UserId is required.");

        var documents = _documentService.GetByUserIdAsync(userId);

        return Ok(documents);
    }

    /// <summary>
    /// Get data by contract number.
    /// </summary>
    /// <param name="contractNumber">ContractNumber</param>
    /// <returns>List DocumentModel</returns>
    [HttpGet("contract-number")]
    public async Task<IActionResult> GetByContractNumber(string contractNumber)
    {
        if (string.IsNullOrEmpty(contractNumber))
            return BadRequest("ContractNumber is required.");

        var documents = await _documentService.GetByContractNumberAsync(contractNumber);

        return Ok(documents);
    }

    /// <summary>
    /// Get data by contract number.
    /// </summary>
    /// <param name="documentId">Document Id</param>
    /// <returns>DocumentModel</returns>
    [HttpGet("by-id")]
    public async Task<IActionResult> GetDocumentById([FromQuery] ulong documentId)
    {
        var document = await _documentService.GetById(documentId);

        return Ok(document);
    }

    /// <summary>
    /// Delete data by user ID or/and by сontract number
    /// </summary>
    /// <param name="userId">UserId</param>
    /// <param name="contractNumber">ContractNumber</param>
    /// <returns></returns>
    [HttpDelete("user-id-or-contract-number")]
    public async Task<IActionResult> DeleteByUserIdOrContractNumber([FromBody] DeleteDocumentModel model)
    {
        ulong deletedRecordsCount = await _documentService.DeleteByUserIdOrContractNumberAsync(model.UserId, model.ContractNumber);

        return Ok(deletedRecordsCount);
    }
}
