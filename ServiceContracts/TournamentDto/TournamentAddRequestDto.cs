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
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public bool BalancedMode { get; set; }
        public bool EchoBan { get; set; }
        public bool CardBan { get; set; }
        public bool TwoWinsInThreeGames { get; set; }
        public bool Rewards { get; set; }
        public string Format { get; set; }
        public string DuelMode { get; set; }


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
                EndDate = EndDate,
                BalancedMode = BalancedMode,
                EchoBan = EchoBan,
                CardBan = CardBan,
                TwoWinsInThreeGames = TwoWinsInThreeGames,
                Rewards = Rewards,
                Format = Format,
                DuelMode = DuelMode,
            };
        }
    }
}
