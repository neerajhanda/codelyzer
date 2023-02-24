using Microsoft.CodeAnalysis;
using System;
using System.Text.Json.Serialization;

namespace Codelyzer.Analysis.Model
{
    public class Reference
    {
        [JsonPropertyName("namespace")]
        [JsonPropertyOrder(1)]
        public string Namespace { get; set; }
        [JsonPropertyName("assembly")]
        [JsonPropertyOrder(2)]
        public string Assembly { get; set; }
        [JsonPropertyName("assembly-location")]
        [JsonPropertyOrder(3)]
        public string AssemblyLocation { get; set; }
        [JsonPropertyName("version")]
        [JsonPropertyOrder(4)]
        public string Version { get; set; }

        [JsonIgnore]
        public IAssemblySymbol AssemblySymbol { get; set; }

        public override bool Equals(object obj)
        {
            Reference o = (Reference)obj;
            return this.Assembly == o.Assembly && this.Namespace == o.Namespace;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Namespace, Assembly);
        }
    }
}
