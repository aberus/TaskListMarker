using Mono.Addins;
using Mono.Addins.Description;

[assembly: Addin(
    "TaskListMarker",
    Namespace = "Aberus.TaskListMarker",
    Version = "0.1"
)]

[assembly: AddinName("Task List Marker")]
[assembly: AddinCategory("IDE extensions")][assembly: AddinDescription("Marks all Task (ToDo statements) from Task List in the Visual Studio editor window")][assembly: AddinAuthor("Aleksander Berus")][assembly: AddinUrl("https://github.com/aberus/TaskListMarker")]
