using System.Text;
using static System.Console;
namespace Builder 
{
    public class HtmlElement
    {
        public string TagName { get; set; }
        public string Content { get; set; }
        public List<HtmlElement> Children { get; set; } = new List<HtmlElement>();
        public const int IndentSize = 4;

        public HtmlElement(string tagName, string content)
        {
            TagName = tagName;
            Content = content;
        }
        public string ToStringImpl(int depth)
        {
            var sb = new StringBuilder();
            var i = new string(' ', depth * IndentSize);
            sb.AppendLine($"{i}<{TagName}>");
            if (!string.IsNullOrEmpty(Content))
            {
                sb.AppendLine($"{i}    {Content}");
            }
            foreach (var child in Children)
            {
                sb.AppendLine(child.ToStringImpl(depth + 1));
            }
            sb.Append($"{i}</{TagName}>");
            return sb.ToString();
        }
        public override string ToString()
        {
            return ToStringImpl(0);
        }
    }
    public class HtmlBuilder
    {
        private readonly HtmlElement _root;
        public HtmlBuilder(string rootName)
        {
            _root = new HtmlElement(rootName, string.Empty);
        }
        public HtmlBuilder AddChild(string childName, string childContent)
        {
            var child = new HtmlElement(childName, childContent);
            _root.Children.Add(child);
            return this;
        }
        public override string ToString()
        {
            return _root.ToString();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new StringBuilder();
            var list = new List<string> { "Item 1", "Item 2" };
            var list2 = new List<string> { "Item 3", "Item 4" };


            builder.AppendLine("<ul>");
            foreach (var item in list)
            {
                builder.AppendLine("    <li>");
                builder.AppendLine($"        {item}");
                builder.AppendLine("    </li>");
            }
            builder.AppendLine("</ul>");
            WriteLine(builder.ToString());
            WriteLine("--------------------------------------------------");
            var body = new HtmlBuilder("body");
            var ul = new HtmlBuilder("ul");
            foreach (var item in list)
            {
                ul.AddChild("li", item);
            }
            var ul2 = new HtmlBuilder("ul");
            foreach (var item in list2)
            {
                ul2.AddChild("li", item);
            }
            body.AddChild("ul", ul.ToString())
           .AddChild("ul", ul2.ToString());
            WriteLine(body.ToString());



            /*
             * <ul>
                <li>
                    Item 1
                </li>
                <li>
                    Item 2
                </li>
               </ul>
             */
        }
    }
}