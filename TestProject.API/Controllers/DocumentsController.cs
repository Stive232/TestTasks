using Microsoft.AspNetCore.Mvc;
using TestProject.Logic.Services;
using TestProject.Logic.Services.Document.Models;

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
    /// Redirects to index with specified params
    /// </summary>
    /// <param name="leadId">LeadId</param>
    /// <returns></returns>
    [HttpGet("ByLeadId")]
    public IActionResult GetDocumentsByLeadId([FromBody] string leadId)
    {
        var documents = _documentService.GetByLeadId(leadId);
        return Ok(documents);
    }

    /// <summary>
    /// Redirects to index with specified params
    /// </summary>
    /// <param name="contractNumber">ContractNumber</param>
    /// <returns></returns>
    [HttpGet("ByContractNumber")]
    public IActionResult GetByContractNumber([FromBody] string contractNumber)
    {
        var documents = _documentService.GetByContractNumber(contractNumber);
        return Ok();
    }

    /// <summary>
    /// Redirects to index with specified params
    /// </summary>
    /// <param name="leadId">LeadId</param>
    /// <param name="contractNumber">CeadId</param>
    /// <returns></returns>
    [HttpDelete("ByLeadIdOrContractNumber")]
    public IActionResult DeleteByLeadIdOrContractNumber([FromBody] string leadId, string contractNumber)
    {
        
        return Ok();
    }


}
