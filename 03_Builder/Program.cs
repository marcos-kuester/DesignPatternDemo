using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace _03_Builder
{
    public class HtmlElement
    {
        public string TagName { get; set; }
        public string InnerText { get; set; }
        public List<HtmlElement> Elements { get; set; } = new List<HtmlElement>();

        public HtmlElement()
        {  }

        public HtmlElement(string tagName, string innerText)
        {
            TagName = tagName;
            InnerText = innerText;
        }

        private string ToStringInterpolation()
        {
            var sb = new StringBuilder();
            sb.Append($"<{TagName}>");

            sb.Append(InnerText);

            foreach (var item in Elements)
            {
                sb.Append(item.ToStringInterpolation());
            }

            sb.Append($"</{TagName}>");
            return sb.ToString();
        }

        public override string ToString()
        {
            return ToStringInterpolation();
        }
    }

    public class HtmlBuilder
    {
        private HtmlElement _rootElement = new HtmlElement();

        public HtmlBuilder(string rootTagName, string rootInnerText)
        {
            _rootElement.TagName = rootTagName;
            _rootElement.InnerText = rootInnerText;
        }

        public void AddChild(string tagName, string innerText)
        {
            var e = new HtmlElement(tagName, innerText);
            _rootElement.Elements.Add(e);
        }

        public HtmlBuilder AddChildFluentInterface(string tagName, string innerText)
        {
            var e = new HtmlElement(tagName, innerText);
            _rootElement.Elements.Add(e);
            return this;
        }

        public HtmlBuilder AddChildElement(HtmlElement element)
        {
            _rootElement.Elements.Add(element);
            return this;
        }

        public override string ToString()
        {
            return _rootElement.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var head = new HtmlElement("head", "");
            head.Elements.Add(new HtmlElement("title", "Builder Demo"));
            head.Elements.Add(new HtmlElement("script", "alert('hello');"));

            var table = new HtmlElement("table", "");
            var tableRow1 = new HtmlElement("tr", "");
            tableRow1.Elements.Add(new HtmlElement("th", "Product"));
            var tableRow2 = new HtmlElement("tr", "");
            tableRow2.Elements.Add(new HtmlElement("td", "Notebook"));
            table.Elements.Add(tableRow1);
            table.Elements.Add(tableRow2);

            var body = new HtmlElement("body", "the body content");
            body.Elements.Add(table);

            var html = new HtmlElement("html", "");
            html.Elements.Add(head);
            html.Elements.Add(body);
            WriteLine(html.ToString());


            // builders
            var htmlBuilder = new HtmlBuilder("html", "");
            htmlBuilder.AddChild("head", "");
            htmlBuilder.AddChild("body", "");
            WriteLine(htmlBuilder.ToString());


            htmlBuilder = new HtmlBuilder("table", "");
            htmlBuilder.AddChild("tr", "");
            htmlBuilder.AddChild("tr", "");
            WriteLine(htmlBuilder.ToString());

            // fluent
            htmlBuilder = new HtmlBuilder("html", "");
            htmlBuilder.AddChildFluentInterface("head", "").AddChildFluentInterface("body", "");
            WriteLine(htmlBuilder.ToString());


            htmlBuilder = new HtmlBuilder("table", "");
            htmlBuilder.AddChildFluentInterface("tr", "").AddChildFluentInterface("tr", "");
            WriteLine(htmlBuilder.ToString());

            // fluent
            htmlBuilder = new HtmlBuilder("table", "");
            htmlBuilder.AddChildElement(new HtmlElement("tr", ""))
                .AddChildElement(new HtmlElement("tr", ""));
            WriteLine(htmlBuilder.ToString());

            // Result:
            // --------------------------
            // <html>
            //  <head>
            //   <title>Builder Demo</title>
            //   <script>alert('hello');</script>
            //  </head>
            //  <body>
            //   the body content
            //   <table>
            //    <tr><th>Product</th></tr>
            //	  <tr><td>Notebook</td></tr>
            //   </table>
            //  </body>
            // </html>
            //
            // <html><head></head><body></body></html>
            // <table><tr></tr><tr></tr></table>
            // <html><head></head><body></body></html>
            // <table><tr></tr><tr></tr></table>
            // <table><tr></tr><tr></tr></table>
        }
    }
}
