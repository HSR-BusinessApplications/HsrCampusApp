using System;
using System.Net;
using Cirrious.CrossCore;
using Hsr.Campus.Core.DomainServices;
using System.Net.Http;

namespace Hsr.Campus.iOS
{
	public class FacebookWebRequest
		: IWebRequestCreate
	{
		// must start with http
		public const string FB_PREFIX = "http-fbs";

		public static void Register()
		{
			var success = WebRequest.RegisterPrefix (FB_PREFIX, Mvx.IocConstruct<FacebookWebRequest>());

			if (!success)
				throw new Exception ("Unable to register");
		}

		readonly IAuth _auth;

		public FacebookWebRequest(IAuth auth)
		{
			_auth = auth;
		}

		#region IWebRequestCreate implementation

		public WebRequest Create (Uri uri)
		{
			var builder = new UriBuilder (uri);

			builder.Scheme = "https";


			var request = new HttpWebRequest(new Uri(builder.ToString()));
		
			request.Headers.Clear ();
			request.UserAgent = "HsrCampus";
			request.Accept = "image/jpeg";

			request.Headers["X-Auth"] = _auth.News;

			return request;
		}
		

		#endregion
	}
}

