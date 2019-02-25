using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class LogMetadata
    {
        public string Message { get; set; }
        public string ActionMethod { get; set; }
        public string Exception { get; set; }
        public bool HasException { get; set; }
        public string RequestContentType { get; set; }
        public string RequestUri { get; set; }
        public string RequestMethod { get; set; }
        public DateTime? RequestTimestamp { get; set; }
        public string ResponseContentType { get; set; }
        public HttpStatusCode ResponseStatusCode { get; set; }
        public DateTime? ResponseTimestamp { get; set; }
    }
}
