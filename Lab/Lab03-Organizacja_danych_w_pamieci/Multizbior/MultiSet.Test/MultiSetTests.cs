using FluentAssertions;

using Multizbior;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiSet.Test
{
    public class MultiSetValueTests
    {
        public MultiSet<char> Init()
        {
            var data = new char[] { 'a', 'd', 'a', 'd' };
            return new MultiSet<char>(data);
        }

        [Test]
        public void BasicConstructor_WhenCalled_CreateClass()
        {
            var ms = new MultiSet<char>();

            ms.Should().NotBeNull();
            ms.IsEmpty.Should().BeTrue();
        }

        [Test]
        public void ConstructorWithData_WhenCalled_CreateClassWithDictionary()
        {
            var ms = Init();

            ms.Should().NotBeNull();
            ms.IsEmpty.Should().BeFalse();
        }

        [Test]
        public void IsReadOnly_WhenCreatedFromEmptyConstructor_ShouldReturnFalse()
        {
            var ms = new MultiSet<char>();

            ms.IsReadOnly.Should().BeFalse();
        }

        [Test]
        public void IsReadOnly_WhenCreateFromConstructorWithData_ShouldReturnFalse()
        {
            var ms = Init();

            ms.IsReadOnly.Should().BeFalse();
        }

        [Test]
        public void SealedMultiSet_WhenCalled_MultiSetShouldBeReadOnly()
        {
            var ms = Init();
            ms.SealedMultiSet();

            ms.IsReadOnly.Should().BeTrue();
        }

        [Test]
        public void OperatorMinus_WhenCalled_ShouldReturnNewMultiSetAfterMinus()
        {
            var data = new char[] { 'a', 'b', 'c', 'd', 'a', 'd' };
            var ms = new MultiSet<char>(data);
            var data2 = new char[] { 'b', 'c' };
            var ms2 = new MultiSet<char>(data2);

            var ms3 = ms - ms2;
            var result = ms3.ToString();

            result.Should().Be("a: 2, d: 2");
        }

        [Test]
        public void OperatorMinus_WhenFirstArgumentIsNull_ShouldReturnArgumentNullExpception()
        {
            MultiSet<char> ms1 = null;
            MultiSet<char> ms2 = new MultiSet<char>();


            Action action = () =>
            {
                var result = ms1 - ms2;
            };

            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Test]
        public void OperatorMinus_WhenSecondArgumentIsNull_ShouldReturnArgumentNullExpception()
        {
            MultiSet<char> ms1 = new MultiSet<char>();
            MultiSet<char> ms2 = null;


            Action action = () =>
            {
                var result = ms1 - ms2;
            };

            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Test]
        public void OperatorMinus_WhenArgumentsAreNull_ShouldReturnArgumentNullExpception()
        {
            MultiSet<char> ms1 = null;
            MultiSet<char> ms2 = null;


            Action action = () =>
            {
                var result = ms1 - ms2;
            };

            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Test]
        public void OperatorPlus_WhenCalled_ShouldReturnNewMultiSetAfterMinus()
        {
            var data = new char[] { 'a','d', 'a', 'd' };
            var ms = new MultiSet<char>(data);
            var data2 = new char[] { 'b', 'c' };
            var ms2 = new MultiSet<char>(data2);

            var ms3 = ms + ms2;
            var result = ms3.ToString();

            result.Should().Be("a: 2, d: 2, b: 1, c: 1");
        }

        [Test]
        public void OperatorPlus_WhenFirstArgumentIsNull_ShouldReturnArgumentNullExpception()
        {
            MultiSet<char> ms1 = null;
            MultiSet<char> ms2 = new MultiSet<char>();


            Action action = () =>
            {
                var result = ms1 + ms2;
            };

            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Test]
        public void OperatorPlus_WhenSecondArgumentIsNull_ShouldReturnArgumentNullExpception()
        {
            MultiSet<char> ms1 = new MultiSet<char>();
            MultiSet<char> ms2 = null;


            Action action = () =>
            {
                var result = ms1 + ms2;
            };

            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Test]
        public void OperatorPlus_WhenArgumentsAreNull_ShouldReturnArgumentNullExpception()
        {
            MultiSet<char> ms1 = null;
            MultiSet<char> ms2 = null;


            Action action = () =>
            {
                var result = ms1 + ms2;
            };

            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Test]
        public void OperatorMultiplicate_WhenCalled_ShouldReturnNewMultiSetAfterMinus()
        {
            var data = new char[] { 'a', 'd', 'a', 'd', 'b', 'c' };
            var ms = new MultiSet<char>(data);
            var data2 = new char[] { 'b', 'c' };
            var ms2 = new MultiSet<char>(data2);

            var ms3 = ms * ms2;
            var result = ms3.ToString();

            result.Should().Be("b: 2, c: 2");
        }

        [Test]
        public void OperatorMultiplicate_WhenFirstArgumentIsNull_ShouldReturnArgumentNullExpception()
        {
            MultiSet<char> ms1 = null;
            MultiSet<char> ms2 = new MultiSet<char>();


            Action action = () =>
            {
                var result = ms1 * ms2;
            };

            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Test]
        public void OperatorMultiplicate_WhenSecondArgumentIsNull_ShouldReturnArgumentNullExpception()
        {
            MultiSet<char> ms1 = new MultiSet<char>();
            MultiSet<char> ms2 = null;


            Action action = () =>
            {
                var result = ms1 * ms2;
            };

            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Test]
        public void OperatorMultiplicate_WhenArgumentsAreNull_ShouldReturnArgumentNullExpception()
        {
            MultiSet<char> ms1 = null;
            MultiSet<char> ms2 = null;


            Action action = () =>
            {
                var result = ms1 * ms2;
            };

            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Test]
        public void Count_WhenCalled_ShouldReturnCountOfAllElements()
        {
            var ms = Init();

            var result = ms.Count;
            result.Should().Be(4);
        }

        [Test]
        public void OperatorIndex_WhenCalled_ShoulReturnElement()
        {
            var ms = Init();

            var reuslt = ms['d'];

            reuslt.Should().Be(2);
        }

        [Test]
        public void OperatorIndex_WhenArgumentsAreNull_ShouldReturnArgumentNullExpception()
        {
            var ms = Init();

            Action action = () =>
            {
                var reuslt = ms['z'];
            };

            action.Should().ThrowExactly<KeyNotFoundException>();
        }

        [Test]
        public void Add_WhenCalled_ShouldAddNewItem()
        {
            var ms = Init();
            ms.Add('z');

            var result = ms.ToString();
            result.Should().Be("a: 2, d: 2, z: 1");
        }

        [Test]
        public void Add_WhenIsReadOnly_ShouldThrowNotSupportedException()
        {
            var ms = Init();
            ms.SealedMultiSet();
            Action action = () => ms.Add('a');

            action.Should().ThrowExactly<NotSupportedException>();
        }

        [Test]
        public void Remove_WhenCalled_ShouldRemoveItem()
        {
            var ms = Init();
            ms.Remove('a');

            var result = ms.ToString();
            result.Should().Be("a: 1, d: 2");
        }

        [Test]
        public void Remove_WhenIsReadOnly_ShouldThrowNotSupportedException()
        {
            var ms = Init();
            ms.SealedMultiSet();
            Action action = () => ms.Remove('a');

            action.Should().ThrowExactly<NotSupportedException>();
        }

        [Test]
        public void Clear_WhenCalled_ShouldClearDictionary()
        {
            var ms = Init();
            ms.Clear();

            var result = ms.Count;
            result.Should().Be(0);
        }

        [Test]
        public void Clear_WhenIsReadOnly_ShouldThrowNotSupportedException()
        {
            var ms = Init();
            ms.SealedMultiSet();
            Action action = () => ms.Clear(); ;

            action.Should().ThrowExactly<NotSupportedException>();
        }

        [TestCase()]
        public void Contains_WhenCalled_CheckIsItemInDictionary()
        {
            var ms = Init();
            ms.Clear();

            var result = ms.Count;
            result.Should().Be(0);
        }

        [Test]
        public void CopyTo_WhneArgumentIsNull_ShouldThrowArgumentNullException()
        {
            var ms = Init();
            char[] array = null;
            Action action = () => ms.CopyTo(array, 1); ;

            action.Should().ThrowExactly<ArgumentNullException>("array");
        }

        [Test]
        public void CopyTo_WhenCalled_ShouldCopyMultiSetToArray()
        {
            var ms = Init();

            var array = new char[3];
            ms.CopyTo(array, 1);

            var targetArray = new char[3] { 'd', 'a', 'd' };
            array.Should().BeEquivalentTo(targetArray);
        }

        [Test]
        public void ToString_WhenCalled_ShouldWriteDataOnConsole()
        {
            var ms = Init();
            var result = ms.ToString();

            result.Should().Be("a: 2, d: 2");
        }

        [Test]
        public void ToSringExpanded_WhenCalled_ShouldWriteDataOnConsole()
        {
            var ms = Init();
            var result = ms.ToStringExpanded();

            result.Should().Be("a, a, d, d");
        }

        [Test]
        public void AddItems_WhenCalled_AddManyItemsToMultiSet()
        {
            var ms = Init();
            ms.Add('z', 10);
            var result = ms.Contains('z');

            result.Should().BeTrue();
        }

        [Test]
        public void AddItems_WhenIsReadOnly_ShouldThrowNotSupportedException()
        {
            var ms = Init();
            ms.SealedMultiSet();

            Action action = () => ms.Add('z', 10);

            action.Should().ThrowExactly<NotSupportedException>();
        }

        [Test]
        public void RemoveItems_WhenCalled_AddManyItemsToMultiSet()
        {
            var ms = Init();
            ms.Remove('a', 2);
            var result = ms.Contains('a');

            result.Should().BeFalse();
        }

        [Test]
        public void RemoveItems_WhenIsReadOnly_ShouldThrowNotSupportedException()
        {
            var ms = Init();
            ms.SealedMultiSet();

            Action action = () => ms.Remove('a', 2);

            action.Should().ThrowExactly<NotSupportedException>();
        }

        [Test]
        public void RemoveAllItems_WhenCalled_AddManyItemsToMultiSet()
        {
            var ms = Init();
            ms.RemoveAll('a');
            var result = ms.Contains('a');

            result.Should().BeFalse();
        }

        [Test]
        public void RemoveAllItems_WhenIsReadOnly_ShouldThrowNotSupportedException()
        {
            var ms = Init();
            ms.SealedMultiSet();

            Action action = () => ms.RemoveAll('a');

            action.Should().ThrowExactly<NotSupportedException>();
        }

        [Test]
        public void UnionWith_WhenCalled_ShouldAddItemsToMultiSet()
        {
            var ms = Init();
            var data = new char[] { 'b', 'c' };

            ms.UnionWith(data);
            var result = ms.ToString();

            result.Should().Be("a: 2, d: 2, b: 1, c: 1");
        }

        [Test]
        public void UnionWith_WhenDataIsNull_ShouldThrowArgumentNullException()
        {
            var ms = Init();
            char[] data = null;

            Action action = () => ms.UnionWith(data);

            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Test]
        public void UnionWith_WhenIsReadOnly_ShouldThrowNotSupportedException()
        {
            var ms = Init();
            ms.SealedMultiSet();
            var data = new char[] { 'b', 'c' };

            Action action = () => ms.UnionWith(data);

            action.Should().ThrowExactly<NotSupportedException>();
        }

        [Test]
        public void IntersectWith_WhenCalled_KeepOnlyCommonElements()
        {
            var ms = Init();
            var data = new char[] { 'a', 'c' };

            ms.IntersectWith(data);
            var result = ms.ToString();

            result.Should().Be("a: 3");
        }

        [Test]
        public void IntersectWith_WhenDataIsNull_ShouldThrowArgumentNullException()
        {
            var ms = Init();
            char[] data = null;

            Action action = () => ms.IntersectWith(data);

            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Test]
        public void IntersectWith_WhenIsReadOnly_ShouldThrowNotSupportedException()
        {
            var ms = Init();
            ms.SealedMultiSet();
            var data = new char[] { 'b', 'c' };

            Action action = () => ms.IntersectWith(data);

            action.Should().ThrowExactly<NotSupportedException>();
        }

        [Test]
        public void ExceptWith_WhenCalled_KeepOnlyMuliSetElements()
        {
            var ms = Init();
            var data = new char[] { 'a', 'c' };

            ms.ExceptWith(data);
            var result = ms.ToString();

            result.Should().Be("a: 1, d: 2");
        }

        [Test]
        public void ExceptWith_WhenDataIsNull_ShouldThrowArgumentNullException()
        {
            var ms = Init();
            char[] data = null;

            Action action = () => ms.ExceptWith(data);

            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Test]
        public void ExceptWithh_WhenIsReadOnly_ShouldThrowNotSupportedException()
        {
            var ms = Init();
            ms.SealedMultiSet();
            var data = new char[] { 'b', 'c' };

            Action action = () => ms.ExceptWith(data);

            action.Should().ThrowExactly<NotSupportedException>();
        }

        [Test]
        public void SymmetricExceptWith_WhenCalled_KeepAllWithoutCommonElements()
        {
            var ms = Init();
            var data = new char[] { 'a', 'c', 'd', 'a' };

            ms.SymmetricExceptWith(data);
            var result = ms.ToString();

            result.Should().Be("d: 1, c: 1");
        }

        [Test]
        public void SymmetricExceptWith_WhenDataIsNull_ShouldThrowArgumentNullException()
        {
            var ms = Init();
            char[] data = null;

            Action action = () => ms.SymmetricExceptWith(data);

            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Test]
        public void SymmetricExceptWith_WhenIsReadOnly_ShouldThrowNotSupportedException()
        {
            var ms = Init();
            ms.SealedMultiSet();
            var data = new char[] { 'b', 'c' };

            Action action = () => ms.SymmetricExceptWith(data);

            action.Should().ThrowExactly<NotSupportedException>();
        }
    }

    public class MultiSetReferenceTests
    {
        [Test]
        public void BasicConstructor_WhenCalled_CreateClass()
        {
            var ms = new MultiSet<StringBuilder>();

            ms.Should().NotBeNull();
            ms.IsEmpty.Should().BeTrue();
        }

        [Test]
        public void ConstructorWithData_WhenCalled_CreateClassWithDictionary()
        {
            var data = new StringBuilder[] { new StringBuilder().Append("Test"), new StringBuilder().Append("Test2") };
            var ms = new MultiSet<StringBuilder>(data);

            ms.Should().NotBeNull();
            ms.IsEmpty.Should().BeFalse();
        }

        [Test]
        public void IsReadOnly_WhenCreatedFromEmptyConstructor_ShouldReturnFalse()
        {
            var ms = new MultiSet<StringBuilder>();

            ms.IsReadOnly.Should().BeFalse();
        }

        [Test]
        public void IsReadOnly_WhenCreateFromConstructorWithData_ShouldReturnFalse()
        {
            var data = new StringBuilder[] { new StringBuilder().Append("Test"), new StringBuilder().Append("Test2") };
            var ms = new MultiSet<StringBuilder>(data);

            ms.IsReadOnly.Should().BeFalse();
        }
    }
}