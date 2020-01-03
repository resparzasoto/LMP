using System;

namespace LMP
{
    public class Literals
    {
        protected Literals()
        {
        }

        public const string FavoriteTeamTitle = "Selección de equipo";

        public static DateTime DefaultDate { get; } = new DateTime(1900, 1, 1);
    }
}
