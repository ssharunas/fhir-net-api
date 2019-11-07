This is the support (now obsolete) API for working with the DSTU version of [HL7 FHIR][1] on the Microsoft .NET (dotnet) platform. 
The API deals with the HTTP and wire format, so you can write code like this to manipulate a patient's data: 

	var client = new FhirClient("http://spark.furore.com/fhir");

	var pat = client.Read<Patient>("Patient/1");
	pat.Resource.Name.Add(HumanName.ForFamily("Kramer")WithGiven("Ewout"));
	client.Update<Patient>(pat);

This library provides:
* Class models for working with the FHIR data model using POCO's
* Xml and Json parsers and serializers
* A REST client for working with FHIR-compliant servers

### ESPBI ###
This branch is customized for Lithuanian e-health system.
It does not implement authentication, but you can do it yourself by modifying raw request on FhirClient.OnBeforeRequest event.

Please note, that not all API is supported by Lithuanian system, i.e. tags, history, validation, system wide search, etc.
However it adds additional functions for signing and retrieving pdf's.

### Get Started ###
Get started by reading the [online documentation][3].

If you want to parcitipate in this project, do it in original repo [https://github.com/FirelyTeam/fhir-net-api]
