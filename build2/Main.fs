module Build.Main

open Build.FableLibrary

// This is a basic help message, as the CLI parser is not a "real" CLI parser
// For now, it is enough as this is just a dev tool
let printHelp () =
    let helpText =
        """
Usage: dotnet run <command> [<args>]

Available commands:
    fable-library

        Options:
            --javascript        Build fable-library for JavaScript
            --typescript        Build fable-library for TypeScript
            --python            Build fable-library for Python
            --dart              Build fable-library for Dart
            --rust              Build fable-library for Rust

    tests
        Subcommands:
            javascript          Run the tests for JavaScript
            typescript          Run the tests for TypeScript
            python              Run the tests for Python
            dart                Run the tests for Dart
            rust                Run the tests for Rust

        Options for all:
            --watch
            --fast
            --no-dotnet         When in watch mode, do not run the .NET tests

        Options for JavaScript:
            --reat-only         Run only the tests for React (can be run in watch mode)

        Options for Rust:
            --ast-only          Run only the tests for the AST (can be run in watch mode)
            --noStd             Compile and run the tests without the standard library
            --threaded          Compile and run the tests with the threaded runtime
        """

    printfn "%s" helpText

[<EntryPoint>]
let main argv =
    let argv = argv |> Array.map (fun x -> x.ToLower()) |> Array.toList

    match argv with
    | "fable-library" :: args ->
        match args with
        | "--javascript" :: _ -> BuildFableLibraryJavaScript().Run()
        | "--typescript" :: _ -> BuildFableLibraryTypeScript().Run()
        | "--python" :: _ -> BuildFableLibraryPython().Run()
        | "--dart" :: _ -> BuildFableLibraryDart().Run()
        | "--rust" :: _ -> BuildFableLibraryRust().Run()
        | _ -> printHelp ()
    | "tests" :: args ->
        match args with
        | "javascript" :: args -> Tests.JavaScript.handle args
        | "typescript" :: args -> Tests.TypeScript.handle args
        | "python" :: args -> Tests.Python.handle args
        | "dart" :: args -> Tests.Dart.handle args
        | "rust" :: args -> Tests.Rust.handle args
        | _ -> printHelp ()

    | "--help" :: _
    | _ -> printHelp ()

    0
