using System.Collections.Generic;

namespace LeGrandRestaurant.Test.Utilities
{
    internal class ServeurGenerator
    {
        private ServeurBuilder _builder = new ();

        public ServeurGenerator Nommés(string nom)
        {
            _builder = _builder.Nommé(nom);
            return this;
        }

        public IEnumerable<Serveur> Generate(int howMany)
        {
            for (var i = 0; i < howMany; i++)
            {
                yield return _builder.Build();
            }
        }
    }
}
