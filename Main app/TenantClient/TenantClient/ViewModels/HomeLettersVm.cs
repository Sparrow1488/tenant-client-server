using TenantClient.Pages;

namespace TenantClient.ViewModels
{
    internal class HomeLettersVm : BaseHomeVm
    {
        public HomeLettersVm()
        {
            SetFirstPage(new MyLetters());
        }
    }
}
