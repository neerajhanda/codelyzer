using System.Text.Json.Serialization;

namespace Codelyzer.Analysis.Model
{
    public class SimpleAsClause : UstNode
    {
        [JsonPropertyName("type")]
[JsonPropertyOrder(10)]
        public string Type { get; set; }
        public SimpleAsClause()
            : base(IdConstants.SimpleAsClauseName)
        {
        }
    }
}
