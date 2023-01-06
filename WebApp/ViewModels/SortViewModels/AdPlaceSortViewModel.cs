using WebApp.Enum;
namespace WebApp.ViewModels.SortViewModels;
public class AdPlaceSotrViewModel
    {
        public SortState PlaceSort { get; }
        public SortState NumberSort { get; }
        public SortState CostSort { get; }
        public SortState TypeSort { get; }

        public SortState Current { get; }

        public AdPlaceSotrViewModel(SortState sortOrder) 
        {
            PlaceSort = sortOrder == SortState.PlaceAsc ? SortState.PlaceDesc : SortState.PlaceAsc;
            NumberSort = sortOrder == SortState.NumberAsc ? SortState.NumberDesc : SortState.NumberAsc;
            CostSort = sortOrder == SortState.CostAsc ? SortState.CostDesc: SortState.CostAsc;
            TypeSort = sortOrder == SortState.TypeAsc ? SortState.TypeDesc : SortState.TypeAsc;
            Current = sortOrder;
        }
    }