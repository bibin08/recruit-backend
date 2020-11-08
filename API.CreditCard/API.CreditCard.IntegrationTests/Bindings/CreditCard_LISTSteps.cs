using System;
using TechTalk.SpecFlow;

namespace API.CreditCard.IntegrationTests.Bindings
{
    //TODO: Fill in the steps for Given, When and Then
    [Binding]
    public class CreditCard_LISTSteps
    {
        [Given(@"I have a HTTP client")]
        public void GivenIHaveAHTTPClient()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"I am testing SelfUrl")]
        public void GivenIAmTestingSelfUrl()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"I am testing v(.*) of Cards")]
        public void GivenIAmTestingVOfCards(int p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I make a GET Request")]
        public void WhenIMakeAGETRequest()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the response status should be OK")]
        public void ThenTheResponseStatusShouldBeOK()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
