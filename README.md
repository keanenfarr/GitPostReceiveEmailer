# GitPostReceiveEmailer
At work we have our own on premises git server ([Bonobo Git Server](https://bonobogitserver.com/)) and I have been searching for a good and simple way to send out an email notification when a commit is made.

After not finding a good solution, I thought I'd make my own. 

This is a .NET Core app that runs on Windows and doesn't need anything else installed on the server.  In my case, I just needed to install Bonobo Git Server and this program.

#Installation Instructions

1. Copy the post-receive file to the hooks directory in your repository.

1. Compile this code to create the GitPostReceiveEmailer.exe.

1. Place the exe you created (and all supporting files that are generated with it) in any accessible location on your Windows git server.

1. Modify the post-receive file and change the "C:/GitPostReceiveEmailer.exe" text to point to the path of your exe.

1. Configure the appsettings.json file (located in the same directory as your exe file) with your smtp server's setting and the applicable email addresses and path to your repo repository on your server.

1. Do a commit and push to your server to test.