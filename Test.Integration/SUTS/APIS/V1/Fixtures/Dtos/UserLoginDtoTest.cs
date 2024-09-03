using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Integration.SUTS.APIS.V1.Fixtures.Dtos
{
    public class UserLoginDtoTest
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public bool? IsRememberMe { get; set; }
        public override string ToString()
        {
            return $"{UserName} : {Password}";
        }

    }
}
