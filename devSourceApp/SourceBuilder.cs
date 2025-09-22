using SevenZip;
using System;
using System.Data;
using System.Diagnostics;
using System.IO.Compression;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using devSourceApp;

namespace devSourceApp
{
    public class SourceBuilder
    {
       public builderMethods builderMethods = new builderMethods();
        public bool GenerateFile(string inputPath, string flatForm)
        {
            bool _isCodeWorked = false;
            
            try
            {


                string PathFolder = inputPath;//@"D:\PRAKASHJ\CURSOR\Giva Jewellery_Web_V5";
                if (!Directory.Exists(PathFolder))
                {
                    ModernMessageBox.ShowBox($"ERROR: Project or solution not found: {PathFolder}");
                }

                string[] csprojFiles = Directory.GetFiles(PathFolder, "*.csproj");

                if (csprojFiles.Length != 1)
                {
                    ModernMessageBox.ShowBox("There is more than 1 csproject present");

                }

                builderMethods.hintPathChange(csprojFiles[0]);
                //adding86Configuration(csprojFiles[0]);
                builderMethods.EnsureReleaseX86Config(csprojFiles[0]);

                //string[] projectFile = Directory.GetFiles(PathFolder, "*.csproj");
                string projectPath = csprojFiles[0];
                string FileName = Path.GetFileNameWithoutExtension(projectPath);


                //string configuration = args.Length >= 2 ? args[1] : "Release";
                //string outputBase = args.Length >= 3 ? args[2] : Path.Combine(Directory.GetCurrentDirectory(), "build-output");

                //newCode

                string configuration = "Release";
                string outputBase = Path.Combine(PathFolder, "bin\\Debug");
                string logPath = Path.Combine(Application.StartupPath,"Logs") ;
                Directory.CreateDirectory(outputBase);
                Directory.CreateDirectory(logPath);

                string msbuildPath = builderMethods.FindMSBuild();
                if (msbuildPath == null)
                {
                    ModernMessageBox.ShowBox("ERROR: msbuild.exe not found");

                }

                // Console.WriteLine($"Using MSBuild: {msbuildPath}");
                // Console.WriteLine($"Project: {projectPath}");
                // Console.WriteLine($"Configuration: {configuration}");
                // Console.WriteLine($"Output base: {outputBase}");
                string[] platforms; 
                // Targets to build
                if (flatForm.Contains("32"))
                {
                    platforms = new[] { "x86", "AnyCPU" }; //AnyCPU
                }
                else
                {
                    platforms = new[] { "x86" };
                }

                int finalExit = 0;

                foreach (var plat in platforms)
                {
                    string platFolderName = plat.Equals("AnyCPU", StringComparison.OrdinalIgnoreCase) ? "AnyCPU" : plat;
                    string bitFolder = "";
                    string bitNanme = "32";
                    if (platFolderName == "AnyCPU")
                    {
                        bitFolder = "32_Bit";
                        bitNanme = "32";
                    }
                    else
                    {
                        bitFolder = "64_Bit";
                        bitNanme = "64";
                    }

                    string outDir = Path.Combine(Application.StartupPath, "builder")+ Path.DirectorySeparatorChar;
                    Directory.CreateDirectory(outDir);
                    string outDirFinal = Path.Combine(outputBase, FileName, DateTime.Today.ToString("dd-Sep-yyyy")) + Path.DirectorySeparatorChar;// OutDir must end with separator

                    Directory.CreateDirectory(outDirFinal);

                    // Build args: OutDir overrides project output location.
                    // Include PlatformTarget to explicitly set compiler target.
                    // string msbuildArgs = $"\"{projectPath}\" /nologo /t:Build /p:Configuration={configuration} /p:Platform={plat} /p:PlatformTarget={plat} /p:OutDir=\"{outDir}\" /v:minimal";
                    //string msbuildArgs =
                    //            $"\"{projectPath}\" /nr:false " +
                    //             $"/p:Configuration={configuration} " +
                    //             $"/p:Platform=\"{platFolderName}\" " +
                    //             $"/p:OutDir=\"{outDir}\\\" " +
                    //             "/p:AllowUnsafeBlocks=true " +// Important: trailing slash + quotes
                    //             "/v:minimal";

                    string msbuildArgs =
                                         $"\"{projectPath}\" /nr:false /t:Clean;Rebuild " +
                                         $"/p:Configuration={configuration} " +
                                        $"/p:Platform=\"{platFolderName}\" " +
                                         $"/p:OutDir=\"{outDir}\\\" " +
                                         "/p:AllowUnsafeBlocks=true " +
                                         "/v:minimal";



                    Console.WriteLine();
                    Console.WriteLine($"Building platform: {plat} -> {outDir}");
                    if (plat == "x86")
                        builderMethods.PatchAssemblyTitle(projectPath, "64");
                    else
                        builderMethods.PatchAssemblyTitle(projectPath, "32");

                    var result = builderMethods.RunProcess(msbuildPath, msbuildArgs, timeoutMs: 20 * 60 * 1000); // 20 min timeout


                    string safePlat = platFolderName.Replace(" ", "_");
                    string logFile = Path.Combine(logPath, $"{safePlat}_{configuration}.log");
                    File.WriteAllText(logFile, result.Output + Environment.NewLine + "=== STDERR ===" + Environment.NewLine + result.Error);

                    Console.WriteLine($"MSBuild exit code: {result.ExitCode}. Log: {logFile}");
                    if (result.ExitCode != 0)
                    {
                        // If AnyCPU fails, try fallback "Any CPU" (with space)
                        if (plat.Equals("AnyCPU", StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("Build failed for 'AnyCPU'. Trying fallback platform name 'Any CPU' ...");
                            string fallbackPlat = "Any CPU";
                            string fallbackOut = Path.Combine(outputBase, "AnyCPU", configuration) + Path.DirectorySeparatorChar;
                            string fallbackArgs = $"\"{projectPath}\" /nologo /t:Build /p:Configuration={configuration} /p:Platform=\"{fallbackPlat}\" /p:PlatformTarget=AnyCPU /p:OutDir=\"{fallbackOut}\" /v:minimal";
                            var fallbackResult = builderMethods.RunProcess(msbuildPath, fallbackArgs, timeoutMs: 20 * 60 * 1000);
                            string fallbackLog = Path.Combine(outputBase, $"AnyCPU_{configuration}_fallback.log");
                            File.WriteAllText(fallbackLog, fallbackResult.Output + Environment.NewLine + "=== STDERR ===" + Environment.NewLine + fallbackResult.Error);
                            Console.WriteLine($"Fallback exit code: {fallbackResult.ExitCode}. Log: {fallbackLog}");
                            if (fallbackResult.ExitCode == 0)
                            {
                                Console.WriteLine("Fallback build succeeded.");
                                continue; 
                            }
                        }
                        if(platforms.Length == 1)
                        {
                            ModernMessageBox.ShowBox($"Build for {plat} FAILED. Check log: {logFile}");
                        }
                        
                        finalExit = result.ExitCode != 0 ? result.ExitCode : 4;
                    }
                    else
                    {
                        string finalOutPutZip = outputBase;
                        builderMethods.ZiptheFile(finalOutPutZip, FileName + "_"+bitNanme, outDirFinal);
                        Console.WriteLine($"Build for {plat} succeeded. Output in: {outDir}");
                    }
                }

                Console.WriteLine();
               
               // ModernMessageBox.ShowBox($"Done. Outputs are inside: {outputBase}");


                _isCodeWorked =  true;
            }
            catch (Exception ex)
            {
                ModernMessageBox.ShowBox("BuildError");

            }
           
            return _isCodeWorked;
        }

    }
}
