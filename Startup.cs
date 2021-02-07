using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Piranha;
using Piranha.AspNetCore.Identity.SQLite;
using Piranha.AttributeBuilder;
using Piranha.CookiePolicy;
using Piranha.Data.EF.SQLite;
using Piranha.Mailer;
using Piranha.Manager.Editor;
using PiranhaMVC.Blocks;

namespace TryPiranha
{
    public class Startup
    {
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _env;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="configuration">The current configuration</param>
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _config = configuration;
            _env = webHostEnvironment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Service setup
            services.AddPiranha(options =>
            {
                if (_env.IsDevelopment())
                    options.AddRazorRuntimeCompilation = true;

                options.UseFileStorage(naming: Piranha.Local.FileStorageNaming.UniqueFolderNames);
                options.UseImageSharp();
                options.UseManager();
                options.UseTinyMCE();
                options.UseMemoryCache();
                options.UseEF<SQLiteDb>(db =>
                    db.UseSqlite(_config.GetConnectionString("piranha")));
                options.UseIdentityWithSeed<IdentitySQLiteDb>(db =>
                    db.UseSqlite(_config.GetConnectionString("piranha")));
            });

            services.AddSendGridModule(_config);
            services.AddCookiePolicyModule(new Piranha.CookiePolicy.Models.CookieBannerConfiguration()
            {
                DataMessage = "Questo sito fa uso di cookies per migliorare l'esperienza di navigazione e per fornire funzionalità aggiuntive",
                DataLinkmsg = "Maggiori informazioni",
                DataMoreinfo = "https://it.wikipedia.org/wiki/Cookie",
                DataCloseText = "Ho capito"
            });

            //if (!_env.IsDevelopment())
            //{
            //services.AddHttpsRedirection(options =>
            //{
            //    options.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
            //    options.HttpsPort = 443;
            //});
            //}
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApi api)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseStatusCodePagesWithRedirects("/cms/statusCode?code={0}");

            app.UseSendGridModule();

            // Initialize Piranha
            App.Init(api);

            // Build content types
            new ContentTypeBuilder(api)
                .AddAssembly(typeof(Startup).Assembly)
                .Build()
                .DeleteOrphans();

            // Configure Tiny MCE
            EditorConfig.FromFile("editorconfig.json");

            RegisterBlocks();

            // Middleware setup
            app.UsePiranha(options =>
            {
                options.UseManager();
                options.UseTinyMCE();
                options.UseIdentity();
            });


        }

        private void RegisterBlocks()
        {
            App.Blocks.Register<AboutSectionBlock>();
            App.Blocks.Register<DepartmentSectionBlock>();
            App.Blocks.Register<DepartmentBlock>();
            App.Blocks.Register<ContactUsBlock>();
        }
    }
}
