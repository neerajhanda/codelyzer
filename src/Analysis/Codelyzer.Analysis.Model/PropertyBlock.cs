using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Codelyzer.Analysis.Model
{
    public class PropertyBlock : UstNode
    {
        [JsonPropertyName("modifiers")]
[JsonPropertyOrder(20)]
        public string Modifiers { get; set; }

        [JsonPropertyName("references")]
[JsonPropertyOrder(99)]
        public Reference Reference { get; set; }
        public string SemanticAssembly { get; set; }

        [JsonPropertyName("parameters")]
[JsonPropertyOrder(100)]
        public List<Parameter> Parameters { get; set; }

        public PropertyBlock()
            : base(IdConstants.PropertyBlockName)
        {
            Reference = new Reference();
        }

        public override bool Equals(object obj)
        {
            if (obj is PropertyBlock)
            {
                return Equals(obj as PropertyBlock);
            }
            return false;
        }
    }
}
