using System;
using System.Collections.Generic;
using System.Linq;
using Nuke.Common;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitVersion;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[CheckBuildProjectConfigurations]
[UnsetVisualStudioEnvironmentVariables]
[GitHubActions("Build", GitHubActionsImage.UbuntuLatest, AutoGenerate = true, On = new GitHubActionsTrigger[]
    {
        GitHubActionsTrigger.Push
    })]
class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main () => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly string Configuration = IsLocalBuild ? "Debug" : "Release";

    string Version = "1.0.0";

    [Solution] readonly Solution Solution;
    [GitRepository] readonly GitRepository GitRepository;

    AbsolutePath SourceDirectory => RootDirectory / "source";

    AbsolutePath NumberSystemProject = RootDirectory / "source" / "GenericNumberSystem" / "GenericNumberSystem.csproj";
    AbsolutePath NumberSystemAbstractionsProject = RootDirectory / "source" / "GenericNumberSystem.Abstractions" / "GenericNumberSystem.Abstractions.csproj";

    AbsolutePath TestsDirectory => RootDirectory / "tests";
    AbsolutePath OutputDirectory => RootDirectory / "output";

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            SourceDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
            TestsDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
            EnsureCleanDirectory(OutputDirectory);
        });

    Target Restore => _ => _
        .Executes(() =>
        {
            DotNetRestore(_ => _
                .SetProjectFile(NumberSystemProject));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            var packages = new List<string>
            {
                NumberSystemProject,
                NumberSystemAbstractionsProject
            };

            foreach (var package in packages)
            {
                DotNetPack(_ => _
                    .SetProject(package)
                    .SetOutputDirectory(OutputDirectory)
                    .SetConfiguration(Configuration)
                    .SetAssemblyVersion(Version)
                    .SetFileVersion(Version)
                    .SetInformationalVersion($"{Version}_{GitRepository.Branch}_{GitRepository.Commit}")
                    .SetVersion(Version)
                    .SetAuthors("Peter Zbinden")
                    .SetPackageProjectUrl("https://github.com/PeterZbinden/GenericNumberSystem")
                    .SetRepositoryUrl("https://github.com/PeterZbinden/GenericNumberSystem")
                    .EnableNoRestore());
            }
        });

}
