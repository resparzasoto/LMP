using System;

namespace LMP
{
    public class Literals
    {
        protected Literals()
        {
        }

        public const string FavoriteTeamTitle = "Selección de equipo";

        public const string SurveyValidation = "Valor(es) incorrecto(s)";

        public const string OK = "Aceptar";

        public static DateTime DefaultDate { get; } = new DateTime(1900, 1, 1);
    }
}
