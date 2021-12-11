# FeedR
**Sample** (and rather **simple**) .NET6 **microservices** solution which acts as the data aggregator for the different feeds.
The **purpose** of this solution is to **explore** the latest framework and **play** with different patterns, tools & libraries that can be useful when building **distributed applications** (but not only).
The overall repository structure consists of the following projects located under `src` directory:

- Gateway - API gateway providing a public facade for the underlying, internal services
- Aggregator - core service responsible for aggregating the data from different feeds
- Notifier - supporting service responsible for sending the notifications
- Feeds
  - News - sample feed providing the worldwide news
  - Quotes - sample feed providing the quotes (e.g. currency)
  - Weather - sample feed providing the weather related data

# Episodes

The **implementation process** can be found on our [DevMentors YouTube](https://www.youtube.com/devmentors) channel, where we publish the **videos** about building this project.
You can navigate through this repository via specific `episode` tags which are related to the videos.

# Requirements

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Docker](https://docs.docker.com/get-docker)

To start the infrastructure via Docker, type the following command at the `compose` directory:

`docker compose -f infrastructure.yml up -d`

Each application can be started separately using dotnet CLI or your favorite IDE.
