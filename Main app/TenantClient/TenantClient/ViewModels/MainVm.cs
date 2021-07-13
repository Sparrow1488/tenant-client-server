using TenantClient.Pages;

namespace TenantClient.ViewModels
{
    internal class MainVm : BaseHomeVm
    {
        public MainVm()
        {
            SetFirstPage(new Profile());
        }
    }
}
