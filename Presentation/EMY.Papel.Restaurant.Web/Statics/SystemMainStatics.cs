using EMY.Papel.Restaurant.Core.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace EMY.Papel.Restaurant.Web.Statics
{
    public static class SystemMainStatics
    {
        public const string DefaultScheme = "EmyRestaurantScheme";

        public const string ClaimIdentity = "EmyIdentityRestaurantService";
        public static string AuthorizeErrorMessage = "You do not have enaugh authorize!";
        public static bool IsBetween(this DateTime selectedDate, DateTime dtBegin, DateTime dtEnd) =>
            (selectedDate <= dtEnd && selectedDate >= dtBegin) || (selectedDate <= dtBegin && selectedDate >= dtEnd);
        public static Guid ActiveUserID(this Controller controller) => controller.User.Identity.Name.ToGuid();
    }
}
