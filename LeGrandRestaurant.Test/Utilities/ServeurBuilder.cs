namespace LeGrandRestaurant.Test.Utilities
{
    internal class ServeurBuilder
    {
        private string _nom = string.Empty;

        public ServeurBuilder Nommé(string nom)
        {
            _nom = nom;
            return this;
        }

        public Serveur Build()
        {
            return new Serveur(_nom, 0m);
        }
    }
}
