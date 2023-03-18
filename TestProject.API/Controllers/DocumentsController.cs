using Microsoft.AspNetCore.Mvc;
using TestProject.API.Models;
using TestProject.Logic.Services;

namespace TestProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentsController : ControllerBase
{
    private readonly IDocumentService _documentService;

    public DocumentsController(IDocumentService documentService)
    {
        _documentService = documentService;
    }

    /// <summary>
    /// Get data by User ID
    /// </summary>
    /// <param name="userId">UserId</param>
    /// <returns></returns>
    [HttpGet("UserId")]
    public IActionResult GetDocumentsByUserId(string userId)
    {
        try
        {
            if (string.IsNullOrEmpty(userId))
                return BadRequest("UserId is required.");

            var documents = _documentService.GetByUserId(userId);

            return Ok(documents);
        }
        catch (Exception ex)
        {
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Get data by contract number
    /// </summary>
    /// <param name="contractNumber">ContractNumber</param>
    /// <returns></returns>
    [HttpGet("contract-number")]
    public async Task<IActionResult> GetByContractNumber(string contractNumber)
    {
        try //ToDo узнать, как лучше обрабатывать исключения.
        {
            if (string.IsNullOrEmpty(contractNumber))
                return BadRequest("ContractNumber is required.");

            var documents = await _documentService.GetByContractNumber(contractNumber);

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Delete data by user ID or/and by сontract number
    /// </summary>
    /// <param name="userId">UserId</param>
    /// <param name="contractNumber">ContractNumber</param>
    /// <returns></returns>
    [HttpDelete("user-id-or-contract-number")]
    public IActionResult DeleteByUserIdOrContractNumber([FromBody] DeleteDocumentModel model)
    {
        try
        {
            if (string.IsNullOrEmpty(model.UserId) && string.IsNullOrEmpty(model.ContractNumber))
                return BadRequest("One of the parameters must not equal an empty string and null");

            _documentService.DeleteByUserIdOrContractNumber(model.UserId, model.ContractNumber);

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500);
        }
    }
}
