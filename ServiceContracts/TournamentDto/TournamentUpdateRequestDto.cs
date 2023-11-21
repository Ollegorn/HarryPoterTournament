using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.TournamentDto
{
    public class TournamentUpdateRequestDto
    {
        public string TournamentName { get; set; }

        public Guid TournamentId { get; set; }

        public string Rules { get; set; }

        public string Prize { get; set; }


    }
}
