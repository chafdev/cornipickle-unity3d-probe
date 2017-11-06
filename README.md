A Cornipickle probe for Unity3d Games
============================================

The Cornipickle testing tool can automatically detect and report violations of 
Unity3d user interface. Users write statements in a high level language,
called Cornichon, and Cornipickle can automatically check during the execution of 
the application that these statements are respected at all times.
The [client code](https://github.com/chafdev/cornipickle-unity3d-probe/tree/master) is a probe  that send all information about 
the current state of the window activity and relaying it to 
[the server](https://github.com/chafdev/cornipickle/commits/master) for further processing.


Table of Contents                                                    {#toc}
-----------------

- [Compiling and installing the Cornipickle probe](#install)
    - [Server](https://github.com/chafdev/cornipickle/)
    - [Client (Probe)](https://github.com/chafdev/cornipickle-unity3d-probe/tree/package).
- [About the author](#about)

Compiling and Installing Cornipickle                             {#install}
------------------------------------

Server: 

- For install and run server ,you can find more information
  [here](https://github.com/liflab/cornipickle) 

Probe:                                                             {#probe}
   
Download or clone the [source for Cornipickle](https://github.com/chafdev/cornipickle-unity3d-probe/tree/package)  using Git and import package into your project:

 

### Built-in Examples

Cornipickle contains a few [examples](https://github.com/chafdev/cornipickle-unity3d-probe/tree/examples) for testing Cornipickle probe for unity3d. 

- Click on key (B) for displaying Bug
- Click on key (N) for returning to normal


About the author                                                   {#about}
----------------
The Cornipickle Android Probe was written by **Chafik Meniar**,
then a Masters' Student at Université du Québec à Chicoutimi, Canada.
