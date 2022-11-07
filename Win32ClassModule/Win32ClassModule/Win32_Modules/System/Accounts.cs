﻿using System.Collections;
using System.DirectoryServices;

#pragma warning disable CA1416 // Valider la compatibilité de la plateforme


namespace WindowsReportingClient.System;

internal class Account
{
    public Account(string name, string authenticationStatus, string guid)
    {
        GetName = name;
        GetAuthenticationType = authenticationStatus;
        GetGuid = guid;
    }

    public string GetName { get; }

    public string GetAuthenticationType { get; }

    public string GetGuid { get; }

    public static List<Account>? GetLocalUsers()
    {
        try
        {
            var accounts = new List<Account>();
            var localMachine = new DirectoryEntry("WinNT://" + Environment.MachineName + ",Computer");
            var admGroup = localMachine.Children.Find("administrateurs", "group");
            var members = admGroup.Invoke("members", null);
            //Change "administrateurs" if you are using others windows languages versions (administrateurs = FR)

            foreach (var groupMember in (IEnumerable)members!)
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

            return accounts;
        }

        catch (Exception e)
        {
            return null;
        }
    }
}