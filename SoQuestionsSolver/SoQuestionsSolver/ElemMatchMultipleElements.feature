Feature: ElemMatchMultipleElements
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario: Add two numbers
	Given collection "marco"
	When try to parse query
	Then resut need be not null
