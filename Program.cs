/**
  * This program reads a file "input.txt" from the current working directory,
  * performs a couple of replacements (replacing "<mimes>" and "</mimes> with "")
  * and writes the result in a file called "output.txt", 
  * using a file "output_tmp.txt" as a swap file.
  * It then prints the duration of the replacements and copying operations.
*/

using static StringReplacement;

var stopwatch = new System.Diagnostics.Stopwatch();
var dir = System.Environment.CurrentDirectory;
var f = (string filename) => System.IO.Path.Combine(dir, filename);
var replacements = new []
{
    new Replacement("<mimes>", ""),
    new Replacement("</mimes>", ""),
};

stopwatch.Start();

File.Copy(f("input.txt"), f("output.txt"), true);

foreach (var r in replacements)
{
    Replace(f("output.txt"), f("output_tmp.txt"), r.SearchText, r.ReplacementText);
    File.Copy(f("output_tmp.txt"), f("output.txt"), true);
}

stopwatch.Stop();

Console.WriteLine($"Time elapsed: {stopwatch.Elapsed.ToString("g")}");