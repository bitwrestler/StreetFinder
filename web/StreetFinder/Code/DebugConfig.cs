namespace ers_config
{
    public static class DebugConfig
    {
        public static string FindAzureConfigInParents(string configFile)
        {
            const string search_path = "azure";
            string? start = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetCallingAssembly().Location);
            string? origStart = start;
            if (start is null)
                throw new ArgumentException("Can not get file location of Calling Assmebly");

            DirectoryInfo? pdir;
            while ((pdir = Directory.GetParent(start)) != null)
            {
                var trydir = Path.Combine(pdir.FullName, search_path);
                if (Directory.Exists(trydir))
                {
                    var test_file = Path.Combine(trydir, configFile);
                    if(File.Exists(test_file))
                    {
                        return test_file;
                    }
                }
                start = pdir.FullName;
            }
            throw new Exception($"Can not find config file '{configFile}' in any directory named '{search_path}' in any arent above '{origStart}'");
        }

        public static void ProcessDotEnvFile(string path)
        {
           foreach(var aenv in File.ReadAllLines(path))
            {
                var delimPos = aenv.IndexOf('=');
                if (delimPos == -1)
                    throw new Exception($".env file '{path}' has a corrupted line");
                var key = aenv.Substring(0, delimPos);
                var val = aenv.Substring(delimPos + 1);
                System.Environment.SetEnvironmentVariable(key, val);
            }
        }
    }
}
