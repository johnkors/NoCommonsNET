#r @"tools/packages/FAKE/tools/FakeLib.dll"
open Fake
open Fake.AssemblyInfoFile

let assemblyVersion = getBuildParamOrDefault "version" "0.1"
let nugetVersion = assemblyVersion + "-alfa0002"
let packageOutputDir = "./.output"
let nugetApiKey = environVarOrDefault "NugetOrgApiKey" "ENV-VARIABLE NugetOrgApiKey is missing"
let project = "./NoCommons/NoCommons.csproj"
let buildOutput = "./.build"

Target "Clean" (fun() ->
    CleanDirs [buildOutput; packageOutputDir]
)

Target "UpdateAssemblyInfo"(fun _ -> 
    CreateCSharpAssemblyInfo "./NoCommons/Properties/AssemblyInfo.cs"
        [ Attribute.Version assemblyVersion
          Attribute.FileVersion assemblyVersion]
)

Target "Build" (fun _ ->
    MSBuildRelease buildOutput "Rebuild" [project] |> ignore 
)

Target "CreatePackage" (fun _ ->
    
    NuGet (fun p -> 
        {p with            
            WorkingDir = "./tools/nuget/" 
            Authors = ["John Korsnes"]
            Project = "NoCommons"
            Description = "stuff"
            OutputPath = packageOutputDir
            Version = nugetVersion
            AccessKey = nugetApiKey
            Publish = false
            }) 
        project
)

Target "Nothing" (fun x -> trace "Try a defined target")

"Clean"
    ==> "UpdateAssemblyInfo"
    ==> "Build"
    ==> "CreatePackage"


RunTargetOrDefault "CreatePackage"