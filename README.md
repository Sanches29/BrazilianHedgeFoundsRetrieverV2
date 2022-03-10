# BrazilianHedgeFoundsRetriever

Hello!
This project is a Job Application Test provided by Daemon Investimentos and resolved/developed by me.

To run this project, just clone the repository to your machine and run it the way you feel confortable to (IDE, CMD). To make calls to application, i recommend using Swagger, but if you want, you can use Postman too.

You Should first call '/api/v1/Data/load' endpoint to populate the DataBase before querying on it. This method brings data from 63 CSV files acessable trough a government site, and tends to be a heavy process with estimated 10 minutes of waiting time (at least in Debug profile, maybe in Release profile the performance can be a little enhanced).

The chosen DataBase was MongoDB and it was configured to use LocalHost to create collection. I tried to create a Cluster from Atlas, but the free subscription had a limit of 512mb Data Storage, but, all the 21.7Million documents need at least 1.1Gb.

After populating the DB, you can call '/api/v1/Data' and use the query filter to recover data from DB.

If you try to call '/api/v1/Data' endpoint before calling '/api/v1/Data/load', as the DataBase is not populated, you will get an empty response.

Unfortunately, i couldnt make the project tests work as intended, but i am working on it.
