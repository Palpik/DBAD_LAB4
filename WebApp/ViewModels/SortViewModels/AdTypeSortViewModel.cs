using WebApp.Enum;
namespace WebApp.ViewModels.SortViewModels;
public class AdTypeSortViewModel
    {
        public SortState NameSort { get; }
        public SortState NumberSort { get; }

        public SortState Current { get; }

        public AdTypeSortViewModel(SortState sortOrder) 
        {
            NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            NumberSort = sortOrder == SortState.NumberAsc ? SortState.NumberDesc : SortState.NumberAsc;
            Current = sortOrder;
        }
    }