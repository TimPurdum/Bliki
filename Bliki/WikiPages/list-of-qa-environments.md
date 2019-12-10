<!-- TITLE: List of QA Environments -->
<!-- SUBTITLE: All you need to know to get connected to any of the QA environments -->

# Records
## Version 5.0
### Virginia
* Dedicated client machine
	* Bomgar -> `1 - ID Networks - QA` -> `QA-RMS-VA-DEV`
* Download Path
	* `\\Idn-rms-qa\va\QaDev\IDN-CJIS\Download`
* Central Configuration Files
	* `\\Idn-rms-qa\va\QaDev\IDN-CJIS\Download\Central.config`
	* `\\Idn-rms-qa\va\QaDev\IDN-CJIS\Download\Central.LocalServices.config`
* Web Updater URL
	* `http://updates.idnetworks.com/VA/DEV/service.asmx`
## Version 3.4
### Virginia
* Dedicated client machine
	* Bomgar -> `1 - ID Networks - QA` -> `QA-RMS-VA-PRD`
	* Bomgar -> `1 - ID Networks - QA` -> `QA-RMS-VA-PRD-S`
* Download Path
	* `\\Idn-rms-qa\va\QaProd\IDN-CJIS\Download`
* Central Configuration Files
	* `\\Idn-rms-qa\va\QaProd\IDN-CJIS\Download\Central.config`
	* `\\Idn-rms-qa\va\QaProd\IDN-CJIS\Download\Central.LocalServices.config`
* Web Updater URL
	* `http://updates.idnetworks.com/VA/QA/service.asmx`

# Workstation Configuration
To setup a workstation to point to one of the QA environments, you need to do two things:
1. Point the `Local.config` at the appropriate `Central.config` 
2. Point the `IDN.ini` at the right download path and web updater

## Central.config
If you don't have a `Local.config` file, you'll need to create one.  The file can contain multiple `CentralConfigurationFilePath` elements, but you'll need the default one to point to the QA environment's central configuration file, like this:

	<?xml version="1.0"?>
	<LocalConfiguration>
		<CentralConfigurationFilePath default="true">\\idn-rms-qa\VA\QaDev\IDN-CJIS\Download\Central.config</CentralConfigurationFilePath>
	</LocalConfiguration>

## IDN.ini
If you don't have an `IDN.ini` file in your `C:\Windows` folder, you'll need to create one.  The file may contain other settings, but at a minimum, it will need to contain the following settings, set to the appropriate values for the QA environment:

	DOWNLOADPATH=\\idn-rms-qa\VA\QaDev\IDN-CJIS\Download
	FORCEDOWNLOADPATH=\\idn-rms-qa\VA\QaDev\IDN-CJIS\Download
	LOCALWEBUPDATE=http://updates.idnetworks.com/VA/DEV/service.asmx
