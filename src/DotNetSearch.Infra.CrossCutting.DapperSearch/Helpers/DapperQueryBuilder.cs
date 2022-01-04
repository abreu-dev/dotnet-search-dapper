using DotNetSearch.Infra.CrossCutting.Search.Contratos;
using DotNetSearch.Infra.CrossCutting.Search.Enums;

namespace DotNetSearch.Infra.CrossCutting.DapperSearch.Helpers
{
    public static class DapperQueryBuilder
    {
        public static string BuildQuery<T>(SearchContrato searchContrato)
        {
            string where = "";

            foreach (var filter in searchContrato.Filters)
            {
                string actualCondition = null;

                if (filter.Operation == SearchFilterOperation.Like)
                {
                    actualCondition = Like(filter.PropertyName, filter.PropertyValue);
                }
                else
                {
                    actualCondition = Equal(filter.PropertyName, filter.PropertyValue);
                }

                if (string.IsNullOrEmpty(where))
                {
                    where = $"{actualCondition}";
                }
                else
                {
                    where = where.Or(actualCondition);
                }
            }

            return $"SELECT * FROM {typeof(T).Name} WHERE {where} " +
                $"ORDER BY Id " +
                $"OFFSET {searchContrato.Page * searchContrato.PageSize} ROWS " +
                $"FETCH NEXT {searchContrato.PageSize} ROWS ONLY";
        }

        public static string Equal(string propertyName, string propertyValue)
        {
            return $"{propertyName} = '{propertyValue}'";
        }

        public static string Like(string propertyName, string propertyValue)
        {
            return $"{propertyName} LIKE '%{propertyValue}%'";
        }

        public static string And(this string leftQuery, string rightQuery)
        {
            return $"{leftQuery} AND {rightQuery}";
        }

        public static string Or(this string leftQuery, string rightQuery)
        {
            return $"{leftQuery} OR {rightQuery}";
        }
    }
}
