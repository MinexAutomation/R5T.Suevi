# R5T.Suevi
A NuGet packaged library containing a critique of the System.IO.Path functionality.

The [`System.IO.Path`](https://docs.microsoft.com/en-us/dotnet/api/system.io.path?view=netcore-2.2) static class is useful, but limited. For example, all methods will treat a directory path as a file path if it does not end with a directory separator. In retrospect this makes sense, since on a string-level of abstraction there is no way to tell a directory path from a file path, but it's easy to get tripped up (especially when dealing with relative path fragments) and find that *yet again* the source of a bug is a snarled path because a directory was treated like a file somewhere.

Some methods are misleadingly named, such as `Path.GetDirectoryName()`, which returns the directory path not the directory name, and `Path.GetTempFileName()` returns the file path, not just a short-and-sweet file name. Not just that, it will also actually **create** a file on disk! Hopefully you remember to delete all of these one day...

Most egregious, the fundamental `Path.Combine()` method, like all `System.IO.Path` methods, will use the current machine environment's directory separator with no option to override. Let's say you combine some non-Windows path parts all of which use the non-Windows directory separator. Well, the resulting path will use the Windows directory separator if run on a Windows machine, or the non-Windows directory separator if run on a non-Windows machine! This can be a big problem for code developed on Windows, but run in production on Linux (say, using AWS EC2). There is no way to specify whether you want a Windows or non-Windows path, and it's just left up to you to always remember that you need to string-replace the current machine environment's directory separator with the desired environment directory separator at every code site that may or may not run in production. Blargh!

As a final kick in the teeth, `Path.Combine()` will just ignore all previous path segments if a rooted path segment is (most likely accidentally) found. Surprise! This makes working with relative path segments fraught with danger.

# Improvements
Improvements to the `System.IO.Path` functionality include:

- This `R5T.Suevi` Nuget-package library to create a `System.IO.Path` wrapper with better documentation, including precise explanations of the gotchas and drawbacks of the underlying `System.IO.Path` methods.

- The [`R5T.Lombardy`](https://github.com/MinexAutomation/R5T.Lombardy) endeavor to create a NuGet-packaged path operator implementation and test-fixture for working with stringly-typed paths.

- The [`R5T.NetStandard.IO.Paths.Types`](https://github.com/MinexAutomation/R5T.NetStandard.IO.Paths.Types) project to create strongly-typed paths and utility methods for working with them (as well as utility methods for working with stringly-typed paths).

- The [`R5T.NetStandard.IO.Paths`](https://github.com/MinexAutomation/R5T.NetStandard.IO.Paths) project that includes further utilities for working with strongly-typed paths.
