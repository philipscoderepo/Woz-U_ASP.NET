#pragma checksum "C:\Users\patas\OneDrive\Documents\Dev\Infosys Training\ASP.NET\SecurityFromScratch\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c513988086418f83fa7fff4da98618b68fab8e83"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c513988086418f83fa7fff4da98618b68fab8e83", @"/Views/Home/Index.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("User: ");
#nullable restore
#line 6 "C:\Users\patas\OneDrive\Documents\Dev\Infosys Training\ASP.NET\SecurityFromScratch\Views\Home\Index.cshtml"
  Write(User);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<br />\r\nUser.Identity: ");
#nullable restore
#line 8 "C:\Users\patas\OneDrive\Documents\Dev\Infosys Training\ASP.NET\SecurityFromScratch\Views\Home\Index.cshtml"
           Write(User?.Identity?.ToString()??"NULL");

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<br />\r\nUser.Identity.Name: ");
#nullable restore
#line 10 "C:\Users\patas\OneDrive\Documents\Dev\Infosys Training\ASP.NET\SecurityFromScratch\Views\Home\Index.cshtml"
                Write(User?.Identity?.Name??"NULL");

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<br />\r\nUser.Identity.IsAuthenticated: ");
#nullable restore
#line 12 "C:\Users\patas\OneDrive\Documents\Dev\Infosys Training\ASP.NET\SecurityFromScratch\Views\Home\Index.cshtml"
                           Write(User?.Identity?.IsAuthenticated.ToString()??"NULL");

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<br />\r\nUser.Identity.AuthenticationType: ");
#nullable restore
#line 14 "C:\Users\patas\OneDrive\Documents\Dev\Infosys Training\ASP.NET\SecurityFromScratch\Views\Home\Index.cshtml"
                              Write(User.Identity.AuthenticationType??"NULL");

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<br />\r\n\r\nCookies.Count ");
#nullable restore
#line 17 "C:\Users\patas\OneDrive\Documents\Dev\Infosys Training\ASP.NET\SecurityFromScratch\Views\Home\Index.cshtml"
         Write(this.Context.Request.Cookies.Count);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<br />\r\n<hr />\r\n");
#nullable restore
#line 20 "C:\Users\patas\OneDrive\Documents\Dev\Infosys Training\ASP.NET\SecurityFromScratch\Views\Home\Index.cshtml"
 foreach (var c in Context.Request.Cookies)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("<p>Cookie: ");
#nullable restore
#line 21 "C:\Users\patas\OneDrive\Documents\Dev\Infosys Training\ASP.NET\SecurityFromScratch\Views\Home\Index.cshtml"
        Write(c);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>");
#nullable restore
#line 21 "C:\Users\patas\OneDrive\Documents\Dev\Infosys Training\ASP.NET\SecurityFromScratch\Views\Home\Index.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("<hr />\r\n\r\nClaims.Count: ");
#nullable restore
#line 24 "C:\Users\patas\OneDrive\Documents\Dev\Infosys Training\ASP.NET\SecurityFromScratch\Views\Home\Index.cshtml"
          Write(User.Claims.Count());

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<br />\r\n<hr />\r\n");
#nullable restore
#line 27 "C:\Users\patas\OneDrive\Documents\Dev\Infosys Training\ASP.NET\SecurityFromScratch\Views\Home\Index.cshtml"
 foreach (var s in User.Claims)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("<p>Clain: ");
#nullable restore
#line 28 "C:\Users\patas\OneDrive\Documents\Dev\Infosys Training\ASP.NET\SecurityFromScratch\Views\Home\Index.cshtml"
       Write(s);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>");
#nullable restore
#line 28 "C:\Users\patas\OneDrive\Documents\Dev\Infosys Training\ASP.NET\SecurityFromScratch\Views\Home\Index.cshtml"
                   }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<hr />\r\n<a href=\"\\Home\\secured\">Click here to access the secured page</a>\r\n<br />\r\n<br />\r\n<a href=\"\\Home\\logout\">Click here to logout</a>\r\n<br />\r\n<a href=\"\\Home\\login\">Click here to login</a>\r\n<br />\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
