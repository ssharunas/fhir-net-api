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

### Template system ###
#### Supported nodes for templates
`<x-include source="" root="" scope=""></x-include>`
	Includes content from another file. Content of <x-include> will be moved to the root node of included XML file content.
	If XML file content contains matching elements to <x-include> (by name and x-id), elements in XML file will be replaced 
	with matching <x-include> elements.
	
##### Properties:
* source - mandatory XML file identifier. It will be passed to _IDataContext.GetInclude()_ to read XML file content.
* root - optional name of root element of XML content. If not provided - name of original XML content is used.
* scope - optional scope for IDataContext. It will be prepended to the key for getter and setter methods.

##### Example:
```
<Composition>
	<x-include source="my-section.xml" root="renamed-root-node" scope="Data.">
		<link href="..."></link>
		<content x-id="abc">CHANGED</content>
	</x-include>
</Composition>
```
my-section.xml:
```
<section>
	<code><coding>123</coding></code>
	<content x-id="abc"></content>
</section>
```
resulting xml:
```
<Composition>
	<renamed-root-node>
		<code><coding>123</coding></code>
		<link href="..."></link>
	<content x-id="abc">CHANGED</content>
	</renamed-root-node>
</Composition>
```
Mapped DTO object:
```
var obj = {
	Data: {...},
	...
}
```

#### Supported attributes for all template nodes:
`x-id="XPath"`
	This property is used to distinguish XML element from other elements. XPath must return non-empty result for the element is is identifying and empty result for other elements.
	Example:
		```<Composition>
			<section x-id="code/@value = '1'"><code value="1"></code></section>
			<section x-id="code/@value = '2'"><code value="2"></code></section>
		</Composition>```

`x-read="true|false|first"`
	Can be used to disable data reading from XML node (including it's child elements). Default value is true (data reading is not disabled)
	"first" is used when there are multiiple xml nodes matching same object's property. 
	If x-read=first, only first matching xml node is used to read data, if x-read=true and there are multiple nodes, matching same xml node (by x-id), an exception is thrown.
	If x-read=false, matching xml nodes are not read.
	
`x-write="true|false|always"`
	Can be used to disable data writing to XML node (including it's child elements). Default value is true (data writing is not disabled)
	By default node is not updated, only created if it has 'x-read=false', but this behavour can be overwriten  with x-write=always.

`x-if="PropertyName"`
	PropertyName is passed to *IDataGetterContext.GetBoolean()*. If data getter returns false - template node is removed. Otherwise template node is not changed.
	
`x-foreach="PropertyName"`
	Repeats element for each value in "PropertyName" array. Array is retrieved by *IDataGetterContext.GetArray()* for data getting. For data 
	setting *IDataSetterContext.GetArrayElementSetter()* is used
	Example:
	XML:
```
<Composition>
	<section x-foreach="Items"><code value="{.}"/></section>
</Composition>
```
for data `var obj = { Items = [1, 2, 3] }` result will be:
```
<Composition>
	<section><code value="1"/></section>
	<section><code value="2"/></section>
	<section><code value="3"/></section>
</Composition>
```

#### Values:
Text in curly brackets `{...}` will be replaced with a string, returned by *IDataGetterContext.GetString()*. For setting *IDataSetterContext.SetData()* is used.
