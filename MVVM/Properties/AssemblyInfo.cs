using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle(" BestCS.MVVM")]
[assembly: AssemblyDescription("A lightweight framework to implement Model-View-ViewModel applications in WPF, Silverlight and Windows Phone 7")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct(" BestCS.MVVM")]
[assembly: AssemblyCopyright("")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

[assembly: ComVisible(false)]
[assembly: CLSCompliant(true)]

[assembly: AssemblyVersion("4.0.0.*")]


// FxCop
[module: SuppressMessage("Microsoft.Naming",
    "CA1704:IdentifiersShouldBeSpelledCorrectly",
    MessageId = "Mvvm")]

[module: SuppressMessage("Microsoft.Naming",
    "CA1704:IdentifiersShouldBeSpelledCorrectly",
    Scope = "namespace",
    Target = " BestCS.MVVM",
    MessageId = "Mvvm")]

[module: SuppressMessage("Microsoft.Naming",
    "CA1704:IdentifiersShouldBeSpelledCorrectly",
    Scope = "namespace",
    Target = " BestCS.MVVM.Messaging",
    MessageId = "Mvvm")]

[module: SuppressMessage("Microsoft.Naming",
    "CA1704:IdentifiersShouldBeSpelledCorrectly",
    Scope = "namespace",
    Target = " BestCS.MVVM.Command",
    MessageId = "Mvvm")]

[module: SuppressMessage("Microsoft.Naming",
    "CA1704:IdentifiersShouldBeSpelledCorrectly",
    Scope = "namespace",
    Target = " BestCS.MVVM.Helpers",
    MessageId = "Mvvm")]

[module: SuppressMessage("Microsoft.Design",
    "CA1020:AvoidNamespacesWithFewTypes",
    Scope = "namespace",
    Target = " BestCS.MVVM")]

[module: SuppressMessage("Microsoft.Design",
    "CA1020:AvoidNamespacesWithFewTypes",
    Scope = "namespace",
    Target = " BestCS.MVVM.Command")]

[module: SuppressMessage("Microsoft.Design",
    "CA1020:AvoidNamespacesWithFewTypes",
    Scope = "namespace",
    Target = " BestCS.MVVM.Helpers")]