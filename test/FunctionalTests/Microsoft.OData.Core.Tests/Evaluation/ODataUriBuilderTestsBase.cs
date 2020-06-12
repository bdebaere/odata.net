﻿//---------------------------------------------------------------------
// <copyright file="ODataUriBuilderBaseTests.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System;
using Microsoft.OData.Evaluation;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Tests.Evaluation
{
    public abstract class ODataUriBuilderTestsBase
    {
        private readonly Uri defaultBaseUri;
        private readonly IEdmStructuredValue defaultProductInstance;
        private readonly IEdmModel model;

        protected ODataUriBuilderTestsBase()
        {
            this.defaultBaseUri = new Uri("http://odata.org/base/");
            this.model = TestModel.BuildDefaultTestModel();
            this.defaultProductInstance = TestModel.BuildDefaultProductValue(TestModel.GetEntityType(this.model, "TestModel.Product"));
        }

        internal void BuildEntitySetUriShouldValidateArgumentsImpl(ODataUriBuilder builder)
        {
            this.VerifyBaseUriAndStringNullOrEmptyArgumentValidation((baseUri, entitySet) => builder.BuildEntitySetUri(baseUri, entitySet), "entitySetName");
        }

        internal void BuildStreamEditLinkUriShouldValidateArgumentsImpl(ODataUriBuilder builder)
        {
            this.VerifyBaseUriAndStringEmptyArgumentValidation((baseUri, streamPropertyName) => builder.BuildStreamEditLinkUri(baseUri, streamPropertyName), "streamPropertyName");
        }

        internal void BuildStreamReadLinkUriShouldValidateArgumentsImpl(ODataUriBuilder builder)
        {
            this.VerifyBaseUriAndStringEmptyArgumentValidation((baseUri, streamPropertyName) => builder.BuildStreamReadLinkUri(baseUri, streamPropertyName), "streamPropertyName");
        }

        internal void BuildNavigationLinkUriShouldValidateArgumentsImpl(ODataUriBuilder builder)
        {
            this.VerifyBaseUriAndStringNullOrEmptyArgumentValidation((baseUri, navigationPropertyName) => builder.BuildNavigationLinkUri(baseUri, navigationPropertyName), "navigationPropertyName");
        }

        internal void BuildAssociationLinkUriShouldValidateArgumentsImpl(ODataUriBuilder builder)
        {
            this.VerifyBaseUriAndStringNullOrEmptyArgumentValidation((baseUri, associationLinkName) => builder.BuildAssociationLinkUri(baseUri, associationLinkName), "navigationPropertyName");
        }

        internal void BuildOperationTargetUriShouldValidateArgumentsImpl(ODataUriBuilder builder)
        {
            this.VerifyBaseUriAndStringNullOrEmptyArgumentValidation((baseUri, operationName) => builder.BuildOperationTargetUri(baseUri, operationName, null, null), "operationName");
        }

        protected virtual void VerifyBaseUriArgumentValidation(Action<Uri> action)
        {
            // base class does not validate baseUri, so nothing to do here
        }

        private void VerifyStringNullOrEmptyArgumentValidation(Action<string> action, string argumentName)
        {
            action.ShouldThrowOnNullOrEmptyStringArgument(argumentName);
        }

        private void VerifyStringEmptyArgumentValidation(Action<string> action, string argumentName)
        {
            action.ShouldThrowOnEmptyStringArgument<ArgumentException>(argumentName);
        }

        private void VerifyBaseUriAndStringNullOrEmptyArgumentValidation(Action<Uri, string> action, string argumentName)
        {
            this.VerifyBaseUriArgumentValidation((baseUri) => action(baseUri, "StringValue"));
            this.VerifyStringNullOrEmptyArgumentValidation((stringArg) => action(this.defaultBaseUri, stringArg), argumentName);
        }

        private void VerifyBaseUriAndStringEmptyArgumentValidation(Action<Uri, string> action, string argumentName)
        {
            this.VerifyBaseUriArgumentValidation((baseUri) => action(baseUri, "StringValue"));
            this.VerifyStringEmptyArgumentValidation((stringArg) => action(this.defaultBaseUri, stringArg), argumentName);
        }
    }
}
