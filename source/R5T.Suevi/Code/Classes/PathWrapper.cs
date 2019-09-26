using System;
using System.IO;


namespace R5T.Suevi
{
    public static class PathWrapper
    {
        /// <summary>
        /// Returns the <see cref="Path.AltDirectorySeparatorChar"/> value.
        /// A directory separator separates directory names in a hierarchical path.
        /// From among the choices of slash ('/') or back-slash ('\'), the alternative directory separator provides the other separator relative to the separator used by the current machine environment.
        /// This alternate separator is '/' on Windows.
        /// Example path using the alternative directory separator (relative to Windows): "/mnt/efs/temp.txt".
        /// </summary>
        public static readonly char AltDirectorySeparatorChar = Path.AltDirectorySeparatorChar;
        /// <summary>
        /// Returns the <see cref="Path.DirectorySeparatorChar"/> value.
        /// A directory separator separates directory names in a hierarchical path.
        /// From among the choices of slash ('/') or back-slash ('\'), the directory separator provides the separator used by the current machine environment. This is '\' on Windows.
        /// Example path using the directory separator (relative to Windows): "C:\temp\temp1\temp2\temp.txt".
        /// </summary>
        public static readonly char DirectorySeparatorChar = Path.DirectorySeparatorChar;
        /// <summary>
        /// Provides the path characters that are invalid in the current machine environment.
        /// This is obsolete, and has been replaced with <see cref="PathWrapper.GetInvalidPathChars"/> (for paths) or <see cref="PathWrapper.GetInvalidFileNameChars"/> (for file names).
        /// </summary>
        [Obsolete("Use PathWrapper.GetInvalidPathChars() or PathWrapper.GetInvalidFileNameChars() instead.")]
        public static readonly char[] InvalidPathCharsSystem = Path.InvalidPathChars;
        /// <summary>
        /// Returns the <see cref="Path.PathSeparator"/> value.
        /// Separates path values, for example in the PATH environment variable value. Generally ';' on Windows.
        /// Example: "C:\temp1;C:\temp2".
        /// </summary>
        public static readonly char PathSeparator = Path.PathSeparator;
        /// <summary>
        /// Returns the <see cref="Path.VolumeSeparatorChar"/> value.
        /// Separates the drive (or volume) token from the path. Generally ':' on Windows.
        /// Example: "C:\temp\temp.txt".
        /// </summary>
        public static readonly char VolumeSeparatorChar = Path.VolumeSeparatorChar;


        /// <summary>
        /// Wraps <see cref="Path.ChangeExtension(string, string)"/>. Changes the extension of the input file path.
        /// Example: ("C:\temp\temp.txt", "jpg") -> "C:\temp\temp.jpg"
        /// The file extension separator can be included, example: ("C:\temp\temp.txt", ".jpeg") -> "C:\temp\temp.jpeg"
        /// </summary>
        public static string ChangeExtension(string filePath, string extension)
        {
            var output = Path.ChangeExtension(filePath, extension);
            return output;
        }

        /// <summary>
        /// Wraps <see cref="Path.Combine(string[])"/>. Combines path segments into a single path.
        /// Example: (@"C:\", @"temp", @"temp.txt") -> "C:\temp\temp.txt".
        /// The method is broken and limited.
        /// Broken in that if any of the path segments startup with the platform directory separator, segments before them are ignored!
        /// Limited in that the executing platform directory separator will be used with no possibility of overloading to allow, for example, creating Linux paths on a Windows machine or vice-versa.
        /// </summary>
        /// <remarks>
        /// For best results, make sure path segments do not start with the platform separator:
        /// Example: (@"C:\", @"\temp", @"temp.txt") -> "\temp\temp.txt".
        /// Example: (@"C:\", @"\temp", @"\temp.txt") -> "\temp.txt".
        /// 
        /// Uses current platform path separator with no overload to specify the path separator. This is a prolematic limitation.
        /// Example: (@"/mnt", @"efs", @"temp.txt") -> "/mnt\efs\temp.txt".
        /// </remarks>
        public static string Combine(params string[] pathSegments)
        {
            var output = Path.Combine(pathSegments);
            return output;
        }

