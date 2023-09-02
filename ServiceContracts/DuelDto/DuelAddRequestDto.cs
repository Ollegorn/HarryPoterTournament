using Entities.Entities;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DuelDto
{
    public class DuelAddRequestDto
    {
        public string DuelName { get; set; }

        public string UserOneUsername { get; set; }

        public string UserTwoUsername { get; set; }


        public Duel ToDuel()
        {
            return new Duel
            {
                DuelName = DuelName
            };
        }

    }

    
}
