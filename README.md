# GracefulExpandoObject

[![Build status](https://img.shields.io/appveyor/ci/mrahhal/GracefulExpandoObject/master.svg)](https://ci.appveyor.com/project/mrahhal/GracefulExpandoObject)
[![Nuget version](https://img.shields.io/nuget/v/GracefulExpandoObject.svg)](https://www.nuget.org/packages/GracefulExpandoObject)
[![Nuget downloads](https://img.shields.io/nuget/dt/GracefulExpandoObject.svg)](https://www.nuget.org/packages/GracefulExpandoObject)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](https://opensource.org/licenses/MIT)

## `GracefulExpandoObject` [![Nuget version](https://img.shields.io/nuget/v/GracefulExpandoObject.svg)](https://www.nuget.org/packages/GracefulExpandoObject)

An `ExpandoObject` (but doesn't actually extend it) that returns null instead of throwing an exception.

```
PM> Install-Package GracefulExpandoObject
```

### Usage

```c#
dynamic geo = new GracefulExpandoObject();
var result = geo.Foo;
Assert.Null(result);
```

You can also convert from a clr object:
```c#
var obj = new { Foo = "foo" };

// Shallow conversion
var shallowGeo = GracefulExpandoObject.FromObject(obj);

// Deep conversion
var deeoGeo = GracefulExpandoObject.FromObject(obj, deep: true);
```

## `GracefulExpandoObject.Json` [![Nuget version](https://img.shields.io/nuget/v/GracefulExpandoObject.Json.svg)](https://www.nuget.org/packages/GracefulExpandoObject.Json)

A Json.Net converter that converts to `GracefulExpandoObject`.

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
