<!-- TITLE: Common Shared Folders -->
<!-- SUBTITLE: A list of folders that are shared by servers on our local network -->

# Misc
## `\\idn-fileserver\service`
Among other things, this folder contains a bunch of user folders, named individually per company employee.  For instance, Allen's user folder is called `Allen`.  On many machines, this shared folder is mapped to the `I:` drive letter.  Not all employees have one.  If you want one, create one.  There's no rules to it, and different people use them for different things, but it's a convenient way to share files between people or as a central, safe place to park important files.

## `\\idn-development\development\source_safe`
The Visual Source Safe database for much of the legacy VB6 code (especially RMS, JMS, and ImageNet).

## `\\idn-development\Development\Icons`
Icons and images

## `\\updates\AgencyFiles\RMS\Agencies`
The source folders for the auto-updater engine.  The QA and Production ones are automatically updated by Jenkins after each applicable build.  Please don't make modifications to these folders, since it will effect live customer environments.  However, this is a good place to go to get a copy of the latest official build of something or to see what version of a component is currently installed in a particular environment.
