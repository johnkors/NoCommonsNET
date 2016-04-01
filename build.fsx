#r @"tools/packages/FAKE/tools/FakeLib.dll"
open Fake
open Fake.AssemblyInfoFile
open Fake.Testing.NUnit3

let assemblyVersion = getBuildParamOrDefault "version" "0.1"
let nugetVersion = assemblyVersion + "-alfa0002"
let packageOutputDir = "./.output"
let nugetApiKey = environVarOrDefault "NugetOrgApiKey" "ENV-VARIABLE NugetOrgApiKey is missing"
let project = "./NoCommons/NoCommons.csproj"
let sln = "./NoCommons.NET.sln"
let buildOutput = "./.build"



Target "Clean" (fun() ->
    CleanDirs [buildOutput; packageOutputDir]
)


Target "UpdateAssemblyInfo"(fun _ -> 
    ReplaceAssemblyInfoVersions (fun p -> 
        {
            p with 
                OutputFileName = "./NoCommons/Properties/AssemblyInfo.cs"
                AssemblyFileVersion = assemblyVersion
                AssemblyVersion = assemblyVersion
        }) 
)

Target "NugetRestore" (fun _ -> 
    RestorePackages() 
)


Target "Build" (fun _ ->
    MSBuildRelease buildOutput "Rebuild" [sln] |> ignore 
)

Target "Test" (fun _ ->
    !! ".build/*.Tests.dll"
      |> NUnit3 (fun p ->
          {  p with
                WorkingDir = "./"                                             
          })     
)

Target "CreatePackage" (fun _ ->
    MSBuildDebug "" "Rebuild" [sln] |> ignore    
    NuGet (fun p -> 
        {p with            
            WorkingDir = "./tools/nuget"
            Authors = ["John Korsnes"]
            Project = "NoCommons"            
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
    ==> "NugetRestore"
    ==> "Build"
    ==> "Test"
    ==> "CreatePackage"


RunTargetOrDefault "CreatePackage"