using Fresh;
using Fresh.Config;

var builder = WebApplication.CreateBuilder(args);

#region 知识点清单——配置读取
// 配置读取分两种情况
// 1、读字符串
string conn = builder.Configuration.GetValue<string>("conn");
// 2、读对象
Person person = builder.Configuration.GetSection("Person").Get<Person>();
#endregion



// Add services to the container.
builder.Services.AddControllersWithViews();
// 在这里调用扩展类
builder.Register();
var app = builder.Build();

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

app.UseAuthentication();//鉴权
app.UseAuthorization(); //授权

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
