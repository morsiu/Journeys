using System;
using System.Collections.Generic;
using System.Linq;

namespace Journeys.Transactions
{
    public class Transaction
    {
        private HashSet<ITransactable> _transactables = new HashSet<ITransactable>();

        public TObject Add<TObject>(IProvideTransacted<TObject> @object)
        {
            var transactedObject = @object.Lift();
            _transactables.Add(transactedObject);
            return transactedObject.Object;
        }

        public void Run(Action action)
        {
            try
            {
                action();
            }
            catch
            {
                foreach (var transactable in _transactables)
                {
                    transactable.Abort();
                }
                throw;
            }
            foreach (var transactable in _transactables)
            {
                transactable.Commit();
            }
        }
    }
}
