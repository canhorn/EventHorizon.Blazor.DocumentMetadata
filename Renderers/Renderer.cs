namespace EventHorizon.Blazor.DocumentMetadata.Renderers
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Rendering;

    [Flags]
    public enum RendererFlag
    {
        BaseHref = 0,
        Charset =  1,
        Meta = 1 << 2,
        OpenGraph = 1 << 3,
        Script = 1 << 4,
        Title = 1 << 5,
        TitleFormat = 1 << 6,
        Stylesheet = 1 << 7,
        Link = 1 << 8,

        UniqueByType = 1 << 16,
        UniqueByName = 1 << 17,
    }

    [JsonConverter(typeof(RendererJsonConverter))]
    public readonly partial struct Renderer : IEquatable<Renderer>
    {
        private readonly RendererFlag _flag;
        private readonly string _name;
        private readonly string _mainAttributeValue;
        private readonly int _optionalAttributes;
        private readonly string _title;
        private readonly string _type;

        private Renderer(
            RendererFlag flag, 
            string mainAttributeValue, 
            string? name = null,
            int optionalAttributes = 0,
            string title = "",
            string type = ""
        )
        {
            _flag = flag;
            _name = name ?? string.Empty;
            _mainAttributeValue = mainAttributeValue;
            _optionalAttributes = optionalAttributes;
            _title = title;
            _type = type;
        }

        public int Render(RenderTreeBuilder renderTreeBuilder, int seq, NavigationManager navigationManager)
        {
            return GetTypeFlagValue(_flag) switch
            {
                RendererFlag.Charset => CharsetRender(renderTreeBuilder, seq, navigationManager),
                RendererFlag.BaseHref => BaseHrefRender(renderTreeBuilder, seq, navigationManager),
                RendererFlag.Meta => MetaRender(renderTreeBuilder, seq, navigationManager),
                RendererFlag.Script => ScriptRender(renderTreeBuilder, seq, navigationManager),
                RendererFlag.Stylesheet => StylesheetRender(renderTreeBuilder, seq, navigationManager),
                RendererFlag.Title => TitleRender(renderTreeBuilder, seq, navigationManager),
                RendererFlag.OpenGraph => OpenGraphRender(renderTreeBuilder, seq, navigationManager),
                RendererFlag.Link => LinkRender(renderTreeBuilder, seq, navigationManager),
                _ => seq,
            };
        }

        public bool Equals(Renderer other)
        {
            if ((_flag & RendererFlag.UniqueByName) == 0 && _name != other._name)
                return false;
            if (GetTypeFlagValue(_flag) != GetTypeFlagValue(other._flag))
                return false;
            return true;
        }

#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
        public override bool Equals(object obj)
#pragma warning restore CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
        {
            if (obj is null)
            {
                return false;
            }

            return Equals((Renderer)obj);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(_flag, _name);
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        static RendererFlag GetTypeFlagValue(RendererFlag flag)
        {
            return (RendererFlag)(short)flag;
        }

        public static bool operator ==(Renderer left, Renderer right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Renderer left, Renderer right)
        {
            return !(left == right);
        }

        class RendererJsonConverter : JsonConverter<Renderer>
        {
            public override Renderer Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                return default;
            }
            public override void Write(Utf8JsonWriter writer, Renderer value, JsonSerializerOptions options)
            {
                writer.WriteStartObject();
                writer.WriteNumber("flag", (int)value._flag);
                if (!string.IsNullOrEmpty(value._name))
                    writer.WriteString("name", value._name);
                if (!string.IsNullOrEmpty(value._mainAttributeValue))
                    writer.WriteString("value", value._mainAttributeValue);
                if (value._optionalAttributes != 0)
                    writer.WriteNumber("opt", value._optionalAttributes);
                writer.WriteEndObject();
            }
        }
    }

}
