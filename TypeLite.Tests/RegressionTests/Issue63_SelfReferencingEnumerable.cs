using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TypeLite.Tests.RegressionTests {
    public class Issue63_SelfReferencingEnumerable {
        /// <summary>
        /// When a self-referencing enumerable is present but ignored, it shouldn't break the build.
        /// </summary>
        [Fact]
        public void WhenBuild_NoSelfReferencingEnumerableInfiniteLoop() {
            var target = new TsModelBuilder();
            target.Add(typeof(IgnoredSelfReferencingEnumerableWrapper));

            // May cause infinite loop or stack overflow, if not handled correctly.
            target.Build();
        }
    }

    public class IgnoredSelfReferencingEnumerableWrapper {
        [TsIgnore]
        public SelfReferencingEnumerable MyProperty { get; set; }
    }

    public class SelfReferencingEnumerable : IEnumerable<SelfReferencingEnumerable> {
        public IEnumerator<SelfReferencingEnumerable> GetEnumerator() {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            throw new NotImplementedException();
        }
    }
}
