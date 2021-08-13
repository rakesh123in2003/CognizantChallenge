namespace ProgrammingComp.Models
{
    public class CodeCompileModel
    {
		public Language Language { get; set; }
		public ProjectType ProjectType { get; set; }
		public Compiler Compiler { get; set; }
		public string CodeBlock { get; set; }
		public string[] ConsoleInputLines { get; set; }
		public MvcCodeBlock MvcCodeBlock { get; set; }
		public NuGetPackages[] NuGetPackages { get; set; }
	}

	public enum Compiler
	{
		Net45 = 1,
		Roslyn = 2
	}

	public enum Language
	{
		CSharp = 1,
		VbNet,
		FSharp
	}

	public enum ProjectType
	{
		Console = 1,
		Script,
		Mvc,
		Nancy
	}


	public class MvcCodeBlock
	{
		public string Model { get; set; }
		public string View { get; set; }
		public string Controller { get; set; }
	}

	public class FiddleExecuteResult
	{
		public string ConsoleOutput { get; set; }
		public RunStatsViewModel Stats { get; set; }
		public string WebPageHtmlOutput { get; set; }
		public bool IsConsoleInputRequested { get; set; }
		public bool HasErrors { get; set; }
		public bool HasCompilationErrors { get; set; }
	}

	public class NuGetPackages
	{
		public string NuGetId { get; set; }
		public string VersionName { get; set; }
	}

	public class RunStatsViewModel
	{
		public string RunAt { get; set; }
		public string CompileTime { get; set; }
		public string ExecuteTime { get; set; }
		public string MemoryUsage { get; set; }
		public string CpuUsage { get; set; }
	}
}