using LeGrandRestaurant.Test.Utilities;
using Xunit;

namespace LeGrandRestaurant.Test
{
    public class RapportsFinanciersTest
    {

        [Fact(DisplayName = "Étant donné un nouveau serveur, alors son chiffre d'affaire est nul")]
        public void Serveur_Initial()
        {
            var serveur = new ServeurBuilder().Build();

            var chiffreAffaires = serveur.ChiffreAffaires;

            Assert.Equal(0, chiffreAffaires);
        }

        [Fact(DisplayName ="Étant donné un nouveau serveur, " +
                           "Quand un client passe une commande, " +
                           "Alors son chiffre d'affaire est augmenté de son montant.")]
        public void Serveur_Increment()
        {
            decimal montantCommande = new decimal(64.5);
            var serveur = new ServeurBuilder().Build();
            var client = new Client();

            serveur.PrendreCommande(montantCommande);

            var chiffreAffaires = serveur.ChiffreAffaires;
            Assert.Equal(montantCommande, chiffreAffaires);
        }

        [Fact(DisplayName = "Étant donné un serveur ayant déjà une commande, " +
                            "Quand un client passe une autre commande, " +
                            "Alors son chiffre d'affaire est augmenté de son montant.")]
        public void Serveur_Increment_With_Existing()
        {
            // Given
            decimal montantPremièreCommande = new decimal(64.5);
            decimal montantSecondeCommande = new decimal(106.7);
            
            var serveur = new ServeurBuilder().Build();
            var client = new Client();

            serveur.PrendreCommande(montantPremièreCommande);

            // When
            serveur.PrendreCommande(montantSecondeCommande);

            // Then
            var chiffreAffaires = serveur.ChiffreAffaires;
            Assert.Equal(montantPremièreCommande + montantSecondeCommande, chiffreAffaires);
        }
    }
}
