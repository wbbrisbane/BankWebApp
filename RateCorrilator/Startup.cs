using System.Web.Http;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;


namespace RateCorrilator
{
    public class Startup{
        
        public void Configuration(IAppBuilder appBuilder){
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "UserControlApi",
                routeTemplate: "api/{controller}/{datefrom}/{dateto}",
                defaults: new { datefrom = RouteParameter.Optional,
                                dateto = RouteParameter.Optional}
            );

            appBuilder.UseWebApi(config);

            var physicalFileSystem = new PhysicalFileSystem(@"../../../webfiles");
            var options = new FileServerOptions{EnableDefaultFiles = true,
                                                FileSystem = physicalFileSystem};
            options.StaticFileOptions.FileSystem = physicalFileSystem;
            options.StaticFileOptions.ServeUnknownFileTypes = true;
            options.DefaultFilesOptions.DefaultFileNames = new[]{"RateRequestGUI.HTML"};

            appBuilder.UseFileServer(options);
        }
    }
}
