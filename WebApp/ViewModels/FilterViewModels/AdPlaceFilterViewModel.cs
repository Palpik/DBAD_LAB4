namespace WebApp.ViewModels.FilterViewModels;
public class AdPlaceFilterViewModel
{
    public string? SelectedName { get; }
    public string? SelectedType { get; }

    public AdPlaceFilterViewModel(string? selectedName, string? selectedType)
    {
        SelectedName = selectedName;
        SelectedType = selectedType;
    }
}
