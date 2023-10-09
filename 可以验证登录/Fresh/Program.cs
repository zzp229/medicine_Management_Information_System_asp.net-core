using Fresh;
using Fresh.Config;

var builder = WebApplication.CreateBuilder(args);

#region ֪ʶ���嵥�������ö�ȡ
// ���ö�ȡ���������
// 1�����ַ���
string conn = builder.Configuration.GetValue<string>("conn");
// 2��������
Person person = builder.Configuration.GetSection("Person").Get<Person>();
#endregion



// Add services to the container.
builder.Services.AddControllersWithViews();
// �����������չ��
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

app.UseAuthentication();//��Ȩ
app.UseAuthorization(); //��Ȩ

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
