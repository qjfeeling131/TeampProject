using BaseAPI.Filter;
using Microsoft.AspNetCore.Mvc;

namespace BaseAPI.Controllers
{
    [ServiceFilter(typeof(ExceptionFilter))]
    public class BaseController : Controller
    {
    }
}
