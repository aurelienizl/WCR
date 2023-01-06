# @author: Izoulet Aurélien 
# @date : 2022 


# SCRIPT D'INSTALLATION DU CLIENT WCR. 


# Nom du service.
$ServiceName = "WCRC" 
# Nom du package.
$MsiPath = "WCRC.msi" 
# Dossier contenant le package et le log d'installation.
$LogPath = "C:\Produits\WCRC\" 
# Nom du fichier de log
$LogFileName = "log.txt"
# Emplacement de l'exécutable.
$MsiExecutablePath = "C:\Program Files (x86)\IEDOM-IEOM\WCRC\WCRC Service.exe" 

# Arguments d'installation du package.
$MSIArguments = @(
    "/i"
    -join($LogPath, $MsiPath)
    "/qn" 
    "/norestart"
    "/L*v"
    -join($LogPath,$LogFileName)
)

# Arguments d'installation du service
$ServiceParams = @{
  Name = $ServiceName
  BinaryPathName = $MsiExecutablePath
  DisplayName = "WCRC Service"
  StartupType = "Auto"
  Description = "This is the service required by WCRC"
}

# Je vérifie l'existance du service, si il existe je le réinstalle, sinon je l'installe. 
If (Get-Service $ServiceName -ErrorAction SilentlyContinue) {
    If ((Get-Service $ServiceName).Status -eq 'Running') {
        Stop-Service $ServiceName 
        sc.exe delete $ServiceName
        New-Service @ServiceParams
    } Else {
        sc.exe delete $ServiceName
        New-Service @ServiceParams
    }
} Else {
    New-Service @ServiceParams
}

#Je vérifie que le dossier qui contiendra le package existe, sinon je le crée.
if (-Not(Test-Path -Path $LogPath)) {
    New-Item -Path $LogPath -ItemType Directory
} 

# Je déplace le package dans le dossier.
Move-Item $MsiPath "$LogPath$MsiPath"
# Je lance l'installation du package.
Start-Process "msiexec.exe" -ArgumentList $MSIArguments -Wait -NoNewWindow 
# Je crée le service.
Start-Service $ServiceName
