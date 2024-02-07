using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DuelDto
{
    public class DuelUpdateRequestDto
    {
        public Guid DuelId { get; set; }

        public int UserOneWins { get; set; }

        public int UserOneDefeats { get; set; }

        public bool isCompleted { get; set; }
        public bool isChallenged { get; set; }

    }
}
