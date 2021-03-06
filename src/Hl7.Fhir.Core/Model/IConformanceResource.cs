﻿/*
  Copyright (c) 2011-2012, HL7, Inc
  All rights reserved.
  
  Redistribution and use in source and binary forms, with or without modification, 
  are permitted provided that the following conditions are met:
  
   * Redistributions of source code must retain the above copyright notice, this 
     list of conditions and the following disclaimer.
   * Redistributions in binary form must reproduce the above copyright notice, 
     this list of conditions and the following disclaimer in the documentation 
     and/or other materials provided with the distribution.
   * Neither the name of HL7 nor the names of its contributors may be used to 
     endorse or promote products derived from this software without specific 
     prior written permission.
  
  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
  ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
  WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
  IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
  INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
  NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
  WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
  ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
  POSSIBILITY OF SUCH DAMAGE.
  
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hl7.Fhir.Introspection;

namespace Hl7.Fhir.Model
{
    public interface IConformanceResource
    {
        string Name { get; set; }
        string Url { get; set; }
        //List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
        //string Display { get; set; }
        string Publisher { get; set; }
        //List<Hl7.Fhir.Model.StructureDefinition.StructureDefinitionContactComponent> Contact { get; set; }
        string Description { get; set; }
        //string Requirements { get; set; }
        //Hl7.Fhir.Model.FhirString CopyrightElement { get; set; }
        //List<Hl7.Fhir.Model.Coding> Code { get; set; }
        ConformanceResourceStatus? Status { get; set; }
        bool? Experimental { get; set; }
        string Date { get; set; }
        List<CodeableConcept> UseContext { get; set; }
        //List<ContactPoint> Contact { get; set; }
    }
    public interface IConformanceResourceContact
    {
        Hl7.Fhir.Model.FhirString NameElement { get; set; }
        string Name { get; set; }
        List<Hl7.Fhir.Model.ContactPoint> Telecom { get; set; }
    }

    public interface IVersionableConformanceResource : IConformanceResource
    {
        string Version { get; set; }
        
    }

    public partial class StructureDefinition : IVersionableConformanceResource
    {
        public partial class StructureDefinitionContactComponent : IConformanceResourceContact
        { }
    }   

    public partial class ValueSet : IVersionableConformanceResource
    {
        public partial class ValueSetContactComponent : IConformanceResourceContact
        { }
    }

    public partial class OperationDefinition : IVersionableConformanceResource
    {
        //Should have UseContext too
        [NotMapped]
        public List<CodeableConcept> UseContext
        {
            get { return null; }
            set {; }
        }

        public partial class OperationDefinitionContactComponent : IConformanceResourceContact
        { }
    }

    public partial class SearchParameter : IConformanceResource
    {
        //Should have UseContext too
        [NotMapped]
        public List<CodeableConcept> UseContext
        {
            get { return null; }
            set {; }
        }

        public partial class SearchParameterContactComponent : IConformanceResourceContact
        { }
    }

    public partial class DataElement : IConformanceResource
    {
        // I think DataElement should have Description too
        [NotMapped]
        public string Description
        {
            get { return null; }
            set { ; }
        }

        public partial class DataElementContactComponent : IConformanceResourceContact
        { }
    }

    public partial class ConceptMap : IVersionableConformanceResource
    {
        public partial class ConceptMapContactComponent : IConformanceResourceContact
        { }
    }

    public partial class Conformance : IVersionableConformanceResource
    {
        //Should have UseContext too
        [NotMapped]
        public List<CodeableConcept> UseContext
        {
            get { return null; }
            set {; }
        }
        public partial class ConformanceContactComponent : IConformanceResourceContact
        { }
    }

    public partial class NamingSystem : IConformanceResource
    {
        // I think NamingSystem should have Experimental too
        [NotMapped]
        public bool? Experimental
        {
            get { return null; }
            set { ; }
        }

        [NotMapped]
        public string Url
        {
            get { return null; }
            set { ; }
        }

        public partial class NamingSystemContactComponent : IConformanceResourceContact
        { }
    }

    public partial class ImplementationGuide : IVersionableConformanceResource
    {
        public partial class ImplementationGuideContactComponent : IConformanceResourceContact
        { }
    }

    public partial class TestScript : IVersionableConformanceResource
    {
        public partial class TestScriptContactComponent : IConformanceResourceContact
        { }
    }
}
