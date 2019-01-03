using AutoMapper;
using System.Security.Claims;
using System.Security.Principal;

namespace GymApp.Helpers
{
	public static class Helpers
	{
		public static TDest MapTo<TDest>(this object src)
		{
			return (TDest)Mapper.Map(src, src.GetType(), typeof(TDest));
		}

		public static IMappingExpression<TSource, TDest> IgnoreAllUnmapped<TSource, TDest>(this IMappingExpression<TSource, TDest> expression)
		{
			expression.ForAllMembers(opt => opt.Ignore());
			return expression;
		}


    }

    public static class IdentityExtensions
    {
        public static string GetTicket(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("ticket");
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}