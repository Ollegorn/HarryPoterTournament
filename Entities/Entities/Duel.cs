﻿namespace Entities.Entities
{
    public class Duel
    {

        public Guid DuelId { get; set; }

        public string DuelName { get; set; }

        public User UserOne { get; set; }

        public User UserTwo { get; set; }

        public Guid TournamentId { get; set; }

        public int DuelWins { get; set; }

        public int DuelDefeats { get; set; }

        public bool isCompleted { get; set; } = false;

        public bool isChallenged { get; set; } = false;
        public Tournament Tournament { get; set; }


        //Date to be taken

        //Actual date taken
    }
}
