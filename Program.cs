using System;
using System.Runtime.Serialization;

namespace Exceptions
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Account a = new Account(1000);
            Account b = new Account(50);

            try
            {
                Terminal.Transfer(b, a, 100);
            }
            catch (NotEnoughMoneyInAccountException ex)
            {
                Console.WriteLine("{0}, {1} < {2}", ex.Message, ex.Balance, ex.Amount);
            }

            Console.ReadKey();
        }
    }

    public static class Terminal
    {
        public static void Transfer(Account from, Account to, decimal amount)
        {
            if (from != null && to != null)
            {
                if (from.Balance < amount)
                {
                    throw new NotEnoughMoneyInAccountException("User not enough money in account to transfer", from.Balance, amount);
                }
                else
                {
                    from.Balance -= amount;
                    to.Balance += amount;
                }
            }
        }
    }

    public class Account
    {
        private decimal _balance;
        public decimal Balance
        {
            get
            {
                return this._balance;
            }
            set
            {
                this._balance = value;
            }
        }
        public Account(decimal balance)
        {
            this.Balance = balance;
        }
    }

    [Serializable]
    public class NotEnoughMoneyInAccountException : Exception
    {
        private decimal _amount;
        private decimal _balance;
        public decimal Amount
        {
            get
            {
                return this._amount;
            }
        }
        public decimal Balance
        {
            get
            {
                return this._balance;
            }
        }
        public NotEnoughMoneyInAccountException(decimal amount, decimal balance)
        {
            this.Amount = amount;
            this.Balance = balance;
        }
        public NotEnoughMoneyInAccountException(string message, decimal amount, decimal balance) : base(message)
        {
            this.Amount = amount;
            this.Balance = balance;
        }
        public NotEnoughMoneyInAccountException(string message, decimal amount, decimal balance, Exception inner) : base(message, inner)
        {
            this.Amount = amount;
            this.Balance = balance;
        }
        protected NotEnoughMoneyInAccountException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            if (info != null)
            {
                this.Amount = info.GetDecimal("Amount");
                this.Balance = info.GetDecimal("Balance");
            }
        }
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Amount", this.Amount);
            info.AddValue("Balance", this.Balance);
        }
    }
}
