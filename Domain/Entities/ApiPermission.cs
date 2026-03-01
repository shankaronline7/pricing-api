using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ApiPermission
    {
        public int PermissionId { get; set; }

        public string PermissionCode { get; set; }

        public string ApiRoute { get; set; }

        public string HttpMethod { get; set; }

        public ICollection<RolePermission> RolePermissions { get; set; }
    }
}
