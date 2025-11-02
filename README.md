Detailed Workflow: Azure Service Bus + Azure Function + SendGrid

Overview

This solution demonstrates a decoupled, event-driven architecture that allows asynchronous communication between microservices using Azure Service Bus and Azure Functions.
It ensures reliability, scalability, and loose coupling between the components.

Components

ServiceBusTestApi (Producer)

A .NET 8 Web API project that publishes order messages.

Responsible for sending structured messages to the Azure Service Bus queue.

Azure Service Bus (Message Broker)

A fully managed enterprise message broker by Microsoft Azure.

Acts as a message queue that stores events until they are processed by the consumer (Azure Function).

Provides decoupling, reliability, and durable delivery.

OrderEmailHandler (Consumer)

A .NET 8 Azure Function App (Isolated Process).

Triggers automatically when a new message arrives in the Service Bus queue.

Processes the incoming message and sends an email via SendGrid.

SendGrid (Email Delivery Service)

A trusted third-party email service used to send transactional emails (e.g., order confirmations).

Integrated inside the Azure Function to notify customers.

Workflow Steps
Step 1: API Publishes the Message

The ServiceBusTestApi exposes an HTTP endpoint:
POST /api/order/publish

When this endpoint is called, the API:

Receives an order payload (CreateOrderDto) from the client.

Serializes the order data into JSON.

Publishes the message to the Azure Service Bus queue using the configured connection string and queue name.

Responds with a success message and the generated OrderId.

Example Message Sent to Queue:

{
  "OrderId": "1b7c2a55-d12a-4fbb-b739-f1b2d11a8d5a",
  "CustomerEmail": "masud@wearistyle.com",
  "TotalAmount": 159.99,
  "OrderDate": "2025-11-02T12:10:05Z"
}

Step 2: Azure Service Bus Stores the Message

The Azure Service Bus queue acts as a buffer between the producer (API) and the consumer (Azure Function).

It guarantees message durability — even if the consumer is temporarily offline.

Each message is stored securely and delivered exactly once when the Function is available.

Step 3: Azure Function Triggered Automatically

The OrderEmailHandler Azure Function is configured with a Service Bus Trigger, using:

[Function(nameof(OrderEmailHandler))]

public async Task Run(
    [ServiceBusTrigger("orderqueue", Connection = "ServiceBusConnection")] string message)


When a new message arrives in the Service Bus queue:

The Azure Function runtime automatically invokes the handler method.

The message payload is passed into the function as a string.

The function deserializes the JSON into a CreateOrderDto object.

Step 4: Send Email via SendGrid

Once the message is processed, the Function constructs an email message using SendGrid:

var msg = new SendGridMessage
{
    From = new EmailAddress(_settings.FromEmail, _settings.FromName),
    Subject = "Your Order Confirmation",
    HtmlContent = $"<p>Dear Customer,</p><p>Your order {order.OrderId} of amount {order.TotalAmount:C} has been received.</p>"
};
msg.AddTo(order.CustomerEmail);
await client.SendEmailAsync(msg);


The SendGrid API Key is securely stored in Application Settings or local.settings.json:

"EmailSettings:ApiKey": "<your_sendgrid_api_key>"


SendGrid delivers the email to the customer’s inbox.

Step 5: Logging and Monitoring

Both the ServiceBusTestApi and OrderEmailHandler log events for observability:

The API logs:
"Order {OrderId} sent to Service Bus."

The Function logs:
"Processing message for OrderId: {OrderId}"

In Azure, you can monitor:

Function execution in Application Insights.

Queue message count in Service Bus → Metrics.

Email success/failure via SendGrid dashboard.
