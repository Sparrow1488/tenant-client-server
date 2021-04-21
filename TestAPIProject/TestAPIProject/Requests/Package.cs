using TenantSystemAPI.Requests.Objects;
using TenantSystemAPI.Requests.Security;

namespace TenantSystemAPI.Requests
{
    public class Package
    {
        public Package(Meta packMeta, IRequestObject attachObj)
        {
            Meta = packMeta;
            AttachObject = attachObj;
        }
        public Meta Meta { get; protected set; }
        public IRequestObject AttachObject { get; protected set; }
        public PackageSecurity SecurityInfo { get; set; }
    }
}
