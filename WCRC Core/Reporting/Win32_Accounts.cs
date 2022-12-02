using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using WCRC_Core;

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
        try
        {
            var accounts = new List<Account>();
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
                    catch (Exception ex)
                    {
                        WCRC._Win32_Error.Account_error += 1;
                        WCRC.log.LogWrite("Internal error on accounts...");
                        WCRC.log.LogWrite(ex.Message);
                    }
                  
                }

                return accounts;
            }

            return null;
        }

        catch (Exception ex)
        {
            WCRC.log.LogWrite("Critical error on accounts...");
            WCRC.log.LogWrite(ex.Message);
            WCRC._Win32_Error.Critical_Account_error += 1;
            return null;
        }
    }
}