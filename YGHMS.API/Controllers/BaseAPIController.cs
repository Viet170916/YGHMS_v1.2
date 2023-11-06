using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YGHMS.API.DTO.AuthenDTO;
using YGHMS.API.Helpers;
using YGHMS.API.Infra.Const;

namespace YGHMS.API.Controllers
{
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        public BaseApiController()
        {
        }
        protected UserHeader? UserHeader
        {
            get
            {
                try
                {
                    var header = HttpContext.Request.Headers;
                    bool result = header.TryGetValue(StaticVariable.KeyHeaderToken.KeyHeader, out var token);
                    if (result)
                    {
                        var tmp = AesCryptoHelper.DecryptString(token, StaticVariable.KeyHeaderToken.SecretKey);
                        var user = JsonConvert.DeserializeObject<UserHeader>(tmp);
                        if (user == null) return null;
                        if (user.Expires <= DateTime.UtcNow)
                        {
                            throw new Exception("token revoked");
                        }
                        return user;
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
    }
}
