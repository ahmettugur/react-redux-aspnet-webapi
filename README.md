Single page basic Online Store app; React and Redux with ASP.NET Web Api.

# Web API
- Generic Repostory Pattern (Dapper Repository, NHibernate Repository, Entity Framework Reository )
- Dapper
- NHibernate
- Entity Framework
- Memcached, Redis Cache and Memory Cache
- Aspect Oriented Programming with Postsharp (4.2.17)
  - AuthorizationAspects
  - CacheAsepcts (MemcachedManager, RedisCacheManager and MemoryCacheManager)
  - ExceptionAspects
  - LogAspects
  - ValidationAspects
- FluentValidation
- log4net with logging
- Bearer Token Authentication
- Ninject IOC
- AutoMapper
- EPPlus Excel Download
- Web Api Self Host

# React Component
  - redux-from
  - material UI
  - axios
  - react-router
  - react-block-ui
  - react-pager
  - bootbox
  
# Prerequisites
  
  - .NetFramework 4.7 (VS 2015/2017)
  - Postsharp (4.2.17)
  - node.js 8 >

### Database, Postsharp and Redis Installation

* Download [Memcahed](https://commaster.net/content/installing-memcached-windows) Install your computer
* Download [Redis](https://github.com/MicrosoftArchive/redis/releases) Install your computer
* Download [Postsahrp (4.2.17)](https://www.postsharp.net/downloads/postsharp-4.2/v4.2.17) Install your computer
* Open SQL Server Management Studio > File > Open > File  select Store.sql and execute
* Change OnlineStore.WebApi > Web.config file connection string Data Source your server name
* Change OnlineStore.WebApi > log4net.config file connection string Data Source your server name


### Installation Node Module

Open command prompt

```sh
cd OnlineStoreReact folder location
npm install 
npm start
```
### Web Site
- http&#58;//localhost:3000/web

### Admin Panel
- http&#58;//localhost:3000/admin
