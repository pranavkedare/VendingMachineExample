using System;

namespace PKLib.VendingSystem.Model
{
    public class CashCard
    {
        private uint _pin;
        private string _cardNum;
        private Account _cardAccount;

        #region Properties
        public string CardProtectedNumber => _cardNum.Substring(12);
        #endregion
        /// <summary>
        /// Initializes the card. If the card is linked card, it uses same Account object.
        /// </summary>
        /// <param name="cardNum"></param>
        /// <param name="pin"></param>
        /// <param name="cardAccount"></param>
        public CashCard(string cardNum, uint pin, Account cardAccount)
        {
            _pin = pin;
            _cardNum = cardNum;
            _cardAccount = cardAccount;
        }

        public bool ValidateCard(uint pin)
        {
            return pin == _pin;
        }

        public bool DebitAmount(float transactionAmount)
        {
            return _cardAccount.WithDrawAmount(transactionAmount) != -1; //sufficient cash condition
        }
    }
}
