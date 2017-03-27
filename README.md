# Blockchain API library (C#, .NET Core, v1)

An official C# (.NET Core) library for interacting with the Blockchain.info API.

## Latest changes

This library is a migration of the original .NET library to .NET Core.

## Getting started

The library requires .NET Core 1.1. It is possible to use this library with the .NET Framework by editing
`Info.Blockchain.Api.csproj`

and changing

`<TargetFramework>netcoreapp1.1</TargetFramework>`

to your project's .NET version, e.g.

`<TargetFramework>net46</TargetFramework>`

The recommended way to install and use the library is via NuGet:
```
PM> Install-Package BlockchainAPI
```

The library consists of the following namespaces:

* `Info.Blockchain.API.Blockexplorer` ([docs](docs/blockexplorer.md)) ([api/blockchain_api][api1])
* `Info.Blockchain.API.CreateWallet` ([docs](docs/createwallet.md)) ([api/blockchain/create_wallet][api2])
* `Info.Blockchain.API.Exchangerates` ([docs](docs/exchangerates.md)) ([api/exchange\_rates\_api][api3])
* `Info.Blockchain.API.PushTx` ([docs](docs/pushtx.md)) ([pushtx][api6])
* `Info.Blockchain.API.Statistics` ([docs](docs/statistics.md)) ([api/charts_api][api4])
* `Info.Blockchain.API.Wallet` ([docs](docs/wallet.md)) ([api/blockchain\_wallet\_api][api5])

In order to use Wallet and CreateWallet functionality, you must provide an URL to an instance of [service-my-wallet-v3](https://github.com/blockchain/service-my-wallet-v3) as first parameter to BlockchainApiHelper.
If you don't intend to use these functionalities, this parameter can be null.

## Error handling

All methods may throw exceptions caused by incorrectly passed parameters or other problems. If a call is rejected server-side, the `APIException` exception will be thrown. Other exceptions may also be thrown by the environment (e.g. no internet connection etc).

## Connection timeouts

It is possible to set arbitrary connection timeouts.

```csharp
Info.Blockchain.API.HttpClient.TimeoutMs = 5000; // time out after 5 seconds
```

## Request limits and API keys

In order to prevent abuse some API methods require an API key approved with some basic contact information and a description of its intended use. Please request an API key [here](https://blockchain.info/api/api_create_code).

The same API key can be used to bypass the request limiter.

[api1]: https://blockchain.info/api/blockchain_api
[api2]: https://blockchain.info/api/create_wallet
[api3]: https://blockchain.info/api/exchange_rates_api
[api4]: https://blockchain.info/api/charts_api
[api5]: https://blockchain.info/api/blockchain_wallet_api
[api6]: https://blockchain.info/pushtx
