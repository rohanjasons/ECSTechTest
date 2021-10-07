Feature: EcsFeature
	As A Test Architect Candidate
	I Want To Impress The Ecs Team
	So That I Can Get The Job
	
Scenario: Succcessfully Complete The Array Challenge
	Given I have started the technical challenge
	When I complete the array challenge
		And I submit the 'completed' challenge results 
	Then a 'success' banner is displayed

Scenario: Submit challenge without completing
	Given I have started the technical challenge
	When I submit the 'incomplete' challenge results 
	Then a 'failure' banner is displayed 