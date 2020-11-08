Feature: CreditCard_LIST
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@CreditCard_v1
Scenario: GetAll Credit Card
	Given I have a HTTP client
		And I am testing SelfUrl
		And I am testing v1 of Cards
	When I make a GET Request
	Then the response status should be OK
	