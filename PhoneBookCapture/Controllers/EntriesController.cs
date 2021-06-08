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
    public class EntriesController : Controller
    {
        private readonly IConfiguration Configuration;
        private readonly string url;

        public EntriesController(IConfiguration configuration)
        {
            Configuration = configuration;
            url = Configuration["PhoneBookAPI:BaseUrl"];
        }

        public async Task<IActionResult> Index(string searchString)
        {

            string entryData = await Helper.GetData(url + "/Entries/GetEntries");

            var obj = JsonConvert.DeserializeObject<List<Entry>>(entryData);


            if (!string.IsNullOrEmpty(searchString))
            {
                var entries = obj.Where(s => s.Name.Contains(searchString)
                                       || s.PhoneNumber.Contains(searchString));

                return View(entries);
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

            string entryData = await Helper.GetData(url + "/Entries/GetEntry/" + id);

            var obj = JsonConvert.DeserializeObject<Entry>(entryData);

            if (entryData == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Entry entry)
        {
            if (ModelState.IsValid)
            {
                bool entryData = await Helper.PostData(url + "/Entries/PostEntry", entry);

                if (entryData)
                {

                    return RedirectToAction(nameof(Index));
                }

            }
            return View(entry);
        }

        public async Task<IActionResult> EditView(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            string entryData = await Helper.GetData(url + "/Entries/GetEntry/" + id);

            var obj = JsonConvert.DeserializeObject<Entry>(entryData);

            if (entryData == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("EntryID,Name,PhoneNumber")] Entry entry)
        {
            bool entryData;

            if (ModelState.IsValid)
            {
                try
                {
                    entryData = await Helper.PutData(url + "/Entries/UpdateEntry/" + entry.EntryID, entry);
                }
                catch (Exception)
                {
                    throw;
                }

                if (entryData)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(entry);
        }


    }
}
