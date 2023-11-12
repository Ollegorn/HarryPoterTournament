﻿using Entities.Entities;
using ServiceContracts.UserDto;

namespace ServiceContracts.DuelDto
{
    public class DuelResponseDto
    {
        public Guid DuelId { get; set; }

        public string DuelName { get; set; }

        public UserResponseDto UserOne { get; set; }

        public UserResponseDto UserTwo { get; set; }


    }
    public static class DuelExtentions
    {
        public static DuelResponseDto ToDuelResponseDto(this Duel duel)
        {
            var dto = new DuelResponseDto
            {
                DuelId = duel.DuelId,
                DuelName = duel.DuelName,

            };

            if (duel.UserOne != null)
            {
                dto.UserOne = new UserResponseDto { UserName = duel.UserOne.UserName, Wins = duel.UserOne.Wins, Defeats = duel.UserOne.Defeats };
            }

            if (duel.UserTwo != null)
            {
                dto.UserTwo = new UserResponseDto { UserName = duel.UserTwo.UserName, Wins = duel.UserTwo.Wins, Defeats = duel.UserTwo.Defeats };
            }

            return dto;
        }

        public static List<DuelResponseDto> ToDuelResponseDtoList(this List<Duel> duels)
        {
            var duelResponse = new List<DuelResponseDto>();
            foreach (var duel in  duels)
            {
                duelResponse.Add(duel.ToDuelResponseDto());
            }
            return duelResponse;
        }
    }
}