# Home Claims Automation

NOTE: This entire exercise was developed with severe technical handicaps:
1. Linux Mint 22.3 on a 9+ year old hardware
2. Visual Studio Code slows down to a crawl.  Hence no intellisense or Github Copilot help for most part of the development.  Had to do with 'xed' text editory
3. Windows laptop available only in the last stages

Yet, managed to create a decent README.md with all the important explanations, with the help of Copilot (on VS Code windows).  

Still, the Test Cases couldn't be completed as I had challenges in fixing package dependencies - .Net 10 seems to have an issue with Xunit.

Earnestly hoping that *_sympathetic consideration_* would be given, for the time taken.  Last but not the least, had to spend some time in setting up the .Net dev infra on my old Linux Mint PC.

## Overview
This project is a .NET 10 web service for automating home insurance claims. It is designed for extensibility, testability, and integration with external policy administration systems. The service exposes REST endpoints for claim settlement, status, and health checks.

## Architecture

- **Entry Point:**
	- [Program.cs](Program.cs): Configures the web host, loads settings from `appsettings.json`, and initializes endpoints.
- **Endpoints:**
	- [Endpoints/Init.cs](Endpoints/Init.cs): Defines HTTP endpoints:
		- `/`: Root health check (plain text)
		- `/status`: Service status and metadata (JSON)
		- `/claim-settlement`: Main business logic (POST, accepts claim request, returns settlement decision)
- **Core Services:**
	- [Core/ClaimSettlementService.cs](Core/ClaimSettlementService.cs): Implements claim evaluation logic, interacts with policy admin system via `IPolicyClient`.
- **Interfaces:**
	- [Interfaces/IClaimSettlementService.cs](Interfaces/IClaimSettlementService.cs): Abstraction for claim settlement logic.
	- [Interfaces/IPolicyClient.cs](Interfaces/IPolicyClient.cs): Abstraction for external policy admin system integration.
	- [Interfaces/IClaimRepository.cs](Interfaces/IClaimRepository.cs): Abstraction for persisting settlement decisions.
- **Models:**
	- [Core/Model.cs](Core/Model.cs): Defines domain models (e.g., `ClaimRequest`, `SettlementDecision`, `PolicyDetails`).
- **Configuration:**
	- [appsettings.Development.json](appsettings.Development.json): Configures service port and external system URL.

## Implementation Details

- **Dependency Injection:**
	- Services are injected via constructors for testability and flexibility.
- **Async Operations:**
	- Claim evaluation and policy lookup are performed asynchronously.
- **Cancellation Tokens:**
	- Long-running operations support cancellation for graceful shutdown.
- **Error Handling:**
	- If the policy admin system is inaccessible, the service returns a structured error in the settlement decision.

## Endpoints

### `POST /claim-settlement`
- Accepts a JSON `ClaimRequest`.
- Orchestrates policy lookup and claim evaluation.
- Returns a `SettlementDecision` (approved/denied, reason).

### `GET /status`
- Returns service metadata, version, OS, framework, and online status.

### `GET /`
- Returns a simple health check message.

## Configuration

Edit `appsettings.Development.json` to set:
- `ServiceConfig:Port`: Service port (default: 9000)
- `ServiceConfig:PolicAdminSystem`: External policy admin system URL

## Running the Service

Build and run using the .NET CLI:

```bash
dotnet build
dotnet run
```

## Extending the Service

- Implement custom `IPolicyClient` for real policy admin integration.
- Add new endpoints in `Endpoints/Init.cs`.
- Extend domain models in `Core/Model.cs`.

## License

See [LICENSE](LICENSE).
