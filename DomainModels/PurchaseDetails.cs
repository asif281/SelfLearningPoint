using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    public class PurchaseDetails
    {   
        int Id { get; set; }
      
        [ForeignKey("Purchase")]
        public int PurchaseId { get;}
        [Required]
        public string ProductName { get; set; }

    }
}
