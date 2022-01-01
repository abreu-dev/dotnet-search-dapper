namespace DotNetSearch.Domain.Common
{
    public static class DomainMessages
    {
        public static DomainMessage RequiredField => new("Please, ensure you enter {0}.");
        public static DomainMessage NotFound => new("The informed {0} was not found.");
    }
}
