namespace DomainModels.Enums
{
    public static class IncomeCategoryExtensions
    {
        private static readonly Dictionary<IncomeCategory, string> CodesInFr = new()
        {
            { IncomeCategory.MEMBERSHIP, "cotisationAnnuelle" },
            { IncomeCategory.TRAINING, "inscriptionCours" },
            { IncomeCategory.TOURNAMENT, "inscriptionCompetition" },
            { IncomeCategory.SHOP, "venteArticle" },
            { IncomeCategory.SPONSORSHIP, "sponsor" },
            { IncomeCategory.EVENT, "evenement" },
            { IncomeCategory.UNDEFINED, "nonDefini" }
        };

        private static readonly Dictionary<IncomeCategory, string> NamesInFr = new()
        {
            { IncomeCategory.MEMBERSHIP, "Cotisation Annuelle" },
            { IncomeCategory.TRAINING, "Inscription Cours" },
            { IncomeCategory.TOURNAMENT, "Inscription Compétition" },
            { IncomeCategory.SHOP, "Vente Article" },
            { IncomeCategory.SPONSORSHIP, "Sponsor" },
            { IncomeCategory.EVENT, "Evènement" },
            { IncomeCategory.UNDEFINED, "Non défini" }
        };

        public static string ToFrenchLabel(this IncomeCategory t) =>
        NamesInFr.TryGetValue(t, out var v) ? v : "Non défini";
        public static string ToFrenchCode(this IncomeCategory t) =>
        CodesInFr.TryGetValue(t, out var v) ? v : "Non défini";

        public static bool TryFromCode(string code, out IncomeCategory type)
        {
            type = CodesInFr.FirstOrDefault(kv =>
                string.Equals(kv.Value, code, StringComparison.OrdinalIgnoreCase)).Key;

            if (type is IncomeCategory.MEMBERSHIP or IncomeCategory.TRAINING or IncomeCategory.TOURNAMENT or IncomeCategory.SHOP or IncomeCategory.SPONSORSHIP or IncomeCategory.EVENT)
                return true;

            type = IncomeCategory.UNDEFINED;
            return false;
        }
    }
}
