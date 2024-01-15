namespace SysCredit.Mobile.Views.Customers;

using SysCredit.Mobile.Models.Customers.Creates;

public class ReferenceListTemplateSelector : DataTemplateSelector
{
    public DataTemplate MaleTemplate { get; set; } = default!;

    public DataTemplate FemaleTemplate { get; set; } = default!;

    protected override DataTemplate OnSelectTemplate(object Item, BindableObject Container)
    {
        var Reference = (CreateReference)Item;

        if (Reference.Gender == SysCredit.Models.Gender.Female)
            return FemaleTemplate;

        return MaleTemplate;
    }
}
