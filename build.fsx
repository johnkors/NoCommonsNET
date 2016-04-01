#r @"tools/packages/FAKE/tools/FakeLib.dll"
open Fake
open Fake.AssemblyInfoFile
open Fake.Testing.NUnit3

let assemblyVersion = getBuildParamOrDefault "version" "0.1.0"
let nugetVersion = assemblyVersion + "-alfa0003"
let packageOutputDir = "./.output"

let nugetApiKey = environVarOrDefault "NugetOrgApiKey" "ENV-VARIABLE NugetOrgApiKey is missing"
let project = "./NoCommons/NoCommons.csproj"
let nuspec = "./NoCommons/NoCommons.nuspec"
let sln = "./NoCommons.NET.sln"

let buildOutputTest = "./.testbuild"

Target "Clean" (fun() ->
    CleanDirs [buildOutputTest; packageOutputDir]
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

Target "BuildTest" (fun _ ->
    MSBuildRelease buildOutputTest "Clean;Rebuild" [sln] |> ignore 
)

Target "Test" (fun _ ->
    !! ".testbuild/*.Tests.dll"
      |> NUnit3 (fun p ->
          {  p with
                WorkingDir = "./"                                             
          })     
)

Target "Buildpackage" (fun _ -> 
    MSBuildRelease "" "Build" [project] |> ignore    
)

Target "CreatePackage" (fun _ ->
    project |>         
        NuGet (fun p -> 
        {p with            
            WorkingDir = "./"
            Authors = ["John Korsnes"]
            Project = "NoCommons"
            Title = "NoCommons.NET"                                    
            OutputPath = packageOutputDir
            Version = nugetVersion
            AccessKey = nugetApiKey
            Publish = false
            Properties = [ ("Configuration", "Release") ]    
            })
        
        
)

Target "Nothing" (fun x -> trace "Try a defined target")

"Clean"    
    ==> "NugetRestore"
    ==> "BuildTest"
    ==> "Test"
    ==> "UpdateAssemblyInfo"    
    ==> "Buildpackage"
    ==> "CreatePackage"


RunTargetOrDefault "Test"