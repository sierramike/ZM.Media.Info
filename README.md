# ZM.Media.Info
A flexible and versatile MediaInfo .NET wrapper

ZM Media Info is another .NET wrapper for the famous MediaInfo library. [MediaInfo](https://www.mediaarea.net/en/MediaInfo) is an open source application capable of analyzing media resources and report a huge bunch of details regarding the file and its contents.

MediaInfo comes packaged as an application, but can also be downloaded as a standalone library. Visit the [download page](https://www.mediaarea.net/en/MediaInfo/Download/Windows) and select the 32 or 64 bits version (depending on your hardware/operating system), and download the DLL "without installer".

The library comes with some code samples in a lot of languages to help you get started in calling the library in your favorite language. The issue is it lacks some object oriented approach, and may be annoying to understand, as you will have to launch their sample application to get the help text, and check the list of properties you can read, and the corresponding descriptions.

ZM Media Info will wrap all calls to the MediaInfo library and put every file details into POCO classes to provide easy access using object properties, and provide descriptions with Visual Studio's intellisense. The source code package includes a project named "ZM.Media.Info.WinTest" which is a sample test application.

## Ease of use:
1. Add ZM.Media.Info.dll reference to your project
2. Add MediaInfo.dll downloaded from the mediaarea.net website in the binary's directory (Debug or Release)
3. Add following code to your application :
```cs
	var MI = new ZM.Media.Info.MediaInfoWrapper();
	var mfi = MI.Read("MyFile.mkv");
	txtDetails.Text = mfi.Display().Replace("\n", "\r\n");

```

The Read() method called on the wrapper will return an object of type "MediaFileInfo".

The Display() method called on the MediaFileInfo object will return a string listing all read properties except blank properties. Call Display(true) to list all properties, including blank ones. The returned string uses newline character as a line separator, therefore you can call .Replace("\n", "\r\n") on the result to have the text display properly in regular windows textbox.

MediaFileInfo object contains the following properties:
1. GeneralInfo: contains general details on the file
2. VideoStreams: contains a list of VideoStream objects, each for a video stream found in the file.
3. AudioStreams: contains a list of AudioStream objects, each for an audio stream found in the file.
4. TextStreams: contains a list of TextStream objects, each for a text stream found in the file.
5. MenuStreams: contains a list of MenuStream objects, each for a menu stream found in the file.

Simply check all these objects properties to get the data from the file.

## Code construction

VideoStream, AudioStream, TextStream and MenuStream objects are built on a hierarchy basis using MediaStream and Stream objects. GeneralInfo is built on a hierarchy to Stream object.

This allows common properties to be declared only once. Here is the class diagram:

![Class Diagram](https://raw.githubusercontent.com/sierramike/ZM.Media.Info/master/classdiagram.PNG)