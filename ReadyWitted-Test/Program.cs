using isaaholic.ReadyWittedLibrary;
// "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"
ReadyWitted.CreateInstance("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");
var result = await ReadyWitted.GetPresentation("Games");
foreach (var item in result)
{
    Console.WriteLine(item);
}