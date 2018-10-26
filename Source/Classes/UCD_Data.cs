using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis.Class
{
    public class UCD_Data
    {
        public class APIOutput
        {
            public DataTable Table;
            public dynamic APIData;
        }

        public class TeamInfo
        {
            public class User
            {
                public string id { get; set; }
                public string name { get; set; }
                public string actualName { get; set; }
                public string displayName { get; set; }
                public string email { get; set; }
                public bool deleted { get; set; }
                public int deletedDate { get; set; }
                public string authenticationRealm { get; set; }
                public bool isLockedOut { get; set; }
                public long lastLoginDate { get; set; }
                public bool isDeletable { get; set; }
            }

            public class Role
            {
                public string id { get; set; }
                public string name { get; set; }
                public string description { get; set; }
                public bool isDeletable { get; set; }
            }

            public class RoleMapping
            {
                public User user { get; set; }
                public Role role { get; set; }
            }

            public class RootObject
            {
                public string id { get; set; }
                public string name { get; set; }
                public bool isDeletable { get; set; }
                public List<RoleMapping> roleMappings { get; set; }
            }
        }

        public class TeamResponse
        {
            public string id { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public bool isDeletable { get; set; }
        }

        public class Agents
        {
            public class RootObject
            {
                public string id { get; set; }
                public string securityResourceId { get; set; }
                public string name { get; set; }
                public bool active { get; set; }
                public string relayId { get; set; }
                public bool licensed { get; set; }
                public string licenseType { get; set; }
                public string description { get; set; }
                public string status { get; set; }
                public string version { get; set; }
                public long lastContact { get; set; }
                public long dateCreated { get; set; }
                public string workingDirectory { get; set; }
                public string impersonationPassword { get; set; }
                public bool impersonationUseSudo { get; set; }
                public bool impersonationForce { get; set; }
                public List<object> tags { get; set; }
            }
        }

        public class Resources
        {
            public class RootObject
            {
                public string id { get; set; }
                public string securityResourceId { get; set; }
                public string name { get; set; }
                public string path { get; set; }
                public bool active { get; set; }
                public string description { get; set; }
                public bool inheritTeam { get; set; }
                public bool discoveryFailed { get; set; }
                public bool prototype { get; set; }
                public string impersonationPassword { get; set; }
                public bool impersonationUseSudo { get; set; }
                public bool impersonationForce { get; set; }
                public bool hasAgent { get; set; }
                public string status { get; set; }
                public List<object> tags { get; set; }
            }
        }

        public class ApplicationProcess
        {
            public class Security
            {
                public bool read { get; set; }
                public bool CreateApplications { get; set; }
                public bool CreateApplicationsFromTemplate { get; set; }
                public bool Delete { get; set; }
                public bool EditBasicSettings { get; set; }
                public bool ManageBlueprints { get; set; }
                public bool ManageComponents { get; set; }
                public bool ManageEnvironments { get; set; }
                public bool ManageProcesses { get; set; }
                public bool ManageProperties { get; set; }
                public bool ManageSnapshots { get; set; }
                public bool ManageTeams { get; set; }
                public bool RunComponentProcesses { get; set; }
                public bool ViewApplications { get; set; }
            }

            public class Application
            {
                public string id { get; set; }
                public string securityResourceId { get; set; }
                public string name { get; set; }
                public string description { get; set; }
                public object created { get; set; }
                public bool enforceCompleteSnapshots { get; set; }
                public bool active { get; set; }
                public List<object> tags { get; set; }
                public bool deleted { get; set; }
                public string user { get; set; }
                public Security security { get; set; }
            }

            public class RootObject
            {
                public string id { get; set; }
                public string name { get; set; }
                public string description { get; set; }
                public bool active { get; set; }
                public string inventoryManagementType { get; set; }
                public string offlineAgentHandling { get; set; }
                public int versionCount { get; set; }
                public int version { get; set; }
                public int commit { get; set; }
                public string path { get; set; }
                public bool deleted { get; set; }
                public string metadataType { get; set; }
                public Application application { get; set; }
            }
        }

        public class ApplicationProcessStatus
        {
            public class RootObject
            {
                public string status { get; set; }
                public string result { get; set; }
            }
        }

        public class GenericProcess
        {
            public class Property
            {
                public string id { get; set; }
                public string name { get; set; }
                public string value { get; set; }
                public string description { get; set; }
                public bool secure { get; set; }
            }

            public class Security
            {
                public bool read { get; set; }
                public bool execute { get; set; }
                public bool CreateProcesses { get; set; }
                public bool Delete { get; set; }
                public bool EditBasicSettings { get; set; }
                public bool ExecuteProcesses { get; set; }
                public bool ManageProperties { get; set; }
                public bool ManageTeams { get; set; }
                public bool ViewProcesses { get; set; }
            }

            public class RootObject
            {
                public string id { get; set; }
                public string securityResourceId { get; set; }
                public string name { get; set; }
                public string description { get; set; }
                public string path { get; set; }
                public int versionCount { get; set; }
                public int version { get; set; }
                public int commit { get; set; }
                public string defaultResourceId { get; set; }
                public List<Property> properties { get; set; }
                public Security security { get; set; }
                public string notificationSchemeId { get; set; }
            }
        }

        public class GenericProcessRequest
        {
            public class RootObject
            {
                public string id { get; set; }
                public long submittedTime { get; set; }
                public string userName { get; set; }
                public string workflowTraceId { get; set; }
                public string processPath { get; set; }
                public int processVersion { get; set; }
                public long startTime { get; set; }
                public string result { get; set; }
                public string state { get; set; }
                public bool paused { get; set; }
            }
        }

        public class GenericProcessStatus
        {
            public class Plugin
            {
                public string id { get; set; }
                public string pluginId { get; set; }
                public string name { get; set; }
                public string description { get; set; }
                public string version { get; set; }
                public int versionNumber { get; set; }
                public int ghostedDate { get; set; }
            }

            public class Property
            {
                public string name { get; set; }
                public string label { get; set; }
                public string type { get; set; }
                public string value { get; set; }
                public bool required { get; set; }
                public string description { get; set; }
                public bool hidden { get; set; }
                public List<object> allowedValues { get; set; }
            }

            public class Command
            {
                public string id { get; set; }
                public string name { get; set; }
                public string description { get; set; }
                public Plugin plugin { get; set; }
                public List<Property> properties { get; set; }
            }

            public class Child
            {
                public string id { get; set; }
                public string type { get; set; }
                public string displayName { get; set; }
                public string name { get; set; }
                public string state { get; set; }
                public string result { get; set; }
                public long startDate { get; set; }
                public long endDate { get; set; }
                public long duration { get; set; }
                public string workflowTraceId { get; set; }
                public string workingDir { get; set; }
                public Command command { get; set; }
                public string specialNameType { get; set; }
                public List<object> extraLogs { get; set; }
                public int graphPosition { get; set; }
            }

            public class Trace
            {
                public string id { get; set; }
                public string type { get; set; }
                public string name { get; set; }
                public string state { get; set; }
                public string result { get; set; }
                public long startDate { get; set; }
                public long endDate { get; set; }
                public int duration { get; set; }
                public bool paused { get; set; }
                public string workflowTraceId { get; set; }
                public List<Child> children { get; set; }
            }

            public class Property2
            {
                public string id { get; set; }
                public string name { get; set; }
                public string value { get; set; }
                public string description { get; set; }
                public bool secure { get; set; }
            }

            public class Process
            {
                public string id { get; set; }
                public string securityResourceId { get; set; }
                public string name { get; set; }
                public string description { get; set; }
                public string path { get; set; }
                public int versionCount { get; set; }
                public int version { get; set; }
                public int commit { get; set; }
                public List<Property2> properties { get; set; }
            }

            public class ContextProperty
            {
                public string id { get; set; }
                public string name { get; set; }
                public string value { get; set; }
                public bool secure { get; set; }
            }

            public class RootObject
            {
                public string id { get; set; }
                public long submittedTime { get; set; }
                public string userName { get; set; }
                public string workflowTraceId { get; set; }
                public string processPath { get; set; }
                public int processVersion { get; set; }
                public long startTime { get; set; }
                public string result { get; set; }
                public string state { get; set; }
                public bool paused { get; set; }
                public Trace trace { get; set; }
                public Process process { get; set; }
                public List<ContextProperty> contextProperties { get; set; }
            }
        }
    }
}
