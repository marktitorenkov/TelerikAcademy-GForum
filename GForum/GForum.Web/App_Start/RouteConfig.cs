﻿using System.Web.Mvc;
using System.Web.Routing;

namespace GForum.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.LowercaseUrls = true;

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "UsersByUsername",
                url: "User/{username}",
                defaults: new { controller = "Users", action = "Index" }
            );

            routes.MapRoute(
                name: "PostSubmit",
                url: "Forum/Category/{catgeoryId}/Submit",
                defaults: new { controller = "Forum", action = "Submit" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
