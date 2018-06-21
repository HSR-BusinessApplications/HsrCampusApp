# HSR Campus App

## Contribution
If you want to contribute to the project, please first read the [Contribution-Guideline](Contribution-Guideline.md) and the [Code-Guideline](Code-Guideline.md).

There are two different approaches to use the project:

1. You do not have the API keys file
2. You do have the API keys file

### Without API Key File(s)
1. Download the Git Repository
2. You should now be able to compile with the Test- Configuration. All other Configurations won't work properly.

If you want to compile in the Debug- and/or Release- Configuration you also have to do the following steps:

3. Create a folder named "CampusAppKeys" in the same directory the repository is in. (Ex.: Repository-Location: C:\Projects\CampusApp then the folder must be C:\Projects\CampusAppKeys).
4. Copy the File "ServiceApi.cs" from the root folder of the repository into the CampusAppKeys folder

Now you should be able to build in the Debug- and Release- Configuration. All the API functionality won't work without the correct API keys! If you want to have the full functionality, follow the "How to optain API Key File" guideline.

### With API Key File(s)
1. Download the Git Repository
2. Make sure you have a folder "CampusAppKeys" on the same folder level as the "CampusApp" folder is (ex.: Git Repository is in "C:\Projects\CampusApp" then the CampusAppKeys folder needs to be "C:\Projects\CampusAppKeys")
3. Put the API keys file in the CampusAppKeys folder
4. You should now be able to compile with the Debug- or Release- Configuration

### Testdata
For development and testing purposes, especially for users that don't have the API key file(s) there's a Test- Configuration. For more information about the Testdata check out [Testdata-Description](Testdata-Description.md).

### How to obtain the API Key File
If you want to develop on the Debug- and Release Configuration please contact the IT Business Applications department of the HSR Rapperswil.

## Tools

* Visual Studio 2017 (15.5)
  * Mobile development with .net (workload)
* Android SDK Manager
  * "Android 8.0 (API 26) > SDK Platform" for "Compile using Android version"
  * "Android 6.0 (API 23) > SDK Platform" for "minimum Android to target"

## Configurations

### Solution Configurations

Configuration   | Description
----------------|----------------------
Debug           | For debugging. Connects to the live MobileServices
Test            | For debugging. Uses Test-Dummies instead of the live MobileServices.
Relese          | For release. Connects to the live MobileServices

### Solution Platforms

To reduce build time select the platform you want to run the app on.

Platform        | Description
----------------|----------------------
Any CPU         | for deployment on Android Emulator or Device
iPhone          | for deployment on the iPhone (Devide)
iPhoneSimulator | for deployment on the iPhone Simulator
AllSimulators   | for deployment on Android Emulator or iPhone Simulator


## Android AVD Emulator

To use hardware acceleration (Intel HAXM) for the AVD Emulator Hyper-V must be disabled.




