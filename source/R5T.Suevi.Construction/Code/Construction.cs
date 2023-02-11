using System;
using System.IO;

using R5T.Salamis;


namespace R5T.Suevi.Construction
{
    public static class Construction
    {
        public static void SubMain()
        {
            Construction.TryDoubleDirectorySeparators();
        }

        /// <summary>
        /// What happens if a path ends with a directory separator?
        /// Result: Ok.
        /// "C:\Directory1\" + "Directory1" => "C:\Directory1\Directory1".
        /// </summary>
        private static void TryDoubleDirectorySeparators()
        {
            var pathPart1 = ExampleDirectoryPaths.WindowsDirectory01Path;
            var pathPart2 = ExampleDirectoryNames.Directory01;

            var combinedPath = Path.Combine(pathPart1, pathPart2);

            var line = $"{pathPart1}\n+ {pathPart2} =>\n{combinedPath}";

            Console.WriteLine(line);
        }
    }
}
