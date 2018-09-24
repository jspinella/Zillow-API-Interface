using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using System.Xml.Serialization;
using Zillow.Models;

namespace Zillow
{
    /// <summary>
    /// Methods for interacting with the Zillow API
    /// </summary>
    public class Zillow
    {
        public ZestimateResult GetZestimate(uint zpid, bool rentzestimate = false)
        {
            var result = new ZestimateResult();
            var url = Constants.baseUrl + 
                Constants.getZestimate + 
                "?zws-id=" + Constants.zwsId + "&zpid=" + zpid;

            if (rentzestimate)
                url = url + "&rentzestimate=true";

            var request = WebRequest.Create(url);
            request.Headers["UserAgent"] = Constants.userAgent;
            request.Method = "GET";

            try
            {
                var response = request.GetResponse();
                var xml = XDocument.Load(response.GetResponseStream());
                var valuationRange = from v in xml.Descendants("valuationRange") select v;
                result.Low = Convert.ToInt32((from l in valuationRange.Descendants("low") select l.Value).First());
                result.High = Convert.ToInt32((from h in valuationRange.Descendants("high") select h.Value).First());

                //TODO: rentZestimate (the second valuationRange descendant, where zestimate is the first ^^^)

            }
            catch (InvalidOperationException)
            {
                //there is something awry with the XML data
                return null;
            }

            return result;
        }
    }
}
