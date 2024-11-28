using Microsoft.AspNetCore.Mvc.Rendering;

namespace AviaMare.Services
{
    public class EnumHelper
    {
        public List<string> GetNames<T>(T userRole)
            where T : Enum
        {
            // userRole = 6 // 0110

            var type = typeof(T);

            var names = type
                .GetEnumValues()    // List<object> { User, Admin, AnimeModerator, AnimeAuthor }
                .Cast<T>()       // List<Role> { User, Admin, AnimeModerator, AnimeAuthor }
                .Where(r => userRole.HasFlag(r)) // List<Role> { Admin, AnimeModerator }
                .Select(r => type.GetEnumName(r)) // List<string> { "Admin", "AnimeModerator" }
                .ToList();

            return names;
        }

        public List<SelectListItem> GetSelectListItems<T>()
             where T : Enum
        {
            var type = typeof(T);
            return type.GetEnumValues()
                .Cast<T>()
                .Select(x => new SelectListItem
                {
                    Value = x.ToString(),
                    Text = type.GetEnumName(x)
                })
                .ToList();
        }
    }
}
