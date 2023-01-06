namespace WebApp.ViewModels.FilterViewModels;
public class OrderFilterViewModel
{
    public string? SelectedName { get; }
    public string? SelectedType { get; }

    public OrderFilterViewModel(string? selectedName, string? selectedType)
    {
        SelectedName = selectedName;
        SelectedType = selectedType;
    }
}
