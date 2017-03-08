using System;
using System.IO;
using System.Text.RegularExpressions;

namespace test
{
    

class Settings
{
	public static string SolutionPath = @"C:\dev\nocommonsnet\";
}

class programolini{


static Regex testFixture = new Regex(@" *?\[TestFixture(\(.*?\))?(, *(?<AdditionalAttributes>.*?))?\]\r?\n", RegexOptions.Compiled);
static Regex test = new Regex(@"\[Test\]", RegexOptions.Compiled);
static Regex testIgnore = new Regex(@"\[Test, Ignore\(", RegexOptions.Compiled);
static Regex testWithTimeout = new Regex(@"\[Test, Timeout\(", RegexOptions.Compiled);
static Regex explicitTest = new Regex(@"\[Explicit\]", RegexOptions.Compiled);
static Regex testWithExplicit = new Regex(@"\[Test, Explicit\]", RegexOptions.Compiled);
static Regex testCaseWithTestAttribute = new Regex(@"\[Fact\]\r?\n\s*?\[TestCase\(", RegexOptions.Compiled | RegexOptions.Multiline);
static Regex testCaseToTheory = new Regex(@"(^[}\s\r]+\n\s*?\[TestCase\()", RegexOptions.Compiled | RegexOptions.Multiline);
//static Regex testCase = new Regex(@"\[TestCase(", RegexOptions.Compiled);
static Regex asyncVoidFix = new Regex(@"(?<Match>(\[Theory|\[Fact|\[InlineData).*\r?\n\s*)public async void", RegexOptions.Compiled | RegexOptions.Multiline);
static Regex setUp = new Regex(@"public class (?<classname>\w+)(?<stuff>.*?)\[SetUp\]\r?\n *?public void \w+", RegexOptions.Compiled | RegexOptions.Singleline);
static Regex tearDown = new Regex(@"public class (?<classname>\w+)(?<stuff>.*?)\[SetUp\]\r?\n *?public void \w+", RegexOptions.Compiled | RegexOptions.Singleline);
static Regex classInherits = new Regex(@"public (abstract )?class \w+.*?(?<inherits>:.*?)?$", RegexOptions.Compiled | RegexOptions.Multiline);
static Regex fixTearDown = new Regex(@"\[TearDown\]\w*?\r?\n\s*?public void \w+", RegexOptions.Compiled);

static void Main()
{
	var allCsFiles = Directory.GetFiles(Settings.SolutionPath, "*.cs", SearchOption.AllDirectories);
	foreach (var csFile in allCsFiles)
	{
		var originalFileContents = File.ReadAllText(csFile);
		var fileContents = originalFileContents;
		if (!fileContents.Contains("using Xunit;")) continue;
		
		// Remove		if (!string.IsNullOrEmpty(testFixture.Match(fileContents).Groups["AdditionalAttributes"].Value))
	 		fileContents = testFixture.Replace(fileContents, "    [${AdditionalAttributes}]\r\n");
		else
	 		fileContents = testFixture.Replace(fileContents, string.Empty);
	 	fileContents = test.Replace(fileContents, "[Fact]");
	 	fileContents = testIgnore.Replace(fileContents, "[Fact(Skip=");
	 	fileContents = testWithTimeout.Replace(fileContents, "[Fact(Timeout=");
	 	fileContents = explicitTest.Replace(fileContents, "[RunnableInDebugOnlyAttribute]");
	 	fileContents = testWithExplicit.Replace(fileContents, "[RunnableInDebugOnlyAttribute]");
		fileContents = testCaseWithTestAttribute.Replace(fileContents, "[Theory]\r\n        [InlineData(");
		fileContents = testCaseToTheory.Replace(fileContents, "[Theory]    $1");
		//fileContents = testCase.Replace(fileContents, "[InlineData(");
		fileContents = asyncVoidFix.Replace(fileContents, "${Match}public async Task");
		fileContents = setUp.Replace(fileContents, "public class ${classname}${stuff}public ${classname}");
		// if (fileContents.Contains("[TearDown]"))
		// {
		// 	var classDefinition = classInherits.Match(fileContents);
		// 	if (classDefinition.Groups["inherits"].Success)
		// 		fileContents = fileContents.Replace(classDefinition.Value, classDefinition.Value.TrimEnd('\r', '\n') + ", IDisposable");
		// 	else
		// 		fileContents = fileContents.Replace(classDefinition.Value, classDefinition.Value.TrimEnd('\r', '\n') + " : IDisposable");
		// 	fileContents = fixTearDown.Replace(fileContents, "public void Dispose");
		// }
		fileContents = fileContents.Replace("using Xunit;", "using Xunit;");
		if (fileContents == originalFileContents) continue;
		//csFile.Dump();
		File.WriteAllText(csFile, fileContents);
	}
    }
}
}


// Define other methods and classes here