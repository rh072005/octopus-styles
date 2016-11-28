///////////////////////////////////////////////////////////////////////////////
// Tools and Addins
///////////////////////////////////////////////////////////////////////////////
#tool "GitVersion.CommandLine"
#addin nuget:?package=Cake.Git

///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var versionType = Argument("VersionType", "patch");
var buildFolder = MakeAbsolute(Directory(Argument("buildFolder", "./build")));
var artifacts = MakeAbsolute(Directory(Argument("artifactPath", "./artifacts")));

var versionInfo = GitVersion(new GitVersionSettings { UpdateAssemblyInfo = true });

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(ctx =>
{
	// Executed BEFORE the first task.
	Information("Tribble Cake");
});

Teardown(ctx =>
{
	// Executed AFTER the last task.
	Information("Finished running tasks.");
});

///////////////////////////////////////////////////////////////////////////////
// SYSTEM TASKS
///////////////////////////////////////////////////////////////////////////////

Task("Version")
	.Does(() =>
	{
		var semVersion = "";
		int major = 0;
		int minor = 1;
		int patch = 0;
		GitVersion assertedVersions = GitVersion(new GitVersionSettings
		{
			OutputType = GitVersionOutput.Json,
		});
		major = assertedVersions.Major;
		minor = assertedVersions.Minor;
		patch = assertedVersions.Patch;
		switch (versionType.ToLower())
		{
			case "patch":
				patch += 1; break;
			case "minor":
				minor += 1; patch = 0; break;
			case "major":
				major += 1;	minor = 0; patch = 0; break;			
		};
		semVersion = string.Format("{0}.{1}.{2}", major, minor, patch);
		GitTag(".", semVersion);
		Information("Changing version: {0} to {1}", assertedVersions.LegacySemVerPadded, semVersion);
	});

///////////////////////////////////////////////////////////////////////////////
// USER TASKS
// PUT ALL YOUR BUILD GOODNESS IN HERE
///////////////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
	{
		CleanDirectory(artifacts);
		CleanDirectory(buildFolder);
	});
	
Task("Restore")
	.IsDependentOn("Clean").Does(() =>
	{
		NuGetRestore("./src/OctopusStyles.sln");
	});

Task("Build")
	.IsDependentOn("Restore")
	.Does(() =>
	{
		Information("Running Build...");
		if(IsRunningOnWindows())
		{
		  MSBuild("./src/OctopusStyles.sln", settings => settings
			.WithProperty("OutDir", buildFolder.ToString())
			.SetVerbosity(Verbosity.Minimal)
			.SetConfiguration(configuration)
			.WithTarget("Rebuild")
		  );
		}
	});

Task("Package")
	.IsDependentOn("Build")
	.Does(() =>
	{
		Information("Running Packaging...");
		CopyFileToDirectory(string.Format("{0}\\{1}", buildFolder, "OctopusStyles.dll"), artifacts);
	});
	
Task("Default")
    .IsDependentOn("Package");

RunTarget(target);