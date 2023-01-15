---
id: CMS_version_2.1
author: Ao XIE
authorURL: aoxie.art
title: Contact Management System
---
# Contact Management System
A contact management system controlled in the terminal based on C#. Contacts of different users are managed using different folders. Contacts are saved in XML format via XML files or as binary files in UTF8 encoded format via Json files.
## __Highlights__
- Force the correct email format.
- User are free to choose the folder used.
- Includes an error handling mechanism that will not accidentally exit the program.
- Includes GUI friendly interface.
- Includes user manual.
- The files generated are encrypted using the SID and cannot be read directly.

## General Idea of Implementation
The program is generally built with three classes and two interfaces to implement all the functions, the functions of each part are described below.

### Class
The three classes in the program implement functions such as the control bar program, file management and contact management.
- __Application_Console:__ Contains the main function of the program, whose main function is the implementation of the user's interaction in the terminal.
- __Contact:__ A class for managing contacts, this class contains basic information properties for contacts and a method for detecting email formats.
- __Document_Management:__  Data management classes, whose main role is to perform various operations in the terminal, such as reading, writing, etc.

### Interface
The program contains two interfaces, iXML and iBinary, whose role is to store and read files using different methods.
- __iXML:__ The interface contains four methods, write, read, encrypted write and encrypted read, where encryption is implemented using overloading.
- __iBinary:__ The interface contains four methods, write, read, encrypted write and encrypted read, where encryption is implemented using overloading.

## Basic application
The basic application is a document management system. Storage is stored in folders by user, with each folder storing that user's contacts. Contacts can be represented as binary files in Json and as XML Extensible Markup Language files.
### Storage of documents
All files are stored in the "ROOT" folder, the path to which is defined by a read-only variable in the program. So if you need to change this path, you only need to change this variable.
There are two modes included in the process of writing documents, namely the mode for reading XML and the mode for reading Json. These two modes need to be selected by the user during the writing process by defining a mode string.
### Reading of documents
Similarly, there are XML and Json schemas for reading documents, but in this case the choice of schema is made by the program itself, based on the endfix of the document being read.
## Serialization
The encryption of files has fixed the problem in previous versions where reading files encrypted or decrypted and then reading them again produced errors. This version of the program encrypts and decrypts files both in the process of writing and reading.
>**IMPORTANT:**
>No user-defined password option is given, because if every file in the program needed to be read with a user-supplied password, it would have disastrous consequences when using the "display" command: the user would need to enter a password for each file. Thus the current system SID is used as a key. However, the program still retains the password interface, so if you need to add a password, simply write the user update "password" parameter to the new file procedure.

>**ATTENTION :**
>A security vulnerability exists in BinaryFromatter and has a high risk level. NET considers this vulnerability to be due to a design issue and will not fix it. Therefore, in the overloaded methods for binary files, we use the better BinaryWritter and BinaryReader methods to do the reading and writing of the binary files, but keep the BinaryFromatter in the encryption methods to do the job required.



