using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices;
using System.Management;
using Microsoft.Win32;

#pragma warning disable CA1416 // Valider la compatibilité de la plateforme


namespace Win32ClassModule.System
{
    internal class Account
    {
        private string Name;
        private string AuthenticationStatus;
        private string Guid;

        public string GetName => Name;
        public string GetAuthenticationType => AuthenticationStatus;
        public string GetGuid => Guid;

        public Account(string name, string authenticationStatus, string guid)
        {
            Name = name;
            AuthenticationStatus = authenticationStatus;
            Guid = guid;
        }

        public static List<Account>? GetLocalUsers()
        {
            try
            {

                List<Account> accounts = new List<Account>();
                var localMachine = new DirectoryEntry("WinNT://" + Environment.MachineName + ",Computer");
                var admGroup = localMachine.Children.Find("administrateurs", "group");
                var members = admGroup.Invoke("members", null);
                //Change "administrateurs" if you are using others windows languages versions (administrateurs = FR)

                foreach (object groupMember in (IEnumerable)members!)
                {
                    var s = new DirectoryEntry(groupMember);
                    Account account = new Account(
                        !String.IsNullOrEmpty(s.Name)
                        ? s.Name
                        : "N/A",
                        !String.IsNullOrEmpty(s.AuthenticationType.ToString())
                        ? s.AuthenticationType.ToString()
                        : "N/A",
                        !String.IsNullOrEmpty(s.NativeGuid)
                        ? s.NativeGuid
                        : "N/A"
                        );
                    accounts.Add(account);
                }
                return accounts;
            }

            catch (Exception e)
            {
                return null;
            }

        }
    }
}
