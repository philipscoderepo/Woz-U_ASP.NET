#pragma checksum "C:\Users\patas\OneDrive\Documents\Dev\Infosys Training\ASP.NET\SecurityFromScratch\Views\Security\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6903e287606390f3112513c5eaff7cb0e58a9432"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Security_Index), @"mvc.1.0.view", @"/Views/Security/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6903e287606390f3112513c5eaff7cb0e58a9432", @"/Views/Security/Index.cshtml")]
    public class Views_Security_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<SecurityFromScratch.Models.SecurityModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<form>\r\n    PlainText: <textarea id=\"PlainText\" name=\"PlainText\">");
#nullable restore
#line 4 "C:\Users\patas\OneDrive\Documents\Dev\Infosys Training\ASP.NET\SecurityFromScratch\Views\Security\Index.cshtml"
                                                    Write(Model?.PlainText);

#line default
#line hidden
#nullable disable
            WriteLiteral("</textarea><br />\r\n    ProtectedText: <textarea id=\"ProtectedText\" name=\"ProtectedText\">");
#nullable restore
#line 5 "C:\Users\patas\OneDrive\Documents\Dev\Infosys Training\ASP.NET\SecurityFromScratch\Views\Security\Index.cshtml"
                                                                Write(Model?.ProtectedText);

#line default
#line hidden
#nullable disable
            WriteLiteral("</textarea><br />\r\n    Mechanism: <input id=\"MethodText\" name=\"MethodText\"");
            BeginWriteAttribute("value", " value=\"", 316, "\"", 342, 1);
#nullable restore
#line 6 "C:\Users\patas\OneDrive\Documents\Dev\Infosys Training\ASP.NET\SecurityFromScratch\Views\Security\Index.cshtml"
WriteAttributeValue("", 324, Model?.MethodText, 324, 18, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" /><br />\r\n\r\n    <input type=\"submit\" id=\"Protect\" value=\"Protect\" formaction=\"/Security/Protect\" formmethod=\"post\" />\r\n    <input type=\"submit\" id=\"UnProtect\" value=\"UnProtect\" formaction=\"/Security/Unprotect\" formmethod=\"post\" />\r\n</form>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<SecurityFromScratch.Models.SecurityModel> Html { get; private set; }
    }
}
#pragma warning restore 1591