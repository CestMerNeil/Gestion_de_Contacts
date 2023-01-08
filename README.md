---
id: CMS_version_1.0
author: Ao XIE
authorURL: aoxie.art
title: Contact Management System
---
# Contact Management System

A contact management system controlled in the terminal based on C#. Contacts of different users are managed using different folders. Contacts are saved in XML format via XML files or as binary files in UTF8 encoded format via Json files.
## Highlights
- Force the correct email format.
- User are free to choose the folder used.
- Includes an error handling mechanism that will not accidentally exit the program.
- Includes GUI friendly interface.
- Includes user manual.

## Basic application
The basic application is a document management system. Storage is stored in folders by user, with each folder storing that user's contacts. Contacts can be represented as binary files in Json and as XML Extensible Markup Language files.
### Storage of documents
All files are stored in the "ROOT" folder, the path to which is defined by a read-only variable in the program. So if you need to change this path, you only need to change this variable.
There are two modes included in the process of writing documents, namely the mode for reading XML and the mode for reading Json. These two modes need to be selected by the user during the writing process by defining a mode string.
### Reading of documents
Similarly, there are XML and Json schemas for reading documents, but in this case the choice of schema is made by the program itself, based on the endfix of the document being read.
## Serialization
>**ATTENTION :**
>A security vulnerability exists in BinaryFromatter and has a high risk level. NET considers this vulnerability to be due to a design issue and will not fix it. Therefore, by design, we use BinaryReader and BinaryWriter instead to perform the task of binary contacts.

