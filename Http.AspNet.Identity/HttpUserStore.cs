namespace Http.AspNet.Identity
{
    using Microsoft.AspNet.Identity;
    using System;
    using System.Configuration;
    using System.Diagnostics.Contracts;
    using System.Net.Http;

    public partial class HttpUserStore<TUser> : IUserStore<TUser>
        where TUser : class, IUser, IIdentityUser
    {
        public HttpUserStore(string userApiBaseAddress)
        {
            Contract.Requires(!string.IsNullOrEmpty(userApiBaseAddress), "An API base address is required");

            _httpClient.BaseAddress = new Uri(userApiBaseAddress);
        }

        private readonly HttpClient _httpClient = new HttpClient();

        private HttpClient HttpClient => _httpClient;

        private string UserResourceUri
        {
            get
            {
                string value = ConfigurationManager.AppSettings["aspnet:identity:http:userResourceUri"];

                Contract.Assert(!string.IsNullOrEmpty(value), "'aspnet:identity:http:userResourceUri' setting not present or empty");

                return value;
            }
        }

        private string UserRoleResourceUri
        {
            get
            {
                string value = ConfigurationManager.AppSettings["aspnet:identity:http:userRoleResourceUri"];

                Contract.Assert(!string.IsNullOrEmpty(value), "'aspnet:identity:http:userRoleResourceUri' setting not present or empty");

                return value;
            }
        }

        private string UserSettingsResourceUri
        {
            get
            {
                string value = ConfigurationManager.AppSettings["aspnet:identity:http:userSettingsResourceUri"];

                Contract.Assert(!string.IsNullOrEmpty(value), "'aspnet:identity:http:userSettingsResourceUri' setting not present or empty");

                return value;
            }
        }

        private string UserPropertiesResourceUri
        {
            get
            {
                string value = ConfigurationManager.AppSettings["aspnet:identity:http:userPropertiesResourceUri"];

                Contract.Assert(!string.IsNullOrEmpty(value), "'aspnet:identity:http:userPropertiesResourceUri' setting not present or empty");

                return value;
            }
        }

        private string UserPhoneNumberStoreResourceUri
        {
            get
            {
                string value = ConfigurationManager.AppSettings["aspnet:identity:http:userPhoneNumberStoreResourceUri"];

                Contract.Assert(!string.IsNullOrEmpty(value), "'aspnet:identity:http:userPhoneNumberStoreResourceUri' setting not present or empty");

                return value;
            }
        }

        private string UserEmailStoreResourceUri
        {
            get
            {
                string value = ConfigurationManager.AppSettings["aspnet:identity:http:userEmailStoreResourceUri"];

                Contract.Assert(!string.IsNullOrEmpty(value), "'aspnet:identity:http:userEmailStoreResourceUri' setting not present or empty");

                return value;
            }
        }

        private string UserLoginStoreResourceUri
        {
            get
            {
                string value = ConfigurationManager.AppSettings["aspnet:identity:http:userLoginStoreResourceUri"];

                Contract.Assert(!string.IsNullOrEmpty(value), "'aspnet:identity:http:userLoginStoreResourceUri' setting not present or empty");

                return value;
            }
        }

        private string UserClaimStoreResourceUri
        {
            get
            {
                string value = ConfigurationManager.AppSettings["aspnet:identity:http:userClaimStoreResourceUri"];

                Contract.Assert(!string.IsNullOrEmpty(value), "'aspnet:identity:http:userClaimStoreResourceUri' setting not present or empty");

                return value;
            }
        }
    }
}