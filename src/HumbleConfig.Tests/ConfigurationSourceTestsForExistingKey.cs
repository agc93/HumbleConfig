﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace HumbleConfig.Tests
{
    [TestFixture(typeof(bool))]
    [TestFixture(typeof(byte))]
    [TestFixture(typeof(char))]
    [TestFixture(typeof(decimal))]
    [TestFixture(typeof(double))]
    [TestFixture(typeof(float))]
    [TestFixture(typeof(int))]
    [TestFixture(typeof(long))]
    [TestFixture(typeof(sbyte))]
    [TestFixture(typeof(short))]
    [TestFixture(typeof(string))]
    public abstract class ConfigurationSourceTestsForExistingKey<TValue, TConfigurationSourceFactory> where TConfigurationSourceFactory : IConfigurationSourceFactory, new ()
    {
        private IConfigurationSource _source;
        private bool _result;
        private TValue _value;
        private readonly Fixture _fixture = new Fixture();
        private string _key;
        private TValue _expectedValue;

        [TestFixtureSetUp]
        public void ConfigRSourceTestsWithExistingConfigRKey()
        {
            _key = _fixture.Create<string>();
            _expectedValue = _fixture.Create<TValue>();
            
            var sourceFactory = new TConfigurationSourceFactory();
            _source = sourceFactory.Create(_key, _expectedValue);
        }

        [SetUp]
        public void WhenTryingToGetTheAppSettings()
        {
            _result = _source.TryGetAppSetting(_key, out _value);
        }

        [Test]
        public void ThenTheCorrectValueIsReturned()
        {
            Assert.That(_value, Is.EqualTo(_expectedValue));
        }

        [Test]
        public void ThenTheResultIsTrue()
        {
            Assert.That(_result, Is.True);
        }
    }
}
