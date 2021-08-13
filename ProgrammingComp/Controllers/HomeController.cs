using ProgrammingComp.Models;
using ProgrammingComp.ViewModels;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;

namespace ProgrammingComp.Controllers
{
    public class HomeController : Controller
    {
        private TasksDBContext db = new TasksDBContext();
        public ActionResult Index()
        {
            ViewBag.TaskList = db.Tasks.ToList();
            return View();
        }

        public ActionResult Contact()
        {
            List<ShowTop3VM> lstTop3Vm = new List<ShowTop3VM>();
            var top3data = db.TasksResults.ToList();
            var tasksList = db.Tasks.ToList();
            if (top3data != null && top3data.Count() > 0)
            {
                var data = top3data.Where(p => p.Result != 0).GroupBy(o => o.Name).OrderByDescending(p => p.Count()).Take(3).ToList();
                if (data != null && data.Count() > 0)
                {
                    foreach (var item in data)
                    {
                        ShowTop3VM top3 = new ShowTop3VM();
                        top3.Task = getTasksNames(item.AsEnumerable<TasksResults>().ToList());
                        top3.Submissions = item.Count();
                        top3.Name = item.Key;
                        lstTop3Vm.Add(top3);
                    }
                }
            }
            return View(lstTop3Vm);
        }

        private string getTasksNames(List<TasksResults> tasksCount)
        {
            var names = from tasksNames in tasksCount
                        join tasks in db.Tasks.ToList()
                        on tasksNames.TaskID equals tasks.taskID
                        select new { TaskName = tasks.taskName };

            return string.Join(",", names.Distinct().Select(o => o.TaskName));
        }

        [HttpPost]
        public ActionResult Index(SubmitTaskViewModel submiTtaskVM)
        {
            ViewBag.DBSuccess = SendCodetoDNFiddle(submiTtaskVM);

            return View("About");
        }

        private bool SendCodetoDNFiddle(SubmitTaskViewModel submiTtaskVM)
        {
            bool success = false;
            if (!string.IsNullOrEmpty(submiTtaskVM.Solution))
            {
                string code = submiTtaskVM.Solution;
                var apiUrl = "https://dotnetfiddle.net/api/fiddles/";

                var client = new RestClient(apiUrl);
                var request = new RestRequest("execute", Method.POST);

                request.RequestFormat = DataFormat.Json;

                var model = new CodeCompileModel()
                {
                    Compiler = Compiler.Net45,
                    Language = Language.CSharp,
                    ProjectType = ProjectType.Console,
                    CodeBlock = code
                };

                request.AddJsonBody(model);

                IRestResponse<FiddleExecuteResult> response = client.Execute<FiddleExecuteResult>(request);
                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    ViewBag.ProgramOutput = response.Data.ConsoleOutput.ToString();
                    if (SaveDatainDB(submiTtaskVM, response.Data.ConsoleOutput.ToString()))
                        success = true;
                }
            }
            return success;
        }


        private bool SaveDatainDB(SubmitTaskViewModel submiTtaskVM, string consoleOutput)
        {
            bool returnStatus = false;
            TasksResults result = new TasksResults();

            var tasks = db.Tasks.ToList();

            if (tasks != null)
            {
                result.Name = submiTtaskVM.Name;
                result.TaskID = submiTtaskVM.Task;
                result.Result = 0;
                result.SubmitDate = DateTime.Now;
                var seltasks = tasks.Where(o => o.taskID == submiTtaskVM.Task).Select(p => p.taskOutputParams).ToList();

                if (seltasks != null && seltasks[0].ToString() == consoleOutput)
                    result.Result = 1;

                returnStatus = true;
                db.TasksResults.Add(result);
                db.SaveChanges();
            }

            return returnStatus;
        }

    }
}