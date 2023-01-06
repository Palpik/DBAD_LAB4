using WebApp.ViewModels.PageViewModels;
using WebApp.ViewModels.SortViewModels;
using WebApp.ViewModels.FilterViewModels;

namespace WebApp.ViewModels.IndexViewModels;

public class AdPlacesViewModel
{
    public IEnumerable<AdPlace> AdPlaces { get; } = new List<AdPlace>();
    public PageViewModel PageViewModel { get; }
    public AdPlaceSotrViewModel SortViewModel { get; }
    public AdPlaceFilterViewModel FilterViewModel { get; }

    public AdPlacesViewModel(IEnumerable<AdPlace> adPlaces, PageViewModel pageViewModel, AdPlaceSotrViewModel sortViewModel, AdPlaceFilterViewModel filterViewModel)
    {
        AdPlaces = adPlaces;
        PageViewModel = pageViewModel;
        SortViewModel = sortViewModel;
        FilterViewModel = filterViewModel;
    }
}