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

Not all available properties have been implemented yet, but only a most current set that were needed by the author. You can Fork this project to add more properties to be retrieved from the MediaInfo library and even send back your additions so they can be added to this project.

To simplify properties additions, the wrapping has been implemented using reflexion. That means, just add a property to an object and the wrapper will map it to the MediaInfo library's corresponding property that has the same name. When the MediaInfo property name is incompatible with .NET property naming, you can decorate the property using the "MediaInfoFieldName" attribute, like the following code sample:
```cs
    [MediaInfoFieldName("Channel(s)")]
    public string Channels { get; set; }
```
This will wrap the "Channel(s)" property of the MediaInfo library to the Channels POCO class property.

You can even add your own calculated (or not) properties and avoid them for being mapped, using the "Ignore" attribure, like in the following code sample:
```cs
    [IgnoreProperty]
    public TimeSpan DurationSpan
    {
        get { return new TimeSpan(TimeSpan.TicksPerMillisecond * (long)Duration); }
    }
```

Some properties may report more than one value (e.g. on DTS Master Audio streams, BitRate property may report 2 or 3 values for Core, MA, ES streams etc.), therefore you can declare a propery as an array, and use the "MediaInfoFieldName" attribute to link it to the right property. The wrapper will automatically detect the array property and split the MediaInfo result on the slash '/' character to build the array. The following code sample shows how you can have the raw BitRate property and another property declared as an array of int:
```cs
    public string BitRate { get; set; }

    [MediaInfoFieldName("BitRate")]
    public long[] BitRateArr { get; set; }
```

As a naming convention, it has been decided that array properties are named according to the original property with the "Arr" suffix, like in the last code sample.

The MediaInfo library returns all properties as strings. If you declare a property as an int, long or double, the wrapper will automatically convert the resulting value in the type you chose. Be carefull as some properties may not always return a single number, as in the BitRate sample, where you can find several values separated by a slash.