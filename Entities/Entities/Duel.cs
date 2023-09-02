using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Duel
    {

        public Guid DuelId { get; set; }

        public string DuelName { get; set; }

        public User UserOne { get; set; }

        public User UserTwo { get; set; }




        //Date to be taken

        //Actual date taken
    }
}
