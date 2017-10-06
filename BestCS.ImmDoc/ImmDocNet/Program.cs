
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using System.IO;
using System.Diagnostics;

using BestCS.DocNet.Documenters;
using BestCS.DocNet.Documenters.HTMLDocumenter;

namespace BestCS.DocNet
{
    class Program
    {
        private const int MAX_VERBOSE_LEVEL = 3;

        // 0 - nothing
        // 1 - errors
        // 2 - errors and warnings
        // 3 - progress and errors and warnings
        private static int verboseLevel = MAX_VERBOSE_LEVEL;

        private static long generatingStartTime;
        private static float generatingTime;
        private static long preparationStartTime;
        private static float preparationTime;

        private static string projectName;
        private static string chmFileNameWithoutExtension;
        private static string outputDirectory = "doc";
        private static AssembliesInfo assembliesInfo;
        private static Dictionary<string, bool> excludedFilesNames;
        private static HashSet<string> excludedNamespaces;
        private static DocumentationGenerationOptions docGenOptions = DocumentationGenerationOptions.None;

        private static readonly string ASSEMBLY_CODE_BASE;
        static XmlSchema xmlSchema;
        static Stream stream;

        #region Constructor(s)

        static Program()
        {
            ASSEMBLY_CODE_BASE = Assembly.GetExecutingAssembly().GetName().CodeBase;

            excludedFilesNames = new Dictionary<string, bool>();
            excludedNamespaces = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            xmlSchema = new XmlSchema();
        }

        #endregion

        #region Application entry point

