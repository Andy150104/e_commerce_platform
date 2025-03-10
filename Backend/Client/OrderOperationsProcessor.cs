using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;

namespace Client;

public class OrderOperationsProcessor : IDocumentProcessor
{
    public void Process(DocumentProcessorContext context)
    {
        // Get all paths and order them by key
        var orderedPaths = context.Document.Paths
            .OrderBy(p => p.Key, StringComparer.OrdinalIgnoreCase)
            .ToList();

        // Delete all paths
        context.Document.Paths.Clear();

        // Add paths in order
        foreach (var path in orderedPaths)
        {
            context.Document.Paths.Add(path.Key, path.Value);
        }
    }
}