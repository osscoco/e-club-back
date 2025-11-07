using E_Club.Application.Interfaces.Common;

namespace E_Club.Services.Common.Message
{
    public class MessageToReturn : IMessageToReturn
    {
        public string MessageSuccess(string model, string operation)
        {
            string caracterFeminine = "";

            // Si model = expense (une dépense) || reservation (une réservation)
            if (!(model == "club" || model == "court" || model == "income" || model == "training" || model == "user"))
            {
                // On passe le return au féminin
                caracterFeminine = caracterFeminine + "e";
            }

            // Liste d'opérations
            if (operation == "get")
            {
                return "Liste (" + model + ") récupérée avec succès !";
            }
            else if (operation == "getById")
            {
                return model + " récupéré" + caracterFeminine + " avec succès !";
            }
            else if (operation == "create")
            {
                return model + " créé" + caracterFeminine + " avec succès !";
            }
            else if (operation == "update")
            {
                return model + " modifié" + caracterFeminine + " avec succès !";
            }
            else if (operation == "delete")
            {
                return model + " supprimé" + caracterFeminine + " avec succès !";
            }
            else if (operation == "auth")
            {
                return "Connexion réussie !";
            }
            else
            {
                return "Model / Type d'action non défini ...";
            }
        }

        public string MessageError(string model, string operation)
        {
            // Liste d'opérations
            if (operation == "get")
            {
                return "Liste (" + model + ") vide ...";
            }
            else if (operation == "getById")
            {
                return model + " introuvable ...";
            }
            else if (operation == "create")
            {
                return "Impossible de créer : " + model + " ...";
            }
            else if (operation == "update")
            {
                return "Impossible de modifier : " + model + " ...";
            }
            else if (operation == "delete")
            {
                return "Impossible de supprimer : " + model + " ...";
            }
            else if (operation == "auth")
            {
                return "Connexion impossible ...";
            }
            else
            {
                return "Model / Type d'action non défini ...";
            }
        }
    }
}