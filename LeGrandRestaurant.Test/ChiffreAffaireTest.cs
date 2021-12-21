using System.Collections.Generic;
using System.Linq;
using LeGrandRestaurant.Test.Utilities;
using Xunit;

namespace LeGrandRestaurant.Test
{
    public class ChiffreAffaireTest
    {
        [Fact(DisplayName = "Étant donné un nouveau serveur " +
                            "Quand je calcule son chiffre d'affaire " +
                            "Alors il est de zéro")]
        public void Serveur_Initial()
        {
            // Étant donné un nouveau serveur
            var serveur = new ServeurBuilder().Build();

            // Quand je calcule son chiffre d'affaire
            var chiffreAffaires = serveur.ChiffreAffaires;

            // Alors il est de zéro
            Assert.Equal(0, chiffreAffaires);
        }

        [Fact(DisplayName = "Étant donné un nouveau serveur " +
                            "Quand il prend une commande " +
                            "Alors son chiffre d'affaires est le montant de cette commande")]
        public void Serveur_Commande()
        {
            // Étant donné un nouveau serveur
            var serveur = new ServeurBuilder().Build();

            // Quand il prend une commande
            var montantCommande = new decimal(67.8);
            serveur.PrendreCommande(montantCommande);

            // Alors son chiffre d'affaires est le montant de cette commande
            var chiffreAffaires = serveur.ChiffreAffaires;
            Assert.Equal(montantCommande, chiffreAffaires);
        }

        [Fact(DisplayName = "Étant donné un serveur ayant déjà une commande " +
                            "Quand il prend une autre commande " +
                            "Alors son chiffre d'affaires est l'addition des deux")]
        public void Serveur_2_Commandes()
        {
            // Étant donné un serveur ayant déjà une commande
            var serveur = new ServeurBuilder().Build();
            var montantPremièreCommande = new decimal(67.8);
            serveur.PrendreCommande(montantPremièreCommande);

            // Quand il prend une autre commande
            var montantSecondeCommande = new decimal(178);
            serveur.PrendreCommande(montantSecondeCommande);

            // Alors son chiffre d'affaires est l'addition des deux
            Assert.Equal(
                montantPremièreCommande + montantSecondeCommande,
                serveur.ChiffreAffaires
            );
        }

        [Fact(DisplayName = "Étant donné un restaurant ayant 2 serveurs " +
                            "Quand chacun prend une commande " +
                            "Alors le chiffre d'affaire du restaurant est la somme des deux")]
        public void Restaurant2Serveurs()
        {
            // Étant donné un restaurant ayant 2 serveurs
            var serveurs = new ServeurGenerator().Generate(2).ToArray();
            var restaurant = new Restaurant(serveurs);

            // Quand chacun prend une commande
            var montantCommande = new decimal(67.8);
            foreach (var serveur in serveurs)
                serveur.PrendreCommande(montantCommande);

            // Alors le chiffre d'affaire du restaurant est la somme des deux
            Assert.Equal(
                montantCommande * 2,
                restaurant.ChiffreAffaires
            );
        }

        [Theory(DisplayName = "Étant donné un restaurant ayant X serveurs " +
                            "Quand chacun prend une commande " +
                            "Alors le chiffre d'affaire du restaurant est le produit de " +
                            "X par le montant de la commande")]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(10000)]
        public void RestaurantXServeurs(int nombreServeurs)
        {
            // Étant donné un restaurant ayant X serveurs
            var serveurs = new ServeurGenerator().Generate(nombreServeurs).ToArray();
            var restaurant = new Restaurant(serveurs);

            // Quand chacun prend une commande
            var montantCommande = new decimal(67.8);

            foreach (var serveur in serveurs)
                serveur.PrendreCommande(montantCommande);

            // Alors le chiffre d'affaire du restaurant est
            // le produit du montant par le nombre de serveurs
            Assert.Equal(
                montantCommande * nombreServeurs,
                restaurant.ChiffreAffaires
            );
        }

        [Fact(DisplayName = "Étant donné une franchise ayant 2 restaurants avec chacun un serveur " +
                            "Quand chaque serveur prend une commande " +
                            "Alors le chiffre d'affaires de la franchise est la somme de ces commandes")]
        public void Franchise()
        {
            // Étant donné une franchise ayant 2 restaurants avec chacun un serveur
            var serveurRestaurant1 = new ServeurBuilder().Build();
            var serveurRestaurant2 = new ServeurBuilder().Build();

            var restaurant1 = new Restaurant(serveurRestaurant1);
            var restaurant2 = new Restaurant(serveurRestaurant2);

            var franchise = new Franchise(restaurant1, restaurant2);

            // Quand chaque serveur prend une commande
            var montantCommande = new decimal(67.8);

            serveurRestaurant1.PrendreCommande(montantCommande);
            serveurRestaurant2.PrendreCommande(montantCommande);

            // Alors le chiffre d'affaires de la franchise est la somme de ces commandes
            Assert.Equal(montantCommande * 2, franchise.ChiffreAffaires);
        }

#if TESTCHARGE
        private const int NombreRestaurantsMax = 10000;
        private const int NombreServeursMaxParRestaurant = 1000;
#else
        private const int NombreRestaurantsMax = 100;
        private const int NombreServeursMaxParRestaurant = 100;
#endif

        [Theory(DisplayName = "Étant donné une franchise ayant X restaurants avec chacun Y serveurs " +
                            "Quand chaque serveur prend une commande " +
                            "Alors le chiffre d'affaires de la franchise est la somme de ces commandes")]
        [InlineData(0, 0)]
        [InlineData(1, 0)]
        [InlineData(2, 0)]
        [InlineData(NombreRestaurantsMax, 0)]
        [InlineData(0, 1)]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(NombreRestaurantsMax, 1)]
        [InlineData(0, 2)]
        [InlineData(1, 2)]
        [InlineData(2, 2)]
        [InlineData(NombreRestaurantsMax, 2)]
        [InlineData(0, NombreServeursMaxParRestaurant)]
        [InlineData(1, NombreServeursMaxParRestaurant)]
        [InlineData(2, NombreServeursMaxParRestaurant)]
        [InlineData(NombreRestaurantsMax, NombreServeursMaxParRestaurant)]
        public void FranchiseAvecParametres(
            int nombreRestaurants, int nombreServeursParRestaurant)
        {
            // Étant donné une franchise ayant X restaurants avec chacun Y serveurs
            var tousLesServeurs = new List<Serveur>();
            var restaurants = Enumerable
                .Range(0, nombreRestaurants)
                .Select(_ =>
                {
                    var serveurs = Enumerable
                        .Range(0, nombreServeursParRestaurant)
                        .Select(_ => new ServeurBuilder().Build())
                        .ToArray();

                    tousLesServeurs.AddRange(serveurs);

                    return new Restaurant(serveurs);
                }).ToArray();

            var franchise = new Franchise(restaurants);

            // Quand chaque serveur prend une commande
            var montantCommande = new decimal(67.8);

            foreach (var serveur in tousLesServeurs)
                serveur.PrendreCommande(montantCommande);

            // Alors le chiffre d'affaires de la franchise est la somme de ces commandes
            Assert.Equal(
                montantCommande * nombreServeursParRestaurant * nombreRestaurants,
                franchise.ChiffreAffaires
            );
        }
    }
}
