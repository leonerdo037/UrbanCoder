using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Jarvis.Class;
using RestSharp;
using RestSharp.Authenticators;
using static UrbanCoder.Classes.Exceptions;

namespace UrbanCoder.Classes
{
    public class UCD_Client
    {
        private Uri ucd_uri;
        private HttpBasicAuthenticator ucd_auth;
        private RestClient client;
        
        public UCD_Client(string uri, string username, string password, bool ignoreCertificate = false)
        {
            if (!uri.EndsWith("/"))
            {
                uri = uri + "/";
            }
            ucd_uri = new Uri(uri);
            ucd_auth = new HttpBasicAuthenticator(username, password);
            ServicePointManager.ServerCertificateValidationCallback +=
            (sender, certificate, chain, sslPolicyErrors) => ignoreCertificate;
            client = new RestClient
            {
                BaseUrl = ucd_uri,
                Authenticator = ucd_auth
            };
        }

        private void ErrorHandler(string message, MethodBase operationType)
        {
            if (message.Contains("Unauthorized"))
            {
                throw new UC_LoginFailed(string.Format("{0} Failed: {1}", operationType.Name, message));
            }
            else if (message.Contains("Team could not be restored for"))
            {
                throw new UC_TeamNotFound(string.Format("{0} Failed: {1}", operationType.Name, message));
            }
            else if(message.Contains("cannot manage membership"))
            {
                throw new UC_UnknownUser(string.Format("{0} Failed: {1}", operationType.Name, message));
            }
            else if(message.Contains("No resource with id"))
            {
                throw new UC_ResourceNotFound(string.Format("{0} Failed: {1}", operationType.Name, message));
            }
            else
            {
                throw new UC_Exception(string.Format("{0} Failed: {1}", operationType.Name, message));
            }
        }

        #region Team
        public UCD_Data.APIOutput GetTeamInfo(string teamName)
        {
            RestRequest request = new RestRequest
            {
                Method = Method.GET,
                Resource = "cli/team/info"
            };
            request.AddParameter("team", teamName, ParameterType.QueryString);
            request.AddHeader("Accept", "application/json");
            UCD_Data.APIOutput result = new UCD_Data.APIOutput();
            IRestResponse<UCD_Data.TeamInfo.RootObject> output = client.Execute<UCD_Data.TeamInfo.RootObject>(request);
            if (output.IsSuccessful)
            {
                // DataTable
                DataTable DT = new DataTable();
                DT = new DataTable();
                // Adding Columns
                DT.Columns.Add("No");
                DT.Columns.Add("Name");
                DT.Columns.Add("Display Name");
                DT.Columns.Add("Actual Name");
                DT.Columns.Add("Role");
                DT.Columns.Add("Last Login Date");
                DT.Columns.Add("Locked Out ?");
                // Adding Rows
                int index = 1;
                foreach (UCD_Data.TeamInfo.RoleMapping roles in output.Data.roleMappings)
                {
                    DataRow DR = DT.NewRow();
                    DR[0] = index;
                    DR[1] = roles.user.name;
                    DR[2] = roles.user.displayName;
                    DR[3] = roles.user.actualName;
                    DR[4] = roles.role.name;
                    DR[5] = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddMilliseconds(Convert.ToDouble(roles.user.lastLoginDate));
                    DR[6] = roles.user.isLockedOut;
                    DT.Rows.Add(DR);
                    index++;
                }
                // Updating the output class with data
                result.APIData = output.Data;
                result.Table = DT;
            }
            else
            {
                ErrorHandler(output.Content, MethodBase.GetCurrentMethod());
            }
            return result;
        }

        public void CreateTeam(string teamName, string teamDescription)
        {
            RestRequest request = new RestRequest
            {
                Method = Method.PUT,
                Resource = "cli/team/create"
            };
            request.AddParameter("team", teamName, ParameterType.QueryString);
            request.AddParameter("description", teamDescription, ParameterType.QueryString);
            request.AddHeader("Accept", "application/json");
            IRestResponse<UCD_Data.TeamResponse> output = client.Execute<UCD_Data.TeamResponse>(request);
            if (!output.IsSuccessful)
            {
                ErrorHandler(output.Content, MethodBase.GetCurrentMethod());
            }
        }

