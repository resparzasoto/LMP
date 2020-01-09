using System;

namespace LMP
{
    public class Literals
    {
        protected Literals()
        {
        }

        public const string FavoriteTeamTitle = "Selección de equipo";

        public const string OK = "Aceptar";

        public const string DeleteSurveyTitle = "Borrar";

        public const string DeleteSurveyConfirmation = "¿Está seguro(a)?";

        public const string Cancel = "Cancelar";

        public static DateTime DefaultDate { get; } = new DateTime(1900, 1, 1);
    }
}
