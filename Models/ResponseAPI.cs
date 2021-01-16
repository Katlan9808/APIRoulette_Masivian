using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class ResponseAPI<T> where T : class
    {
        public string iResponseCode { get; set; }
        public string strResponseMessage { get; set; }
        public IEnumerable<T> lstResponseList { get; set; }
        public T objResponseObject { get; set; }
        public bool bResponse { get; set; }

        public ResponseAPI() { }

        public ResponseAPI(string iResponseCode, string strResponseMessage, IEnumerable<T> lstResponseList)
        {
            this.iResponseCode = iResponseCode;
            this.strResponseMessage = strResponseMessage;
            this.lstResponseList = lstResponseList;
            bResponse = true;

        }

        public ResponseAPI(string iResponseCode, string strResponseMessage)
        {
            this.iResponseCode = iResponseCode;
            this.strResponseMessage = strResponseMessage;
        }

        public ResponseAPI(string iResponseCode, string strResponseMessage, bool bResponse)
        {
            this.iResponseCode = iResponseCode;
            this.strResponseMessage = strResponseMessage;
            this.bResponse = bResponse;
        }

        public ResponseAPI(string iResponseCode, string strResponseMessage, T objResponseObject)
        {
            this.iResponseCode = iResponseCode;
            this.strResponseMessage = strResponseMessage;
            this.objResponseObject = objResponseObject;
            bResponse = true;

        }
        public ResponseAPI(string iResponseCode, string strResponseMessage, IEnumerable<T> lstResponseList, T objResponseObject)
        {
            this.iResponseCode = iResponseCode;
            this.strResponseMessage = strResponseMessage;
            this.lstResponseList = lstResponseList;
            this.objResponseObject = objResponseObject;
            bResponse = true;
        }
    }
}
