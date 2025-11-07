namespace DomainModels.Enums
{
    public static class ExpenseCategoryExtensions
    {
        private static readonly Dictionary<ExpenseCategory, string> CodesInFr = new()
        {
            { ExpenseCategory.MAINTENANCE, "maintenance" },
            { ExpenseCategory.EQUIPMENT, "equipment" },
            { ExpenseCategory.UTILITIES, "utilities" },
            { ExpenseCategory.SALARIES, "salaries" },
            { ExpenseCategory.COACHING_FEES, "coachingFees" },
            { ExpenseCategory.EXTERNAL_SERVICE, "externalService" },
            { ExpenseCategory.ADMINISTRATION, "administration" },
            { ExpenseCategory.COMMUNICATION, "communication" },
            { ExpenseCategory.EVENT_ORGANIZATION, "eventOrganization" },
            { ExpenseCategory.TOURNAMENT_FEES, "tournamentFees" },
            { ExpenseCategory.BANK_FEES, "bankFees" },
            { ExpenseCategory.TAXES, "taxes" },
            { ExpenseCategory.INSURANCE, "insurance" },
            { ExpenseCategory.REFUND, "refund" },
            { ExpenseCategory.SPONSORSHIP_COST, "sponsorshipCost" },
            { ExpenseCategory.ADVERTISING, "advertising" },
            { ExpenseCategory.UNDEFINED, "nonDefini" }
        };

        private static readonly Dictionary<ExpenseCategory, string> NamesInFr = new()
        {
            { ExpenseCategory.MAINTENANCE, "Entretien des courts" },
            { ExpenseCategory.EQUIPMENT, "Achat de matériel" },
            { ExpenseCategory.UTILITIES, "Eau, électricité ..." },
            { ExpenseCategory.SALARIES, "Salaires / honoraires" },
            { ExpenseCategory.COACHING_FEES, "Paiement des entraîneurs" },
            { ExpenseCategory.EXTERNAL_SERVICE, "Prestations externes" },
            { ExpenseCategory.ADMINISTRATION, "Frais de logiciels" },
            { ExpenseCategory.COMMUNICATION, "Flyers, affiches, site web" },
            { ExpenseCategory.EVENT_ORGANIZATION, "Organisation d'évènements" },
            { ExpenseCategory.TOURNAMENT_FEES, "Frais de déplacements tournois" },
            { ExpenseCategory.BANK_FEES, "Frais bancaires / commissions" },
            { ExpenseCategory.TAXES, "Taxes, impôts" },
            { ExpenseCategory.INSURANCE, "Assurance du club / membres" },
            { ExpenseCategory.REFUND, "Remboursement" },
            { ExpenseCategory.SPONSORSHIP_COST, "Participation à un partenariat" },
            { ExpenseCategory.ADVERTISING, "Campagnes pub locales / Google Ads" },
            { ExpenseCategory.UNDEFINED, "Non défini" }
        };

        public static string ToFrenchLabel(this ExpenseCategory t) =>
        NamesInFr.TryGetValue(t, out var v) ? v : "Non défini";
        public static string ToFrenchCode(this ExpenseCategory t) =>
        CodesInFr.TryGetValue(t, out var v) ? v : "Non défini";

        public static bool TryFromCode(string code, out ExpenseCategory type)
        {
            type = CodesInFr.FirstOrDefault(kv =>
                string.Equals(kv.Value, code, StringComparison.OrdinalIgnoreCase)).Key;

            if (type is ExpenseCategory.MAINTENANCE 
                or ExpenseCategory.EQUIPMENT 
                or ExpenseCategory.UTILITIES 
                or ExpenseCategory.SALARIES
                or ExpenseCategory.COACHING_FEES
                or ExpenseCategory.EXTERNAL_SERVICE
                or ExpenseCategory.ADMINISTRATION
                or ExpenseCategory.COMMUNICATION
                or ExpenseCategory.TOURNAMENT_FEES
                or ExpenseCategory.BANK_FEES
                or ExpenseCategory.TAXES
                or ExpenseCategory.EVENT_ORGANIZATION
                or ExpenseCategory.INSURANCE
                or ExpenseCategory.SPONSORSHIP_COST
                or ExpenseCategory.ADVERTISING)
                return true;

            type = ExpenseCategory.UNDEFINED;
            return false;
        }
    }
}
