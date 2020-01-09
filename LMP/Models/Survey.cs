﻿using System;

namespace LMP.Models
{
    public class Survey
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime Birthdate { get; set; }

        public string FavoriteTeam { get; set; }

        public double Lat { get; set; }

        public double Lon { get; set; }

        public override string ToString()
        {
            return $"{Id} {Name} | {Birthdate} | {FavoriteTeam} | {Lat} | {Lon}";
        }
    }
}
