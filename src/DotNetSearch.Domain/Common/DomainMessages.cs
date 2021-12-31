namespace DotNetSearch.Domain.Common
{
    public static class DomainMessages
    {
        public static DomainMessage RequiredField => new("Please, ensure you enter {0}.");
    }
}
