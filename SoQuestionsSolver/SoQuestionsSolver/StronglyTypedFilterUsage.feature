Feature: StronglyTypedFilterUsage
http://stackoverflow.com/questions/38361093/c-sharp-mongodb-driver-strongly-typed-filter-usage

	 
@mytag
Scenario: use strongly typed filter
	Given Sample data from lapsus
	When Asking for x = 10 and y < 20 
	Then result should be x= 10, y  = 15



Scenario: Use as queryable method
	Given Sample data from lapsus
	When Asking as queryable for x = 10 and y < 20 
	Then result should be x= 10, y  = 15
