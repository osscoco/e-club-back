namespace DomainModels.Enums
{
    public static class ReservationStatusExtensions
    {
        private static readonly Dictionary<ReservationStatus, string> CodesInFr = new()
        {
            { ReservationStatus.PENDING, "attente" },
            { ReservationStatus.CONFIRMED, "valide" },
            { ReservationStatus.CANCELLED, "annule" },
            { ReservationStatus.COMPLETED, "complete" },
            { ReservationStatus.UNDEFINED, "nonDefini" }
        };

        private static readonly Dictionary<ReservationStatus, string> NamesInFr = new()
        {
            { ReservationStatus.PENDING, "Attente" },
            { ReservationStatus.CONFIRMED, "Validée" },
            { ReservationStatus.CANCELLED, "Annulée" },
            { ReservationStatus.COMPLETED, "Complétée" },
            { ReservationStatus.UNDEFINED, "Non défini" }
        };

        public static string ToFrenchLabel(this ReservationStatus t) =>
        NamesInFr.TryGetValue(t, out var v) ? v : "Non défini";
        public static string ToFrenchCode(this ReservationStatus t) =>
        CodesInFr.TryGetValue(t, out var v) ? v : "Non défini";

        public static bool TryFromCode(string code, out ReservationStatus type)
        {
            type = CodesInFr.FirstOrDefault(kv =>
                string.Equals(kv.Value, code, StringComparison.OrdinalIgnoreCase)).Key;

            if (type is ReservationStatus.PENDING or ReservationStatus.CONFIRMED or ReservationStatus.CANCELLED or ReservationStatus.COMPLETED)
                return true;

            type = ReservationStatus.UNDEFINED;
            return false;
        }
    }
}
