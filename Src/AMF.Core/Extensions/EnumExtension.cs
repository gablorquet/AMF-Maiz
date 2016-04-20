using AMF.Core.Enums;

namespace AMF.Core.Extensions
{
    public static class EnumExtension
    {
        public static string AsDisplayable(this Language lng)
        {
            switch (lng)
            {
                case (Language.All):
                    return "Toutes";
                case(Language.Commun):
                    return "Commun";
                case(Language.Draconique):
                    return "Draconique";
                case(Language.Elfique):
                    return "Elfique";
                case(Language.Goblinoide):
                    return "Goblinoïde";
                case(Language.Profondeur):
                    return "Profondeur";
                default:
                    return string.Empty;
            }
        }

        public static string AsDisplayable(this Ressource ress)
        {
            switch (ress)
            {
                case(Ressource.Any):
                    return "Au choix";
                case(Ressource.Contact):
                    return "Contact";
                case(Ressource.Labor):
                    return "Main d'oeuvre";
                case(Ressource.Material):
                    return "Matériel";
                case(Ressource.Research):
                    return "Recherche";
                default:
                    return string.Empty;
            }
        }
        public static string AsDisplayable(this Reward reward)
        {
            switch (reward)
            {
                case (Reward.Gold):
                    return "Pièces (5)";
                case (Reward.Contact):
                    return "Contact";
                case (Reward.Labor):
                    return "Main d'oeuvre";
                case (Reward.Material):
                    return "Matériel";
                case (Reward.Research):
                    return "Recherche";
                case (Reward.Influence):
                    return "Influence (1 à 5 personnes)";
                default:
                    return string.Empty;
            }
        }
    }
}
