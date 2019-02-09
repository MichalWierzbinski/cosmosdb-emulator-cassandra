# Developing a .net app with Cassandra API using Azure Cosmos DB Emulator
Azure Cosmos DB is Microsoft's globally distributed multi-model database service. You can quickly create and query document, table, key-value, and graph databases, all of which benefit from the global distribution and horizontal scale capabilities at the core of Azure Cosmos DB. 

Cosmos Emulator now supports Cassandra API (src: http://blog.mashfords.com/2018/12/19/azure-cosmos-db-emulator-support-for-cassandra-api/), however no one bothered to mention how to run it and how to connect to it. The Cosmos DB Emulator documentation page also seems to be out of date, so I decided to figure it out on my own.

This quick start demonstrates how to connect to Cosmos DB Emulator's Cassandra Endpoint. The code is based on https://github.com/Azure-Samples/azure-cosmos-db-cassandra-dotnet-getting-started. You'll build a user profile console app, output as shown in the following image, with sample data.

## Running this sample

1. Clone this repository/

2. Open the CassandraQuickStartSample.sln solution and install the Cassandra .NET driver. Use the .NET Driver's NuGet package. From the Package Manager Console window in Visual Studio:

```bash
PM> Install-Package CassandraCSharpDriver
```
3. Go to your Cosmos DB Emulator install location and open PowerShell window in that location. Default install path is `C:\Program Files\Azure Cosmos DB Emulator`
4. Cassandra Endpoint is not enabled by default. To enable it run Cosmos DB Emulator from CMD/PowerShell using following command: 
```powershell
.\CosmosDB.Emulator.exe /EnableCassandraEndpoint
```
 5. Cassandra Endpoint will be opened on port `10350` by default. If you want to change that port run Emulator wiht follwing command:
 
 ```powershell
.\CosmosDB.Emulator.exe /EnableCassandraEndpoint /CassandraPort=<port_number>
```
6. Endpoints in the sample are preconfigured to run with the Emulator. You don't have to change anything.
7. Compile and Run the project.

8. Output Image: 

![User Data](/img.PNG?raw=true "user data")

## About the code
The code included in this sample is intended to get you quickly started with a C# application using Cassandra C# driver that connects to Azure Cosmos DB with the Cassandra API.

## More information

- [Azure Cosmos DB](https://docs.microsoft.com/azure/cosmos-db/introduction)
- [Cassandra DB](http://cassandra.apache.org/)
