using PKLib.VendingSystem.Exceptions;

namespace PKLib.VendingSystem.Model
{
    public class CashCard : ICard
    {
        private uint _pin;
        private string _cardNum;
        private IAccount _cardAccount;

        #region Properties
        public string CardProtectedNumber => _cardNum.Substring(12);
        #endregion
        /// <summary>
        /// Initializes the card. If the card is linked card, it uses same Account object.
        /// </summary>
        /// <param name="cardNum"></param>
        /// <param name="pin"></param>
        /// <param name="cardAccount"></param>
        public CashCard(string cardNum, uint defaultPin, Account cardAccount):this(cardNum,defaultPin)
        {
            _cardAccount = cardAccount;
        }

        public CashCard(string cardNum, uint defaultPin)
        {
            _pin = defaultPin;
            _cardNum = cardNum;
        }

        public bool ValidatePin(uint pin)
        {
            if (pin != _pin)
                throw new InvalidPinException("Input pin is invalid!!");
            else
                return true;
        }

        public bool DebitAmount(float transactionAmount)
        {
            return _cardAccount.WithDrawAmount(transactionAmount) != -1; //sufficient cash condition
        }

        public void SetPin(uint pin)
        {
            _pin = pin;
        }

        public void LinkAccount(IAccount account)
        {
            _cardAccount = account;
        }

      
    }
}
