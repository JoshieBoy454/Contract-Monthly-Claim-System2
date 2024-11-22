using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Contract_Monthly_Claim_System2.Data;
using Contract_Monthly_Claim_System2.Models;
using Contract_Monthly_Claim_System2.Areas.Identity.Data;
namespace Contract_Monthly_Claim_System2
{
    /// <summary>
    /// Joshua Gain 
    /// ST10369044
    /// PROG6212
    /// Group 1
    /// </summary>
    /// <Refernces>
    /// Link:https://www.w3schools.com/asp/razor_syntax.asp
    /// Link:https://www.youtube.com/watch?v=xuFdrXqpPB0
    /// Link:https://www.youtube.com/watch?v=xuFdrXqpPB0&t=857s
    /// Link:https://www.youtube.com/watch?v=wzaoQiS_9dI&list=TLPQMTYxMDIwMjQtyPiQEOad5g&index=3
    /// Link:https://www.youtube.com/watch?v=wzaoQiS_9dI&list=TLPQMTYxMDIwMjQtyPiQEOad5g&index=3
    /// Link:https://www.w3schools.com/html/html_form_input_types.asp
    /// Link:https://learn.microsoft.com/en-us/sql/t-sql/data-types/decimal-and-numeric-transact-sql?view=sql-server-ver16
    /// Link:https://learn.microsoft.com/en-us/aspnet/core/mvc/views/razor?view=aspnetcore-8.0
    /// Link:https://www.w3schools.com/cssref/pr_text_text-decoration.php
    /// Link:https://www.w3schools.com/cssref/playdemo.php?filename=playcss_text-decoration-line
    /// Link:https://uiverse.io/Yaya12085/short-panda-24
    /// Link:
    /// Link:
    /// Link:
    /// Link:
    /// 
    /// </Refernces>
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //var connectionString = builder.Configuration.GetConnectionString("CMCSContextConnection") ?? throw new InvalidOperationException("Connection string 'CMCSContextConnection' not found.");//

            //builder.Services.AddDefaultIdentity<CMCSUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<CMCSContext>();//

            builder.Services.AddDbContext<ClaimDBContext>(options => options.UseInMemoryDatabase("CMCSdb"));


            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //builder.Services.AddRazorPages();//
            var app = builder.Build();

            app.UseMiddleware<Contract_Monthly_Claim_System2.Middleware.ExceptionHandling>();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            //app.MapRazorPages();//

            app.Run();
        }
    }
}
