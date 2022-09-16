using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimchFund.Data;

namespace SimchaFund.Web.Models
{
    public class ViewModel
    {
        public List<History> History { get; set; }
        public List<Simcha> Simcha { get; set; }
        public List<Contributor> Contributor { get; set; }
        public List<Contribution> Contribution { get; set; }
        public List<Deposits> Deposits { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public decimal Balance { get; set; }
        public decimal Total { get; set; }
        public bool Action { get; set; }
        public int Id { get; set; }
    }
}
