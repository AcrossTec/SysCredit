namespace SysCredit.Mobile.Models.Catalogs;


public partial class LoanType : ModelBase
{
    public long m_LoanTypeId { get; set; }

    public string m_Name { get; set; } = string.Empty;
}
