using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.ResponseHandler
{
    [DataContract]
    public class ApiResponse<T>
    {
        public ApiResponse(int statusCode, T result, string errorMessage = null)
        {
            StatusCode = statusCode;
            Result = result;
            ErrorMessage = errorMessage;
        }

        public ApiResponse()
        {

        }

        [DataMember]
        public string Version { get { return "1.0"; } }

        [DataMember]
        public int StatusCode { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string ErrorMessage { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public T Result { get; set; }
    }
}
