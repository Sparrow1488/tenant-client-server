using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using TenantClient.Commands;
using TenantClient.Exceptions;
using TenantClient.Pages;

namespace TenantClient.ViewModels
{
    internal class MainVm : BaseHomeVm
    {
        public MainVm()
        {
            SelectedPage = new Profile();
        }
    }
}
