using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.TournamentDto
{
    public class TournamentAddRequestDto
    {
        public string TournamentName { get; set; }

        public List<string> Rules { get; set; }

        public string Prize { get; set; }

        public int ImageNumber { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }


        public Tournament ToTournament()
        {
            return new Tournament
            {
                TournamentName = TournamentName,
                Rules = Rules,
                Prize = Prize,
                ImageNumber = ImageNumber,
                Description = Description,
                StartDate = StartDate,
                EndDate = EndDate
            };
        }
    }
}
