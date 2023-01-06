using WebApp.ViewModels.PageViewModels;
using WebApp.ViewModels.SortViewModels;
using WebApp.ViewModels.FilterViewModels;

namespace WebApp.ViewModels.IndexViewModels;

public class AdTypesViewModel
{
    public IEnumerable<AdType> AdTypes { get; } = new List<AdType>();
    public PageViewModel PageViewModel { get; }
    public AdTypeSortViewModel SortViewModel { get; }
    public AdTypeFilterViewModel FilterViewModel { get; }
    public AdTypesViewModel(IEnumerable<AdType> adTypes, PageViewModel pageViewModel, AdTypeSortViewModel sortViewModel, AdTypeFilterViewModel filterViewModel)
    {
        AdTypes = adTypes;
        PageViewModel = pageViewModel;
        SortViewModel = sortViewModel;
        FilterViewModel = filterViewModel;
    }
}