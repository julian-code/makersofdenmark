using AutoFixture;
using FluentAssertions;
using MakersOfDenmark.Domain.Models;
using TechTalk.SpecFlow;

namespace MakersOfDenmark.BDD.Steps
{
    [Binding]
    public sealed class EventStepDefinitions
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _scenarioContext;
        private MakerSpace _makerSpace;
        private MakerSpaceEvent _event;
        private MODUser _user;

        public EventStepDefinitions(ScenarioContext scenarioContext)
        {
            var fixture = new Fixture();
            _makerSpace = fixture.Build<MakerSpace>().Without(x => x.Organization).Without(x => x.Tools).Create();
            _event = fixture.Build<MakerSpaceEvent>().Without(x => x.Registrants).Create();
            _user = fixture.Create<MODUser>();
            _scenarioContext = scenarioContext;
        }
        //Given a MakerSpace has an active Event  
        [Given("a MakerSpace has an active Event")]
        public void GivenAMakerSpaceHasAnActiveEvent()
        {
            _makerSpace.ActiveEvents.Add(_event);
        }
        // When a user registers to that Event
        [When("a user registers to that Event")]
        public void WhenAUserRegistersToThatEvent()
        {
            _event.Registrants.Add(_user);
        }
        [Then("the User is registered to the event")]
        public void ThenTheUserIsRegisteredToThatEvent()
        {
            _event.Registrants.Should().Contain(_user);
        }
    }
}