        private static void Main(string[] args)
        {
            long totalStartTime = Environment.TickCount;

            List<string> options = new List<string>();
            List<string> fileNames = new List<string>();

            foreach (string arg in args)
            {
                if (arg.StartsWith("-"))
                {
                    if (arg.Length > 1)
                    {
                        options.Add(arg.Substring(1));
                    }
                    else
                    {
                        PrintUsageAndExit();
                    }
                }
                else
                {
                    fileNames.Add(arg);
                }
            }

            ProcessOptions(options);

            if ((docGenOptions & DocumentationGenerationOptions.IncludeInternalMembers) != 0)
            {
                Utils.IncludeInternalMembers = true;
            }

            if ((docGenOptions & DocumentationGenerationOptions.IncludePrivateMembers) != 0)
            {
                Utils.IncludePrivateMembers = true;
            }

            if (verboseLevel > 2)
            {
                Console.WriteLine("BestCS.DocNet");
                string yearString;

                if (DateTime.Now.Year > 2007)
                {
                    yearString = "2017 - " + DateTime.Now.Year;
                }
                else
                {
                    yearString = "20017";
                }

                Console.WriteLine("Copyright (C) " + yearString + " BestCS Framework");
                Console.WriteLine();
            }

            List<int> indicesToBeRemoved;

            if (fileNames.Count == 0)
            {
                // add files from the current directory
                fileNames.AddRange(Directory.GetFiles(Environment.CurrentDirectory, "*.exe"));
                fileNames.AddRange(Directory.GetFiles(Environment.CurrentDirectory, "*.dll"));
                fileNames.AddRange(Directory.GetFiles(Environment.CurrentDirectory, "*.xml"));
                fileNames.AddRange(Directory.GetFiles(Environment.CurrentDirectory, "*.docs"));
            }
            else
            {
                // remove files with unknown extensions
                indicesToBeRemoved = new List<int>();

                for (int i = 0; i < fileNames.Count; i++)
                {
                    string fullFileName = fileNames[i];
                    string ext = Path.GetExtension(fullFileName).ToLower();

                    if (ext != ".exe" && ext != ".dll" && ext != ".xml" && ext != ".docs")
                    {
                        if (verboseLevel > 2) { Console.WriteLine("Исключён файл {0} (неподдерживаемый тип).", Path.GetFileName(fullFileName)); }

                        indicesToBeRemoved.Add(i);
                    }
                }

                Utils.RemoveItems(fileNames, indicesToBeRemoved);
            }

            // remove program files
            indicesToBeRemoved = new List<int>();

            string programExeFileNameLower = Path.GetFileName(ASSEMBLY_CODE_BASE).ToLower();

            for (int i = 0; i < fileNames.Count; i++)
            {
                string fileName = Path.GetFileName(fileNames[i]);

                if (fileName.ToLower() == programExeFileNameLower)
                {
                    if (verboseLevel > 2) { Console.WriteLine("Исключён файл {0} (модуль программы).", fileName); }
                }

                if (fileName.ToLower() == "bestcs.docnet.dll" || fileName.ToLower() == "dotdoc.exe")
                {
                    if (verboseLevel > 2) { Console.WriteLine("Исключён файл {0} (библиотека программы).", fileName); }
                }

                indicesToBeRemoved.Add(i);

            }

            Utils.RemoveItems(fileNames, indicesToBeRemoved);

            // remove vshost files
            indicesToBeRemoved = new List<int>();

            for (int i = 0; i < fileNames.Count; i++)
            {
                string fullFileName = fileNames[i];

                if (fullFileName.ToLower().EndsWith(".vshost.exe"))
                {
                    if (verboseLevel > 2) { Console.WriteLine("Исключён файл {0} (генерирован Visual Studio).", Path.GetFileName(fullFileName)); }

                    indicesToBeRemoved.Add(i);
                }
            }

            Utils.RemoveItems(fileNames, indicesToBeRemoved);

            long processingStartTime = Environment.TickCount;

            ProcessFilesNames(fileNames, excludedNamespaces);

            if (verboseLevel > 2) { Console.WriteLine(); }

            float processingTime = (Environment.TickCount - processingStartTime) / 1000.0f;

            Documenter documenter = new HTMLDocumenter(assembliesInfo, chmFileNameWithoutExtension);

            documenter.DirectoryDeleteStarted += documenter_DirectoryDeleteStarted;
            documenter.DirectoryDeleteFinished += documenter_DirectoryDeleteFinished;
            documenter.GeneratingStarted += documenter_GeneratingStarted;
            documenter.GeneratingFinished += documenter_GeneratingFinished;

            preparationStartTime = Environment.TickCount;

            bool success = documenter.GenerateDocumentation(outputDirectory, docGenOptions);

            if (verboseLevel > 2)
            {
                Console.WriteLine();

                Console.WriteLine("Время обработки  :  {0:F2} s", processingTime);
                Console.WriteLine("Время подготовки : {0:F2} s", preparationTime);
                Console.WriteLine("Время генерации  :{0:F2} s", generatingTime);
                Console.WriteLine("Общее время      : {0:F2} s", (Environment.TickCount - totalStartTime) / 1000.0f);

                Console.WriteLine();

                Console.WriteLine("Предупреждения: {0}", Logger.WarningsCount);
                Console.WriteLine("Ошибки:   {0}", Logger.ErrorsCount);
            }

            if (verboseLevel > 1 && Logger.WarningsCount > 0)
            {
                if (verboseLevel > 2)
                {
                    Console.Error.WriteLine();
                }

                Logger.WriteWarnings(Console.Error);
            }

            if (!success || Logger.ErrorsCount > 0)
            {
                if (verboseLevel > 0 && Logger.ErrorsCount > 0)
                {
                    if (verboseLevel > 2 || (verboseLevel > 1 && Logger.WarningsCount > 0))
                    {
                        Console.Error.WriteLine();
                    }

                    Logger.WriteErrors(Console.Error);
                }

                Environment.Exit(1);
            }
            else
            {
                Environment.Exit(0);
            }
        }

        #endregion

        #region Private helper methods

