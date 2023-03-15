namespace TestProject.Repositories.Entities;

public class DbDocument
{
    public string LeadId { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string ContractNumber { get; set; }
    public decimal WithdrawalAmount { get; set; }
    public bool IsDeleted { get; set; }
}

