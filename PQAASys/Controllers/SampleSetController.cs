using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PQAASys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PQAASys.Controllers
{
    public class SampleSetController : Controller
    {
        private readonly PQASysContext _db;

        public SampleSetController(PQASysContext db)
        {
            _db = db;
        }
        [Authorize]
        
        public IActionResult Index()
        {
            SampleSetViewModel SSVM = new SampleSetViewModel()
            {
                sampleSet = new SampleSet()
            };
            return View(SSVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SampleSetViewModel SSVM)
        {
            if (ModelState.IsValid)
            {
                _db.Add(SSVM.sampleSet);
                _db.SaveChanges();
            }
            int SetId = SSVM.sampleSet.sampleSetId;
            return RedirectToAction("MakeRequest","Request", new { SetId=SetId});
        }
    }
}
