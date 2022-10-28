using System.Management;    

namespace Win32ClassModule
{
    internal class Win32_Battery
    {
        private UInt16 Availability;
        private UInt32 BatteryRechargeTime;
        private UInt16 BatteryStatus;
        private string Caption;
        private UInt16 Chemistry;
        private UInt32 ConfigManagerErrorCode;
        private bool ConfigManagerUserConfig;
        private string CreationClassName;
        private string Description;
        private UInt32 DesignCapacity;
        private UInt64 DesignVoltage;
        private string DeviceID;
        private bool ErrorCleared;
        private string ErrorDescription;
        private UInt16 EstimatedChargeRemaining;
        private UInt32 EstimatedRunTime;
        private UInt32 ExpectedBatteryLife;
        private UInt32 ExpectedLife;
        private UInt32 FullChargeCapacity;
        private string InstallDate;
        private UInt32 LastErrorCode;
        private UInt32 MaxRechargeTime;
        private string Name;
        private string PNPDeviceID;
        private UInt16[] PowerManagementCapabilities;
        private bool PowerManagementSupported;
        private string SmartBatteryVersion;
        private string Status;
        private UInt16 StatusInfo;
        private string SystemCreationClassName;
        private string SystemName;
        private UInt32 TimeOnBattery;
        private UInt32 TimeToFullCharge;
    }
}
