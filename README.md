# WebLearning
## Messaging testing
1. Run RabbitMQ locally with Docker:
```
docker run -d --hostname my-rabbit --name some-rabbit -e RABBITMQ_DEFAULT_USER=user -e RABBITMQ_DEFAULT_PASS=password rabbitmq:3-management
```
```
docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management
```
2. Paste Azure Service Bus connection string into appsettings.json files in TaskMicroservice and UserMicroservice projects:
```
"AzureServiceBus": {
    "ConnectionString": "your_connection_string",
    "QueueName": "testqueue"
  }
```
3. Run all projects in console mode (in order to see logs) and send POST request to http://localhost:62964/api/tasks with body:
```
{
    "id": 1,
    "title": "test task",
    "assignedByUserId": 15,
    "assignedToUserId": 10
}
```
