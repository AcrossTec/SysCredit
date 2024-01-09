namespace SysCredit.Mobile.ViewModels.Catalogs.LoanTypes;

using SysCredit.Mobile.Models.Catalogs;
using System.Collections.ObjectModel;

public partial class ViewLoanTypeCatalogPageViewModel : ViewModelBase
{
    public ObservableCollection<LoanType> LoanTypes { get; } = new ObservableCollection<LoanType>()
    {
        new LoanType()
        {
            m_LoanTypeId = 1,
            m_Name = "Personal"
        },
        new LoanType()
        {
            m_LoanTypeId = 2,
            m_Name = "Business"
        },
        new LoanType()
        {
            m_LoanTypeId = 3,
            m_Name = "Mortgage"
        },
        new LoanType()
        {
            m_LoanTypeId = 4,
            m_Name = "Car"
        },
        new LoanType()
        {
            m_LoanTypeId = 5,
            m_Name = "Student"
        },
    };
}
