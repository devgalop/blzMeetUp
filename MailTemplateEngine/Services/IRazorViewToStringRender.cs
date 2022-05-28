using System.Threading.Tasks;

namespace MailTemplateEngine.Services;

public interface IRazorViewToStringRenderer
{
    Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model);
}