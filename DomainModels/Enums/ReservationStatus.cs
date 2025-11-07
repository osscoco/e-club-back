namespace DomainModels.Enums
{
    public enum ReservationStatus
    {
        PENDING = 1,      // Attente de validation   
        CONFIRMED = 2,    // Validée   
        CANCELLED = 3,    // Annulée  
        COMPLETED = 4,    // Terminée  
        UNDEFINED = 5     // Non défini 
    }
}
