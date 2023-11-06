using YGHMS.API.DTO.Common;
using YGHMS.API.Infra.Config;

namespace YGHMS.API.Infra.Const
{
    public static class StaticVariable
    {
        public static KeyHeaderToken KeyHeaderToken = AppSettings.Get<KeyHeaderToken>("KeyHeaderToken");
        public static string PsSecretKey = AppSettings.Get<string>("PsSecretKey");
    }
}
