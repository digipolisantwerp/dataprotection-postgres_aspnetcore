The purpose of this project is to generate the database migrations.

Because the dotnet cli extension for ef is not working on class libraries this app project is serving as entry point for the ef cli tool.

The DataContext.cs and model files are imported into this library using the project.json compile includefile section.