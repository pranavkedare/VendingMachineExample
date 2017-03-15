using System.Threading;

namespace PKLib.VendingSystem.Model
{
    public class Account : IAccount
    {
        #region Variables
        private uint _accountNumber;
        private float _balance;
        //  static readonly object _object = new object(); //If required to test this approach, uncomment it.
        private static int usingTransactionSrc = 0;
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
        /// <param name="initBalance">Opening Balance</param>
        public Account(uint accountNum, float initBalance)
        {
            _accountNumber = accountNum;
            _balance = initBalance; 
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
                //We can also use SpinLock but operations within the following methods are non blocking.
                //So Interlocked is chosen.
                if (Interlocked.Exchange(ref usingTransactionSrc, 1) == 0)
                {
                    if (_balance >= transactionAmount)
                    {
                        _balance -= transactionAmount;

                        ///We can also use Monitor.Enter()/Monitor.Exit in place of Interlocked. However, for the following quick
                        ///operation , Interlocked ensures atomic operation is performed.
                        // Interlocked.Exchange(ref _balance, _balance - transactionAmount);
                        return _balance;
                    }
                }
            }
            finally
            { //Monitor.Exit(_object); //If required to test this approach, uncomment it.
                Interlocked.Exchange(ref usingTransactionSrc, 0);
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
            try
            {
                if (Interlocked.Exchange(ref usingTransactionSrc, 1) == 0)
                {
                    _balance += transactionAmount;
                }
                    return _balance;
            }
            finally
            {
                Interlocked.Exchange(ref usingTransactionSrc, 0);
            }
        }

        #endregion

    }
}
