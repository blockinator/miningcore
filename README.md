[![Build status](https://ci.appveyor.com/api/projects/status/sm8t6xesfctr879i?svg=true)](https://ci.appveyor.com/project/minernl/miningcore)
[![license](https://img.shields.io/github/license/mashape/apistatus.svg)]()

MinerNL - Miningcore Stratum Pool
=================================

![Miningcore running ubuntu](http://i.imgur.com/sYF5s2c.jpg)

### Changes in this Miningcore fork
- Added nice Web frontend [https://miningcore.com](https://miningcore.com)
- Pool time set to UTC time zone
  Local time conversion should be used in the web frontend as that can be anyone on the globe
- Faster mining statistics calculation
- Added stats setting in config.json
```config
	"statistics": {
		// Stats broadcast (seconds)
		"statsUpdateInterval": 60,
		// Stats calculation window (minutes)
		"hashrateCalculationWindow": 5,
		// Stats DB cleanup interval (hours)
		"statsCleanupInterval": 48,
		// Stats history to cleanup is DB. older then x (days)
		"statsDBCleanupHistory": 365
	},
```

### Features

- Supports clusters of pools each running individual currencies
- Ultra-low-latency, multi-threaded Stratum implementation using asynchronous I/O
- Adaptive share difficulty ("vardiff")
- PoW validation (hashing) using native code for maximum performance
- Session management for purging DDoS/flood initiated zombie workers
- Payment processing
- Banning System
- Live Stats [API](https://github.com/coinfoundry/miningcore/wiki/API) on Port 4000
- WebSocket streaming of notable events like Blocks found, Blocks unlocked, Payments and more
- POW (proof-of-work) & POS (proof-of-stake) support
- Detailed per-pool logging to console & filesystem
- Runs on Linux and Windows
- [Gitter Channel](https://gitter.im/miningcore/Lobby)

### Supported Coins

Refer to [this file](https://github.com/minernl/miningcore/blob/master/src/Miningcore/coins.json) for a complete list.

#### Ethereum

Miningcore implements the [Ethereum stratum mining protocol](https://github.com/nicehash/Specifications/blob/master/EthereumStratum_NiceHash_v1.0.0.txt) authored by NiceHash. This protocol is implemented by all major Ethereum miners.

- Claymore Miner must be configured to communicate using this protocol by supplying the <code>-esm 3</code> command line option
- Genoil's ethminer must be configured to communicate using this protocol by supplying the <code>-SP 2</code> command line option

#### ZCash

- Pools needs to be configured with both a t-addr and z-addr (new configuration property "z-address" of the pool configuration element)
- First configured zcashd daemon needs to control both the t-addr and the z-addr (have the private key)
- To increase the share processing throughput it is advisable to increase the maximum number of concurrent equihash solvers through the new configuration property "equihashMaxThreads" of the cluster configuration element. Increasing this value by one increases the peak memory consumption of the pool cluster by 1 GB.
- Miners may use both t-addresses and z-addresses when connecting to the pool

### Donations

This software comes with a built-in donation of 0.5% per block-reward to support the ongoing development of this project.
You can also send donations directly to the developers using the following accounts:

* BTC:  `1Jtnju5EuWFs5QZNmp8g5JYhQHqRjwzw78`
* LTC:  `LKF12Fi92zuxDhpHLe7gSWBtTdJbcULa85`
* DASH: `XmGYLq6YFpMc6EMpsNHDsuMFGd37RMTaqA`
* ETH:  `0x745F2Bc9570B8C8DcD51249d7fdC2528f03efF1c`
* ETC:  `0xBCB57A44dCD7b4B4834EF509eCE271BF27eB0ccB`
* XMR: `44c7umSm7TyXxKch9q4R5QfoTAf663A8yEFfJbxmxUJ1JCWq2kFu33oAAydrgNDQA8619rSQhZaFV3ScpESWCfcQB3Fqc6w`
* ZEC:  `t1eM9SymZUnJi1fw8PY3LU9SPmetBp4y4WS`

### Running Miningcore on Windows

- [.Net Core 3.1 Runtime](https://www.microsoft.com/net/download/core)
- [PostgreSQL Database v12 or higher](https://www.postgresql.org/)
- Coin Daemon (per pool)

### Running pre-built Release Binaries on Windows

- Download miningcore-win-x64.zip from the latest [Release](https://github.com/minernl/miningcore/releases)
- Extract the Archive
- Setup the database as outlined below
- Create a configuration file <code>config.json</code> as described [here](https://github.com/minernl/miningcore/wiki/Configuration)
- Run <code>dotnet Miningcore.dll -c config.json</code>


### Running Miningcore on Linux

- Install [.Net Core 3.1 SDK](https://www.microsoft.com/net/download/core)

  Example Ubuntu 20.04:
````console
wget https://packages.microsoft.com/config/ubuntu/20.10/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo apt-get update; \
  sudo apt-get install -y apt-transport-https && \
  sudo apt-get update && \
  sudo apt-get install -y dotnet-sdk-3.1
````
- Install [PostgreSQL Database v12 or higher](https://www.postgresql.org/)

  Example Ubuntu 20.04:
````console
# Create the file repository configuration:
sudo sh -c 'echo "deb http://apt.postgresql.org/pub/repos/apt $(lsb_release -cs)-pgdg main" > /etc/apt/sources.list.d/pgdg.list'

# Import the repository signing key:
wget --quiet -O - https://www.postgresql.org/media/keys/ACCC4CF8.asc | sudo apt-key add -

# Update the package lists:
sudo apt-get update

# Install the latest version of PostgreSQL.
# If you want a specific version, use 'postgresql-12' or similar instead of 'postgresql':
sudo apt-get -y install postgresql-12
````

- Create the database config:
````console
# login as postgres user
sudo -i -u postgres
psql
````
````sql
CREATE USER miningcore WITH ENCRYPTED PASSWORD 'some-secure-password';
CREATE DATABASE miningcore;
ALTER DATABASE miningcore OWNER TO miningcore;
ALTER USER postgres WITH PASSWORD 'new_password';
GRANT ALL privileges ON DATABASE miningcore TO miningcore;
````
list shows the databases and privileges like below:
````console
\list
                               List of databases
    Name    |  Owner   | Encoding | Collate |  Ctype  |     Access privileges
------------+----------+----------+---------+---------+---------------------------
 miningcore | postgres | UTF8     | C.UTF-8 | C.UTF-8 | =Tc/miningcore           +
            |          |          |         |         | miningcore=CTc/miningcore
 postgres   | postgres | UTF8     | C.UTF-8 | C.UTF-8 |
 template0  | postgres | UTF8     | C.UTF-8 | C.UTF-8 | =c/postgres              +
            |          |          |         |         | postgres=CTc/postgres
 template1  | postgres | UTF8     | C.UTF-8 | C.UTF-8 | =c/postgres              +
            |          |          |         |         | postgres=CTc/postgres
(4 rows)

# exit PostgresDB
\quit

# exit user postgres
$ exit					
````

- Import Miningcore database tables
````console
sudo wget https://raw.githubusercontent.com/minernl/miningcore/master/src/Miningcore/Persistence/Postgres/Scripts/createdb.sql

sudo -u postgres -i
psql -d miningcore -f createdb.sql
exit
````
- Advanced PostgreSQL Database setup

The following step needs to performed **once for every new coin** you add to your server or cluster.
Be sure to **replace all occurences** of <code>pools_id</code> in the statement below with the id of your pool from your <code>config.json</code> file:
````console
sudo -u postgres -i
psql -d miningcore
````
````sql
CREATE TABLE shares_pools_id PARTITION OF shares FOR VALUES IN ('pools_id');
````
<b>!!! Do this for every Coin you add to you server. If you have multiple server, add it on every server !!!</b>

<b>EXAMPLE:</b>

lookup for the pools id in you config.json file. In this example pools id is VerusCoin
```
  CREATE TABLE shares_VerusCoin PARTITION OF shares FOR VALUES IN ('VerusCoin');

  config.json:
  "pools": [
      {
        "id": "VerusCoin",
        "enabled": true,
        "coin": "VerusCoin",
        "address": "RE9v8tCKiALVmkWbirTKc5cZpSJtuXswJ8",
```

- Coin Daemon (per pool)
- Miningcore needs to be built from source on Linux.

  Example Ubuntu 20.04:
````console
sudo apt-get update -y
sudo apt-get install git cmake build-essential libssl-dev pkg-config libboost-all-dev libsodium-dev libzmq5
sudo git clone https://github.com/minernl/miningcore
cd miningcore/src/Miningcore
dotnet publish -c Release --framework netcoreapp3.1  -o ../../build
````
- Running Miningcore

  Create a configuration file <code>config.json</code> as described [here](https://github.com/minernl/miningcore/wiki/Configuration)
````console
cd ../../build
dotnet Miningcore.dll -c config.json
````

### [Configuration](https://github.com/minernl/miningcore/wiki/Configuration)

### [API](https://github.com/minernl/miningcore/wiki/API)


#### Building on Windows

Download and install the [.Net Core 3.1 SDK](https://www.microsoft.com/net/download/core)

````console
git clone https://github.com/minernl/miningcore
cd miningcore/src/Miningcore
dotnet publish -c Release --framework netcoreapp3.1  -o ..\..\build
````

#### Building on Windows - VISUAL STUDIO

- Download and install the [.Net Core 3.1 SDK](https://www.microsoft.com/net/download/core)
- Install [Visual Studio 2019](https://www.visualstudio.com/vs/). Visual Studio Community Edition is fine.
- Open `Miningcore.sln` in VS 2019


## Running a production pool

#### Running Miningcore

Create a configuration file <code>config.json</code> as described [here](https://github.com/minernl/miningcore/wiki/Configuration)

````console
cd ../../build
dotnet Miningcore.dll -c config.json
````

A public production pool requires a web-frontend for your users to check their hashrate, earnings etc.
You can use the web frontend that come with this fork [Miningcore.Web](https://github.com/minernl/miningcore/src/Miningcore.WebUI)

Feel free to discuss ideas/issues with fellow pool operators using our [Gitter Channel](https://gitter.im/miningcore/Lobby).
