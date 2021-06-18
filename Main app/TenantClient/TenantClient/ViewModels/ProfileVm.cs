using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantClient.ViewModels
{
    public class ProfileVm : BaseVM
    {
        private int _accountId;
        public ProfileVm(int accountId)
        {
            _accountId = accountId;
        }
    }
}
