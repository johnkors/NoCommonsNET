#r @"tools/packages/FAKE/tools/FakeLib.dll"
open Fake
open Fake.AssemblyInfoFile

let assemblyVersion = getBuildParamOrDefault "version" "0.3"
let nugetVersion = assemblyVersion + "-alfa0001"
let packageOutputDir = "./output"
let nugetApiKey = environVarOrDefault "nuget-api-key" "ENV-VARIABLE nuget-api-key is missing"

Target "Clean" (fun() ->
    CleanDirs [packageOutputDir]
)

Target "UpdateAssemblyInfo"(fun _ -> 
    trace "TODO"
)

Target "CreatePackage" (fun _ ->
    
    NuGet (fun p -> 
        {p with
            WorkingDir = "./tools/nuget/" 
            Authors = ["John Korsnes"]
            Project = "NoCommons.NET"
            Description = "stuff"
            OutputPath = packageOutputDir
            Version = nugetVersion
            AccessKey = "My-API-Key-In-NuGet"
            Publish = false }) 
        "./NoCommons/NoCommons.csproj"
)

Target "Nothing" (fun x -> trace "Try a defined target")

"Clean"
    ==> "UpdateAssemblyInfo"
    ==> "CreatePackage"


RunTargetOrDefault "CreatePackage"