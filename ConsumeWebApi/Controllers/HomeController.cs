using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ConsumeWebApi.Models;
using CoreApiClient;
using System.Threading.Tasks;
using WebApiModels;
using Newtonsoft.Json;

namespace ConsumeWebApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApiClient apiClient;

        public HomeController(IApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<IActionResult> Index()
        {
            ToDoInputModel data = new ToDoInputModel();

            try
            {
                var all = await this.apiClient.GetApiObjects<ToDoViewModel>();
                data.Items = all;
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
            
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ToDoViewModel model)
        {
            try
            {
                await this.apiClient.PostApiObject<ToDoViewModel>(model);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ToDoInputModel model)
        {
            ToDoViewModel todo = JsonConvert.DeserializeObject<ToDoViewModel>(model.JsonObj);
            todo.IsComplete = model.IsComplete;
            try
            {
                await this.apiClient.EditApiObject(todo.Id, todo);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ToDoInputModel model)
        {
            ToDoViewModel todo = JsonConvert.DeserializeObject<ToDoViewModel>(model.JsonObj);

            try
            {
                await this.apiClient.DeleteApiObject(todo.Id);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
