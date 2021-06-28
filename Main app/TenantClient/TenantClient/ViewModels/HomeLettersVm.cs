using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantClient.Pages;

namespace TenantClient.ViewModels
{
    internal class HomeLettersVm : BaseHomeVm
    {
        public HomeLettersVm()
        {
            SelectedPage = new MyLetters();
        }
    }
}
