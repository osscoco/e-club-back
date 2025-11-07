namespace DomainModels.Enums
{
    public static class ReservationTypeExtensions
    {
        private static readonly Dictionary<ReservationType, string> CodesInFr = new()
        {
            { ReservationType.CASUAL, "detente" },
            { ReservationType.TRAINING, "entrainement" },
            { ReservationType.TOURNAMENT, "tournament" },
            { ReservationType.UNDEFINED, "nonDefini" }
        };

        private static readonly Dictionary<ReservationType, string> NamesInFr = new()
        {
            { ReservationType.CASUAL, "Détente" },
            { ReservationType.TRAINING, "Entraînement" },
            { ReservationType.TOURNAMENT, "Compétition" },
            { ReservationType.UNDEFINED, "Non défini" }
        };

        public static string ToFrenchLabel(this ReservationType t) =>
        NamesInFr.TryGetValue(t, out var v) ? v : "Non défini";
        public static string ToFrenchCode(this ReservationType t) =>
        CodesInFr.TryGetValue(t, out var v) ? v : "Non défini";

        public static bool TryFromCode(string code, out ReservationType type)
        {
            type = CodesInFr.FirstOrDefault(kv =>
                string.Equals(kv.Value, code, StringComparison.OrdinalIgnoreCase)).Key;

            if (type is ReservationType.CASUAL or ReservationType.TRAINING or ReservationType.TOURNAMENT)
                return true;

            type = ReservationType.UNDEFINED;
            return false;
        }
    }
}