        public void AddUserToTeam(string teamName, string userName, string role)
        {
            RestRequest request = new RestRequest
            {
                Method = Method.PUT,
                Resource = "cli/teamsecurity/users"
            };
            request.AddParameter("team", teamName, ParameterType.QueryString);
            request.AddParameter("user", userName, ParameterType.QueryString);
            request.AddParameter("role", role, ParameterType.QueryString);
            request.AddHeader("Accept", "application/json");
            IRestResponse output = client.Execute(request);
            if (!output.IsSuccessful)
            {
                ErrorHandler(output.Content, MethodBase.GetCurrentMethod());
            }
        }

        public void DeleteTeam(string teamName)
        {
            RestRequest request = new RestRequest
            {
                Method = Method.PUT,
                Resource = "cli/team/delete"
            };
            request.AddParameter("team", teamName, ParameterType.QueryString);
            request.AddHeader("Accept", "application/json");
            IRestResponse output = client.Execute(request);
            if (!output.IsSuccessful)
            {
                ErrorHandler(output.Content, MethodBase.GetCurrentMethod());
            }
        }
        #endregion

        #region Resources
        public UCD_Data.APIOutput ListResources(string parentResource)
        {
            RestRequest request = new RestRequest
            {
                Method = Method.GET,
                Resource = "cli/resource"
            };
            request.AddParameter("parent", parentResource, ParameterType.QueryString);
            request.AddHeader("Accept", "application/json");
            UCD_Data.APIOutput result = new UCD_Data.APIOutput();
            IRestResponse<List<UCD_Data.Resources.RootObject>> output = client.Execute<List<UCD_Data.Resources.RootObject>>(request);
            if (output.IsSuccessful)
            {
                // DataTable
                DataTable DT = new DataTable();
                DT = new DataTable();
                // Adding Columns
                DT.Columns.Add("No");
                DT.Columns.Add("Hostname");
                DT.Columns.Add("IP Address");
                DT.Columns.Add("OS Type");
                DT.Columns.Add("Status");
                DT.Columns.Add("RID");
                // Adding Rows
                int index = 1;
                foreach (UCD_Data.Resources.RootObject resouce in output.Data)
                {
                    DataRow DR = DT.NewRow();
                    DR[0] = index;
                    //DR[1] = resouce.name.Split('|')[1];
                    //DR[2] = resouce.name.Split('|')[2];
                    //DR[3] = resouce.name.Split('|')[0];
                    DR[1] = resouce.name;
                    DR[2] = resouce.name;
                    DR[3] = resouce.name;
                    DR[4] = resouce.status;
                    DR[5] = resouce.id;
                    DT.Rows.Add(DR);
                    index++;
                }
                // Updating the output class with data
                result.APIData = output.Data;
                result.Table = DT;
            }
            else
            {
                ErrorHandler(output.Content, MethodBase.GetCurrentMethod());
            }
            return result;
        }

        public void CreateResource(string name, string description, string parent="", string agent="")
        {
            RestRequest request = new RestRequest
            {
                Method = Method.PUT,
                Resource = "cli/resource/create"
            };
            request.AddHeader("Accept", "application/json");
            // PayLoad
            StreamReader SR = new StreamReader(Environment.CurrentDirectory + @"\JSON\CreateResource.json");
            string payLoad = SR.ReadToEnd();
            SR.Dispose();
            SR.Close();
            payLoad = payLoad.Replace("_name_", name).Replace("_description_", description);
            payLoad = payLoad.Replace("_parent_", parent);
            payLoad = payLoad.Replace("_agent_", agent);
            request.AddJsonBody(payLoad);
            IRestResponse output = client.Execute(request);
            if (!output.IsSuccessful)
            {
                ErrorHandler(output.Content, MethodBase.GetCurrentMethod());
            }
        }

        public void CreateResourceProperty(string resourceName, string name, string value = null)
        {
            RestRequest request = new RestRequest
            {
                Method = Method.PUT,
                Resource = "cli/resource/setProperty"
            };
            request.AddParameter("resource", resourceName, ParameterType.QueryString);
            request.AddParameter("name", name, ParameterType.QueryString);
            if(value != null)
            {
                request.AddParameter("value", value, ParameterType.QueryString);
            }
            request.AddHeader("Accept", "application/json");
            IRestResponse output = client.Execute(request);
            if (!output.IsSuccessful)
            {
                ErrorHandler(output.Content, MethodBase.GetCurrentMethod());
            }
        }
        #endregion
    }
}