        private static void ProcessOptions(List<string> options)
        {
            foreach (string option in options)
            {
                string opName;
                string opArg = null;
                int indexOfColon = option.IndexOf(':');

                if (indexOfColon == -1)
                {
                    opName = option.ToLower();
                }
                else
                {
                    opName = option.Substring(0, indexOfColon).ToLower();
                    opArg = indexOfColon + 1 < option.Length ? option.Substring(indexOfColon + 1) : null;
                }

                if (opName == "help" || opName == "h")
                {
                    PrintUsageAndExit();
                }
                else if (opName == "projectname" || opName == "pn")
                {
                    if (opArg == null) { PrintUsageAndExit(); }

                    projectName = opArg;
                }
                else if (opName == "chmname" || opName == "cn")
                {
                    if (opArg == null) { PrintUsageAndExit(); }

                    chmFileNameWithoutExtension = opArg;

                    if (chmFileNameWithoutExtension.EndsWith(".chm", true, null))
                    {
                        chmFileNameWithoutExtension = chmFileNameWithoutExtension.Substring(0, chmFileNameWithoutExtension.Length - 4);
                    }
                }
                else if (opName == "verboselevel" || opName == "vl")
                {
                    if (opArg == null) { PrintUsageAndExit(); }

                    if (!Int32.TryParse(opArg, out verboseLevel) || verboseLevel < 0 || verboseLevel > MAX_VERBOSE_LEVEL)
                    {
                        PrintUsageAndExit();
                    }
                }
                else if (opName == "exclude" || opName == "ex")
                {
                    if (opArg == null) { PrintUsageAndExit(); }

                    excludedFilesNames[opArg.ToLower()] = true;
                }
                else if (opName == "excludenamespace" || opName == "exn")
                {
                    if (opArg == null) { PrintUsageAndExit(); }

                    excludedNamespaces.Add(opArg.ToLower());
                }
                else if (opName == "outputdirectory" || opName == "od")
                {
                    if (opArg == null) { PrintUsageAndExit(); }

                    if (Path.GetFileName(opArg) == "")
                    {
                        Console.WriteLine("Ошибка:Директория вывода не пуста.");
                        Environment.Exit(1);
                    }

                    outputDirectory = opArg;
                }
                else if (opName == "forcedelete" || opName == "fd")
                {
                    if (opArg != null) { PrintUsageAndExit(); }

                    docGenOptions |= DocumentationGenerationOptions.DeleteOutputDirIfItExists;
                }
                else if (opName == "includeinternalmembers" || opName == "iim")
                {
                    if (opArg != null) { PrintUsageAndExit(); }

                    docGenOptions |= DocumentationGenerationOptions.IncludeInternalMembers;
                }
                else if (opName == "includeprivatemembers" || opName == "ipm")
                {
                    if (opArg != null) { PrintUsageAndExit(); }

                    docGenOptions |= DocumentationGenerationOptions.IncludePrivateMembers;
                }
                else
                {
                    PrintUsageAndExit();
                }
            }
        }

