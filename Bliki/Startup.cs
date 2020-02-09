using Bliki.Areas.Identity;
using Bliki.Components;
using Bliki.Data;
using Bliki.Interfaces;
using Bliki.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace Bliki
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDefaultIdentity<BlikiUser>()
                .AddUserStore<BlikiUserStore>()
                .AddRoles<IdentityRole>()
                .AddRoleStore<BlikiRoleStore>()
                .AddUserManager<BlikiUserManager>();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddScoped<AuthenticationStateProvider,
                RevalidatingIdentityAuthenticationStateProvider<BlikiUser>>();
            services.AddScoped<PageManager>();
            services.AddScoped<IGitManager, GitManager>();
            services.AddScoped<MarkdownEditorManager>();
            services.AddScoped<ModalService>();
            services.AddScoped<FileModalService>();
            services.AddScoped<FileManager>();
            services.AddHttpContextAccessor();
            services.AddMvc();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Map("/uploads", fileApp =>
            {
                fileApp.UseStaticFiles(new StaticFileOptions()
                {
                    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"WikiPages", @"uploads")),
                    ServeUnknownFileTypes = true
                });
            });

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // This is a bug workaround for BlazorInputFile and IIS
            app.Use(async (context, next) =>
            {
                context.Features.Get<IHttpMaxRequestBodySizeFeature>()
                    .MaxRequestBodySize = null; // setting this to a number will just postpone the bug until that limit has reached (i.e 50Mb will still break after three pushes of the button in the example above)

                await next.Invoke();
            });

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }


}