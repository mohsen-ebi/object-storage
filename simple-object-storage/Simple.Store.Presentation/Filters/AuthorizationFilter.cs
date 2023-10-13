using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Simple.Object.Storage.Presentation.Filters;

// public class AuthorizationAttribute : TypeFilterAttribute
// {
// public AuthorizationAttribute(string policy) : base(
//     typeof(OnAuthorization))
// {
//     Arguments = new object[]
//     {
//         policy
//     };
// }

// private class OnAuthorization : IAsyncActionFilter
// {
//     private readonly IServiceClient<IdentityGrpc.IdentityGrpcClient> _identityGrpcService;
//     private readonly string[] _policies;
//
//     public OnAuthorization(IServiceClient<IdentityGrpc.IdentityGrpcClient> identityGrpcService, string[] policies)
//     {
//         _identityGrpcService = identityGrpcService;
//         _policies = policies;
//     }
//
//     public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
//     {
//         var token = context.HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", string.Empty);
//
//         if (!string.IsNullOrEmpty(token))
//         {
//             var userClaims = token.ReadJwt();
//             var exp = userClaims.FirstOrDefault(h => h.Type.Equals("exp"))?.Value;
//
//             if (DateTimeOffset.UtcNow > DateTimeOffset.UnixEpoch.AddSeconds(double.Parse(exp)))
//             {
//                 context.Result = new UnauthorizedResult();
//                 return;
//             }
//
//             context.HttpContext.User.AddIdentity(new ClaimsIdentity(userClaims));
//
//             #region check policy
//
//             if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Development" &&
//                 !_policies.Any(item => item.Equals(string.Empty)))
//             {
//                 var headers = new Metadata();
//                 headers.Add("Authorization", $"Bearer {token}");
//
//                 var authentication = new AuthenticationDTO();
//                 authentication.Condition = _authOperator.Equals(AuthOperator.AND) ? true : false;
//                 authentication.Policies.AddRange(_policies);
//
//                 var response =
//                     _identityGrpcService.Send<AuthenticationDTO, AuthenticationViewModel>(
//                         x => x.ValidateAuthentication, authentication, false, headers);
//
//                 if (response is null || !response.Authenticate)
//                 {
//                     context.Result = new AccessRestrictedResult();
//                     return;
//                 }
//             }
//
//             #endregion
//         }
//         else
//         {
//             context.Result = new UnauthorizedResult();
//             return;
//         }
//
//         await next();
//     }
// }
//}

public static class TokenUtil
{
    public static List<Claim> ReadJwt(this string token) =>
        new JwtSecurityTokenHandler().ReadJwtToken(token).Claims.ToList<Claim>();
}