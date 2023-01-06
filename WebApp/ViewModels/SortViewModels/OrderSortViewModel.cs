using WebApp.Enum;
namespace WebApp.ViewModels.SortViewModels;
public class OrderSortViewModel
    {
        
        public SortState NumberSort { get; }
        public SortState AcceptDateSort { get; }
        public SortState StartDateSort { get; }
        public SortState EndDateSort { get; }

        public SortState CostSort { get; }
        public SortState Current { get; }
        
        public OrderSortViewModel(SortState sortOrder) 
        {
            NumberSort = sortOrder == SortState.NumberAsc ? SortState.NumberDesc : SortState.NumberAsc;
            AcceptDateSort = sortOrder == SortState.AcceptDateAsc ? SortState.AcceptDateDesc : SortState.AcceptDateAsc;
            StartDateSort = sortOrder == SortState.StartDateAsc ? SortState.StartDateDesc : SortState.StartDateDesc;
            EndDateSort = sortOrder == SortState.EndDateAsc ? SortState.EndDateDesc : SortState.EndDateDesc;
            CostSort = sortOrder == SortState.CostAsc ? SortState.CostDesc: SortState.CostAsc;
            Current = sortOrder;
        }
    }