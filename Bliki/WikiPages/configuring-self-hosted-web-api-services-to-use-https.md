<!-- TITLE: Configuring Self-Hosted Web API Services to Use HTTPS -->

# E-mail
The following instructions were sent in an e-mail from Steven Doggart to Allen Sharrer, Derek Tapper, and Josh Weeks on November 2, 2017.  At the time, there was a concern that the core service would need to be configured to be accessed via HTTPS so that all of the communication with it would be safely encrypted.  Technically, if it's only visible from within a VPN connection, it's already encrypted by the VPN, but if it's exposed to the internet directly, or if someone is concerned that someone on the LAN could intercept the messages and read them, then HTTPS becomes necessary.  This is especially important when using the security Web API, since it does not encyrpt anything, not even the login credentials.  At the time, Steve's recommendation was that we should require all agencies wishing to use HTTPS to purchase their own certificate and maintain that registration themself, rather than having us purchasing one to share among all customers, which is not very secure, or having all customers use a self-signed one, which is also insecure.

## Subject: Configuring IdnCoreService to use HTTPS

I was able to successfully get IdnCoreService to use HTTPS on my own machine.  It doesn’t actually require any coding changes or even app config settings or anything like that.  There are only two things that have to be in place to make it work:

1. Set the protocol prefix on the URL in the Central.config file to HTTPS (e.g. https://localhost/IdnCore/)
1. Add the certificate to the store and bind it to the IP and port

Unfortunately that second part isn’t as simple as it sounds.  It might be easier via IIS, but I don’t have that installed, so I did it all manually.  Here’s the steps I took to get the certificate installed:

* First, I created a self-signed certificate and private key, using OpenSSL.  OpenSSL comes with Git for Windows, so if you run your Git Bash, the openssl command will work from there.  Here’s the command I ran to create the certificate:
```
    openssl req -x509 -nodes -days 365 -newkey rsa:2048 -keyout privatekey.key -out certificate.crt
```
* Next, I needed to add the certificate to the store using the Certificates “snap-in”, but it doesn’t allow you to set the private key on a certificate as a secondary step, so the only way I could find to import the certificate with the private key attached was to first combine the two into a single PFX file (see [this SO Q&A](https://stackoverflow.com/questions/13076915/ssl-certificate-add-failed-when-binding-to-port) for more info).  To do that, I just had to run another openssl command to export a PFX, using the CRT and KEY files as input (see [this page](https://myonlineusb.wordpress.com/2011/06/19/what-are-the-differences-between-pem-der-p7bpkcs7-pfxpkcs12-certificates/) for info about the different file types and how to convert between them).  Also, since it asks for a password with which to encrypt the PFX file, it needs to display things to and read things from the console.  Since there seems to be some incongruity in the implementation of that console IO in Git’s Bash and Linux/Unix, when I ran it, it would look like it was hanging.  So, running it through winpty fixes that, for whatever stupid reason (see [this SO Q&A](https://stackoverflow.com/questions/34156938/openssl-hangs-during-pkcs12-export-with-loading-screen-into-random-state) for more info).  So, here’s the full command I ran to export the PFX file:
```
winpty openssl pkcs12 -export -out certificate.pfx -inkey privatekey.key -in certificate.crt
```
* The next step was to import the certificate into the Certificates “snap-in” via mmc (I feel stupider just saying it)
  * Run mmc 
  * Add snap-in 
  * Choose the Certificates snap-in
  * Choose to install it for the Computer account 
  * Navigate to the Personal -> Certificates folder
  * Right click on the folder and choose the All Tasks -> Import option
  * Choose the PFX file that was exported in the previous step
  * Double click on the certificate
  * Go to the Details tab
  * Select the Thumbprint item
  * Copy the Hex string value and paste it into a text editor (e.g. `06 8a e1 5a 6e ee 6b ab 48 38 81 d0 03 44 93 d9 4c 59 8c 84`)
  * Remove all the spaces from the hex string (this will be needed in the next step)
* Now that I had the certificate installed in the store, along with its private key, I just needed to bind it to the IP/port so that the OS would use it as the certificate for HTTPS encryption.  To do that, I just needed to run the [netsh](https://en.wikipedia.org/wiki/Netsh) command, telling it which IP/port to bind and which certificate to bind to.  If you specify all 0’s for the IP address, it acts as a wildcard and binds it to all IPs on the machine.  The default port for HTTPS is 443, so I used that, but I did test it on other ports too.  If you use a different port, it works fine, as long as you specify the port in the Central.config (e.g. https://localhost:12345/IdnCore/).  To specify which certificate you want to bind, you use that thumbprint hex string, minus the spaces.  The only other thing that it needs is an application ID, which is apparently irrelevant, as long as it’s a valid GUID, so I just created a new GUID.  For more info, I found [this MS blog post](https://blogs.msdn.microsoft.com/drnick/2007/10/15/configuring-ssl-certificates-for-windows-vista/) semi-helpful.  So, here’s the command I ran to bind them:
```
netsh http add sslcert ipport=0.0.0.0:443 certhash=068ae15a6eee6bab483881d0034493d94c598c84 appid={12e5d875-8e63-44ff-aafc-5fd5c684795c}
```
I think, if you have IIS installed, you can install the certificate and key via the IIS management application and you can setup the SSL/certificate bindings in there as well, so it may not be as difficult.  Also, that first step would hopefully be unnecessary in production since we’d be using an already existing certificate.  The binding binds that certificate for all HTTPS traffic on that IP address and port.  In other words, once the SSL/certificate binding is specified, all websites hosted on that IP/port are automatically HTTPS-enabled and all share that same certificate.  You cannot bind multiple certificates to the same IP/port.  If you want to view all the existing bindings, you can run:
```
netsh http show sslcert
```
And it you want to remove the binding, you can run (obviously specifying the right IP/port):
```
netsh http delete sslcert ipport=0.0.0.0:443
```
Anyway, once the certificate/key is installed and the SSL/certificate binding is setup, then you can switch the IdnCoreService between HTTP and HTTPS just by changing the URL in the Central.config and restarting it. 

