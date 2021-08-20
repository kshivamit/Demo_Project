using BL.Repository;
using CandidateDL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateCRUD.Controllers
{
    public class CandidateController : Controller
    {
        private readonly ICandidateRepository _ICandidateRepository;
        public CandidateController(ICandidateRepository candidateRepository)
        {
            _ICandidateRepository = candidateRepository;
        }
        public IActionResult Index(int Id)
        {
            if (Id == 0)
                return View("~/Views/Candidate/Index.cshtml");
            else
            {
                var response = _ICandidateRepository.GetCandidateById(Id);
                return View("~/Views/Candidate/Index.cshtml",response);
            }

        }
        [HttpPost]
        public IActionResult CreateCandidate(Candidate model) //IFormCollection fc -Give data in key-value pair
        {
            if (model.Id == 0)
            {
                var response = _ICandidateRepository.CreateCandidate(model);
            }
            else
            {
                var response = _ICandidateRepository.UpdateCandidate(model);
            }
            return RedirectToAction("GetCandidates", "Candidate");
        }
        public IActionResult GetCandidates()
        {
            var response = _ICandidateRepository.GetCandidates();
            return View("~/Views/Candidate/CandidateList.cshtml", response);
        }
        public IActionResult DeleteCandidate(int Id)
        {
            var response = _ICandidateRepository.DeleteCandidate(Id);
            return Json(response);
        }
    }
}
