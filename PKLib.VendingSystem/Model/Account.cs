using System.Threading;

namespace PKLib.VendingSystem.Model
{
    public class Account
    {
        #region Variables
        private uint _accountNumber;
        private float _balance;
      //  static readonly object _object = new object(); //If required to test this approach, uncomment it.

        #endregion

        #region Properties
        public float Balance
        {
            get { return _balance; }
        }
        #endregion

        /// <summary>
        /// Initialize a new account
        /// </summary>
        /// <param name="accountNum">Account Number</param>
        /// <param name="balance">Opening Balance</param>
        public Account(uint accountNum, float balance)
        {
            _accountNumber = accountNum;
            _balance = balance; 
        }

        #region Methods

        /// <summary>
        /// It withdraws amount from account and sends back updated balance.
        /// The method needs to call from Card. However, for testing, it is made public.
        /// </summary>
        /// <param name="transactionAmount"></param>
        /// <returns></returns>
        public float WithDrawAmount(float transactionAmount)
        {
            try
            {


                //   Monitor.Enter(_object); //If required to test this approach, uncomment it.

                if (_balance >= transactionAmount)
                {
                    ///We can also use Monitor.Enter()/Monitor.Exit in place of Interlocked. However, for the following quick
                    ///operation , Interlocked ensures atomic operation is performed.
                    Interlocked.Exchange(ref _balance, _balance - transactionAmount);
                    return _balance;
                }
            }
            finally
            { //Monitor.Exit(_object); //If required to test this approach, uncomment it.
            }

                return -1; //If account has insufficient balance the following value will be served.
        }

        /// <summary>
        ///It is used for depositing the amount.
        /// </summary>
        /// <param name="transactionAmount"></param>
        /// <returns></returns>
        public float DepositAmount(float transactionAmount)
        {
            Interlocked.Exchange(ref _balance, _balance + transactionAmount);
            return _balance;
        }

        #endregion

    }
}
