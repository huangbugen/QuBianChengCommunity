using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FileDownLoadSystem.Core.Configuration;
using FileDownLoadSystem.Core.Extensions;
using FileDownLoadSystem.Entity.Core;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace FileDownLoadSystem.Core.Untity
{
    public class JwtHelper
    {
        public static string IssueJwt(UserClaim userClaim)
        {
            string exp = new DateTimeOffset(DateTime.Now.AddMinutes(AppSetting.ExpMinutes)).ToUnixTimeSeconds() + "";
            var claims = new List<Claim>
            {
                // Jti => Jwt Id 是JWT的唯一标识
                // Iat => Issued At: 确定JWT的发布时间
                // Nbf => 在这个时间之前不能用
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti,userClaim.User_Id.ToString()),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()+""),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Nbf,new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()+""),
                new Claim("Role",1+""),
               
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Exp, exp),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Iss, AppSetting.Secret.Issuer),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Aud, AppSetting.Secret.Audience),
            };
            // 密钥 16位的
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSetting.Secret.JWT));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: AppSetting.Secret.Issuer,
                claims: claims,
                signingCredentials: creds
            );
            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }

        public static UserClaim SerializerJwt(string token)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(token);
            UserClaim userClaim = new UserClaim
            {
                User_Id = Convert.ToInt32(jwtToken.Id),
                Role_Id = (jwtToken.Payload["Role"] ?? 0).GetInt(),
                // UserName = jwtToken.Payload[ClaimTypes.Name].ToString()
            };
            return userClaim;
        }

        public static DateTime GetExp(string token)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(token);
            DateTime expDate = jwtToken.Payload[System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Exp].GetInt().GetTimeStampToDate();
            return expDate;
        }
    }
}