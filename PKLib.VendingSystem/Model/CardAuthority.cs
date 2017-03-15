using System.Collections.Concurrent;

namespace PKLib.VendingSystem.Model
{
    /// <summary>
    /// Implementation of Card Authority which stores card data.
    /// </summary>
    public  class CardAuthority : ICardAuthority
    {
        //thread - safe key pair collection.
        private static readonly ConcurrentDictionary<string, ICard> _registerdCards=new 
            ConcurrentDictionary<string, ICard>();
        


        public  ICard FindCard(string cardNumber)
        {
            //In real world, it finds card in secured environment.
            ICard card;
            _registerdCards.TryGetValue(cardNumber, out card);
            return card;
        }

        public void RegisterCard(string cardNumber)
        {
            _registerdCards.TryAdd(cardNumber, new CashCard(cardNumber,4321));
        }
    }
}
