using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimchFund.Data;
using SimchaFund.Web.Models;

namespace SimchaFund.Web.Controllers
{
    public class Simchah : Controller
    {
        private string _connectionString = @"Data Source=.\sqlexpress;Initial Catalog=SimchaFund;Integrated Security=true;";

        public IActionResult New(Simcha simcha)
        {
            var manager = new Manager(_connectionString);
            manager.NewSimcha(simcha);
            return Redirect("/simchah/Simchas");
        }
        public IActionResult Simchas()
        {
            var manager = new Manager(_connectionString);
            var list = manager.GetSimchas();
            var count = manager.GetContributors();
            string message = (string)TempData["Message"];

            var vm=new ViewModel
            {
                Simcha = list,
                Contributor = count
            };
            if (!String.IsNullOrEmpty(message))
            {
                vm.Message = message;
            }
            return View(vm);
        }
        public IActionResult contributors(int simchaId)
        {
            var manager = new Manager(_connectionString);
            var list = manager.GetContributors();
            foreach (var c in list)
            {
                c.Balance = manager.GetBalance(c.ID);
            }
            string message = (string)TempData["Message"];
            var name = manager.GetSimchaName(simchaId);
            var total = manager.Total();
            var vm = new ViewModel
            {
                Contributor = list,
                Name = name,
                Id = simchaId,
                Total=total
            };
                if (!String.IsNullOrEmpty(message))
            {
                vm.Message = message;
            }
            return View(vm);


      
        }
        public IActionResult Contributions(int simchaId)
        {
            var db = new Manager(_connectionString);
            var contributors = db.GetContributors();
            foreach (var con in contributors)
            {
                con.Balance = db.GetBalance(con.ID);
                con.Amount = db.GetAmountForSimcha(simchaId, con.ID);
                if (con.Amount == 0)
                {
                    con.Amount = 5;
                    con.Include = false;
                }
                else
                {
                    con.Include = true;
                }
            }

            return View(new ViewModel
            {
                Contributor = contributors,
                Id = simchaId,
                Name = db.GetSimchaName(simchaId)
            });
        }

    }
}
