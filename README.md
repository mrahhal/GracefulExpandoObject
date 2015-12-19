# GracefulExpandoObject

[![Build status](https://img.shields.io/appveyor/ci/mrahhal/GracefulExpandoObject/master.svg)](https://ci.appveyor.com/project/mrahhal/GracefulExpandoObject)
[![Nuget version](https://img.shields.io/nuget/v/GracefulExpandoObject.svg)](https://www.nuget.org/packages/GracefulExpandoObject)
[![Nuget downloads](https://img.shields.io/nuget/dt/GracefulExpandoObject.svg)](https://www.nuget.org/packages/GracefulExpandoObject)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](https://opensource.org/licenses/MIT)

## `GracefulExpandoObject`

An ExpandoObject that returns null instead of throwing an exception.

```
PM> Install-Package GracefulExpandoObject
```

### Usage

```c#
dynamic geo = new GracefulExpandoObject();
var result = geo.Foo;
Assert.Null(result);
```

## `GracefulExpandoObject.Json`

A Json.Net converter that converts GracefulExpandoObject.

```
PM> Install-Package GracefulExpandoObject.Json
```

### Usage

```c#
var value = "{}";
dynamic geo = JsonConvert.DeserializeObject<GracefulExpandoObject>(
    value,
    new GracefulExpandoObjectConverter());
var result = geo.foo;
Assert.Null(result);
```
