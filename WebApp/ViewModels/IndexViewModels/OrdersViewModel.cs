using WebApp.ViewModels.PageViewModels;
using WebApp.ViewModels.SortViewModels;
using WebApp.ViewModels.FilterViewModels;

namespace WebApp.ViewModels.IndexViewModels;

public class OrdersViewModel
{
    public IEnumerable<Order> Orders { get; } = new List<Order>();
    public PageViewModel PageViewModel { get; }
    public OrderSortViewModel SortViewModel { get; }

    public OrdersViewModel(IEnumerable<Order> orders, PageViewModel pageViewModel, OrderSortViewModel sortViewModel)
    {
        Orders = orders;
        PageViewModel = pageViewModel;
        SortViewModel = sortViewModel;
    }
}