        /// <summary>
        /// Wraps <see cref="Path.GetDirectoryName(string)"/>. Returns full directory path.
        /// This System method is mis-named. It should be GetDirectoryPath().
        /// Example: (@"C:\temp\temp.txt") -> "C:\temp".
        /// The returned string will use the current platform path separator, and if a directory path is given (path ends with a directory separator), the returned path will lack the path separator. 
        /// </summary>
        /// <remarks>
        /// Example: (@"C:\temp\temp") -> "C:\temp".
        /// Example: (@"C:\temp\temp\") -> "C:\temp\temp".
        /// Example: (@"/mnt/efs/temp.txt") -> "\mnt\efs" (on Windows).
        /// Example: (@"/mnt/efs/temp/") -> "\mnt\efs\temp" (on Windows).
        /// </remarks>
        public static string GetDirectoryName(string path)
        {
            var output = Path.GetDirectoryName(path);
            return output;
        }

        /// <summary>
        /// Wraps <see cref="Path.GetExtension(string)"/>. Returns the extension of the input file path.
        /// Example: (@"C:\temp\temp.txt") -> ".txt".
        /// Includes the file extension separator char '.', and does not change any capitalization.
        /// </summary>
        public static string GetExtension(string filePath)
        {
            var output = Path.GetExtension(filePath);
            return output;
        }

        /// <summary>
        /// Wraps <see cref="Path.GetFileName(string)"/>. Returns the file-name and extension of the input file path.
        /// Example: (@"C:\temp\temp.txt") -> "temp.txt".
        /// This method works the way it should.
        /// </summary>
        /// <remarks>
        /// Example: (@"/mnt/efs/temp.txt") -> "temp.txt".
        /// </remarks>
        public static string GetFileName(string filePath)
        {
            var output = Path.GetFileName(filePath);
            return output;
        }

        /// <summary>
        /// Wraps <see cref="Path.GetFileNameWithoutExtension(string)"/>. Returns only the file-name (without file extension or file extension separator).
        /// Example: (@"C:\temp\temp.txt") -> "temp".
        /// This method works the way it should.
        /// </summary>
        /// <remarks>
        /// Example: (@"/mnt/efs/temp.txt") -> "temp".
        /// </remarks>
        public static string GetFileNameWithoutExtension(string filePath)
        {
            var output = Path.GetFileNameWithoutExtension(filePath);
            return output;
        }

        /// <summary>
        /// Wraps <see cref="Path.GetFullPath(string)"/>. Prefixes the input path with either the current directory, or the current root drive (if the input starts with a path separator).
        /// Example: (@"temp.txt") -> "{Current Directory}\temp.txt".
        /// Example: (@"\temp.txt") -> "C:\temp.txt".
        /// A weird method with schizophrenic behavior based on whether the input begins with a path separator.
        /// </summary>
        /// <remarks>
        /// Example: (@"/temp.txt") -> "C:\temp.txt" (on Windows).
        /// </remarks>
        public static string GetFullPath(string path)
        {
            var output = Path.GetFullPath(path);
            return output;
        }

        /// <summary>
        /// Wraps <see cref="Path.GetInvalidFileNameChars"/>. Returns the chars that are invalid in file-names on the currently executing platform.
        /// Example: () -> ",&lt;,&gt;,|,:,*,?,\,/ on Windows, plus ~35 that are not printable in the console window.
        /// This method should have overload that allows inputting a platform to get invalid file name characters for that platform.
        /// </summary>
        public static char[] GetInvalidFileNameChars()
        {
            var output = Path.GetInvalidFileNameChars();
            return output;
        }

        /// <summary>
        /// Wraps <see cref="Path.GetInvalidPathChars"/>. Returns the chars that are invalid in paths on the currently executing platform.
        /// Example: () -> |, plus ~35 that are not printable in the console window.
        /// This method should have overload that allows inputting a platform to get invalid path characters for that platform.
        /// </summary>
        public static char[] GetInvalidPathChars()
        {
            var output = Path.GetInvalidPathChars();
            return output;
        }

