# .NET-Promises
A .NET framework implementation of JavaScript / JQuery promises.

This repo is based on https://bitbucket.org/mattkotsenas/c-promises

## Examples
```csharp
public Promise Foo() {
    Deferred d = new Deffered();
    
    try {
        var service = new Service();
        var data = service.GetAllData();
        if (data != null && data.meetsRequirements) {
            d.resolve(data);
        } else {
            d.reject("No data found");
        }
    } catch(Exception e) {
        d.reject(e.Message);
    }
    
    return d.promise();
}

public void Bar() {
    Promise p = Foo();
    p.done((data) => Console.WriteLine("Data arrived"));
    p.fail((error) => Console.WriteLine("Error: " + error));
}
```
