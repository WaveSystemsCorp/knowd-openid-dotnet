// -----------------------------------------------------------------------
// <copyright file="UserController.cs" company="Wave Systems Corp.">
// Copyright (C) 2013. Wave Systems Corp. All Rights Reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace OpenIdRelyingPartyMvc.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.Web.Mvc;
	using System.Web.Security;
	using DotNetOpenAuth.Messaging;
	using DotNetOpenAuth.OpenId;
	using DotNetOpenAuth.OpenId.RelyingParty;
    using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
    using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;

	public class UserController : Controller {
		private static OpenIdRelyingParty openid = new OpenIdRelyingParty();

		public ActionResult Index() {
			if (!User.Identity.IsAuthenticated) {
				Response.Redirect("~/User/Login?ReturnUrl=Index");
			}

			return View("Index");
		}

		public ActionResult Logout() {
			FormsAuthentication.SignOut();
			return Redirect("~/Home");
		}

		public ActionResult Login() {
			// Stage 1: display login form to user
			return View("Login");
		}

		[ValidateInput(false)]
		public ActionResult Authenticate(string returnUrl) {
			var response = openid.GetResponse();
			if (response == null) {
				// Stage 2: user submitting Identifier
				Identifier id;
				if (Identifier.TryParse(Request.Form["openid_identifier"], out id)) {
					try {

                        IAuthenticationRequest req = BuildOpenIdRequest(Request.Form["openid_identifier"]);
                        
                        return req.RedirectingResponse.AsActionResult();
					} catch (ProtocolException ex) {
						ViewData["Message"] = ex.Message;
						return View("Login");
					}
				} else {
					ViewData["Message"] = "Invalid identifier";
					return View("Login");
				}
			} else {
				// Stage 3: OpenID Provider sending assertion response
				switch (response.Status) {
					case AuthenticationStatus.Authenticated:

                        ConsumeOpenIdResponse(response);
                       
						if (!string.IsNullOrEmpty(returnUrl)) {
							return Redirect(returnUrl);
						} else {
							return RedirectToAction("Index", "Home");
						}
					case AuthenticationStatus.Canceled:
						ViewData["Message"] = "Canceled at provider";
						return View("Login");
					case AuthenticationStatus.Failed:
						ViewData["Message"] = response.Exception.Message;
						return View("Login");
				}
			}
			return new EmptyResult();
		}

        private IAuthenticationRequest BuildOpenIdRequest(string openIdProviderIdentifier)
        {
            IAuthenticationRequest req = openid.CreateRequest(openIdProviderIdentifier);

            FetchRequest fetch = new FetchRequest();
            fetch.Attributes.Add(new AttributeRequest("http://machineid.com/schema/trustScore", true /* required */));
            fetch.Attributes.Add(new AttributeRequest("http://machineid.com/schema/needsSetup", false /* optional */));
            fetch.Attributes.Add(new AttributeRequest("http://machineid.com/schema/setupURL", false /* optional */));
            req.AddExtension(fetch);

            return req;
        }

        private void ConsumeOpenIdResponse(IAuthenticationResponse response)
        {
            FetchResponse fetch = response.GetExtension<FetchResponse>();
            if (null != fetch)
            {
                Session["OpenIdResponse_trustScore"] = fetch.GetAttributeValue("http://machineid.com/schema/trustScore");
                Session["OpenIdResponse_needsSetup"] = fetch.GetAttributeValue("http://machineid.com/schema/needsSetup");
                Session["OpenIdResponse_setupURL"] = fetch.GetAttributeValue("http://machineid.com/schema/setupURL");
                // needsSetup and setupURL only returned if needed. Returned value is null otherwise.
            }
            Session["FriendlyIdentifier"] = response.FriendlyIdentifierForDisplay;
            FormsAuthentication.SetAuthCookie(response.ClaimedIdentifier, false);

            return;
        }
	}
}
