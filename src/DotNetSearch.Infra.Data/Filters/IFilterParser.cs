namespace DotNetSearch.Infra.Data.Filters
{
    public interface IFilterParser
    {
        // Tem algum problema que acontece após versão 1.0.18 no System.Linq.Dynamic.Core em relação a nomes de métodos
        // O nome And/Or/Eq ele não reconhece resultando no erro 'Identifier Expected'

        string And(string leftCondition, string rightCondition);
        string Or(string leftCondition, string rightCondition);
        string Eq(string propertyPath, string value);
        string Ne(string propertyPath, string value);
        string Like(string propertyPath, string value);
    }
}
