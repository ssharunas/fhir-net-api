using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Hl7.Fhir.Specification")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyProduct("Hl7.Fhir.Specification")]
[assembly: AssemblyCopyright("Copyright © Ewout Kramer and collaborators 2014")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: NeutralResourcesLanguage("en")]
[assembly: ComVisible(false)]
//[assembly: System.CLSCompliant(true)]
// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("0.11.5.*")]

#if DEBUG
[assembly: InternalsVisibleTo("Hl7.Fhir.Specification.Tests, PublicKey=0024000004800000940000000602000000240000525341310004000001000100b71254d32b9529b2a334aa7c5e661cbb6a570fa264b48de5e3ab44ecf99a58708ec111ff6c478ee36148f0b2273f7a3d118d76e834aa552c105a04f237a0fbb7815bac0a467a456247d3738a1c6eb4e3567d8af829e670854c31ee030e3d7a4cb5ab6a50685c05dd259e6e75c64d0285310411947428b6bf02a6c44f66d456b9")]
#endif

#if RELEASE
[assembly:AssemblyKeyFileAttribute("..\\FhirNetApi.snk")]
#endif