        private static void ProcessFilesNames(List<string> fileNames, IEnumerable<string> excludedNamespaces)
        {
            assembliesInfo = new AssembliesInfo(projectName == null ? "Проект Документации" : projectName);

            // proces assemblies (*.exe || *.dll)
            foreach (string fullFileName in fileNames)
            {
                string fileName = Path.GetFileName(fullFileName);
                string ext = Path.GetExtension(fileName).ToLower();

                if ((ext != ".exe" && ext != ".dll"))
                {
                    continue;
                }

                if (excludedFilesNames.ContainsKey(fileName.ToLower()))
                {
                    if (verboseLevel > 2) { Console.WriteLine("Исключён файл {0} (исключение определено в опциях).", Path.GetFileName(fullFileName)); }

                    continue;
                }

                if (!File.Exists(fullFileName))
                {
                    if (verboseLevel > 2) { Console.WriteLine("Пропущена сборка {0} (файла не существует).", fileName); }

                    continue;
                }

                if (verboseLevel > 2) { Console.Write("Обрабатывается сборка {0}... ", fileName); }

                assembliesInfo.ReadMyAssemblyInfoFromAssembly(fullFileName, excludedNamespaces);

                if (verboseLevel > 2) { Console.WriteLine("ГОТОВО"); }
            }

            // process xml documentations (*.xml)
            foreach (string fullFileName in fileNames)
            {
                string fileName = Path.GetFileName(fullFileName);
                string ext = Path.GetExtension(fileName).ToLower();

                if (ext != ".xml" || excludedFilesNames.ContainsKey(fileName.ToLower()))
                {
                    continue;
                }

                if (!File.Exists(fullFileName))
                {
                    if (verboseLevel > 2) { Console.WriteLine("Пропущен файл документации {0} (файл не существует).", fileName); }

                    continue;
                }

                if (verboseLevel > 2) { Console.Write("Обрабатывается файл документации {0}... ", fileName); }

                assembliesInfo.ReadMyAssemblyInfoFromXmlDocumentation(fullFileName);

                if (verboseLevel > 2) { Console.WriteLine("ГОТОВО"); }
            }

            // process additional documentation (*.docs)
            foreach (string fullFileName in fileNames)
            {
                string fileName = Path.GetFileName(fullFileName);
                string ext = Path.GetExtension(fileName).ToLower();

                if (ext != ".docs" || excludedFilesNames.ContainsKey(fileName.ToLower()))
                {
                    continue;
                }

                if (!File.Exists(fullFileName))
                {
                    if (verboseLevel > 2) { Console.WriteLine("Пропущен дополнительный файл документации {0} (файл не существует).", fileName); }

                    continue;
                }

                if (!ValidateAdditionalDocumentationFile(fullFileName))
                {
                    if (verboseLevel > 2) { Console.WriteLine("Пропущен дополнительный файл документации {0} (формат файла задан неправильно)", fileName); }

                    continue;
                }

                if (verboseLevel > 2) { Console.Write("Чтение дополнительного файла документации {0}... ", fileName); }

                assembliesInfo.ReadAdditionalDocumentation(fullFileName);

                if (verboseLevel > 2) { Console.WriteLine("ГОТОВО"); }
            }
        }

        private static void PrintUsageAndExit()
        {
            string verboseLevels = "";

            for (int i = 0; i <= MAX_VERBOSE_LEVEL; i++)
            {
                if (i == MAX_VERBOSE_LEVEL && i != 0) { verboseLevels += " or "; }
                else if (i > 0) { verboseLevels += ", "; }

                verboseLevels += i;
            }

            Console.WriteLine("Использование: {0} [ОПЦИЯ]... [ФАЙЛ]... ", Path.GetFileName(ASSEMBLY_CODE_BASE));
            Console.WriteLine("Генерировать документацию HTML из набора сборок или файлов XML.");

            Console.WriteLine();
            Console.WriteLine("Опции:");
            Console.WriteLine("  -h,   -Help                    показать данное сообщение");
            Console.WriteLine("  -pn,  -ProjectName:СТРОКА      устанавливает СТРОКА как название проекта");
            Console.WriteLine("  -cn,  -CHMName:СТРОКА          устанавливает СТРОКА как название выходного файла CHM");
            Console.WriteLine("  -ex,  -Exclude:ФАЙЛ            исключает ФАЙЛ из обработки");
            Console.WriteLine("  -exn, -ExcludeNamespace:СТРОКА Исключает пространство имён СТРОКА из обработки");
            Console.WriteLine("  -od,  -OutputDirectory:DIR     устанавливает ДИР как папку вывода");
            Console.WriteLine("  -fd,  -ForceDelete             Заставляет программу удалить папку вывода");
            Console.WriteLine("  -iim, -IncludeInternalMembers  будут обработаны внутренние члены");
            Console.WriteLine("  -ipm, -IncludePrivateMembers   будут обработаны приватные члены");
            Console.WriteLine("  -vl,  -VerboseLevel:УРОВЕНЬ      устанавливает подробный уровень; УРОВЕНЬ = {0}", verboseLevels);

            Console.WriteLine();
            Console.WriteLine("Если файлы не указаны явно, то все файлы .exe, .dll,");
            Console.WriteLine(".xml и .docs мз указанной папки будут обработаны (за исключением");
            Console.WriteLine("пропущенных по опции -Exclude и исполняемых модулей самой программы).");

            Console.WriteLine();
            Console.WriteLine("Для вывода в формате CHM должен быть установлен HTML Help Workshop.");
            Console.WriteLine("Если HTML Help Workshop установлен не в папку по умолчанию,");
            Console.WriteLine("нужно установить переменную среды HHC_HOME на правильную");
            Console.WriteLine("папку установки.");

            Console.WriteLine();
            Console.WriteLine("Комментарии и отчёты об ошибках направляйте на э-почту <dinruspro@mail.ru>.");

            Environment.Exit(1);
        }