        /// <summary>
        /// Wraps <see cref="Path.GetPathRoot(string)"/>. Returns just the root of a path (generally a drive letter).
        /// Example: (@"C:\temp\temp.txt") -> "C:\".
        /// Does not work with non-Windows paths, at least when the currently executing platform is Windows.
        /// </summary>
        /// <remarks>
        /// Example: (@"/mnt/efs/temp.txt") -> "\".
        /// </remarks>
        public static string GetPathRoot(string path)
        {
            var output = Path.GetPathRoot(path);
            return output;
        }

        /// <summary>
        /// Wraps <see cref="Path.GetRandomFileName()"/>. Returns a cryptographically-secure path segment like 'ulcdtig4.v53'
        /// Example () -> "ulcdtig4.v53".
        /// What's with the '.'? The function seems to be trying to produce a file-name with a random file extension. Instead it should produce a random path segment with no file extension separator.
        /// </summary>
        public static string GetRandomFileName()
        {
            var output = Path.GetRandomFileName();
            return output;
        }

        // Somehow the System.IO.Path in the System.Runtime.Extensions assembly is different than the System.IO.Path in the netstandard assembly?
        // See: https://github.com/dotnet/standard/issues/962
        // Issue is .NET Core 2.1 (class library) has the new methods, while .NET Standard 2.0 does not. Note that .NET Standard 2.1 will have the methods, but it is not yet out!
        //public static string GetRelativePathSystem(string source, string path)
        //{
        //    var output = Path.GetRelativePath(relativeTo, path);
        //    return output;
        //}

        /// <summary>
        /// Wraps <see cref="Path.GetTempFileName()"/>. Returns the path to an actually created temporary file, with a temporary file name, in the %TEMP% directory (../{User}/AppData/Local/Temp).
        /// Example: () -> "C:\Users\david\AppData\Local\Temp\tmpB013.tmp"
        /// This method is a disaster. 1) It should *NOT* create 0 KB file at the given path! 2) it should just return the temp file-name ("tmpB013.tmp"), not the whole path. 3) It should include an -Path() method that takes a directory path to which to add the temp file-name.
        /// </summary>
        public static string GetTempFileName()
        {
            var output = Path.GetTempFileName();
            return output;
        }

        /// <summary>
        /// Wraps <see cref="Path.GetTempPath()"/>. Returns the directory-path of the current user's Temp directory.
        /// Example: () -> C:\Users\david\AppData\Local\Temp\
        /// This method should be called "GetTempDirectoryPath", but otherwise works as advertised.
        /// </summary>
        public static string GetTempPath()
        {
            var output = Path.GetTempPath();
            return output;
        }

        /// <summary>
        /// Wraps <see cref="Path.HasExtension(string)"/>.
        /// Litterally just returns whether or not the given path has a file extension (this is useful in determining whether a path should be assumed to be a file-path or a directory-path).
        /// Example: ("C:\temp\temp.txt") -> True.
        /// Example: ("C:\temp\temp") -> False.
        /// This method should have an overload that allows testing whether a path has a specific file-extension.
        /// </summary>
        /// <remarks>
        /// Example: ("C:\temp\temp.") -> False.
        /// </remarks>
        public static bool HasExtension(string path)
        {
            var output = Path.HasExtension(path);
            return output;
        }

        // Somehow the System.IO.Path in the System.Runtime.Extensions assembly is different than the System.IO.Path in the netstandard assembly?
        ///// <summary>
        ///// Returns whether the path is a Windows path and fully qualified.
        ///// Example: ("C:\temp\temp.txt") -> True.
        ///// This method does not work with non-Windows paths.
        ///// </summary>
        ///// <remarks>
        ///// Example: ("/mnt/efs/temp.txt") -> False (wrongly).
        ///// </remarks>
        //public static void IsPathFullyQualifiedSystem(string path)
        //{
        //    var output = Path.IsPathFullyQualified
        //}

        /// <summary>
        /// Wraps <see cref="Path.IsPathRooted(string)"/>.
        /// Returns whether the path starts with a Windows root drive, or with a non-Windows path separator.
        /// Example: ("C:\temp\temp.txt") -> True.
        /// Example: ("/mnt/efs/temp.txt") -> True.
        /// This method works well.
        /// </summary>
        public static bool IsPathRooted(string path)
        {
            var output = Path.IsPathRooted(path);
            return output;
        }
    }
}
