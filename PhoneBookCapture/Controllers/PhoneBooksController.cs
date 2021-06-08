using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PhoneBookCapture.Data;
using PhoneBookCapture.Models;

namespace PhoneBookCapture.Controllers
{
    public class PhoneBooksController : Controller
    {
        private readonly IConfiguration Configuration;
        private readonly string url;

        public PhoneBooksController(IConfiguration configuration)
        {
            Configuration = configuration;
            url = Configuration["PhoneBookAPI:BaseUrl"];
        }

        public async Task<IActionResult> Index(string searchString)
        {
            string data = await Helper.GetData(url + "/PhoneBooks/GetPhoneBook");

            var obj = JsonConvert.DeserializeObject<List<PhoneBook>>(data);

            if (!string.IsNullOrEmpty(searchString))
            {
                var bookList = obj.Where(s => s.Name.Contains(searchString));

                return View(bookList);
            }
            else
            {

                return View(obj);

            }
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            string data = await Helper.GetData(url + "/PhoneBooks/GetPhoneBookById/" + id);

            var obj = JsonConvert.DeserializeObject<PhoneBook>(data);

            if (data == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        public async Task<ActionResult> EntryPartial(string id)
        {
            string entryData = await Helper.GetData(url + "/Entries/GetEntry/" + id);

            if (!entryData.Contains("["))
            {
                entryData = "[" + entryData + "]";
            }

            var obj = JsonConvert.DeserializeObject<List<Entry>>(entryData);

            return PartialView("_EntryPartial", obj);
        }

        public async Task<IActionResult> Create()
        {
            string entryData = await Helper.GetData(url + "/Entries/GetEntries");

            var obj = JsonConvert.DeserializeObject<List<Entry>>(entryData);

            ViewBag.EntryID = new SelectList(obj, "EntryID", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PhoneBook phoneBookData)
        {
            if (ModelState.IsValid)
            {
                bool data = await Helper.PostData(url + "/PhoneBooks/PostPhoneBooks", phoneBookData);                

                if (data)
                {

                    return RedirectToAction(nameof(Index));
                }

            }

            return View(phoneBookData);
        }
    }
}
