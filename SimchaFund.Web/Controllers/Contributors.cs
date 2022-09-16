using Microsoft.AspNetCore.Mvc;
using SimchaFund.Web.Models;
using SimchFund.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimchaFund.Web.Controllers
{
    public class Contributors : Controller
    {
        private string _connectionString = @"Data Source=.\sqlexpress;Initial Catalog=SimchaFund;Integrated Security=true;";
       
        [HttpPost]
        public IActionResult UpdateContributions(List<Contributor> contributors, int simchaId)
        {
            var db = new Manager(_connectionString);
            contributors = contributors.Where(c => c.Include).ToList();
            List<Contribution> contributions = new List<Contribution>();
            foreach (var c in contributors)
            {
                contributions.Add(new Contribution
                {
                    ContributorId = c.ID,
                    SimchaId = simchaId,
                    Amount = c.Amount
                });
            }
            db.UpdateContributions(contributions, simchaId);
            return Redirect("/simchah/simchas");
        }

        [HttpPost]
        public IActionResult Deposit(Deposits d)
        {
            var manager = new Manager(_connectionString);
            int n = manager.NewDeposit(d);
            TempData["Message"] =$"Deposit added successfully! Id {n}";
            return Redirect("/simchah/contributors");
        }
        [HttpPost]
        public IActionResult New(Deposits d, Contributor c)
        {
            var manager = new Manager(_connectionString);
            int n = manager.NewContributor(c);
            manager.NewDeposit(d, n);
            TempData["Message"] = $"New Contributer added successfully! Id {n}";
            return Redirect("/simchah/contributors");
        }
        [HttpPost]
        public IActionResult Edit(Deposits d, Contributor c)
        {
            var manager = new Manager(_connectionString);
             manager.EditContributor(c);
           
            TempData["Message"] = $" Contributer edited successfully!";
            return Redirect("/simchah/contributors");
        }
        public IActionResult History(int contribid)
        {
            var manager = new Manager(_connectionString);
            var list=manager.GetHistory(contribid);
            var vm = new ViewModel
            {
                Name = manager.GetContributorName(contribid),
                History = list,
                Balance = manager.GetBalance(contribid)
            };


            return View(vm);
        }
       
    }
}
