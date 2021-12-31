using System.Collections.Generic;

namespace DotNetSearch.API.Common
{
    public class UnprocessableEntityResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
