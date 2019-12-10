<!-- TITLE: Puffin Vm Host -->
<!-- SUBTITLE: Virtual machines used by Puffins to run things on a LAN-connected machine -->

# Introduction
Some things are too network intensive to run over the VPN.  For instance, Visual Source Safe is exceedingly slow over the VPN, so you really need to run it from a machine that is directly connected to the company's LAN.  Running legacy software can also be painfully slow, since it goes through MS Access databases and directly connects to network shares for various tasks.  In such situations, as a remote developer, you will likely find it more convenient to use Remote Desktop or Bomgar to connect to a LAN-connected machine with screen sharing.  For that purpose, the Puffins have a Dell Laptop, installed in Steve's office, which does nothing but act as the virtual machine host for said machines.  It essentially just has VirtualBox installed on it, and nothing else.

All Puffins have access to the host machine, so when you need to use a machine, simply connect to the host and start the desired virtual machine.  If you need a new machine, clone or create a new one.  Once you have the virtual machine started, then you should connect to it, via screen sharing, and disconnect from the host, just to keep the connection to the host open for others who might need it.  When you're done using the virtual machine, you should shut it down (the virtual one) so that it doesn't consume resources.  Since the host is only a standard development laptop, it can only run three, or so, VMs at the same time before it runs out of resources.

# How to Connect
## Host
* Name - `PUFFIN-VM-HOST`
* Domain - N/A
* Workgroup - `WORKGROUP`
* OS - Windows 10
* User/Password - `idn`/`idn`
* Bomgar - `1 - ID Networks - Employees` -> `Puffin VMs` -> `Puffing VM - Host`

## Virtual Machines
The following is a list of the main virtual machines that currently exist on the host.  There may be others that are not listed here, but if so, hopefully their names are sufficient to convey their purpose.

### Clean Win7
A new machine, with nothing installed on it except the operating system.

This is meant to be a starter/base machine to be cloned.  Do not install anything on this machine unless it is a necessity for all future Windows 7 VMs.  Do not install anything on this machine except for standard windows updates.

When cloning this machine, please assign new MAC addresses to the clone and then change the workstation name, as well, once you start it up.

* Name - `VM-WIN7`
* Domain - N/A
* Workgroup - `WORKGROUP`
* OS - Windows 7
* User/Password - `idn`/`idn`
* Bomgar - N/A

### Clean Win10
A new machine, with nothing installed on it except the operating system.

This is meant to be a starter/base machine to be cloned.  Do not install anything on this machine unless it is a necessity for all future Windows 10 VMs.  Do not install anything on this machine except for standard windows updates.  Do not activate Windows on this clean VM.

* Name - `VM-WIN10`
* Domain - N/A
* Workgroup - `WORKGROUP`
* OS - Windows 10
* User/Password - `idn`/`idn`
* Bomgar - N/A

### Clean VB6 Development
A new development machine, with nothing installed on it except the core tools for VB6 development, such as Visual Source Safe and VB6.

This is meant to be a starter/base machine to be cloned.  Do not install anything on this machine unless it is a necessity for all future VB6 development VMs.  Before installing anything on this machine, please try to remember to create a VM snapshot so that we can roll back if we need to.  Do not activate Windows on this clean VM.

* Name -  `VB6DEV`
* Domain - N/A
* Workgroup - `WORKGROUP`
* OS - Windows 7
* User/Password - `idn`/`idn`
* Bomgar - N/A

# Cloning
Since creating a new VM from scratch, and installing an operating system on it, is time consuming, there are some pre-created "Clean" VMs which can be cloned.  When cloning any VM, always assign the clone new MAC addresses so that it doesn't conflict with the original VM when both are running at the same time.  For the same reason, once you start the clone up for the first time, please change the network name for the worstation to something unique.  Since windows is intentionally left unactivated on the clean machines, you may need to activate the operating system on the clone using one of the OS licenses from your MSDN subscription.  You may also want to install the Bomgar support client on the VM, and assign it to the `1 - ID Networks - Employees` -> `Puffin VMs` group, for convenient access.