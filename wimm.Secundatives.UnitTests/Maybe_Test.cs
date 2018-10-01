using System;
using Xunit;

namespace wimm.Secundatives.UnitTests
{
    public class Maybe_Test
    {
        [Fact]
        public void Construct_ValueTypeDefault_EqualsNone() => Assert.Equal(Maybe<int>.None, new Maybe<int>());

        [Fact]
        public void Construct_RefernceTypeDefault_EqualsNone()
        {
            Assert.Equal(Maybe<string>.None, new Maybe<string>());
        }

        [Fact]
        public void Construct_ReferenceTypeNull_Throws()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new Maybe<string>(null));

            Assert.Equal("value", ex.ParamName);
        }

        [Fact]
        public void Construct_ValueTypeDefaultValue_DoesNotEqualNone() =>
            Assert.NotEqual(Maybe<int>.None, new Maybe<int>(default(int)));

        [Fact]
        public void Value_ValueTypeNone_Throws() =>
            Assert.Throws<InvalidOperationException>(() => new Maybe<int>().Value);

        [Fact]
        public void Value_ReferenceTypeNone_Throws() =>
            Assert.Throws<InvalidOperationException>(() => new Maybe<string>().Value);

        [Fact]
        public void Value_ValueTypeValue_IsValue()
        {
            var expectedValue = 42;

            var underTest = new Maybe<int>(expectedValue);

            Assert.Equal(expectedValue, underTest.Value);
        }

        [Fact]
        public void Value_ReferenceTypeValue_IsValue()
        {
            var expectedValue = "forty two";

            var underTest = new Maybe<string>(expectedValue);

            Assert.Equal(expectedValue, underTest.Value);
        }

        [Fact]
        public void Equals_ValueTypeSameValue_True()
        {
            var underTest = new Maybe<int>(42);
            var other = new Maybe<int>(underTest.Value);

            Assert.True(underTest.Equals(other));
        }

        [Fact]
        public void Equals_ReferenceTypeSameValue_True()
        {
            var underTest = new Maybe<string>("forty two");
            var other = new Maybe<string>(underTest.Value);

            Assert.True(underTest.Equals(other));
        }

        [Fact]
        public void Equals_ValueTypeDifferentValue_False()
        {
            var underTest = new Maybe<int>(42);
            var other = new Maybe<int>(underTest.Value + 1);

            Assert.False(underTest.Equals(other));
        }

        [Fact]
        public void Equals_ReferenceTypeDifferentValue_False()
        {
            var underTest = new Maybe<string>("forty two");
            var other = new Maybe<string>($"{underTest.Value} plus one");

            Assert.False(underTest.Equals(other));
        }

        [Fact]
        public void Equals_ValueTypeOneNone_False()
        {
            var underTest = new Maybe<int>(42);
            var other = new Maybe<int>();

            Assert.False(underTest.Equals(other));
        }

        [Fact]
        public void Equals_ReferenceTypeOneNone_False()
        {
            var underTest = new Maybe<string>("forty two");
            var other = new Maybe<string>();

            Assert.False(underTest.Equals(other));
        }

        [Fact]
        public void Equals_ValueTypeBothNone_True()
        {
            var underTest = new Maybe<int>();
            var other = new Maybe<int>();

            Assert.True(underTest.Equals(other));
        }

        [Fact]
        public void Equals_ReferenceTypeBothNone_True()
        {
            var underTest = new Maybe<string>();
            var other = new Maybe<string>();

            Assert.True(underTest.Equals(other));
        }

        [Fact]
        public void EqualsObject_SameObject_True()
        {
            var underTest = new Maybe<int>();
            var other = underTest as object;

            Assert.True(underTest.Equals(other));
        }

        [Fact]
        public void EqualsObject_DifferentObject_False()
        {
            var underTest = new Maybe<string>();
            var other = new object();

            Assert.False(underTest.Equals(other));
        }

        [Fact]
        public void EqualsOperator_ValueTypeSameValue_True()
        {
            var underTest = new Maybe<int>(42);
            var other = new Maybe<int>(underTest.Value);

            Assert.True(underTest == other);
        }

        [Fact]
        public void EqualsOperator_ReferenceTypeSameValue_True()
        {
            var underTest = new Maybe<string>("forty two");
            var other = new Maybe<string>(underTest.Value);

            Assert.True(underTest == other);
        }

        [Fact]
        public void EqualsOperator_ValueTypeDifferentValue_False()
        {
            var underTest = new Maybe<int>(42);
            var other = new Maybe<int>(underTest.Value + 1);

            Assert.False(underTest == other);
        }

        [Fact]
        public void EqualsOperator_ReferenceTypeDifferentValue_False()
        {
            var underTest = new Maybe<string>("forty two");
            var other = new Maybe<string>($"{underTest.Value} plus one");

            Assert.False(underTest == other);
        }

        [Fact]
        public void EqualsOperator_ValueTypeOneNone_False()
        {
            var underTest = new Maybe<int>(42);
            var other = new Maybe<int>();

            Assert.False(underTest == other);
        }

        [Fact]
        public void EqualsOperator_ReferenceTypeOneNone_False()
        {
            var underTest = new Maybe<string>("forty two");
            var other = new Maybe<string>();

            Assert.False(underTest == other);
        }

        [Fact]
        public void EqualsOperator_ValueTypeBothNone_True()
        {
            var underTest = new Maybe<int>();
            var other = new Maybe<int>();

            Assert.True(underTest == other);
        }

        [Fact]
        public void EqualsOperator_ReferenceTypeBothNone_True()
        {
            var underTest = new Maybe<string>();
            var other = new Maybe<string>();

            Assert.True(underTest == other);
        }

        [Fact]
        public void NotEqualsOperator_ValueTypeSameValue_False()
        {
            var underTest = new Maybe<int>(42);
            var other = new Maybe<int>(underTest.Value);

            Assert.False(underTest != other);
        }

        [Fact]
        public void NotEqualsOperator_ReferenceTypeSameValue_False()
        {
            var underTest = new Maybe<string>("forty two");
            var other = new Maybe<string>(underTest.Value);

            Assert.False(underTest != other);
        }

        [Fact]
        public void NotEqualsOperator_ValueTypeDifferentValue_True()
        {
            var underTest = new Maybe<int>(42);
            var other = new Maybe<int>(underTest.Value + 1);

            Assert.True(underTest != other);
        }

        [Fact]
        public void NotEqualsOperator_ReferenceTypeDifferentValue_True()
        {
            var underTest = new Maybe<string>("forty two");
            var other = new Maybe<string>($"{underTest.Value} plus one");

            Assert.True(underTest != other);
        }

        [Fact]
        public void NotEqualsOperator_ValueTypeOneNone_True()
        {
            var underTest = new Maybe<int>(42);
            var other = new Maybe<int>();

            Assert.True(underTest != other);
        }

        [Fact]
        public void NotEqualsOperator_ReferenceTypeOneNone_True()
        {
            var underTest = new Maybe<string>("forty two");
            var other = new Maybe<string>();

            Assert.True(underTest != other);
        }

        [Fact]
        public void NotEqualsOperator_ValueTypeBothNone_False()
        {
            var underTest = new Maybe<int>();
            var other = new Maybe<int>();

            Assert.False(underTest != other);
        }

        [Fact]
        public void NotEqualsOperator_ReferenceTypeBothNone_False()
        {
            var underTest = new Maybe<string>();
            var other = new Maybe<string>();

            Assert.False(underTest != other);
        }

        [Fact]
        public void GetHashCode_Always_Throws()
        {
            var underTest = new Maybe<int>(42);

            Assert.Throws<InvalidOperationException>(() => underTest.GetHashCode());
        }
    }
}
