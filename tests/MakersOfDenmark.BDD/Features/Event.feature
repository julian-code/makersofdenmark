Feature: Event
	As an User
	I want to register to a MakerSpace Event
	So I can attend that event

@mytag
Scenario: User registers to an Event at a MakerSpace
  Given a MakerSpace has an active Event
  When a user registers to that Event
  Then the User is registered to the event