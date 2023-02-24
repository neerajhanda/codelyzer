using System;
using System.Globalization;
using System.Net.Http.Json;
using System.Runtime.Serialization.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;

namespace Codelyzer.Analysis.Model
{
    public partial class UstNode
    {
        [JsonIgnore]
        public readonly string type;

        [JsonPropertyName("type")]
        [JsonPropertyOrder(1)]
        public string NodeType { get => type; }

        [JsonPropertyName("identifier")]
        [JsonPropertyOrder(2)]
        public string Identifier { get; set; }

        [JsonPropertyName("location")]
        [JsonPropertyOrder(3)]
        public TextSpan TextSpan { get; set; }

        [JsonIgnore]
        public UstNode Parent { get; set; }

        [JsonIgnore]
        public string FullIdentifier { get; set; }

        [JsonPropertyName("children")]
        [JsonPropertyOrder(100)]
        public UstList<UstNode> Children { get; set; }

        public UstNode(string nodeType)
        {
            type = nodeType;
            Children = new UstList<UstNode>();
        }

        public override bool Equals(object obj)
        {
            if (obj is UstNode)
            {
                return Equals((UstNode)obj);
            }
            else return false;
        }

        public bool Equals(UstNode compareNode)
        {
            return (NodeType?.Equals(compareNode.NodeType) != false)
                && (Identifier?.Equals(compareNode.Identifier) != false)
                && (TextSpan?.Equals(compareNode.TextSpan) != false)
                && (Children?.Equals(compareNode.Children) != false);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(NodeType, Identifier, TextSpan, Parent, Children);
        }
    }

    public partial class NodeType
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        public NodeType(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public partial class TextSpan
    {
        [JsonPropertyName("start-char-position")]
        public long StartCharPosition { get; set; }

        [JsonPropertyName("end-char-position")]
        public long EndCharPosition { get; set; }

        [JsonPropertyName("start-line-position")]
        public long StartLinePosition { get; set; }

        [JsonPropertyName("end-line-position")]
        public long EndLinePosition { get; set; }

        public override bool Equals(object obj)
        {
            return Equals((TextSpan)obj);
        }

        public bool Equals(TextSpan compareSpan)
        {
            return compareSpan != null &&
                StartCharPosition == compareSpan.StartCharPosition
                && EndCharPosition == compareSpan.EndCharPosition
                && StartLinePosition == compareSpan.StartLinePosition
                && EndLinePosition == compareSpan.EndLinePosition;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(StartCharPosition, EndCharPosition, StartLinePosition, EndLinePosition);
        }
    }

    public partial class Parameter
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("semantic-type")]
        public string SemanticType { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Parameter)
            {
                return Equals(obj as Parameter);
            }
            return false;
        }

        public bool Equals(Parameter compareNode)
        {
            return
                compareNode != null &&
                Name?.Equals(compareNode.Name) != false &&
                Type?.Equals(compareNode.Type) != false &&
                SemanticType?.Equals(compareNode.SemanticType) != false;

        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Type, SemanticType);
        }
    }


    public partial class UstNode
    {
        //public static UstNode FromJson(string json) => JsonConvert.DeserializeObject<UstNode>(json, Codelyzer.Analysis.Model.Converter.Settings);
        public static UstNode FromJson(string json) => JsonSerializer.Deserialize<UstNode>(json, Codelyzer.Analysis.Model.Converter.Settings);
    }

    public static class Serialize
    {
        //public static string ToJson(this UstNode self) => JsonConvert.SerializeObject(self, Codelyzer.Analysis.Model.Converter.Settings);
        public static string ToJson(this UstNode self) => JsonSerializer.Serialize(self, Codelyzer.Analysis.Model.Converter.Settings);
    }

    internal static class Converter
    {
        //public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        //{
        //    MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
        //    DateParseHandling = DateParseHandling.None,
        //    NullValueHandling = NullValueHandling.Ignore,
        //    Formatting = Formatting.Indented,
        //    Converters =
        //    {
        //        new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
        //    },
        //};

        public static readonly JsonSerializerOptions Settings = new JsonSerializerOptions
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            //Converters =
            //{
            //    //new 
            //}
        };
    }
}
