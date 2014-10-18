using System;
using System.Collections.Generic;

namespace Journeys.Support.Transactions
{
    public sealed class Transaction
    {
        private readonly HashSet<ITransactional> _transactables = new HashSet<ITransactional>();

        public TObject Register<TObject>(IProvideTransactional<TObject> @object)
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
