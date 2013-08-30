using System;
using Journeys.Queries;

namespace Journeys.Adapters
{
    public class QueryKey
    {
        private readonly Type _queryType;

        private QueryKey(Type queryType)
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

        private bool Equals(QueryKey other)
        {
            return other._queryType.Equals(_queryType);
        }
    }
}
