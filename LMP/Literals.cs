using System;

namespace LMP
{
    public class Literals
    {
        protected Literals()
        {
        }

        public const string SurveysViewTitle = "Encuestas";

        public const string SurveyDetailsViewTitle = "Nueva Encuesta";

        public const string FavoriteTeamTitle = "Selección de equipo";

        public const string OK = "Aceptar";

        public static DateTime DefaultDate { get; } = new DateTime(1900, 1, 1);
    }
}
