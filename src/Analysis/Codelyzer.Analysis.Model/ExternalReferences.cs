using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Codelyzer.Analysis.Model
{
    public class ExternalReferences
    {
        public ExternalReferences()
        {
            NugetReferences = new List<ExternalReference>();
            NugetDependencies = new List<ExternalReference>();
            SdkReferences = new List<ExternalReference>();
            ProjectReferences = new List<ExternalReference>();
        }
        [JsonPropertyName("nuget")]
[JsonPropertyOrder(1)]
        public List<ExternalReference> NugetReferences { get; set; }
        [JsonPropertyName("nuget-dependencies")]
[JsonPropertyOrder(2)]
        public List<ExternalReference> NugetDependencies { get; set; }
        [JsonPropertyName("sdk")]
[JsonPropertyOrder(3)]
        public List<ExternalReference> SdkReferences { get; set; }
        [JsonPropertyName("project")]
[JsonPropertyOrder(4)]
        public List<ExternalReference> ProjectReferences { get; set; }
    }
}
