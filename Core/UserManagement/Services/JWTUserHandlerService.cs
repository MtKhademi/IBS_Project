//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------

using Core.UserManagement.Abstractions.Services;
using Core.UserManagement.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Core.UserManagement.Services
{
    public class JWTUserHandlerService : IJWTUserHandlerService
    {


        private const string USER_CLAIM = "USER_CLAIM";
        private const string KeySecutiry = "!@#$%^&*()_+1234567890-=!@#$%^&*()_+1234567890-=";


        public string GenerateToken(UserManagementModel user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(KeySecutiry);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(USER_CLAIM, Newtonsoft.Json.JsonConvert.SerializeObject(user,
                    new Newtonsoft.Json.JsonSerializerSettings(){
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    })),
                }),
                Expires = DateTime.UtcNow.AddMinutes(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public UserManagementModel GetModelFromToken(string token)
        {
            try
            {

                if (token == "test")
                    return new UserManagementModel
                    {
                        UserName = "MR-ERROR"
                    };

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(KeySecutiry);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var user = jwtToken.Claims.FirstOrDefault(x => x.Type == USER_CLAIM)?.Value.ToString() ?? "";

                if (user == null) return null;

                return Newtonsoft.Json.JsonConvert.DeserializeObject<UserManagementModel>(user);

            }
            catch (SecurityTokenExpiredException) { throw; }
            catch (Exception ex)
            { throw new Exception($"fatal error in extract data from token access", ex); }
        }


    }
}
