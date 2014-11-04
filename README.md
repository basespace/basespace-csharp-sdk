basespace-csharp-sdk
====================

## Branches ##

###### Master ######
Master branch is stable and running in production environments.

###### Develop ######
Develop branch is pre-release and not fully functional or ready for production environments.

## Contributing ##
- VS 2012
- [XUnit Extension added to VS 2012](http://visualstudiogallery.msdn.microsoft.com/463c5987-f82b-46c8-a97e-b1cde42b9099 "XUnit Extension for VS 2012")
- Resharper recommended
- Do not commit your key/password to the Tests App.config
- 
## To Enable Integration Tests ##
* Create a file called appSettings.config.hidden and put it in the BaseSpace.SDK.Tests directory
* Put the following in this file:

```
<?xml version="1.0" encoding="utf-8" ?>
  <appSettings>
    <add key="basespace:api-key" value="YOURVALUE"/>
    <add key="basespace:api-secret" value="YOURVALUE"/>
    <add key="basespace:api-authcode" value="YOURVALUE"/>
    <add key="basespace:api-url" value="https://api.basespace.illumina.com"/>
    <add key="basespace:web-url" value="https://basespace.illumina.com"/>
    <add key="basespace:api-version" value="v1pre3"/>
</appSettings>
```
* Replace the values with the ones you want to test
* Unit tests assume access token has read/write privileges and access to multiple runs/projects/samples but doesn't assume specific data unless global

## Contributions ##
All contributions are welcome, but must be made as a pull request and be useful for a wide range of users.  Please follow coding style in code and resharper rules if present.
