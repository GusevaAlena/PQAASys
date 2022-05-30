using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PQAASys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PQAASys.Controllers
{
    public class RequestController : Controller
    {
        private readonly PQASysContext _db;
        public RequestController(PQASysContext db)
        {
            _db = db;
        }

        [Authorize]
        public IActionResult MakeRequest(int? id,int SetId)
        {
            ViewData["SetId"] = SetId;
            RequestViewModel ReqVM = new RequestViewModel()
            {                   
                request = new Request() { 
                    InfoAboutSamples=SetId
                },
                UserList = _db.Users.Select(i => new SelectListItem
                {
                    Value = i.UserId.ToString(),
                    Text = i.Login
                }),
                OrganizationList = _db.Organizations.Select(i => new SelectListItem
                {
                     Value = i.OrganizationId.ToString(),
                     Text = i.OrganizationName
                }),
                TestList = _db.Tests.Select(i => new SelectListItem
                {
                     Value = i.TestId.ToString(),
                     Text = i.TestName
                }),
                StandartList = _db.Standarts.Select(i => new SelectListItem
                {
                     Value = i.Standart_id.ToString(),
                     Text = i.StandartSeries + " " + i.StandartNumber + " " + i.StandartName
                }),
                ConditionList = _db.Conditions.Select(i => new SelectListItem
                {
                     Value = i.ConditionId.ToString(),
                     Text = i.Temperature + ", " + i.Environment
                }),
            };
            if (id == null)
            {
                return View(ReqVM);
            }
            else
            {
                ReqVM.request = _db.Requests.Find(id);
                if (ReqVM.request == null)
                {
                    return NotFound();
                }
                return View();
            }                                                
        }
     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MakeRequest(RequestViewModel ReqVM)
        {
            Request Request = null;
            
            if (ModelState.IsValid)
            {               
                _db.Add(ReqVM.request);
                _db.SaveChanges();
                Request = _db.Requests.Where(i => i.requestNumber == ReqVM.request.requestNumber).FirstOrDefault();
                if (Request != null)
                {
                    return RedirectToAction("RequestCreated", new { id = Request.requestNumber });                                   
                }
            }
            ReqVM.UserList = _db.Users.Select(i => new SelectListItem
            {
                Value = i.UserId.ToString(),
                Text = i.Login
            });
            ReqVM.OrganizationList = _db.Organizations.Select(i => new SelectListItem
            {
                Value = i.OrganizationId.ToString(),
                Text = i.OrganizationName
            });
            ReqVM.TestList = _db.Tests.Select(i => new SelectListItem
            {
                Value = i.TestId.ToString(),
                Text = i.TestName
            });
            ReqVM.StandartList = _db.Standarts.Select(i => new SelectListItem
            {
                Value = i.Standart_id.ToString(),
                Text = i.StandartSeries + " " + i.StandartNumber + " " + i.StandartName
            });
            ReqVM.ConditionList = _db.Conditions.Select(i => new SelectListItem
            {
                Value = i.ConditionId.ToString(),
                Text = i.Temperature + ", " + i.Environment
            });
            
            return View(ReqVM);            
        }
        public IActionResult RequestCreated(int? id)
        {
            ViewData["Message"] = "Заявка успешно создана! Вашей заявке присвоен номер " + id
                       + ". Всю дальнейшую информацию по заявке можно узнать по этому номеру";
            return View();
        }
        
        public IActionResult ShowRequests(string SearchString)
        {
            IEnumerable<Request> objList = _db.Requests.Include(u => u.StatusNavigation);
            if (SearchString != null)
            {
                objList = objList.Where(s => s.requestNumber.ToString().Contains(SearchString));
            }
            return View(objList);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult EditRequests(string SearchString)
        {
            IEnumerable<Request> objList = _db.Requests.Include(u => u.InnerCustomerNavigation)
                .Include(u => u.TypeOfTestNavigation).Include(u => u.StatusNavigation)
                .Include(u => u.StandartNavigation).Include(u => u.InfoAboutSamplesNavigation);
            if (SearchString != null)
            {
                objList = objList.Where(s => s.requestNumber.ToString().Contains(SearchString));
            }
            return View(objList);
        }
       
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            RequestViewModel ReqVM = new RequestViewModel() {
                
                request = _db.Requests.Where(i => i.requestNumber == id).FirstOrDefault(),
                StatusList = _db.Statuses.Select(i => new SelectListItem()
                {
                    Value = i.StatusId.ToString(),
                    Text=i.Status1
                })
            };            
            return View(ReqVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(RequestViewModel ReqVM)
        {
            _db.Requests.Update(ReqVM.request);
            _db.SaveChanges();
            ReqVM.StatusList = _db.Statuses.Select(i => new SelectListItem()
            {
                Value = i.StatusId.ToString(),
                Text = i.Status1
            });
            return RedirectToAction("EditRequests");
        }

    }
}
