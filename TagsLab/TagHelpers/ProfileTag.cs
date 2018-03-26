using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Web;
namespace TagsLab.TagHelpers
{
    [HtmlTargetElement("profile")]
    public class ProfileTag : TagHelper
    {
        private IHostingEnvironment _env;
        public ProfileTag(IHostingEnvironment env)
        {
            _env = env;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string html = string.Empty;
            html += "<h1>Robbie Perry</h1>";
            html += "<img src ='/images/Robargh.jpg' style='width: 50px' runat='server' />";

               html += "<h3>Place of Birth:</h3>";
            html += "Richmond, BC";

            html += "<h3>Passion:</h3>";
            html += "React.js";

            html += "<h3>Future ambitions:</h3>";
            html += "Figure out wtf is Redux";

            output.Content.SetHtmlContent(html);
        }
    }
}
