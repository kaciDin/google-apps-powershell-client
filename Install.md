# Installation #

**TODO: Install process that doesn't require registering in GAC and machine.config redirects.**

1. Register the following in GAC (gacutil /i path of dll)
  * System.Net.Http 2.1.10.0
  * System.Net.Http.Primitives 2.1.10.0
  * log4net 1.2.10.0

2. Add Binding Redirect to "C:\Windows\Microsoft.NET\Framework64\v4.0.30319\config\machine.config" (Replace current runtime tag)

```
<runtime>
<assemblyBinding xmlns="urn:schemas-microsoft-com:asm.v1">
  <dependentAssembly>
    <assemblyIdentity name="System.Runtime" publicKeyToken="b03f5f7f11d50a3a" culture="neutral" />
    <bindingRedirect oldVersion="0.0.0.0-2.5.19.0" newVersion="2.5.19.0" />
  </dependentAssembly>
  <dependentAssembly>
    <assemblyIdentity name="System.Threading.Tasks" publicKeyToken="b03f5f7f11d50a3a" culture="neutral" />
    <bindingRedirect oldVersion="0.0.0.0-2.5.19.0" newVersion="2.5.19.0" />
  </dependentAssembly>
  <dependentAssembly>
    <assemblyIdentity name="System.Net.Http" publicKeyToken="b03f5f7f11d50a3a" culture="neutral" />
    <bindingRedirect oldVersion="0.0.0.0-2.1.10.0" newVersion="2.1.10.0" />
  </dependentAssembly>
  <dependentAssembly>
    <assemblyIdentity name="System.Net.Http.Primitives" publicKeyToken="b03f5f7f11d50a3a" culture="neutral" />
    <bindingRedirect oldVersion="0.0.0.0-2.1.10.0" newVersion="2.1.10.0" />
  </dependentAssembly>
</assemblyBinding>
  </runtime>
```


# Import Module #

**TODO: Register snappin, load by Name**

  1. Open Powershell
  1. Set-ExecutionPolicy RemoteSigned
  1. Import-Module **DLLPATH**\GaPoSh.dll
  1. New-GaPoShConnection -ServiceEmail **ServiceAccountEmail** -ServicePath **CertPath** -ServiceUser **AdminEmailAddress** -Domain **GoogleAppsDomain**
  1. Get-GaPoShUser -Session $session -All $true