using System;
using Journeys.Data.Queries;

namespace Journeys.Hosting.Adapters.Dispatching
{
    internal sealed class QueryKey
    {
        private readonly Type _queryType;

        public QueryKey(Type queryType)
        {
            _queryType = queryType;
        }

        public static QueryKey From<TResult>(IQuery<TResult> query)
        {
            var queryType = query.GetType();
            return new QueryKey(queryType);
        }

        public static QueryKey From<TQuery, TResult>()
            where TQuery : IQuery<TResult>
        {
            var queryType = typeof(TQuery);
            return new QueryKey(queryType);
        }

        public override bool Equals(object obj)
        {
            return !ReferenceEquals(obj, null)
                && obj is QueryKey
                && Equals((QueryKey)obj);
        }

        public override int GetHashCode()
        {
            return _queryType.GetHashCode();
        }

        public override string ToString()
        {
            return _queryType.ToString();
        }

        private bool Equals(QueryKey other)
        {
            return other._queryType.Equals(_queryType);
        }
    }
}