        #endregion

        #region Event handlers

        private static void documenter_DirectoryDeleteStarted(object sender, EventArgs e)
        {
            if (verboseLevel > 2) { Console.Write("Удаляется папка вывода...     "); }
        }

        private static void documenter_DirectoryDeleteFinished(object sender, EventArgs e)
        {
            preparationTime = (Environment.TickCount - preparationStartTime) / 1000.0f;

            if (verboseLevel > 2) { Console.WriteLine("ГОТОВО"); }
        }

        private static void documenter_GeneratingStarted(object sender, EventArgs e)
        {
            generatingStartTime = Environment.TickCount;

            if (verboseLevel > 2) { Console.Write("Генерируется документация HTML... "); }
        }

        private static void documenter_GeneratingFinished(object sender, EventArgs e)
        {
            generatingTime = (Environment.TickCount - generatingStartTime) / 1000.0f;

            if (verboseLevel > 2) { Console.WriteLine("ГОТОВО"); }
        }

        #endregion

        #region Additional documentation file validation

        private static bool valid;
        private static XmlReaderSettings xrs = null;

        private static bool ValidateAdditionalDocumentationFile(string fullFileName)
        {
            valid = true;

            XmlReaderSettings xrs = GetXmlReaderSettings();
            XmlReader xr = null;

            try
            {
                xr = XmlReader.Create(fullFileName, xrs);

                while (xr.Read())
                    ;
            }
            catch (Exception)
            {
                valid = false;
            }
            finally
            {
                if (xr != null)
                {
                    xr.Close();
                }
            }

            return valid;
        }

        private static XmlReaderSettings GetXmlReaderSettings()
        {
            if (xrs != null)
            {
                return xrs;
            }

            var assembly = Assembly.GetExecutingAssembly();
            stream = assembly.GetManifestResourceStream(typeof(Program).Namespace + ".AdditionalDocumentation.xsd");
            if (stream != null) { xmlSchema = XmlSchema.Read(stream, new ValidationEventHandler(SchemaValidation)); }
            else Console.WriteLine("Переменная stream имеет значение null");

            stream.Close();

            xrs = new XmlReaderSettings();
            xrs.Schemas.Add(xmlSchema);
            xrs.ValidationType = ValidationType.Schema;
            xrs.ValidationEventHandler += new ValidationEventHandler(xrs_ValidationEventHandler);

            return xrs;
        }

        private static void SchemaValidation(object sender, ValidationEventArgs e)
        {
            Debug.Assert(false, "Невозможно!");
        }

        private static void xrs_ValidationEventHandler(object sender, ValidationEventArgs e)
        {
#if DEBUG

      Console.WriteLine("");

#endif

            Console.WriteLine("XSD: " + e.Severity.ToString() + ": " + e.Message);

            valid = false;
        }

        #endregion
    }
}

