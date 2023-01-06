using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WebApp.ViewModels.FilterViewModels;

public class AdTypeFilterViewModel
{
    public string? SelectedName { get; }

    public AdTypeFilterViewModel(string? adTypeName)
    {
        SelectedName = adTypeName;
    }
}