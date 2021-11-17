using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using MockAssessment7.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MockAssessment7.Controllers
{
    public class HomeController : Controller
    {
        // GET: DonutController
        public ActionResult Index()
        {
            return View();
        }

        // GET: DonutController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DonutController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DonutController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DonutController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DonutController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DonutController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DonutController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Search(int Id)
        {
            var url = "https://grandcircusco.github.io/demo-apis/donuts.json";
            Donut donut = new Donut();
            List<Donut> donuts = new List<Donut>();
            List<Donut> donutsList = new List<Donut>();
            List<string> donutsJson = new List<string>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage message = await client.GetAsync(url);
                dynamic result = await message.Content.ReadAsStringAsync();
                JObject parsedJson = JObject.Parse(result);
                var datasetRef = parsedJson["results"].First(n => n.SelectToken("id").ToString() == $"{Id}")
                                                      .SelectToken("ref")
                                                      .ToString();

                HttpResponseMessage message2 = await client.GetAsync(datasetRef);
                dynamic result2 = await message2.Content.ReadAsStringAsync();
                var output = JsonConvert.DeserializeObject(result2);
                donut.Id = output.id;
                donut.Name = output.name;
                donut.Calories = output.calories;
                donut.Extras = output.extras.ToObject<string[]>();
                donut.PhotoURL = output.photo;
            }
            donuts.Add(donut);
            return View(donuts);
        }
    }
}
