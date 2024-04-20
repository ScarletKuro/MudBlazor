// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using Bunit;
using FluentAssertions;
using NUnit.Framework;
using static Bunit.ComponentParameterFactory;
using static MudBlazor.MudCollapse;

namespace MudBlazor.UnitTests.Components
{
    [TestFixture]
    public class CollapseTests : BunitTest
    {
        [Test]
        public async Task Collapse_Test1()
        {
            //TO DO for %100 coverage we need js test
            var comp = Context.RenderComponent<MudCollapse>(Parameter(nameof(MudCollapse.MaxHeight), 1600));

            _ = comp.Instance._state = CollapseState.Exiting;
            await comp.InvokeAsync(() => comp.Instance.AnimationEndAsync());
            comp.WaitForAssertion(() => comp.Instance._height.Should().Be(0));

            //MaxHeight accepts minus value?
            _ = comp.Instance._state = CollapseState.Entering;

            var maxHeightParameter = Parameter(nameof(MudCollapse.MaxHeight), -1);
            comp.SetParametersAndRender(maxHeightParameter);
            await comp.InvokeAsync(() => comp.Instance.AnimationEndAsync());
            await comp.InvokeAsync(() => comp.Instance.UpdateHeightAsync());
            comp.WaitForAssertion(() => comp.Instance._height.Should().Be(-1));
        }
    }
}
