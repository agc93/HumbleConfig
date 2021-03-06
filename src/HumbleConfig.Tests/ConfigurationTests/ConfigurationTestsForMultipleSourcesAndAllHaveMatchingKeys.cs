﻿using HumbleConfig.Tests.Stubs;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace HumbleConfig.Tests.ConfigurationTests
{
    [TestFixtureSource(typeof(NonNullableTestFixtureCases))]
    [TestFixtureSource(typeof(NullableTestFixtureCases))]
    public class ConfigurationTestsForMultipleSourcesAndAllHaveMatchingKeys<TValue>
    {
        private Configuration _configuration;
        private TValue _value;

        private ConfigurationSourceStub _source1;
        private ConfigurationSourceStub _source2;
        private string _key;

        [OneTimeSetUp]
        public void GivenAConfigurationWithMultipleSourcesAndAllHaveMatchingKeys()
        {
            var fixture = new Fixture();
            _key = fixture.Create<string>();
            _configuration = new Configuration();

            _source1 = new ConfigurationSourceStub();
            _source1.AppSettings.Add(_key, fixture.Create<TValue>());

            _source2 = new ConfigurationSourceStub();
            _source2.AppSettings.Add(_key, fixture.Create<TValue>());

            _configuration.AddConfigurationSource(_source1);
            _configuration.AddConfigurationSource(_source2);
        }

        [SetUp]
        public void WhenGettingAnAppSetting()
        {
            _value = _configuration.GetAppSettingAsync<TValue>(_key).Result;
        }

        [Test]
        public void ThenTheValueReturnedMatchesTheFirstSource()
        {
            Assert.That(_value, Is.EqualTo(_source1.AppSettings[_key]));
        }
    }
}
