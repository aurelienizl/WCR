using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using WCRC_Service;

internal class Account
{
    public string GetName { get; }
    public string GetAuthenticationType { get; }
    public string GetGuid { get; }
    public Account(string name, string authenticationStatus, string guid)
    {
        GetName = name;
        GetAuthenticationType = authenticationStatus;
        GetGuid = guid;
    }
    public static List<Account> GetLocalUsers()
    {
        var accounts = new List<Account>();

        try
        {
            var localMachine = new DirectoryEntry("WinNT://" + Environment.MachineName + ",Computer");
            var admGroup = localMachine.Children.Find("administrateurs", "group");
            var members = admGroup.Invoke("members", null);
            //Change "administrateurs" if you are using others windows languages versions (administrateurs = FR)
            if (members != null)
            {
                foreach (var groupMember in members as IEnumerable)
                {
                    try
                    {
                        var s = new DirectoryEntry(groupMember);
                        var account = new Account(
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

                    }
                  
                }

            }

            WCRC.log.LogWrite("Got local accounts successfully");
            return accounts;
        }

        catch (Exception)
        {
            WCRC.log.LogWrite("Error : local accounts");
            return accounts;
        }
    }
}