#pragma checksum "C:\Projetos\AgendaOdontologica\AgendaOdontologica\Views\Shared\Error.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "bda836dd06835874aa09a14493b39a532eebb4cb"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Error), @"mvc.1.0.view", @"/Views/Shared/Error.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/Error.cshtml", typeof(AspNetCore.Views_Shared_Error))]
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
#line 1 "C:\Projetos\AgendaOdontologica\AgendaOdontologica\Views\_ViewImports.cshtml"
using AgendaOdontologica;

#line default
#line hidden
#line 2 "C:\Projetos\AgendaOdontologica\AgendaOdontologica\Views\_ViewImports.cshtml"
using AgendaOdontologica.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bda836dd06835874aa09a14493b39a532eebb4cb", @"/Views/Shared/Error.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"340dced6ca245596f9f743138b219c153216b240", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Error : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<AgendaOdontologica.Models.ErrorViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\Projetos\AgendaOdontologica\AgendaOdontologica\Views\Shared\Error.cshtml"
  
    ViewData["Title"] = "Error";

#line default
#line hidden
            BeginContext(90, 63, true);
            WriteLiteral("\r\n<h1 class=\"text-danger\">Error.</h1>\r\n<h2 class=\"text-danger\">");
            EndContext();
            BeginContext(154, 14, false);
#line 7 "C:\Projetos\AgendaOdontologica\AgendaOdontologica\Views\Shared\Error.cshtml"
                   Write(Model.Mensagem);

#line default
#line hidden
            EndContext();
            BeginContext(168, 9, true);
            WriteLiteral("</h2>\r\n\r\n");
            EndContext();
#line 9 "C:\Projetos\AgendaOdontologica\AgendaOdontologica\Views\Shared\Error.cshtml"
 if (Model.ShowRequestId)
{

#line default
#line hidden
            BeginContext(207, 52, true);
            WriteLiteral("    <p>\r\n        <strong>Request ID:</strong> <code>");
            EndContext();
            BeginContext(260, 15, false);
#line 12 "C:\Projetos\AgendaOdontologica\AgendaOdontologica\Views\Shared\Error.cshtml"
                                      Write(Model.RequestId);

#line default
#line hidden
            EndContext();
            BeginContext(275, 19, true);
            WriteLiteral("</code>\r\n    </p>\r\n");
            EndContext();
#line 14 "C:\Projetos\AgendaOdontologica\AgendaOdontologica\Views\Shared\Error.cshtml"
}

#line default
#line hidden
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<AgendaOdontologica.Models.ErrorViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
