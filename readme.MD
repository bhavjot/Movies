CBA.Movies project has projects:
1. DAL project - which is accessing the 3rd party data source
2. Repository Project- which is fetching data from 3rd party and storing in cache. 
It provides all services to Web layer for fetching, filtering, sorting data.
3. Web Project- It is Web Api project which provides all services of getting, updating or creating data.

The project uses common framework for IOC and logging purposes.

Assumptions for data:

1. Adding or updating movie invalidates the cache. 

Future enhancements:
1. Add more unit tests for testing each method.
2. Persisting the changes in cache
3. Unit Testing of Web layer