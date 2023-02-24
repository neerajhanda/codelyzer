using System;
using System.Text.Json.Serialization;

namespace Codelyzer.Analysis.Model
{
    public class ExternalReference
    {
        [JsonPropertyName("identifier")]
        [JsonPropertyOrder(1)]
        public string Identity { get; set; }
        [JsonPropertyName("version")]
        [JsonPropertyOrder(2)]
        public string Version { get; set; }
        [JsonPropertyName("assembly-location")]
        [JsonPropertyOrder(3)]
        public string AssemblyLocation { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as ExternalReference);
        }

        public bool Equals(ExternalReference compare)
        {
            return compare != null &&
                Identity == compare.Identity &&
                Version == compare.Version &&
                AssemblyLocation == compare.AssemblyLocation;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Identity, Version, AssemblyLocation);
        }
    }
}
