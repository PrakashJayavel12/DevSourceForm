using SevenZip;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace devSourceApp
{
    public  class builderMethods
    {

        //public void CopyDirectory(string sourceDir, string destinationDir)
        //{
        //    Directory.CreateDirectory(destinationDir);

        //    // Copy files
        //    foreach (var file in Directory.GetFiles(sourceDir))
        //    {
        //        string destFile = Path.Combine(destinationDir, Path.GetFileName(file));
        //        File.Copy(file, destFile, true);
        //    }

        //    // Copy subdirectories recursively
        //    foreach (var dir in Directory.GetDirectories(sourceDir))
        //    {
        //        string destSubDir = Path.Combine(destinationDir, Path.GetFileName(dir));
        //        CopyDirectory(dir, destSubDir);
        //    }
        //}


        public static string CopyFoldersFromNearestNamedAncestor(string[] files, string anchorFolderName, string destRoot)
        {
            var copiedTargets = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            string destFolder = "";
            foreach (var file in files)
            {
                string sourceFolder = Path.GetDirectoryName(file);
                if (string.IsNullOrEmpty(sourceFolder) || !Directory.Exists(sourceFolder))
                {
                    Console.WriteLine($"Source folder not found for file: {file}");
                    continue;
                }

                // climb up to find the nearest folder named anchorFolderName
                DirectoryInfo cur = new DirectoryInfo(sourceFolder);
                DirectoryInfo anchor = null;
                while (cur != null)
                {
                    if (cur.Name.Equals(anchorFolderName, StringComparison.OrdinalIgnoreCase))
                    {
                        anchor = cur;
                        break;
                    }
                    cur = cur.Parent;
                }

                
                if (anchor == null)
                {
                    // anchor not found: fallback to copying the source folder itself under destRoot
                    destFolder = Path.Combine(destRoot, Path.GetFileName(sourceFolder));
                }
                else
                {
                    // relative path from the anchor to the source folder (e.g. "20-Sep-2025")
                    string rel = Path.GetRelativePath(anchor.FullName, sourceFolder);
                    destFolder = Path.Combine(destRoot, anchor.Name, rel); // destRoot\Tasva_Web_V4\20-Sep-2025
                }

                // avoid copying same target twice
                if (!copiedTargets.Add(destFolder))
                {
                    Console.WriteLine($"Already copied: {destFolder}");
                    continue;
                }

                CopyDirectory(sourceFolder, destFolder);
                Console.WriteLine($"Copied: {sourceFolder} -> {destFolder}");
            }
            return destFolder;
        }

        static void CopyDirectory(string sourceDir, string destinationDir)
        {
            Directory.CreateDirectory(destinationDir);

            foreach (var file in Directory.GetFiles(sourceDir))
            {
                string destFile = Path.Combine(destinationDir, Path.GetFileName(file));
                File.Copy(file, destFile, overwrite: true);
            }

            foreach (var dir in Directory.GetDirectories(sourceDir))
            {
                string destSubDir = Path.Combine(destinationDir, Path.GetFileName(dir));
                CopyDirectory(dir, destSubDir);
            }
        }
        public bool ZiptheFile(string finalOutPutZip, string lastFileName, string outDirFinal)
        {
            try
            {
                // Delete unwanted files
                List<string> filesToZip = new List<string>();
                string[] totalFiles = Directory.GetFiles(finalOutPutZip, "*");

                foreach (string fileName in totalFiles)
                {
                    string ofileName = Path.GetFileNameWithoutExtension(fileName);
                    if (ofileName.Contains("DCIA") || ofileName.Contains("Newton") || ofileName.Contains("ADSR"))
                    {
                        if (Path.GetFileName(fileName).Contains("dll"))

                        { filesToZip.Add(fileName); }
                        //  ModernMessageBox.ShowBox(fileName);
                    }
                }



                // Load 7z library (first one that exists)
                string[] libs = { "7z32.dll", "7z64.dll" };
                int count = 1;
                foreach (var lib in libs)
                {
                    if (count == 2)
                        return false;
                    try
                    {
                        string libPath = Path.Combine(
                            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? Environment.CurrentDirectory,
                            lib);

                        if (File.Exists(libPath))
                        {
                            SevenZipBase.SetLibraryPath(libPath);
                            //break;
                        }
                           
                        // Setup compressor
                        SevenZipCompressor compressor = new SevenZipCompressor
                        {
                            CompressionMethod = CompressionMethod.Lzma,
                            ArchiveFormat = OutArchiveFormat.Zip, // force zip
                            CompressionLevel = SevenZip.CompressionLevel.Normal,
                            FastCompression = false
                        };

                        // Final zip path
                        string zipPath = Path.Combine(outDirFinal, lastFileName + ".rar");

                        // Create ZIP
                        compressor.CompressFiles(zipPath, filesToZip.ToArray());
                        count++;
                        Console.WriteLine($"Zip created: {zipPath}");
                    }
                    catch (Exception ex) { Console.WriteLine(ex.ToString()); count = 0; }

                }
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while zipping: " + ex.Message);
                return false;
            }
        }

        public static void EnsureReleaseX86Config(string csprojPath)
        {
            //NEW
            XDocument doc = XDocument.Load(csprojPath);
            XNamespace ns = "http://schemas.microsoft.com/developer/msbuild/2003";

            // Add x86 to Project Platforms
            var platformGroup = doc.Root
                .Elements(ns + "PropertyGroup")
                .FirstOrDefault(e => e.Element(ns + "ProjectConfigurationPlatforms") != null);
            if (platformGroup == null)
            {
                platformGroup = new XElement(ns + "PropertyGroup",
                    new XElement(ns + "ProjectConfigurationPlatforms", "Debug|AnyCPU;Release|AnyCPU;Debug|x86;Release|x86"));
                doc.Root.Add(platformGroup);
                Console.WriteLine("✅ Added x86 to ProjectConfigurationPlatforms");
            }
            else
            {
                var platforms = platformGroup.Element(ns + "ProjectConfigurationPlatforms")?.Value;
                if (!platforms.Contains("x86"))
                {
                    platformGroup.Element(ns + "ProjectConfigurationPlatforms")?.SetValue(platforms + ";Debug|x86;Release|x86");
                    Console.WriteLine("✅ Updated ProjectConfigurationPlatforms with x86");
                }
            }

            // Ensure Release|x86 PropertyGroup
            var releaseX86Group = doc.Root
                .Elements(ns + "PropertyGroup")
                .FirstOrDefault(e => (string)e.Attribute("Condition") == "'$(Configuration)|$(Platform)' == 'Release|x86'");

            if (releaseX86Group == null)
            {
                XElement newGroup = new XElement(ns + "PropertyGroup",
                    new XAttribute("Condition", "'$(Configuration)|$(Platform)' == 'Release|x86'"),
                    new XElement(ns + "OutputPath", @"bin\x86\Release\"),
                    new XElement(ns + "DefineConstants", "TRACE"),
                    new XElement(ns + "Optimize", "true"),
                    new XElement(ns + "DebugType", "pdbonly"),
                    new XElement(ns + "PlatformTarget", "x86"),
                    new XElement(ns + "ErrorReport", "prompt"),
                    new XElement(ns + "CodeAnalysisRuleSet", "MinimumRecommendedRules.ruleset")
                );
                doc.Root.Add(newGroup);
                Console.WriteLine("✅ Added PropertyGroup for Release|x86");
            }
            else
            {
                Console.WriteLine("ℹ️ PropertyGroup Release|x86 already exists.");
            }

            // Save with explicit UTF-8 BOM and force flush
            using (var fs = new FileStream(csprojPath, FileMode.Create, FileAccess.Write, FileShare.None))
            using (var sw = new StreamWriter(fs, new UTF8Encoding(true)))
            {
                doc.Save(sw);
                sw.Flush();
                fs.Flush(true);
            }

            // Verify file content
            string content = File.ReadAllText(csprojPath);
            if (!content.Contains("'$(Configuration)|$(Platform)' == 'Release|x86'"))
            {
                Console.WriteLine("❌ ERROR: Release|x86 config not found in saved file!");
                throw new Exception("Failed to save x86 config.");
            }
            Console.WriteLine("✅ Verified: Release|x86 config exists in file.");

            Thread.Sleep(3000); // Wait for file system
            Console.WriteLine("💾 Project file patched, saved, and verified.");
        }

        public static void hintPathChange(string v)
        {
            try
            {
                DataSet dt = new DataSet();
                dt.ReadXml(v);

                foreach (DataRow dr in dt.Tables["Reference"].Rows)
                {
                    string? hintPath = dr["HintPath"].ToString();
                    string[] myArray = hintPath.Split('\\');
                    if (hintPath.Contains("Debug") || hintPath.Contains("dll"))
                    {
                        var last = myArray.Last();
                        dr["HintPath"] = "bin\\Debug\\" + last;
                    }
                }
                dt.WriteXml(v);
            }
            catch { }
        }

       public static void PatchAssemblyTitle(string projectPath, string suffix)
        {
            // Find AssemblyInfo.cs
            string assemblyInfo = Directory.GetFiles(Path.GetDirectoryName(projectPath),
                                                     "AssemblyInfo.cs",
                                                     SearchOption.AllDirectories)[0];

            string[] lines = File.ReadAllLines(assemblyInfo);
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Trim().StartsWith("[assembly: AssemblyTitle("))
                {
                    int start = lines[i].IndexOf("\"");
                    int end = lines[i].LastIndexOf("\"");
                    if (start >= 0 && end > start)
                    {
                        //string original = lines[i].Substring(start + 1, end - start - 1);
                        // string newValue = original.Split('_')[0] + "_" + suffix;

                        string fileName = Path.GetFileNameWithoutExtension(projectPath);
                        lines[i] = $"[assembly: AssemblyTitle(\"{fileName+"_" + suffix}\")]";
                    }
                }
            }
            File.WriteAllLines(assemblyInfo, lines);
        }


        // Find msbuild via 'where msbuild' or common locations
        public static string FindMSBuild()
        {
            try
            {
                var whereResult = RunProcessSync("where", "msbuild");
                if (!string.IsNullOrWhiteSpace(whereResult) && whereResult.IndexOf("Could not find", StringComparison.OrdinalIgnoreCase) < 0)
                {
                    // take first non-empty line
                    using (StringReader sr = new StringReader(whereResult))
                    {
                        string first = sr.ReadLine();
                        if (!string.IsNullOrWhiteSpace(first) && File.Exists(first.Trim()))
                            return first.Trim();
                    }
                }
            }
            catch { /* ignore */ }

            // Common candidate paths (not exhaustive)
            string windir = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            string programFilesX86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);

            string[] candidates = new[]
            {
                Path.Combine(windir, "Microsoft.NET", "Framework64", "v4.0.30319", "MSBuild.exe"),
                Path.Combine(windir, "Microsoft.NET", "Framework", "v4.0.30319", "MSBuild.exe"),
                // VS 2015/2017/2019/2022 typical locations (these may vary by edition and install path)
                Path.Combine(programFilesX86, "MSBuild", "14.0", "Bin", "MSBuild.exe"),
                Path.Combine(programFilesX86, "Microsoft Visual Studio", "2017", "BuildTools", "MSBuild", "15.0", "Bin", "MSBuild.exe"),
                Path.Combine(programFilesX86, "Microsoft Visual Studio", "2017", "Community", "MSBuild", "15.0", "Bin", "MSBuild.exe"),
                Path.Combine(programFilesX86, "Microsoft Visual Studio", "2019", "BuildTools", "MSBuild", "Current", "Bin", "MSBuild.exe"),
                Path.Combine(programFilesX86, "Microsoft Visual Studio", "2019", "Community", "MSBuild", "Current", "Bin", "MSBuild.exe"),
                Path.Combine(programFilesX86, "Microsoft Visual Studio", "2022", "BuildTools", "MSBuild", "Current", "Bin", "MSBuild.exe"),
                Path.Combine(programFilesX86, "Microsoft Visual Studio", "2022", "Community", "MSBuild", "Current", "Bin", "MSBuild.exe")
            };

            foreach (var cand in candidates)
            {
                if (File.Exists(cand)) return cand;
            }

            return null;
        }

        // Runs process but returns output/error/result synchronously
       public static (int ExitCode, string Output, string Error) RunProcess(string fileName, string arguments, int timeoutMs = 600000)
        {
            var psi = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                StandardOutputEncoding = Encoding.UTF8,
                StandardErrorEncoding = Encoding.UTF8
            };

            var output = new StringBuilder();
            var error = new StringBuilder();

            using (var proc = new Process { StartInfo = psi, EnableRaisingEvents = true })
            {
                proc.OutputDataReceived += (s, e) => { if (e.Data != null) output.AppendLine(e.Data); };
                proc.ErrorDataReceived += (s, e) => { if (e.Data != null) error.AppendLine(e.Data); };

                proc.Start();
                proc.BeginOutputReadLine();
                proc.BeginErrorReadLine();

                if (!proc.WaitForExit(timeoutMs))
                {
                    try { proc.Kill(); } catch { }
                    return (999, output.ToString(), error.ToString() + Environment.NewLine + "Process timeout and was killed.");
                }

                return (proc.ExitCode, output.ToString(), error.ToString());
            }
        }

        // Small helper to run a process and return stdout (used for 'where')
       public static string RunProcessSync(string fileName, string arguments, int timeoutMs = 10000)
        {
            var psi = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            using (var p = Process.Start(psi))
            {
                p.WaitForExit(timeoutMs);
                var outText = p.StandardOutput.ReadToEnd();
                var errText = p.StandardError.ReadToEnd();
                return outText + Environment.NewLine + errText;
            }
        }

    }
}

