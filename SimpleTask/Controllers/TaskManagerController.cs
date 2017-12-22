using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using SimpleTask.Extensions;
using SimpleTask.DbModels;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace SimpleTask.Controllers
{
    public class TaskManagerController : Controller
    {
        #region prop
        public string ApiUrl
        {
            get
            {
                return "https://localhost:44351/api/tasks";
            }
        }
        #endregion

        // GET: TaskManager
        public async Task<ActionResult> Index()
        {
            List<TaskModel> modeldata = new List<TaskModel>();
            var client = new RestClient(ApiUrl);
            var request = new RestRequest(Method.GET);

            var response = await client.ExecuteAsync(request);

            var taskModelS = JsonConvert.DeserializeObject<List<TaskModel>>(response.Content);


            return View(taskModelS);

        }

        // GET: TaskManager/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }
            var client = new RestClient(ApiUrl + "/?Id=" + id);
            var request = new RestRequest(Method.GET);


            var response = await client.ExecuteAsync(request);

            var taskModel = JsonConvert.DeserializeObject<List<TaskModel>>(response.Content);


            if (taskModel == null && !taskModel.Any())
            {
                return NotFound();
            }

            return View(taskModel[0]);
        }

        // GET: TaskManager/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,DueDate,Status")] TaskModel taskModel)
        {
            if (ModelState.IsValid)
            {
                var client = new RestClient(ApiUrl);
                var request = new RestRequest(Method.POST);
                var model = JsonConvert.SerializeObject(taskModel);
                request.AddParameter("application/json", model, ParameterType.RequestBody);

                var response = await client.ExecuteAsync(request);

                return RedirectToAction(nameof(Index));
            }
            return View(taskModel);
        }




    }
}