using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    public class Purchase
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public IList<PurchaseDetails> purchaseDetails { get; set; }

    }
}
