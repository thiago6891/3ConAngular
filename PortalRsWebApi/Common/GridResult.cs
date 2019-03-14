using System.Collections.Generic;

namespace PortalRSApi.Common
{
    public class GridResult<T>
    {
        public int Cont { get; set; }

        public List<T> Items { get; set; }
    }
}