Feature: CompareOnSameDocumentFields
http://stackoverflow.com/questions/38258074/linq-to-mongodb-return-list-only-when-values-between-2-columns-match/38295682?noredirect=1#comment64008903_38295682

jira ticket
https://jira.mongodb.org/browse/CSHARP-1700


@mytag
Scenario: comparison
Given ComparisionSampleData
    Then Will get only result with same fields value
#    Then Try to use where clause


 
