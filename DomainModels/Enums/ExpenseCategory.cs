namespace DomainModels.Enums
{
    public enum ExpenseCategory
    {
        // Infrastructure et maintenance
        MAINTENANCE = 1,         // Entretien des courts
        EQUIPMENT = 2,           // Achat de matériel
        UTILITIES = 3,           // Eau, électricité ...

        // Ressources humaines et prestations
        SALARIES = 4,            // Salaires / honoraires
        COACHING_FEES = 5,       // Paiement des entraîneurs
        EXTERNAL_SERVICE = 6,    // Prestations externes

        // Gestion du club
        ADMINISTRATION = 7,      // Frais de logiciels
        COMMUNICATION = 8,       // Flyers, affiches, site web
        EVENT_ORGANIZATION = 9,  // Organisation d'évènements
        TOURNAMENT_FEES = 10,    // Frais de déplacements

        // Finances et divers
        BANK_FEES = 11,          // Frais bancaires / commissions
        TAXES = 12,              // Taxes, impôts
        INSURANCE = 13,          // Assurance du club / membres
        REFUND = 14,             // Remboursement

        // Marketing / partenariats
        SPONSORSHIP_COST = 15,   // Participation à un partenariat
        ADVERTISING = 16,        // Campagnes pub locales / Google Ads

        UNDEFINED = 17           // Non défini
    }
}
