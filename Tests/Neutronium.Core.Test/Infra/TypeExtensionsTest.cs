﻿using System;
using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using Neutronium.Core.Infra;
using Xunit;

namespace Neutronium.Core.Test.Infra
{
    public class TypeExtensionsTest
    {   
        private class ListDecimal: List<decimal> 
        {
        }

        [Theory]
        [InlineData(typeof(IEnumerable<int>), typeof(int))]
        [InlineData(typeof(List<object>), typeof(object))]
        [InlineData(typeof(IList<string>), typeof(string))]
        [InlineData(typeof(int[]), typeof(int))]
        [InlineData(typeof(ListDecimal), typeof(decimal))]
        public void GetEnumerableBase_returns_collection_item_type(Type source, Type expected)
        {
            source.GetEnumerableBase().Should().Be(expected);
        }

        [Theory]
        [InlineData(typeof(int))]
        [InlineData(typeof(string))]
        [InlineData(typeof(IList))]
        [InlineData(null)]
        public void GetEnumerableBase_returns_null_when_no_match(Type source)
        {
            source.GetEnumerableBase().Should().BeNull();
        }

        [Theory]
        [InlineData(typeof(Nullable<int>), typeof(int))]
        [InlineData(typeof(Nullable<double>), typeof(double))]
        public void GetUnderlyingNullableType_Returns_Expected_Value(Type type, Type expected)
        {
            type.GetUnderlyingNullableType().Should().Be(expected);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(typeof(IList<string>))]
        [InlineData(typeof(string))]
        public void GetUnderlyingNullableType_Returns_Expected_Null_Value(Type type)
        {
            type.GetUnderlyingNullableType().Should().BeNull();
        }

        [Theory]
        [InlineData(typeof(Nullable<int>), typeof(int))]
        [InlineData(typeof(Nullable<double>), typeof(double))]
        [InlineData(typeof(IList<string>), typeof(IList<string>))]
        [InlineData(typeof(string), typeof(string))]
        public void GetUnderlyingType_Returns_Expected_Null_Value(Type type, Type expected)
        {
            type.GetUnderlyingType().Should().Be(expected);
        }

        [Theory]
        [InlineData(typeof(UInt16), true)]
        [InlineData(typeof(UInt32), true)]
        [InlineData(typeof(UInt64), true)]
        [InlineData(typeof(Int16), false)]
        [InlineData(typeof(Int32), false)]
        [InlineData(typeof(Int64), false)]
        [InlineData(null, false)]
        public void IsUnsigned_returns_expected_value(Type type, bool result)
        {
            var isUnsigned = type.IsUnsigned();
            isUnsigned.Should().Be(result);
        }

        [Theory]
        [InlineData(typeof(Dictionary<string, object>), typeof(object))]
        [InlineData(typeof(Dictionary<string, string>), typeof(string))]
        [InlineData(typeof(Dictionary<string, int>), typeof(int))]
        public void GetDictionaryStringValueType_Returns_Expected_Null_Value(Type type, Type expected)
        {
            type.GetDictionaryStringValueType().Should().Be(expected);
        }


        [Theory]
        [InlineData(typeof(int))]
        [InlineData(typeof(string))]
        [InlineData(typeof(IList))]
        [InlineData(typeof(Dictionary<object,object>))]
        [InlineData(typeof(Dictionary<int, string>))]
        public void GetDictionaryStringValueType_returns_null_when_no_match(Type source)
        {
            source.GetDictionaryStringValueType().Should().BeNull();
        }

        [Theory]
        [InlineData(typeof(Dictionary<string, object>), typeof(IDictionary<,>), typeof(string))]
        [InlineData(typeof(List<int>), typeof(IList<>), typeof(int))]
        [InlineData(typeof(List<decimal>), typeof(IEnumerable<>), typeof(decimal))]
        public void GetInterfaceGenericType_Returns_Expected_Null_Value(Type type, Type genericInterfaceType, Type expected) 
        {
            type.GetInterfaceGenericType(genericInterfaceType).Should().Be(expected);
        }


        [Theory]
        [InlineData(typeof(Array), typeof(IList<>))]
        [InlineData(typeof(Dictionary<int, string>), typeof(IList<>))]
        [InlineData(typeof(List<int>), typeof(IDictionary<,>))]
        public void GetInterfaceGenericType_returns_null_when_no_match(Type source, Type genericInterfaceType) 
        {
            source.GetInterfaceGenericType(genericInterfaceType).Should().BeNull();
        }
    }
}
