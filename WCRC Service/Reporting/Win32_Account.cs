using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using WCRC_Service;
using WCRC_Service.Modules;

internal class Win32_Account
{
    public string GetName { get; }
    public string GetAuthenticationType { get; }
    public string GetGuid { get; }
    public Win32_Account(string name, string authenticationStatus, string guid)
    {
        GetName = name;
        GetAuthenticationType = authenticationStatus;
        GetGuid = guid;
    }
    public static List<Win32_Account> GetLocalUsers()
    {
        var accounts = new List<Win32_Account>();

        try
        {
            var localMachine = new DirectoryEntry("WinNT://" + Environment.MachineName + ",Computer");
            var admGroup = localMachine.Children.Find("administrateurs", "group");
            var members = admGroup.Invoke("members", null);
            //Change "administrateurs" if you are using others windows languages versions (administrateurs = FR)
            foreach (var groupMember in (IEnumerable)members)
            {
                try
                {
                    var s = new DirectoryEntry(groupMember);
                    var account = new Win32_Account(
                        !string.IsNullOrEmpty(s.Name)
                            ? s.Name
                            : "N/A",
                        !string.IsNullOrEmpty(s.AuthenticationType.ToString())
                            ? s.AuthenticationType.ToString()
                            : "N/A",
                        !string.IsNullOrEmpty(s.NativeGuid)
                            ? s.NativeGuid
                            : "N/A"
                    );
                    accounts.Add(account);
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            Logs.LogWrite("Got local accounts successfully");
            return accounts;
        }

        catch (Exception)
        {
            Logs.LogWrite("Error : local accounts");
            return accounts;
        }
    }
}