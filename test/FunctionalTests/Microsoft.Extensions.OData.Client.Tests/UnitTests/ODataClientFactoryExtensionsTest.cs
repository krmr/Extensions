﻿//---------------------------------------------------------------------
// <copyright file="ODataClientFactoryExtensionsTest.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.OData.V3Client;
using System;
using System.Data.Services.Client;
using Xunit;

namespace Microsoft.Extensions.OData.Client.Tests.Netcore.UnitTests
{
    public class ODataClientFactoryExtensionsTest
    {
        [Theory]
        [InlineData("")]
        [InlineData("Verification")]
        public void TestAddODataClient(string clientName)
        {
            var sc = new ServiceCollection();
            var builder = sc.AddODataV3Client(clientName);
            builder.Name.Should().Be(clientName);

            var sp = sc.BuildServiceProvider();
            var factory = sp.GetRequiredService<IODataV3ClientFactory>();
            factory.Should().BeOfType<DefaultODataClientFactory>();

            var client = factory.CreateClient<DataServiceContext>(new Uri("http://localhost"), clientName);
            client.Should().NotBeNull();
        }

        [Theory]
        [InlineData(null)]
        public void TestAddODataClientNullName(string clientName)
        {
            var sc = new ServiceCollection();
            Assert.Throws<ArgumentNullException>(() => sc.AddODataV3Client(clientName));
        }

        [Theory]
        [InlineData("", "InvalidName")]
        [InlineData("InvalidName", "")]
        [InlineData("Verification", "InvalidName")]
        public void TestAddODataClientNameNotRegistered(string registerName, string createName)
        {
            var sc = new ServiceCollection();
            var builder = sc.AddODataV3Client(registerName);
            builder.Name.Should().Be(registerName);

            var sp = sc.BuildServiceProvider();
            var factory = sp.GetRequiredService<IODataV3ClientFactory>();

            var client = factory.CreateClient<DataServiceContext>(new Uri("http://localhost"), createName);
            client.Should().NotBeNull();
        }

        [Theory]
        [InlineData("", "InvalidName")]
        [InlineData("InvalidName", "")]
        [InlineData("Verification", "InvalidName")]
        public void Factory_MultipleCalls(string clientName1, string clientName2)
        {
            var sc = new ServiceCollection();
            var builder1 = sc.AddODataV3Client(clientName1);
            builder1.Name.Should().Be(clientName1);

            var builder2 = sc.AddODataV3Client(clientName2);
            builder2.Name.Should().Be(clientName2);

            var sp = sc.BuildServiceProvider();
            var factory = sp.GetRequiredService<IODataV3ClientFactory>();

            var client1 = factory.CreateClient<DataServiceContext>(new Uri("http://localhost"), clientName1);
            client1.Should().NotBeNull();

            var client2 = factory.CreateClient<DataServiceContext>(new Uri("http://localhost"), clientName2);
            client2.Should().NotBeNull();

            clientName2.Should().NotBeSameAs(clientName1);
        }
    }
}
