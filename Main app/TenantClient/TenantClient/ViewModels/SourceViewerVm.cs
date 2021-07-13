using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TenantClient.Commands;

namespace TenantClient.ViewModels
{
    internal class SourceViewerVm : BaseVM
    {
        private ICollection<int> _sourcesId;
        public SourceViewerVm(ICollection<int> sourcesId)
        {
            _sourcesId = sourcesId;
        }
        public MyCommand GetSource
        {
            get => new MyCommand(async (obj) =>
            {
                await Task.Run(()=>
                {

                });
            });
        }
    }
}
